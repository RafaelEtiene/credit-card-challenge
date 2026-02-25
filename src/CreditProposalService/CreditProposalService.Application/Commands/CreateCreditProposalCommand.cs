using MediatR;

namespace CreditProposalService.Application.Commands;

public class CreateCreditProposalCommand : IRequest<Unit>
{
    public Guid CustomerId { get; }
    public int Score { get; }
    
    public CreateCreditProposalCommand(Guid customerId, int score)
    {
        CustomerId = customerId;
        Score = score;
    }
}