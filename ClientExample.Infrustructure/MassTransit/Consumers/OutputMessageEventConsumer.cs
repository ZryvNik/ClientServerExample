using ClientExample.Contracts.AddMessage;
using ClientExample.Contracts.Common;
using MassTransit;
using MediatR;
using Schemas.OutputMessage;

namespace ClientExample.Infrustructure.MassTransit.Consumers
{
    public class OutputMessageEventConsumer : IConsumer<IOutputMessageEvent>
    {
        private readonly IMediator _mediatr;

        public OutputMessageEventConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<IOutputMessageEvent> context)
        {
            var @event = context.Message;
            var request = new AddInputMessageRequest()
            {
                InputMessage = new InputMessageDto()
                {
                    Message = @event.Message,
                    Priority = @event.Priority
                }
            };

            await _mediatr.Send(request);
        }
    }
}
