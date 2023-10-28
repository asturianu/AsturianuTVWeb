using AsturianuTV.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

namespace AsturianuTV.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Character> _characterRepository;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IRepository<Plan> _planRepository;
        private readonly IRepository<Blog> _blogRepository;

        public HomeController(
            IRepository<User> userRepository,
            IRepository<Character> characterRepository,
            IRepository<News> newsRepository,
            IRepository<Subscription> subscriptionRepository,
            IRepository<Plan> planRepository,
            IRepository<Blog> blogRepository)
        {
            _userRepository = userRepository;
            _characterRepository = characterRepository;
            _newsRepository = newsRepository;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
            _blogRepository = blogRepository;
        }

        public IActionResult SomeAction()
        {
            ViewData["SharedData"] = "This data is shared across the application.";
            return View();
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userRepository.Read()
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
        public async Task<IActionResult> Heroes(CancellationToken cancellationToken)
        {
            var characters = await _characterRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.Skills)
                .ToListAsync(cancellationToken);

            return View(characters);
        }

        [HttpGet]
        public async Task<IActionResult> Character(int? id, CancellationToken cancellationToken)
        {
            var character = await _characterRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.Skills)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (character != null) return View(character);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> News(CancellationToken cancellationToken)
        {
            var news = await _newsRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.NewsTags)
                    .ThenInclude(x => x.Tag)
                .ToListAsync(cancellationToken);

            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> CurrentNews(int id, CancellationToken cancellationToken)
        {
            var news = await _newsRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.NewsTags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.NewsMaterials)
                .ThenInclude(x => x.Material)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (news != null) return View(news);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Subscriptions(CancellationToken cancellationToken) =>
            View(await _subscriptionRepository.ListAsync(cancellationToken));

        [HttpGet]
        public async Task<IActionResult> Subscription(int id, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.Plans)
                .ThenInclude(x => x.Blogs)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (subscription != null) return View(subscription);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Plans(int subscriptionId, CancellationToken cancellationToken)
        {
            var plans = await _planRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.Subscription)
                .Include(x => x.Blogs)
                .Where(x => x.SubscriptionId == subscriptionId)
                .ToListAsync(cancellationToken);

            return View(plans);
        }

        [HttpGet]
        public async Task<IActionResult> Plan(int id, CancellationToken cancellationToken)
        {
            var plan = await _planRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.Subscription)
                .Include(x => x.Blogs)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (plan != null) View(plan);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Blog(int id, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.BlogMaterials)
                .ThenInclude(x => x.Material)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (blog != null) View(blog);
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
