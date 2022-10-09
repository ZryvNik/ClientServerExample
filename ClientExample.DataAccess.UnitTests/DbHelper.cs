using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample.DataAccess.UnitTests
{
    internal static class DbHelper
    {
        public static InputMessagesContext GetContext()
        {
            DbContextOptions<InputMessagesContext> options = new DbContextOptionsBuilder<InputMessagesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new InputMessagesContext(options);
            // Load Companies
            context.SaveChanges();

            return context;
        }
    }
}
