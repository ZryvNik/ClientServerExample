namespace Schemas.OutputMessage
{
    public interface IOutputMessageEvent
    {
        int Priority { get; }
        string Message { get; }
    }
}
