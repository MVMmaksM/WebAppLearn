using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebAppMVC.DB.Repository;
using WebAppMVC.Models.Db;

namespace WebAppMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private IBlogRepository _blogRepository;
        public UsersController(IBlogRepository blogRepository, ILogger<UsersController> logger)
        {
            _blogRepository = blogRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _blogRepository.GetUser();
            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            await _blogRepository.AddUser(user);
            return View(user);
        }
    }
}
