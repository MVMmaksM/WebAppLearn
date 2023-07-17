using System.Threading.Tasks;
using WebAppMVC.Models.Db;

namespace WebAppMVC.DB.LoggingRepository
{
    public interface ILoggingRepository
    {
        Task AddRequest(Request request);
        Task<Request[]> GetRequests();
    }
}
