using ClientExample.CrossCutting.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientExample.DataAccess.Entities
{
    public class InputMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Message { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifyAt { get; set; }
    }
}
