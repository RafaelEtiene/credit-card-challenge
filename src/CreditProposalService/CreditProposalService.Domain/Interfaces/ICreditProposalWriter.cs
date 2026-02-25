using CreditProposalService.Domain.Entities;

namespace CreditProposalService.Domain.Interfaces;

public interface ICreditProposalWriter
{
    Task SaveAsync(CreditProposal proposal, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken);
}