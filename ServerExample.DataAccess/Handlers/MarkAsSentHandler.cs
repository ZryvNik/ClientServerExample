using ServerExample.Contracts.MarkAsSent;
using ServerExample.DataAccess.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ServerExample.DataAccess.Handlers
{
    public class MarkAsSentHandler : IRequestHandler<MarkAsSentRequest, Unit>
    {
        private readonly OutputMessagesContext _outputMessagesContext;

        public MarkAsSentHandler(OutputMessagesContext outputMessagesContext)
        {
            _outputMessagesContext = outputMessagesContext;
        }

        public async Task<Unit> Handle(MarkAsSentRequest request, CancellationToken cancellationToken)
        {
            var message = await _outputMessagesContext
                .OutputMessages
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (message == null)
                throw new EntityNotFoundException($"OutputMessage with id={request.Id} is not found");

            message.IsSent = true;
            message.SentAt = DateTime.Now;

            await _outputMessagesContext.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
