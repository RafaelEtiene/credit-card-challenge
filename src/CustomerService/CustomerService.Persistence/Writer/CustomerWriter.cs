using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MongoDB.Driver;

namespace CustomerService.Persistence.Writer;

public class CustomerWriter : ICustomerWriter
{
    private readonly IMongoCollection<Customer> _collection;

    public CustomerWriter(IMongoDatabase database)
    {
        _collection = database.GetCollection<Customer>("customers");
    }

    public async Task SaveAsync(Customer customer, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(customer, cancellationToken: cancellationToken);
    }
}