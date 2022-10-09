using ClientExample.Contracts.Common;
using ClientExample.Contracts.GetNextMessageByPriority;
using ClientExample.CrossCutting.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientExample.DataAccess.Handlers
{
    public class TakeToProcessingNextMessageByPriorityHandler : IRequestHandler<TakeToProcessingNextMessageByPriorityRequest, TakeToProcessingNextMessageByPriorityResult>
    {
        private readonly InputMessagesContext _context;

        public TakeToProcessingNextMessageByPriorityHandler(InputMessagesContext context)
        {
            _context = context;
        }
        public async Task<TakeToProcessingNextMessageByPriorityResult> Handle(TakeToProcessingNextMessageByPriorityRequest request, CancellationToken cancellationToken)
        {
            var message = await _context.InputMessages
                .Where(x => x.Status == MessageStatus.Added)
                .OrderByDescending(x => x.Priority)
                .FirstOrDefaultAsync(cancellationToken);

            if (message == null)
                return new TakeToProcessingNextMessageByPriorityResult();

            message.Status = MessageStatus.InProgress;

            await _context.SaveChangesAsync(cancellationToken);

            return new TakeToProcessingNextMessageByPriorityResult()
            {
                Message = new ExtendedInputMessageDto()
                {
                    Id = message.Id,
                    Message = message.Message,
                    Priority = message.Priority
                }
            };
        }
    }
}
