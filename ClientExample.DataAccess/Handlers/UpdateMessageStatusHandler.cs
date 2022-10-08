using ClientExample.Contracts.UpdateMessageStatus;
using ClientExample.DataAccess.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientExample.DataAccess.Handlers
{
    public class UpdateMessageStatusHandler : IRequestHandler<UpdateMessageStatusRequest>
    {
        private readonly InputMessagesContext _context;

        public UpdateMessageStatusHandler(InputMessagesContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMessageStatusRequest request, CancellationToken cancellationToken)
        {
            var message = await _context.InputMessages
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (message == null)
                throw new EntityNotFoundException($"InputMessage with id={request.Id} is not found");

            message.Status = request.Status;
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
