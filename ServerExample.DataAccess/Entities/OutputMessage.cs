using System.ComponentModel.DataAnnotations.Schema;

namespace ServerExample.DataAccess.Entities
{
    public class OutputMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
