using ClientExample.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientExample.DataAccess
{
    public class InputMessagesContext : DbContext
    {
        public InputMessagesContext(DbContextOptions<InputMessagesContext> options)
        : base(options) { }
        public DbSet<InputMessage> InputMessages { get; set; }
    }
}