using System.Text;
using System.Text.Json;
using CreditProposalService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CreditProposalService.Infrastructure.Messaging.Publishers;

public class RabbitMqPublisher : IEventPublisher
{
    private readonly IConfiguration _configuration;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"]
        };

        await using var connection = await factory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        var queueName = typeof(T).Name;

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            body: body,
            cancellationToken: cancellationToken);
    }
}