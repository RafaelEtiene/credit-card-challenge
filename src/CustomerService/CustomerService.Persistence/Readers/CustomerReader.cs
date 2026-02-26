using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MongoDB.Driver;

namespace CustomerService.Persistence.Readers;

public class CustomerReader : ICustomerReader
{
    private readonly IMongoCollection<Customer> _collection;

    public CustomerReader(IMongoDatabase database)
    {
        _collection = database.GetCollection<Customer>("customers");
    }

    public async Task<IEnumerable<Customer>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(FilterDefinition<Customer>.Empty)
            .ToListAsync(cancellationToken);
    }
}