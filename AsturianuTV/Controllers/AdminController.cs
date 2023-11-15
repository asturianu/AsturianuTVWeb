using AsturianuTV.Dto.OpenDotaSync;
using AsturianuTV.Infrastructure.Data.Enums;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRepository<League> _leagueRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Player> _playerRepository;

        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<PlayerMatchStats> _playerMatchStatsRepository;

        private readonly IRepository<News> _newsRepository;

        private readonly IRepository<Character> _heroeRepository;
        private readonly IRepository<Skill> _skillRepository;
        private readonly IRepository<Item> _itemRepository;

        public AdminController(
            IHttpClientFactory httpClientFactory,
            IRepository<League> leagueRepository,
            IRepository<Team> teamRepository,
            IRepository<Player> playerRepository,
            IRepository<Match> matchRepository,
            IRepository<PlayerMatchStats> playerMatchStatsRepository,
            IRepository<News> newsRepository,
            IRepository<Character> heroeRepository,
            IRepository<Skill> skillRepository,
            IRepository<Item> itemRepository) 
        {
            _httpClientFactory = httpClientFactory;
            _leagueRepository = leagueRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
            _playerMatchStatsRepository = playerMatchStatsRepository;
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

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SyncCharacters()
        {
            string url = "https://api.opendota.com/api/heroStats";

            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var heroes = JsonConvert.DeserializeObject<List<OpenDotaCharacterDto>>(jsonString);

                foreach (var hero in heroes)
                {
                    var existCharacter = await _heroeRepository.Read()
                        .AnyAsync(x => x.OpenDotaId == hero.Id);

                    if (!existCharacter)
                    {
                        var character = new Character
                        {
                            OpenDotaId = hero.Id,
                            Name = hero.Localized_Name,
                            Armor = (int)hero.Base_Armor,
                            MagicResist = 25,
                            Mana = (int)hero.Base_Mana,
                            ManaRegeneration = (int)hero.Base_Mana_Regen,
                            Health = (int)hero.Base_Health,
                            HealthRegeneration = (int)hero.Base_Health_Regen,
                            MoveSpeed = (int)hero.Move_Speed,
                            Damage = (int)hero.Base_Attack_Max,
                            ImagePath = hero.Img,
                            Attribute =
                                hero.Primary_Attr == "all" ? CharacterAttribute.None
                                : hero.Primary_Attr == "str" ? CharacterAttribute.Strength
                                : hero.Primary_Attr == "agi" ? CharacterAttribute.Agility
                                : CharacterAttribute.Intelligence,
                            RangeType =
                                hero.Attack_Type == "Ranged" ? RangeType.Range : RangeType.Melee
                        };

                        await _heroeRepository.AddAsync(character);
                    }
                }
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> SyncItems()
        {
            string url = "https://api.opendota.com/api/constants/items";

            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var items = JsonConvert.DeserializeObject<Dictionary<string, OpenDotaItemDto>>(jsonString);

                foreach (var item in items)
                {
                    if (item.Value.dname == null) { continue; }

                    var existItem = await _itemRepository.Read()
                        .AnyAsync(x => x.OpenDotaId == item.Value.Id);

                    if (!existItem)
                    {
                        var newItem = new Item
                        {
                            OpenDotaId = item.Value.Id,
                            Name = item.Value.dname,
                            ImagePath = item.Value.Img,
                            Description = item.Value.notes,
                            Price = item.Value.cost
                        };

                        await _itemRepository.AddAsync(newItem);
                    }
                }
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Matches()
        {
            var match = await _matchRepository.Read()
                .AsNoTracking()
                .Include(x => x.RadiantTeam)
                .Include(x => x.DireTeam)
                .Include(x => x.League)
                .ToListAsync();

            return View(match);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> MatchStats()
        {
            var matchStats = await _playerMatchStatsRepository.Read()
              .AsNoTracking()
              .Include(x => x.Player)
              .Include(x => x.Character)
              .Include(x => x.Match)
                  .ThenInclude(x => x.DireTeam)
              .Include(x => x.Match)
                  .ThenInclude(x => x.RadiantTeam)
              .Include(x => x.Match)
                  .ThenInclude(x => x.League)
              .ToListAsync();

            return View(matchStats);
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
    }
}
