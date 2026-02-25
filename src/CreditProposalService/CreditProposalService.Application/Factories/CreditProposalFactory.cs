using CreditProposalService.Domain.Entities;

namespace CreditProposalService.Application.Factories;

public static class CreditProposalFactory
{
    public static CreditProposal Create(Guid customerId, int score) =>
        new CreditProposal(customerId, score);
}