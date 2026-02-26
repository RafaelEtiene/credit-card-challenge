namespace CardService.Domain.Entities;

public class Card(string customerId, decimal creditLimit, DateTime createdAt)
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; private set; } = customerId;
    public decimal CreditLimit { get; private set; } = creditLimit;
    public DateTime CreatedAt { get; private set; } = createdAt;
}