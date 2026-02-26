using CreditProposalService.Domain.Entities;
using CreditProposalService.Domain.Interfaces;
using MongoDB.Driver;

namespace CreditProposalService.Persistence.Readers;

public class CreditProposalReader : ICreditProposalReader
{
    private readonly IMongoCollection<CreditProposal> _collection;

    public CreditProposalReader(IMongoDatabase database)
    {
        _collection = database.GetCollection<CreditProposal>("credit_proposals");
    }

    public async Task<IEnumerable<CreditProposal>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(FilterDefinition<CreditProposal>.Empty)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> ExistsByCustomerIdAsync(
        string customerId,
        CancellationToken cancellationToken)
    {
        var filter = Builders<CreditProposal>.Filter
            .Eq(x => x.CustomerId, customerId);

        var count = await _collection.CountDocumentsAsync(
            filter,
            cancellationToken: cancellationToken);

        return count > 0;
    }
}