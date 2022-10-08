using ClientExample.Contracts.Common;

namespace ClientExample.Contracts.GetNextMessageByPriority
{
    public class TakeToProcessingNextMessageByPriorityResult
    {
        public ExtendedInputMessageDto Message { get; set; }
    }
}
