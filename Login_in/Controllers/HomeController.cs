using Login_in.Data;
using Login_in.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Login_in.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly UsersDbContext _usersDbContext;

        public HomeController(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }


        public IActionResult Index()
        {
            IEnumerable<User> users = _usersDbContext.Users;
            return View(users);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}