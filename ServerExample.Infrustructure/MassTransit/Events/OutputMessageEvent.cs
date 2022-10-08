using Schemas.OutputMessage;

namespace ServerExample.Infrustructure.MassTransit.Events
{
    public class OutputMessageEvent : IOutputMessageEvent
    {
        public OutputMessageEvent(int priority, string message)
        {
            Priority = priority;
            Message = message;
        }
        public int Priority { get; }

        public string Message { get; }
    }
}
