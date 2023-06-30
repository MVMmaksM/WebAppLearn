using System.Threading.Tasks;
using WebAppMVC.Models.Db;

namespace WebAppMVC.DB.Repository
{
    public class BlogRepositorycs : IBlogRepository
    {
        private BlogContext _blogContext;
        public BlogRepositorycs(BlogContext context)
        {
            _blogContext = context;
        }
        public async Task AddUser(User user)
        {
            var entry = _blogContext.Entry(user);
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                await _blogContext.Users.AddAsync(user);
            }

            await _blogContext.SaveChangesAsync();
        }
    }
}
