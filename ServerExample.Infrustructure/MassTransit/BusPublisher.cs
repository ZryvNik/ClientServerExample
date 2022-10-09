using MassTransit;
using Microsoft.Extensions.Logging;
using ServerExample.Infrustructure.Handlers;

namespace ServerExample.Infrustructure.MassTransit
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBus _bus;
        private readonly ILogger<GenerateOutputMessagesHandler>? _logger;

        public BusPublisher(IBus bus, 
            ILogger<GenerateOutputMessagesHandler>? logger = null)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task Pubish<T>(T @event, CancellationToken cancellationToken)
        {
            await _bus.Publish(@event, cancellationToken);
        }
    }
}
