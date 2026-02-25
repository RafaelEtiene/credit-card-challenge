using CreditProposalService.Application.Commands;
using CreditProposalService.Application.Events;
using CreditProposalService.Application.Factories;
using CreditProposalService.Domain.Interfaces;
using MediatR;
using IEventPublisher = CreditProposalService.Domain.Interfaces.IEventPublisher;

namespace CreditProposalService.Application.CommandHandlers;

public class CreateCreditProposalCommandHandler 
    : IRequestHandler<CreateCreditProposalCommand, Unit>
{
    private readonly ICreditProposalWriter _writer;
    private readonly IEventPublisher _publisher;

    public CreateCreditProposalCommandHandler(
        ICreditProposalWriter writer,
        IEventPublisher publisher)
    {
        _writer = writer;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(
        CreateCreditProposalCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _writer.ExistsByCustomerIdAsync(
            request.CustomerId,
            cancellationToken);

        if (exists)
            return Unit.Value;

        var proposal = CreditProposalFactory.Create(
            request.CustomerId,
            request.Score);

        await _writer.SaveAsync(proposal, cancellationToken);
        
        var @event = new CreditProposalCreatedEvent(
            proposal.Id,
            proposal.CustomerId,
            proposal.Approved,
            proposal.CreditLimit);
        
        await _publisher.PublishAsync(@event,
            cancellationToken);

        return Unit.Value;
    }
}