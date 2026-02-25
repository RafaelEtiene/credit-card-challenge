using CustomerService.Application.Commands;
using CustomerService.Application.Events;
using CustomerService.Application.Factories;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Interfaces;
using MediatR;

namespace CustomerService.Application.CommandHandlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
{
    private readonly ICustomerWriter _writer;
    private readonly IEventPublisher _publisher;

    public CreateCustomerCommandHandler(ICustomerWriter writer, IEventPublisher publisher)
    {
        _writer = writer;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = CustomerFactory.Create(
            request.DocumentNumber,
            request.Name,
            request.BithDate,
            request.Score
        );

        await _writer.SaveAsync(customer, cancellationToken);

        var @event = new CustomerCreatedEvent(
            customer.Id,
            customer.Name,
            customer.DocumentNumber,
            customer.BirthDate,
            customer.Score
        );

        await _publisher.PublishAsync(@event, cancellationToken);

        return Unit.Value;
    }
}