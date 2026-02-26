namespace CardService.Application.Events;

public record CreditProposalCreatedEvent(
    string ProposalId,
    string CustomerId,
    decimal CreditLimit,
    int TotalCards
);