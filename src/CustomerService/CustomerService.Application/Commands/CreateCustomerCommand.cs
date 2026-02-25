using MediatR;

namespace CustomerService.Application.Commands;

public class CreateCustomerCommand : IRequest<Unit>
{
    public string Name { get; }
    public string DocumentNumber { get; }
    public DateTime BithDate { get; }
    public int Score { get; }
    
    public CreateCustomerCommand(string name, string documentNumber, DateTime bithDate, int score)
    {
        Name = name;
        DocumentNumber = documentNumber;
        BithDate = bithDate;
        Score = score;
    }
}