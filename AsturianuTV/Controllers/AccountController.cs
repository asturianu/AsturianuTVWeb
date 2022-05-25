using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private const string RoleName = "DefaultUser";

        public AccountController(
            IRepository<User> userRepository,
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository
                    .Read()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user == null)
                {
                    user = new User 
                    {
                        Surname = model.Surname,
                        Name = model.Name,
                        Email = model.Email, 
                        Password = model.Password 
                    };

                    Role userRole = await _roleRepository.Read()
                        .FirstOrDefaultAsync(r => r.Name == RoleName);

                    if (userRole != null)
                        user.Role = userRole;

                    await _userRepository.AddAsync(user);

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository
                    .Read()
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неккоректные логин или пароль");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
            };

            var claimIdentity = new ClaimsIdentity(
                    claims, 
                    "ApplicationCookie", 
                    ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
        }
    }
}
