namespace CustomerService.Domain.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default);
}