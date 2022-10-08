using ClientExample.Contracts.Common;
using ClientExample.Contracts.GetNextMessageByPriority;
using ClientExample.CrossCutting.Enums;
using ClientExample.DataAccess.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .OrderBy(x => x.Priority)
                .FirstOrDefaultAsync(cancellationToken);

            if (message == null)
                return new TakeToProcessingNextMessageByPriorityResult();

            message.Status = MessageStatus.InProgress;

            await _context.SaveChangesAsync();

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
