using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<League> _leagueRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Player> _playerRepository;

        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IRepository<Plan> _planRepository;
        private readonly IRepository<News> _newsRepository;

        private readonly IRepository<Character> _heroeRepository;
        private readonly IRepository<Skill> _skillRepository;
        private readonly IRepository<Item> _itemRepository;

        public AdminController(
            IRepository<League> leagueRepository,
            IRepository<Team> teamRepository,
            IRepository<Player> playerRepository,
            IRepository<Subscription> subscriptionRepository,
            IRepository<Plan> planRepository,
            IRepository<News> newsRepository,
            IRepository<Character> heroeRepository,
            IRepository<Skill> skillRepository,
            IRepository<Item> itemRepository) 
        {
            _leagueRepository = leagueRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
            _newsRepository = newsRepository;
            _heroeRepository = heroeRepository;
            _skillRepository = skillRepository;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Leagues()
        {
            var league = await _leagueRepository.Read()
                .AsNoTracking()
                .ToListAsync();

            return View(league);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Teams()
        {
            var teams = await _teamRepository.Read()
                 .ToListAsync();

            return View(teams);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Players()
        {
            var players = await _playerRepository.Read()
                .Include(x => x.Team) 
                .ToListAsync();

            return View(players);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> News()
        {
            var news = await _newsRepository.Read()
                .Include(x => x.League)
                .Include(x => x.Team)
                .Include(x => x.Player)
                .ToListAsync();

            return View(news);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Heroes()
        {
            var heroes = await _heroeRepository.Read()
                 .ToListAsync();

            return View(heroes);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Skills()
        {
            var skills = await _skillRepository.Read()
                 .Include(x => x.Character)
                 .ToListAsync();

            return View(skills);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Items()
        {
            var items = await _itemRepository.Read()
                .ToListAsync();

            return View(items);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Users()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Subscriptions()
        {
            var subscriptions = await _subscriptionRepository.Read()
                .ToListAsync();

            return View(subscriptions);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Plans()
        {
            var plans = await _planRepository.Read()
                .Include(x => x.Subscription)
                .ToListAsync();

            return View(plans);
        }
    }
}
