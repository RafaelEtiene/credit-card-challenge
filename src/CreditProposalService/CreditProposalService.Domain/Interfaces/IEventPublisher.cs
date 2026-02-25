namespace CreditProposalService.Domain.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
}