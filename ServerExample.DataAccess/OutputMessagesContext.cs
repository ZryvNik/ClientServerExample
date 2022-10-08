using ServerExample.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerExample.DataAccess
{
    public class OutputMessagesContext : DbContext
    {
        public OutputMessagesContext(DbContextOptions<OutputMessagesContext> options)
        : base(options) { }
        public DbSet<OutputMessage> OutputMessages { get; set; }
    }
}