using ClientExample.Contracts.Common;
using MediatR;

namespace ClientExample.Contracts.InputMessageProcessing
{
    public class InputMessageProcessingRequest : IRequest
    {
        public InputMessageProcessingRequest(InputMessageDto dto)
        {
            Message = dto;
        }
        public InputMessageDto Message { get; set; }
    }
}
