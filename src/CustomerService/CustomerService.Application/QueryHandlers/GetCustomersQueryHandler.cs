using CustomerService.Application.Query;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.QueryHandlers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerReader _reader;

    public GetCustomersQueryHandler(ICustomerReader reader)
    {
        _reader = reader;
    }

    public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _reader.GetAsync(cancellationToken);
    }
}