using ServerExample.Contracts.AddMessages;
using ServerExample.DataAccess.Entities;
using MediatR;

namespace ServerExample.DataAccess.Handlers
{
    public class AddOutputMessageRangeHandler : IRequestHandler<AddOutputMessageRangeRequest, Unit>
    {
        private readonly OutputMessagesContext _outputMessagesContext;

        public AddOutputMessageRangeHandler(OutputMessagesContext outputMessagesContext)
        {
            _outputMessagesContext = outputMessagesContext;
        }
        public async Task<Unit> Handle(AddOutputMessageRangeRequest request, CancellationToken cancellationToken)
        {
            var range = request.Messages.Select(x => new OutputMessage()
            {
                Message = x.Message,
                Priority = x.Priority
            });
            await _outputMessagesContext.OutputMessages.AddRangeAsync(range, cancellationToken);
            await _outputMessagesContext.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
