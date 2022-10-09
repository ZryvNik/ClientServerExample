using ClientExample.Contracts.GetNextMessageByPriority;
using ClientExample.Contracts.InputMessageProcessing;
using ClientExample.Contracts.UpdateMessageStatus;
using ClientExample.CrossCutting.Enums;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace ClientExample.Infrustructure
{
    public class InputMessageProcessingService : BackgroundService
    {
        private readonly ISender _mediator;

        public InputMessageProcessingService(ISender mediator)
        {
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var input = await _mediator.Send(new TakeToProcessingNextMessageByPriorityRequest(), stoppingToken);
                if (input.Message == null)
                    continue;

                try
                {
                    await _mediator.Send(new InputMessageProcessingRequest(input.Message), stoppingToken);
                    await _mediator.Send(new UpdateMessageStatusRequest(input.Message.Id, MessageStatus.Complete), stoppingToken);
                }
                catch (Exception ex)
                {
                    await _mediator.Send(new UpdateMessageStatusRequest(input.Message.Id, MessageStatus.Added), stoppingToken);
                }
            }
        }
    }
}
