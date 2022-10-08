using ClientExample.Contracts.InputMessageProcessing;
using ClientExample.CrossCutting.Configuration;
using MediatR;

namespace ClientExample.Infrustructure.Handlers
{
    public class InputMessageProcessingHandler : IRequestHandler<InputMessageProcessingRequest>
    {
        private readonly int _delay;
        public InputMessageProcessingHandler(IBackgroundWorkingConfiguration configuration)
        {
            _delay = configuration.ProccessMessageDelay;
        }
        public async Task<Unit> Handle(InputMessageProcessingRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(_delay, cancellationToken);
            Console.WriteLine($"Priority: {request.Message.Priority} Message:{request.Message.Message} ");
            return await Unit.Task;
        }
    }
}
