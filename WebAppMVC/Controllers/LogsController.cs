using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class LogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
