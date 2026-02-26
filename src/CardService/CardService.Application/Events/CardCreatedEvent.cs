namespace CardService.Application.Events;

public record CardCreatedEvent(
    string Id,
    string CustomerId,
    decimal Limit
);