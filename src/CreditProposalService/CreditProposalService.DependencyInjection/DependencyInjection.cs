using CreditProposalService.Application.Commands;
using CreditProposalService.Domain.Interfaces;
using CreditProposalService.Infrastructure.Messaging.Consumers;
using CreditProposalService.Infrastructure.Messaging.Publishers;
using CreditProposalService.Persistence.Readers;
using CreditProposalService.Persistence.Writers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CreditProposalService.DependencyInjection;

public static class DependencyInjection
{
    public static void AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(CreateCreditProposalCommand).Assembly));
        
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = configuration["Mongo:ConnectionString"];
            return new MongoClient(connectionString);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("CreditProposalDb");
        });
        
        services.AddHostedService<CustomerCreatedConsumer>();

        services.AddScoped<ICreditProposalWriter, CreditProposalWriter>();
        services.AddScoped<ICreditProposalReader, CreditProposalReader>();
        services.AddScoped<IEventPublisher, RabbitMqPublisher>();
    }
}