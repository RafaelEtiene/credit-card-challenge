using MediatR;

namespace CreditProposalService.Application.Commands;

public record CreateCreditProposalCommand(
    string CustomerId, int Score) : IRequest<Unit>;