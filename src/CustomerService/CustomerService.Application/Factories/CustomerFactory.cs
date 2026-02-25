using CustomerService.Domain.Entities;

namespace CustomerService.Application.Factories;

public static class CustomerFactory
{
    public static Customer Create(string documentNumber, string name, DateTime birthDate, int score)
    {
        return new Customer(documentNumber, name, birthDate, score);
    }
}