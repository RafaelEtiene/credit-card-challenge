namespace CardService.Domain.Interfaces;

public interface IRabbitPublisher
{
    Task PublishAsync<T>(T message);
}