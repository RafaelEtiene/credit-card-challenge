using MediatR;

namespace CreditProposalService.Application.Commands;

public class CreateCreditProposalCommand : IRequest<Unit>
{
    public string CustomerId { get; }
    public int Score { get; }
    
    public CreateCreditProposalCommand(string customerId, int score)
    {
        CustomerId = customerId;
        Score = score;
    }
}