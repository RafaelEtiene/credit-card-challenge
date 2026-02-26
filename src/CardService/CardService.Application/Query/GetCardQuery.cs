using CardService.Domain.Entities;
using MediatR;

namespace CardService.Application.Query;

public record GetCardQuery() : IRequest<IEnumerable<Card>>;