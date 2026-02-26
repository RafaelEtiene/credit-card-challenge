using CardService.Application.Commands;
using CardService.Domain.Interfaces;
using CardService.Infrastructure.Messaging.Consumers;
using CardService.Infrastructure.Messaging.Publishers;
using CardService.Persistence.Readers;
using CardService.Persistence.Writers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CardService.DependencyInjection;

public static class DependencyInjection
{
    public static void AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(CreateCardCommand).Assembly));
        
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
        
        services.AddHostedService<CreditProposalCreatedConsumer>();
        
        services.AddScoped<ICardReader, CardReader>();
        services.AddScoped<ICardWriter, CardWriter>();
        services.AddScoped<IRabbitPublisher, RabbitPublisher>();
    }
}