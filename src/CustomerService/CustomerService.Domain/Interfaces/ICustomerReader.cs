using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Interfaces;

public interface ICustomerReader
{
    Task<IEnumerable<Customer>> GetAsync(CancellationToken cancellationToken = default);
}