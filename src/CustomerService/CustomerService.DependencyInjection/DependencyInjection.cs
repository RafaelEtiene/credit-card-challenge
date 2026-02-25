using CustomerService.Application.Commands;
using CustomerService.Domain.Interfaces;
using CustomerService.Infrastructure.Messaging;
using CustomerService.Persistence.Writer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CustomerService.DependencyInjection;

public static class DependencyInjection
{
    public static void AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(CreateCustomerCommand).Assembly));
        
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = configuration["Mongo:ConnectionString"];
            return new MongoClient(connectionString);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("CustomerDb");
        });

        services.AddScoped<ICustomerWriter, CustomerWriter>();
        services.AddScoped<IEventPublisher, RabbitMqPublisher>();
    }
}