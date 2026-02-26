using CardService.Domain.Entities;
using CardService.Domain.Interfaces;
using MongoDB.Driver;

namespace CardService.Persistence.Readers;

public class CardReader : ICardReader
{
    private readonly IMongoCollection<Card> _collection;

    public CardReader(IMongoDatabase database)
    {
        _collection = database.GetCollection<Card>("cards");
    }

    public async Task<IEnumerable<Card>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(FilterDefinition<Card>.Empty)
            .ToListAsync(cancellationToken);
    }
}