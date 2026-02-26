using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using CardService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CardService.Infrastructure.Messaging.Publishers;

public class RabbitPublisher : IRabbitPublisher
{
    private readonly IConfiguration _configuration;

    public RabbitPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublishAsync<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"]
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        var queueName = typeof(T).Name;

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            body: body);
    }
}