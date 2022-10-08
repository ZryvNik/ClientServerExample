using ClientExample.Contracts.AddMessage;
using ClientExample.CrossCutting.Enums;
using ClientExample.DataAccess.Entities;
using MediatR;

namespace ClientExample.DataAccess.Handlers
{
    public class AddInputMessageRequestHandler : IRequestHandler<AddInputMessageRequest>
    {
        private readonly InputMessagesContext _context;

        public AddInputMessageRequestHandler(InputMessagesContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddInputMessageRequest request, CancellationToken cancellationToken)
        {
            var message = new InputMessage()
            {
                Message = request.InputMessage.Message,
                Priority = request.InputMessage.Priority,
                CreatedAt = DateTime.Now,
                Status = MessageStatus.Added
            };
            await _context.InputMessages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
