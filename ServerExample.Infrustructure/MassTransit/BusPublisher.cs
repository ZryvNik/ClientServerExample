using MassTransit;

namespace ServerExample.Infrustructure.MassTransit
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBus _bus;

        public BusPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Pubish<T>(T @event)
        {
            await _bus.Publish(@event);
        }
    }
}
