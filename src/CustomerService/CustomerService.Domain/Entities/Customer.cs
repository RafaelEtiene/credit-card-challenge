namespace CustomerService.Domain.Entities;

public class Customer
{
    public Guid Id { get; }
    public string DocumentNumber { get; }
    public string Name { get; }
    public DateTime BirthDate { get; }
    public int Score { get; }
    
    public Customer(string documentNumber, string name, DateTime birthDate, int score)
    {
        Id = Guid.NewGuid();
        DocumentNumber = documentNumber;
        Name = name;
        BirthDate = birthDate;
        Score = score;
    }
}