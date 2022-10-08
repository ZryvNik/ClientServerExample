namespace ClientExample.Contracts.Common
{
    public class InputMessageDto
    {
        public int Priority { get; set; }
        public string Message { get; set; }
    }

    public class ExtendedInputMessageDto : InputMessageDto
    {
        public int Id { get; set; }
    }
}
