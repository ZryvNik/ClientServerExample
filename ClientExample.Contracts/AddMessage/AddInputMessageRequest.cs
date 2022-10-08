using ClientExample.Contracts.Common;
using MediatR;

namespace ClientExample.Contracts.AddMessage
{
    public class AddInputMessageRequest : IRequest
    {
        public InputMessageDto InputMessage { get; set; }
    }
}
