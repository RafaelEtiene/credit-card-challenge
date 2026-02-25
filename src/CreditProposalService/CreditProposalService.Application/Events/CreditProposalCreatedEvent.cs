namespace CreditProposalService.Application.Events;

public record CreditProposalCreatedEvent(
    string ProposalId,
    string CustomerId,
    bool Approved,
    decimal CreditLimit 
);