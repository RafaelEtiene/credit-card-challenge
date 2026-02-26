using System.Text;
using System.Text.Json;
using CustomerService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Polly;
using RabbitMQ.Client;

namespace CustomerService.Infrastructure.Messaging;

public class RabbitMqPublisher : IEventPublisher
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _factory;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        _configuration = configuration;

        _factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"]
        };
    }

    public async Task PublishAsync<T>(
        T message,
        CancellationToken cancellationToken = default)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

        await retryPolicy.ExecuteAsync(async () =>
        {
            await using var connection =
                await _factory.CreateConnectionAsync(cancellationToken);

            await using var channel =
                await connection.CreateChannelAsync(
                    cancellationToken: cancellationToken);

            var queueName = typeof(T).Name;

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken);

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message));

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: true,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken);
        });
    }
}