using CardService.Application.Commands;
using CardService.Application.Events;
using CardService.Application.Factories;
using CardService.Domain.Interfaces;
using MediatR;

namespace CardService.Application.CommandHandlers;

public class CreateCardCommandHandler 
    : IRequestHandler<CreateCardCommand, Unit>
{
    private readonly ICardWriter _writer;
    private readonly IRabbitPublisher _publisher;

    public CreateCardCommandHandler(
        ICardWriter writer,
        IRabbitPublisher publisher)
    {
        _writer = writer;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(
        CreateCardCommand request,
        CancellationToken cancellationToken)
    {
        for (int i = 0; i < request.TotalCards; i++)
        {
            var card = CardFactory.Create(
                request.CustomerId,
                request.CreditLimit,
                DateTime.UtcNow
            );

            await _writer.SaveAsync(card, cancellationToken);

            await _publisher.PublishAsync(new CardCreatedEvent(
                card.Id,
                card.CustomerId,
                card.CreditLimit
            ));
        }

        return Unit.Value;
    }
}