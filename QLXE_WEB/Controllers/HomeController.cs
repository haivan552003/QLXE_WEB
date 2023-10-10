using Microsoft.AspNetCore.Mvc;
using QLXE_WEB.Models;
using System.Diagnostics;

namespace QLXE_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Login _login = new Login();
            return View(_login);
        }

        [HttpPost]
        public IActionResult Index(Login _login)
        {
            QlxeWfContext _db = new QlxeWfContext();

            var status = _db.Users.Where(x => x.Account == _login.AccountID && x.Password == _login.Password).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
            return View(_login);
        }

        public IActionResult Products()
        { 
            return View(); 
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}