namespace ServerExample.Contracts.Common
{
    public class OutputMessageDto
    {
        public int Priority { get; set; }
        public string Message { get; set; }
    }

    public class ExtendedOutputMessageDto : OutputMessageDto
    {
        public int Id { get; set; }
    }
}
