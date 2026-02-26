using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using CardService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Polly;

namespace CardService.Infrastructure.Messaging.Publishers;

public class RabbitPublisher : IRabbitPublisher
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _factory;

    public RabbitPublisher(IConfiguration configuration)
    {
        _configuration = configuration;

        _factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"],
            AutomaticRecoveryEnabled = true
        };
    }

    public async Task PublishAsync<T>(T message)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

        await retryPolicy.ExecuteAsync(async () =>
        {
            await using var connection =
                await _factory.CreateConnectionAsync();

            await using var channel =
                await connection.CreateChannelAsync();

            var queueName = typeof(T).Name;

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

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
                body: body);
        });
    }
}