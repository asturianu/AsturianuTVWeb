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

namespace AsturianuTV.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Character> _characterRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(
            IRepository<User> userRepository,
            IRepository<Character> characterRepository,
            IHttpClientFactory httpClientFactory)
        {
            _userRepository = userRepository;
            _characterRepository = characterRepository;
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
        public async Task<IActionResult> CheckMatch(int id, CancellationToken cancellationToken)
        {
            string matchIfo = $" https://api.opendota.com/api/matches/{id}";

            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(matchIfo);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<OpenDotaGeneralInfoUser>(jsonString);
                return View(userInfo);
            }
            return null;
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
