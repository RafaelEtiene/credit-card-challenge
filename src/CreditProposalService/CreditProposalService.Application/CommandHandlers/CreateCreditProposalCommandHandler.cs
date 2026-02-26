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
    private readonly ICreditProposalReader _reader;
    private readonly IEventPublisher _publisher;

    public CreateCreditProposalCommandHandler(
        ICreditProposalWriter writer,
        ICreditProposalReader reader,
        IEventPublisher publisher)
    {
        _writer = writer;
        _publisher = publisher;
        _reader = reader;
    }

    public async Task<Unit> Handle(
        CreateCreditProposalCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _reader.ExistsByCustomerIdAsync(
            request.CustomerId,
            cancellationToken);

        if (exists)
            return Unit.Value;

        var proposal = CreditProposalFactory.Create(
            request.CustomerId,
            request.Score);

        await _writer.SaveAsync(proposal, cancellationToken);

        if (proposal.Approved)
        {
            var @event = new CreditProposalCreatedEvent(
                proposal.Id,
                proposal.CustomerId,
                proposal.CreditLimit,
                proposal.TotalCards);

            await _publisher.PublishAsync(@event,
                cancellationToken);
        }
        
        return Unit.Value;
    }
}