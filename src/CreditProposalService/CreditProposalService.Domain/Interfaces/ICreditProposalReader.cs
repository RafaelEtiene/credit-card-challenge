using CreditProposalService.Domain.Entities;

namespace CreditProposalService.Domain.Interfaces;

public interface ICreditProposalReader
{
    Task<IEnumerable<CreditProposal>> GetAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsByCustomerIdAsync(
        string customerId,
        CancellationToken cancellationToken);
}