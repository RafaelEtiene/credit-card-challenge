namespace CreditProposalService.Application.Events;

public record CustomerCreatedEvent(
    string CustomerId,
    string Name,
    string Document,
    DateTime BirthDate,
    int Score
);