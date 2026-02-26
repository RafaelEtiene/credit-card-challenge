using CustomerService.Domain.Entities;
using MediatR;

namespace CustomerService.Application.Query;

public record GetCustomersQuery() : IRequest<IEnumerable<Customer>>;