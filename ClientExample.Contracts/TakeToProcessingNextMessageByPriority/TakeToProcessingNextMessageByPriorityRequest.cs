using MediatR;

namespace ClientExample.Contracts.GetNextMessageByPriority
{
    public class TakeToProcessingNextMessageByPriorityRequest : IRequest<TakeToProcessingNextMessageByPriorityResult>
    {

    }
}
