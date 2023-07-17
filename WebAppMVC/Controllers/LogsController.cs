using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var requests = _loggingRepository.GetRequests();
            return View(requests);
        }
    }
}
