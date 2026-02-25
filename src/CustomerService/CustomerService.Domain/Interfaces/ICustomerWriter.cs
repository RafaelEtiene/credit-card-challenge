using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Interfaces;

public interface ICustomerWriter
{
    public Task SaveAsync(Customer customer, CancellationToken cancellationToken = default);
}