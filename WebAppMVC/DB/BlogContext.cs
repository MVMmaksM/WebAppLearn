using Microsoft.EntityFrameworkCore;
using WebAppMVC.Models.Db;

namespace WebAppMVC.DB
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options) => Database.EnsureCreated();
    }
}
