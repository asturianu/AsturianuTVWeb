using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AsturianuTV.ViewModels.System.UserViewModels;
using System.Net.Http;
using AsturianuTV.Dto.OpenDotaSync;
using Newtonsoft.Json;
using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Dto.OpenDotaSync.OpenDotaMatch;
using System.Linq;

namespace AsturianuTV.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Character> _characterRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(
            IRepository<User> userRepository,
            IRepository<Character> characterRepository,
            IRepository<Item> itemRepository,
            IHttpClientFactory httpClientFactory)
        {
            _userRepository = userRepository;
            _characterRepository = characterRepository;
            _itemRepository = itemRepository;
            _httpClientFactory = httpClientFactory;
        }

        // my id: 185724694

        [HttpGet]
        public async Task<IActionResult> Statistic(string id)
        {
            var userGeneralInfo = await GetOpenDotaPlayerInfo(id);
            var userWinLoseStatistic = await GetOpenDotaWinLose(id);
            var userMatches = await GetOpenDotaPlayerMatches(id);
            var userHeroes = await GetOpenDotaPlayerHeroes(id);

            var result = new OpenDotaStatisticDto
            {
                OpenDotaGeneralInfoUser = userGeneralInfo,
                OpenDotaWinLose = userWinLoseStatistic,
                OpenDotaPlayerMatches = userMatches,
                OpenDotaPlayerHeroes = userHeroes
            };

            return View(result);
        }

        private async Task<List<OpenDotaPlayerMatch>> GetOpenDotaPlayerMatches(string steamProfileId)
        {
            string urlPlayerMatches = $"https://api.opendota.com/api/players/{steamProfileId}/matches";
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(urlPlayerMatches);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var openDotaPlayerMatches = JsonConvert.DeserializeObject<List<OpenDotaPlayerMatch>>(jsonString);

                foreach (var match in openDotaPlayerMatches)
                {
                    var heroe = await _characterRepository.Read()
                        .FirstOrDefaultAsync(x => x.OpenDotaId == match.Hero_Id);
                    match.Character = heroe;
                }

                return openDotaPlayerMatches;
            }
            return null;
        }

        private async Task<List<OpenDotaPlayerHeroe>> GetOpenDotaPlayerHeroes(string steamProfileId)
        {
            string urlPlayerHeroes = $"https://api.opendota.com/api/players/{steamProfileId}/heroes";
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(urlPlayerHeroes);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var opendotaPlayerHeroes = JsonConvert.DeserializeObject<List<OpenDotaPlayerHeroe>>(jsonString);

                foreach (var heroe in opendotaPlayerHeroes)
                {
                    var character = await _characterRepository.Read()
                        .FirstOrDefaultAsync(x => x.OpenDotaId == heroe.Hero_Id);
                    heroe.Character = character;
                }

                return opendotaPlayerHeroes;
            }
            return null;
        }

        private async Task<OpenDotaWinLose> GetOpenDotaWinLose(string steamProfileId)
        {
            string urlWinLose = $"https://api.opendota.com/api/players/{steamProfileId}/wl";
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(urlWinLose);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var winLose = JsonConvert.DeserializeObject<OpenDotaWinLose>(jsonString);
                return winLose;
            }
            return null;
        }

        private async Task<OpenDotaGeneralInfoUser> GetOpenDotaPlayerInfo(string steamProfileId)
        {
            string urlGeneralInfo = $"https://api.opendota.com/api/players/{steamProfileId}";
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(urlGeneralInfo);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<OpenDotaGeneralInfoUser>(jsonString);
                return userInfo;
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> SettingsProfile(CancellationToken cancellationToken)
        {
            var user = await _userRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email.Equals(User.Identity.Name), cancellationToken);

            if (user == null)
            {
                return View();
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Read()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CheckMatch(string id, CancellationToken cancellationToken)
        {
            string matchIfo = $"https://api.opendota.com/api/matches/{id}";

            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(matchIfo);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var matchInfo = JsonConvert.DeserializeObject<OpenDotaMatch>(jsonString);
                await Initialize(matchInfo);
                return View(matchInfo);
            }
            return NotFound();
        }

        private async Task Initialize(OpenDotaMatch matchInfo)
        {
            var pbs = matchInfo.picks_bans.Select(x => x.hero_id).ToList();
            var pls = matchInfo.players.Select(x => x.hero_id).ToList();

            var resultHeroeIds = pbs.Union(pls).Distinct().ToList();

            List<int?> itemIds = new List<int?>();

            var result1Ids = matchInfo.players.Where(x => x.item_0 != 0).Select(x => x.item_0).ToList();
            var result2Ids = matchInfo.players.Where(x => x.item_1 != 0).Select(x => x.item_1).ToList();
            var result3Ids = matchInfo.players.Where(x => x.item_2 != 0).Select(x => x.item_2).ToList();
            var result4Ids = matchInfo.players.Where(x => x.item_3 != 0).Select(x => x.item_3).ToList();
            var result5Ids = matchInfo.players.Where(x => x.item_4 != 0).Select(x => x.item_4).ToList();
            var result6Ids = matchInfo.players.Where(x => x.item_5 != 0).Select(x => x.item_5).ToList();
            var result7Ids = matchInfo.players.Where(x => x.item_neutral != 0).Select(x => x.item_neutral).ToList();
            itemIds.AddRange(result1Ids);
            itemIds.AddRange(result2Ids);
            itemIds.AddRange(result3Ids);
            itemIds.AddRange(result4Ids);
            itemIds.AddRange(result5Ids);
            itemIds.AddRange(result6Ids);
            itemIds.AddRange(result7Ids);

            var resultItemIds = itemIds.Distinct().ToList();

            var heroes = await _characterRepository.Read()
                .Where(x => resultHeroeIds.Contains(x.OpenDotaId))
                .ToListAsync();

            var items = await _itemRepository.Read()
                .Where(x => resultItemIds.Contains(x.OpenDotaId))
                .ToListAsync();

            foreach (var player in matchInfo.players)
            {
                player.heroe = heroes.Where(x => player.hero_id == x.OpenDotaId).FirstOrDefault();
                player.item_0obj = items.Where(x => player.item_0 == x.OpenDotaId).FirstOrDefault();
                player.item_1obj = items.Where(x => player.item_1 == x.OpenDotaId).FirstOrDefault();
                player.item_2obj = items.Where(x => player.item_2 == x.OpenDotaId).FirstOrDefault();
                player.item_3obj = items.Where(x => player.item_3 == x.OpenDotaId).FirstOrDefault();
                player.item_4obj = items.Where(x => player.item_4 == x.OpenDotaId).FirstOrDefault();
                player.item_5obj = items.Where(x => player.item_5 == x.OpenDotaId).FirstOrDefault();
                player.item_neutral_obj = items.Where(x => player.item_neutral == x.OpenDotaId).FirstOrDefault();
            }

            foreach (var pb in matchInfo.picks_bans)
            {
                pb.Character = heroes.Where(x => pb.hero_id == x.OpenDotaId).FirstOrDefault();
            }

            var disctionary1 = new Dictionary<OpenDotaPickBan, OpenDotaMatchPlayer>();
            var disctionary2 = new Dictionary<OpenDotaPickBan, OpenDotaMatchPlayer>();

            var picks1 = matchInfo.picks_bans.Where(x => x.team == 0 && x.is_pick).ToList();
            var picks2 = matchInfo.picks_bans.Where(x => x.team == 1 && x.is_pick).ToList();

            foreach (var item in picks1)
            {
                var gg = matchInfo.players.Where(x => x.hero_id == item.hero_id).FirstOrDefault();
                disctionary1.Add(item, gg);
            }

            foreach (var item in picks2)
            {
                var gg = matchInfo.players.Where(x => x.hero_id == item.hero_id).FirstOrDefault();
                disctionary2.Add(item, gg);
            }
            matchInfo.fteam = disctionary1;
            matchInfo.steam = disctionary2;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateUserViewModel userViewModel, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Read()
                .SingleOrDefaultAsync(x => x.Id == userViewModel.Id, cancellationToken);

            if (user != null)
            {
                user.Surname = userViewModel.Surname;
                user.Name = userViewModel.Name;
                user.Email = userViewModel.Email;
                user.Password = userViewModel.Password;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.SteamProfileId = userViewModel.SteamProfileId;

                await _userRepository.UpdateAsync(user);
            }

            return RedirectToAction("SettingsProfile", "User");
        }
    }
}
