namespace ServerExample.Infrustructure.MassTransit
{
    public interface IBusPublisher
    {
        Task Pubish<T>(T @event, CancellationToken cancellationToken);
    }
}
