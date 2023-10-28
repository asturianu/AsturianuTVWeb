using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsturianuTV.Controllers
{
    public class AdminController : Controller
    {
        public AdminController() { }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Leagues()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Teams()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Players()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult News()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Heroes()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Skills()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Items()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Subscriptions()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Plans()
        {
            return View();
        }
    }
}
