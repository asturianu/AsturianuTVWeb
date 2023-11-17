using AsturianuTV.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Character> _characterRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<League> _leagueRepository;

        public HomeController(
            IRepository<Character> characterRepository,
            IRepository<Item> itemRepository,
            IRepository<News> newsRepository,
            IRepository<League> leagueRepository)
        {
            _characterRepository = characterRepository;
            _itemRepository = itemRepository;
            _newsRepository = newsRepository;
            _leagueRepository = leagueRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cybersports()
        {
            var news = await _newsRepository.Read()
                .Include(x => x.NewsMaterials)
                    .ThenInclude(x => x.Material)
                .Include(x => x.NewsTags)
                    .ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Players()
        {
            var leagues = await _leagueRepository.Read().ToListAsync();

            var resources = leagues.Select(l => new {
                id = l.Id.ToString(),
                title = l.Name
            }).ToList();

            var events = leagues.Select(l => new {
                resourceId = l.Id.ToString(),
                title = l.Name,
                start = l.StartDate.ToString("O"),
                end = l.EndDate.ToString("O"),
                allDay = false
            }).ToList();

            ViewBag.Resources = JsonConvert.SerializeObject(resources);
            ViewBag.Events = JsonConvert.SerializeObject(events);
            ViewBag.CurrentAction = "Players";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Leagues()
        {
            var leagues = await _leagueRepository.Read()
                .OrderByDescending(x => x.EndDate)
                .ToListAsync();

            var resources = leagues.Select(l => new {
                id = l.Id.ToString(),
                title = l.Name
            }).ToList();

            var events = leagues.Select(l => new {
                resourceId = l.Id.ToString(),
                title = l.Name,
                start = l.StartDate.ToString("O"),
                end = l.EndDate.ToString("O"),
                allDay = false
            }).ToList();

            ViewBag.Resources = JsonConvert.SerializeObject(resources);
            ViewBag.Events = JsonConvert.SerializeObject(events);
            ViewBag.CurrentAction = "Leagues";
            return View(leagues);
        }


        [HttpGet]
        public async Task<IActionResult> LeaguesTimeline()
        {
            var leagues = await _leagueRepository.Read()
                .OrderByDescending(x => x.EndDate)
                .ToListAsync();

            var resources = leagues.Select(l => new {
                id = l.Id.ToString(),
                title = l.Name
            }).ToList();

            var events = leagues.Select(l => new {
                resourceId = l.Id.ToString(),
                title = l.Name,
                start = l.StartDate.ToString("O"),
                end = l.EndDate.ToString("O"),
                allDay = false
            }).ToList();

            ViewBag.Resources = JsonConvert.SerializeObject(resources);
            ViewBag.Events = JsonConvert.SerializeObject(events);
            ViewBag.CurrentAction = "Leagues";
            return View(leagues);
        }

        [HttpGet]
        public async Task<IActionResult> BaseKnowledge(CancellationToken cancellationToken)
        {
            var characters = await _characterRepository.Read()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var items = await _itemRepository.Read()
               .AsNoTracking()
               .ToListAsync(cancellationToken);

            var result = new KnowledgeDto
            {
                AgilityHeroes = characters.Where(x => x.Attribute == CharacterAttribute.Agility).ToList(),
                StrenghtHeroes = characters.Where(x => x.Attribute == CharacterAttribute.Strength).ToList(),
                IntHeroes = characters.Where(x => x.Attribute == CharacterAttribute.Intelligence).ToList(),
                UniversalHeroes = characters.Where(x => x.Attribute == CharacterAttribute.None).ToList(),
                NeutralItems = items.Where(x => x.Price == null).ToList(),
                DefaultItems = items.Where(x => x.Price != null).ToList()
            };

            return View(result);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
