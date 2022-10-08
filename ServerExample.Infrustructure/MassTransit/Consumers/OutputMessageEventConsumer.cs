using MassTransit;
using MediatR;
using Schemas.OutputMessage;
using ServerExample.Contracts.AddMessages;
using ServerExample.Contracts.Common;

namespace ServerExample.Infrustructure.MassTransit.Consumers
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
            var request = new AddOutputMessageRangeRequest()
            {
                Messages = new List<OutputMessageDto>() 
                { 
                    new OutputMessageDto() 
                    { 
                        Message = @event.Message, 
                        Priority = @event.Priority 
                    } 
                }
            };

            await _mediatr.Send(request);
        }
    }
}
