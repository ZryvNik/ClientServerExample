using ClientExample.CrossCutting.Enums;
using MediatR;

namespace ClientExample.Contracts.UpdateMessageStatus
{
    public class UpdateMessageStatusRequest : IRequest
    {
        public UpdateMessageStatusRequest(int id, MessageStatus status)
        {
            Id = id;
            Status = status;
        }
        public int Id { get; set; }
        public MessageStatus Status { get; set; }
    }
}
