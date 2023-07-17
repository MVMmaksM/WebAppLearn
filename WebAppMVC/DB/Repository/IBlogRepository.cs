using System.Threading.Tasks;
using WebAppMVC.Models.Db;

namespace WebAppMVC.DB.Repository
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUser();
    }
}
