using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using CreditProposalService.Application.Commands;
using CreditProposalService.Application.Events;
using Microsoft.Extensions.Configuration;

namespace CreditProposalService.Infrastructure.Messaging.Consumers;

public class CustomerCreatedConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;

    public CustomerCreatedConsumer(
        IConfiguration configuration,
        IServiceScopeFactory scopeFactory)
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"],
            AutomaticRecoveryEnabled = true
        };

        var connection = await factory.CreateConnectionAsync(stoppingToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: "CustomerCreatedEvent",
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<CustomerCreatedEvent>(json);

                if (message is null)
                {
                    await channel.BasicNackAsync(ea.DeliveryTag, false, false);
                    return;
                }

                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(new CreateCreditProposalCommand(
                    message.CustomerId,
                    message.Score
                ));

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch
            {
                await channel.BasicNackAsync(ea.DeliveryTag, false, true);
            }
        };

        await channel.BasicConsumeAsync(
            queue: "CustomerCreatedEvent",
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}