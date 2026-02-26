using CardService.Domain.Entities;

namespace CardService.Domain.Interfaces;

public interface ICardReader
{
    Task<IEnumerable<Card>> GetAsync(CancellationToken cancellationToken = default);
}