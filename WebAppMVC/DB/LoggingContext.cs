using Microsoft.EntityFrameworkCore;
using WebAppMVC.Models.Db;

namespace WebAppMVC.DB
{
    public class LoggingContext: DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public LoggingContext(DbContextOptions<BlogContext> options) : base(options) => Database.EnsureCreated();
    }
}
