using MediatR;
using Microsoft.Extensions.Logging;
using ServerExample.Contracts.AddMessages;
using ServerExample.Contracts.Common;
using ServerExample.Infrustructure.Extensions;
using ServerExample.Infrustructure.MassTransit;
using ServerExample.Infrustructure.MassTransit.Events;

namespace ServerExample.Infrustructure.Handlers
{
    public class GenerateOutputMessagesHandler : IRequestHandler<GenerateOutputMessagesRequest, GenerateOutputMessagesResult>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<GenerateOutputMessagesHandler>? _logger;
        public GenerateOutputMessagesHandler(IBusPublisher busPublisher,
            ILogger<GenerateOutputMessagesHandler>? logger = null)
        {
            _busPublisher = busPublisher;
            _logger = logger;
        }

        public async Task<GenerateOutputMessagesResult> Handle(GenerateOutputMessagesRequest request, CancellationToken cancellationToken)
        {
            var messages = Enumerable
                .Range(0, request.Count)
                .Select(x => new OutputMessageDto()
                {
                    Priority = new Random().Next(1, 1000),
                    Message = new Random().RandomString(8)
                });

            var sendedCount = 0;

            foreach (var @event in messages.Select(x => new OutputMessageEvent(x.Priority, x.Message)))
            {
                try
                {
                    await _busPublisher.Pubish(@event, cancellationToken);
                    sendedCount++;
                    _logger?.LogInformation("Success send {@event}", @event);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "failed send {@event}", @event);
                }
            }
            _logger?.LogInformation("Successfully sent {sendedInputMessageCount} of {generatedInputMessageCount}", sendedCount, messages.Count());

            return new GenerateOutputMessagesResult()
            {
                OutputMessages = messages
            };
        }
    }
}
