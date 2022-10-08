using MediatR;

namespace ServerExample.Contracts.GetMessages
{
    public class GetUnSentOutputMessagesRequest : IRequest<GetUnSentOutputMessagesResult>
    {
        public GetUnSentOutputMessagesRequest(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
