using MediatR;

namespace ServerExample.Contracts.MarkAsSent
{
    public class MarkAsSentRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
