using MediatR;
using ServerExample.Contracts.AddMessages;
using ServerExample.Contracts.Common;
using ServerExample.Infrustructure.Extensions;
using ServerExample.Infrustructure.MassTransit;
using ServerExample.Infrustructure.MassTransit.Events;

namespace ServerExample.Infrustructure.Handlers
{
    public class GenerateOutputMessagesHandler : IRequestHandler<GenerateOutputMessagesRequest, GenerateOutputMessagesResult>
    {
        public readonly IBusPublisher _busPublisher;

        public GenerateOutputMessagesHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
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

            foreach(var @event in messages.Select(x => new OutputMessageEvent(x.Priority, x.Message)))
            {
                await _busPublisher.Pubish(@event);
            }

            return new GenerateOutputMessagesResult()
            {
                OutputMessages = messages
            };
        }
    }
}
