using CardService.Domain.Entities;
using CardService.Domain.Interfaces;
using MongoDB.Driver;

namespace CardService.Persistence.Writers;

public class CardWriter : ICardWriter
{
    private readonly IMongoCollection<Card> _collection;

    public CardWriter(IMongoDatabase database)
    {
        _collection = database.GetCollection<Card>("cards");
    }

    public async Task SaveAsync(Card card, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(card, cancellationToken: cancellationToken);
    }
}