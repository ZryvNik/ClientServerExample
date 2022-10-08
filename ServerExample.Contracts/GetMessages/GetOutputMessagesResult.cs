using ServerExample.Contracts.Common;
using System.Collections.Generic;

namespace ServerExample.Contracts.GetMessages
{
    public class GetUnSentOutputMessagesResult
    {
        public IEnumerable<OutputMessageDto> OutputMessages { get; set; }
    }
}
