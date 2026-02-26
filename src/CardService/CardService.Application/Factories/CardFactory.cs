using CardService.Domain.Entities;

namespace CardService.Application.Factories;

public static class CardFactory
{
    public static Card Create(string customerId, decimal creditLimit, DateTime createdAt) =>
        new Card(customerId, creditLimit, createdAt);
}