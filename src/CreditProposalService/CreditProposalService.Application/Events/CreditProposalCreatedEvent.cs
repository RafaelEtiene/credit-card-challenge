namespace CreditProposalService.Application.Events;

public record CreditProposalCreatedEvent(
    Guid ProposalId,
    Guid CustomerId,
    bool Approved,
    decimal CreditLimit
);