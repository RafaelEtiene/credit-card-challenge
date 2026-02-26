using MediatR;

namespace CardService.Application.Commands;
public record CreateCardCommand(
    string ProposalId,
    string CustomerId,
    decimal CreditLimit,
    int TotalCards
) : IRequest<Unit>;