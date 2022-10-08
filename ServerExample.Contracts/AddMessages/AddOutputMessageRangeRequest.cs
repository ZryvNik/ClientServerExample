using ServerExample.Contracts.Common;
using MediatR;
using System.Collections.Generic;

namespace ServerExample.Contracts.AddMessages
{
    public class AddOutputMessageRangeRequest : IRequest
    {
        public IEnumerable<OutputMessageDto> Messages { get; set; }
    }
}
