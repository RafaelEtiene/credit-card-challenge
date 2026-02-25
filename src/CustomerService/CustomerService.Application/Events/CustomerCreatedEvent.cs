namespace CustomerService.Application.Events;

public record CustomerCreatedEvent(
    string CustomerId,
    string FullName,
    string Document,
    DateTime BirthDate,
    int Score
);