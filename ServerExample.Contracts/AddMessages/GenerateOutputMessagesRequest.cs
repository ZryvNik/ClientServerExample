using MediatR;

namespace ServerExample.Contracts.AddMessages
{
    public class GenerateOutputMessagesRequest : IRequest<GenerateOutputMessagesResult>
    {
        public int Count { get; set; }
    }
}
