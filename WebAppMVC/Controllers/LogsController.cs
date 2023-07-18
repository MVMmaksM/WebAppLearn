using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppMVC.DB.LoggingRepository;

namespace WebAppMVC.Controllers
{
    public class LogsController : Controller
    {
        private ILoggingRepository _loggingRepository;

        public LogsController(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }
        public async Task<IActionResult> Index()
        {
            var requests = await _loggingRepository.GetRequests();
            return View(requests);
        }
    }
}
