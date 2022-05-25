using AsturianuTV.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<User> _userRepository;
        public HomeController(
            ILogger<HomeController> logger,
            IRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email.Equals(User.Identity.Name), cancellationToken);

            if (user == null) return View();
            return View(user);
        } 

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int? id, CancellationToken cancellationToken)
        {

            if (id != null)
            {
                var user = await _userRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> SettingsProfile(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var user = await _userRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Subscriptions(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var user = await _userRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> News(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var user = await _userRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
