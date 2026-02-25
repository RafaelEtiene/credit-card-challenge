namespace CreditProposalService.Domain.Entities;

public class CreditProposal
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public bool Approved { get; private set; }
    public decimal CreditLimit { get; private set; }
    public int TotalCards { get; private set; }

    public CreditProposal(Guid customerId, int score)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;

        if (score <= 100)
        {
            Approved = false;
            CreditLimit = 0;
            TotalCards = 0;
        }
        if (score is > 100 and <= 500)
        {
            Approved = true;
            CreditLimit = 1000;
            TotalCards = 1;
        }
        if(score > 500)
        {
            Approved = true;
            CreditLimit = 5000;
            TotalCards = 2;
        }
    }
}