using ServerExample.Contracts.Common;
using ServerExample.Contracts.GetMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ServerExample.DataAccess.Handlers
{
    public class GetUnSentOutputMessagesHandler : IRequestHandler<GetUnSentOutputMessagesRequest, GetUnSentOutputMessagesResult>
    {
        private readonly OutputMessagesContext _outputMessagesContext;

        public GetUnSentOutputMessagesHandler(OutputMessagesContext outputMessagesContext)
        {
            _outputMessagesContext = outputMessagesContext;
        }

        public async Task<GetUnSentOutputMessagesResult> Handle(GetUnSentOutputMessagesRequest request, CancellationToken cancellationToken)
        {
            var results = await _outputMessagesContext
                .OutputMessages
                .Where(x => !x.IsSent)
                .OrderBy(x => x.Id)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(x => new ExtendedOutputMessageDto()
                {
                    Id = x.Id,
                    Message = x.Message,
                    Priority = x.Priority
                })
                .ToListAsync();

            return new GetUnSentOutputMessagesResult()
            {
                OutputMessages = results
            };
        }
    }
}
