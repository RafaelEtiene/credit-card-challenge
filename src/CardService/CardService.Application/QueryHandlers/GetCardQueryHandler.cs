using CardService.Application.Query;
using CardService.Domain.Entities;
using CardService.Domain.Interfaces;
using MediatR;

namespace CardService.Application.QueryHandlers;

public class GetCardQueryHandler : IRequestHandler<GetCardQuery, IEnumerable<Card>>
{
    private readonly ICardReader _reader;

    public GetCardQueryHandler(ICardReader reader)
    {
        _reader = reader;
    }

    public async Task<IEnumerable<Card>> Handle(GetCardQuery request, CancellationToken cancellationToken)
    {
        return await _reader.GetAsync(cancellationToken);
    }
}