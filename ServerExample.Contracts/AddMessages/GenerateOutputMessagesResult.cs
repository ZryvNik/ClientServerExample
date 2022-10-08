using ServerExample.Contracts.Common;
using System.Collections.Generic;

namespace ServerExample.Contracts.AddMessages
{
    public class GenerateOutputMessagesResult
    {
        public IEnumerable<OutputMessageDto> OutputMessages { get; set; }
    }
}
