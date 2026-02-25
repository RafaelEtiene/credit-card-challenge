namespace CreditProposalService.Application.Events;

public record CustomerCreatedEvent(
    Guid CustomerId,
    string Name,
    string Document,
    DateTime BirthDate,
    int Score
);