using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
using AsturianuTV.Dto.OpenDotaSync;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.CharacterViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AsturianuTV.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IRepository<Character> _characterRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public CharacterController(
            IRepository<Character> characterRepository,
            IRepository<Item> itemRepository,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            _characterRepository = characterRepository;
            _itemRepository = itemRepository;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            var heroe = await _characterRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == id);

            var openDotaId = heroe.OpenDotaId;

            var matchUp = await GetHeroeMatchup(openDotaId);
            var listBestHeroes = await AddBestHeroesToPick(matchUp, openDotaId);
            var listWorstHeroes = await AddWorstHeroesToPick(matchUp, openDotaId);

            var popularItems = await GetPopularItemsForHeroe(openDotaId);

            var startitems = await PopularItemsStart(popularItems);
            var earlyitems = await PopularItemsEarly(popularItems);
            var miditems = await PopularItemsMid(popularItems);
            var lateitems = await PopularItemsLate(popularItems);

            var result = new HeroeDetailsResultDto
            {
                Id = heroe.Id,
                Attribute = heroe.Attribute,
                Armor = heroe.Armor,
                RangeType = heroe.RangeType,
                Health = heroe.Health,
                Mana = heroe.Mana,
                HealthRegeneration = heroe.HealthRegeneration,
                ManaRegeneration = heroe.ManaRegeneration,
                MagicResist = heroe.MagicResist,
                MoveSpeed = heroe.MoveSpeed,
                Damage = heroe.Damage,
                OpenDotaId = heroe.OpenDotaId,
                ImagePath = heroe.ImagePath,
                Name = heroe.Name,
                Description = heroe.ShortDescription,
                Best = listBestHeroes,
                Worst = listWorstHeroes,
                StartGameItems = startitems,
                EarlyGameItems = earlyitems,
                MidGameItems = miditems,
                LateGameItems = lateitems
            };

            return View(result);
        }

        private Task<List<PopularItemsResultDto>> PopularItemsStart(
            OpenDotaItemPopularity items)
        {
             var itemIds = items.start_game_items
                .Where(pair => int.TryParse(pair.Key, out _)) 
                .ToDictionary(
                    pair => int.Parse(pair.Key), 
                    pair => pair.Value          
                );

            return _itemRepository.Read()
                .AsNoTracking()
                .Where(x => itemIds.Keys.Contains(x.OpenDotaId))
                .Select(t => new PopularItemsResultDto
                {
                    Id = t.Id,
                    OpenDotaId = t.OpenDotaId,
                    Description = t.Description,
                    Logo = t.ImagePath,
                    Count = itemIds[t.OpenDotaId],
                    Name = t.Name
                })
                .ToListAsync();
        }

        private Task<List<PopularItemsResultDto>> PopularItemsEarly(
            OpenDotaItemPopularity items)
        {
            var itemIds = items.early_game_items
               .Where(pair => int.TryParse(pair.Key, out _))
               .ToDictionary(
                   pair => int.Parse(pair.Key),
                   pair => pair.Value
               );

            return _itemRepository.Read()
                .AsNoTracking()
                .Where(x => itemIds.Keys.Contains(x.OpenDotaId))
                .Select(t => new PopularItemsResultDto
                {
                    Id = t.Id,
                    OpenDotaId = t.OpenDotaId,
                    Description = t.Description,
                    Logo = t.ImagePath,
                    Count = itemIds[t.OpenDotaId],
                    Name = t.Name
                })
                .ToListAsync();
        }

        private Task<List<PopularItemsResultDto>> PopularItemsMid(
            OpenDotaItemPopularity items)
        {
            var itemIds = items.mid_game_items
               .Where(pair => int.TryParse(pair.Key, out _))
               .ToDictionary(
                   pair => int.Parse(pair.Key),
                   pair => pair.Value
               );

            return _itemRepository.Read()
                .AsNoTracking()
                .Where(x => itemIds.Keys.Contains(x.OpenDotaId))
                .Select(t => new PopularItemsResultDto
                {
                    Id = t.Id,
                    OpenDotaId = t.OpenDotaId,
                    Description = t.Description,
                    Logo = t.ImagePath,
                    Count = itemIds[t.OpenDotaId],
                    Name = t.Name
                })
                .ToListAsync();
        }

        private  Task<List<PopularItemsResultDto>> PopularItemsLate(
            OpenDotaItemPopularity items)
        {
            var itemIds = items.late_game_items
               .Where(pair => int.TryParse(pair.Key, out _))
               .ToDictionary(
                   pair => int.Parse(pair.Key),
                   pair => pair.Value
               );

            return _itemRepository.Read()
                .AsNoTracking()
                .Where(x => itemIds.Keys.Contains(x.OpenDotaId))
                .Select(t => new PopularItemsResultDto
                {
                    Id = t.Id,
                    OpenDotaId = t.OpenDotaId,
                    Description = t.Description,
                    Logo = t.ImagePath,
                    Count = itemIds[t.OpenDotaId],
                    Name = t.Name
                })
                .ToListAsync();
        }

        private async Task<OpenDotaItemPopularity> GetPopularItemsForHeroe(int id)
        {
            string url = $"https://api.opendota.com/api/heroes/{id}/itemPopularity";

            HttpClient items = _httpClientFactory.CreateClient();
            var response = await items.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpenDotaItemPopularity>(jsonString);
            }
            return null;
        }

        // Добавляет в список героев против которых герой силен
        private async Task<List<OpenDotaCharacterMatchupDto>> AddBestHeroesToPick(
            List<OpenDotaMatchup> heroeMatchups,
            int myTeamHero)
        {
            var bestHeroes = heroeMatchups
                .OrderByDescending(matchup => (matchup.Wins * 100.0f) / matchup.Games_Played)
                .Take(5)
                .ToList();

            var bestHeroeIds = bestHeroes.Select(x => x.Hero_Id).ToList();
            bestHeroeIds.Add(myTeamHero);

            var characters = await _characterRepository.Read()
                 .Where(x => bestHeroeIds.Contains(x.OpenDotaId))
                 .ToListAsync();

            return bestHeroes
                .Select(matchup => new OpenDotaCharacterMatchupDto
                {
                    Id = matchup.Hero_Id,
                    MainId = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id).Id,
                    Name = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.Name,
                    ImagePath = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.ImagePath,
                    WinRate = (matchup.Wins * 100.0f) / matchup.Games_Played,
                    Games = matchup.Games_Played,
                    Wins = matchup.Wins
                })
                .ToList();
        }

        // Добавляет в список героев против которых герой слаб
        private async Task<List<OpenDotaCharacterMatchupDto>> AddWorstHeroesToPick(
            List<OpenDotaMatchup> heroeMatchups,
            int myTeamHero)
        {
            var worstHeroes = heroeMatchups
                .OrderBy(matchup => (matchup.Wins * 100.0f) / matchup.Games_Played)
                .Take(5)
                .ToList();

            var worstHeroeIds = worstHeroes.Select(x => x.Hero_Id).ToList();
            worstHeroeIds.Add(myTeamHero);

            var characters = await _characterRepository.Read()
                 .Where(x => worstHeroeIds.Contains(x.OpenDotaId))
                 .ToListAsync();

            return worstHeroes
                .Select(matchup => new OpenDotaCharacterMatchupDto
                {
                    Id = matchup.Hero_Id,
                    MainId = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id).Id,
                    Name = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.Name,
                    ImagePath = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.ImagePath,
                    WinRate = (matchup.Wins * 100.0f) / matchup.Games_Played,
                    Games = matchup.Games_Played,
                    Wins = matchup.Wins
                })
                .ToList();
        }

        private async Task<List<OpenDotaMatchup>> GetHeroeMatchup(int heroeOpenDotaId)
        {
            string url = $"https://api.opendota.com/api/heroes/{heroeOpenDotaId}/matchups";

            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<OpenDotaMatchup>>(jsonString);
            }
            return null;
        }


        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterViewModel characterViewModel)
        {
            var character = _mapper.Map<Character>(characterViewModel);
            await _characterRepository.AddAsync(character);
            return RedirectToAction("Heroes", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
                
                return View(character);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCharacterViewModel characterViewModel)
        {
            var character = await _characterRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == characterViewModel.Id);

            _mapper.Map(characterViewModel, character);
            await _characterRepository.UpdateAsync(character);

            return RedirectToAction("Heroes", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x=>x.Id == id, cancellationToken);
                
            await _characterRepository.DeleteAsync(character);
            return RedirectToAction("Heroes", "Admin");
        }
    }
}
