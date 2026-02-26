using CardService.Domain.Entities;

namespace CardService.Domain.Interfaces;

public interface ICardWriter
{
    Task SaveAsync(Card card, CancellationToken cancellationToken = default);
}