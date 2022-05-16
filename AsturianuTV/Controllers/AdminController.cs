using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AsturianuTV.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<User> _currentUser;
        public AdminController(IRepository<User> currentUser)
        {
            _currentUser = currentUser;
        }

        [Authorize(Roles = "Administrator, DefaultUser")]
        [HttpGet]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            return View();
        }
    }
}
