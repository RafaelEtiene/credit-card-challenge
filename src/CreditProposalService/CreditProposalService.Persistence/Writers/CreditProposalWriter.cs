using CreditProposalService.Domain.Entities;
using CreditProposalService.Domain.Interfaces;
using MongoDB.Driver;

namespace CreditProposalService.Persistence.Writers;

public class CreditProposalWriter : ICreditProposalWriter
{
    private readonly IMongoCollection<CreditProposal> _collection;

    public CreditProposalWriter(IMongoDatabase database)
    {
        _collection = database.GetCollection<CreditProposal>("credit_proposals");
    }

    public async Task SaveAsync(CreditProposal proposal, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(proposal, cancellationToken: cancellationToken);
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