using AsturianuTV.Dto.OpenDotaSync;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.AnalizeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class AutopickerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRepository<Character> _characterRepository;

        public AutopickerController(
            IHttpClientFactory httpClientFactory,
            IRepository<Character> characterRepository)
        {
            _httpClientFactory = httpClientFactory;
            _characterRepository = characterRepository;
        }

        public IActionResult Autopicker()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnalizePicks([FromBody] AnalyzePickViewModel model)
        {
            ValidationData(model);

            var listBestHeroesMyTeam = new Dictionary<Character, List<OpenDotaCharacterMatchupDto>>();
            var listWorstHeroesMyTeam = new Dictionary<Character, List<OpenDotaCharacterMatchupDto>>();
            var listBestHeroesEnemyTeam = new Dictionary<Character, List<OpenDotaCharacterMatchupDto>>();
            var listWorstHeroesEnemyTeam = new Dictionary<Character, List<OpenDotaCharacterMatchupDto>>();

            int countWinRateCalucation = 0;
            float resultWinRate = 0f;

            var actualMyTeamHeroes = model.MyTeamHeroes.Where(x => x != 0).ToList();
            var actualEnemyTeamHeroes = model.EnemyTeamHeroes.Where(x => x != 0).ToList();

            foreach (var myTeamHero in actualMyTeamHeroes)
            {
                var heroeMatchup = await GetHeroeMatchup(myTeamHero);
                if (heroeMatchup == null) { return BadRequest("Bad request"); }

                countWinRateCalucation++;
                var heroeWinRate = GetHeroeWinRate(heroeMatchup, actualEnemyTeamHeroes);
                resultWinRate = (resultWinRate + heroeWinRate) / countWinRateCalucation;

                await AddBestHeroesToPick(listBestHeroesMyTeam, heroeMatchup, myTeamHero);
                await AddWorstHeroesToPick(listWorstHeroesMyTeam, heroeMatchup, myTeamHero);
            }

            foreach (var enemyTeamHeroe in actualEnemyTeamHeroes)
            {
                var heroeMatchup = await GetHeroeMatchup(enemyTeamHeroe);

                await AddBestHeroesToPick(listBestHeroesEnemyTeam, heroeMatchup, enemyTeamHeroe);
                await AddWorstHeroesToPick(listWorstHeroesEnemyTeam, heroeMatchup, enemyTeamHeroe);
            }

            return Json(new
            {
                Success = true,
                Result = new
                {
                    BestHeroesMyTeam = listBestHeroesMyTeam.Select(kvp => new { Hero = kvp.Key, Matchups = kvp.Value }),
                    WorstHeroesMyTeam = listWorstHeroesMyTeam.Select(kvp => new { Hero = kvp.Key, Matchups = kvp.Value }),
                    BestHeroesEnemyTeam = listBestHeroesEnemyTeam.Select(kvp => new { Hero = kvp.Key, Matchups = kvp.Value }),
                    WorstHeroesEnemyTeam = listWorstHeroesEnemyTeam.Select(kvp => new { Hero = kvp.Key, Matchups = kvp.Value })
                },
                Message = "Analysis complete."
            });
        }

        // Добавляет в список героев против которых герой силен
        private async Task AddBestHeroesToPick(
            Dictionary<Character, List<OpenDotaCharacterMatchupDto>> listBestHeroes,
            List<OpenDotaMatchup> heroeMatchups,
            int myTeamHero)
        {
            var bestHeroes = heroeMatchups
                .OrderByDescending(matchup => (matchup.Wins * 100.0f) / matchup.Games_Played)
                .Take(3)
                .ToList();

            var bestHeroeIds = bestHeroes.Select(x => x.Hero_Id).ToList();
            bestHeroeIds.Add(myTeamHero);

            var characters = await _characterRepository.Read()
                 .Where(x => bestHeroeIds.Contains(x.OpenDotaId))
                 .ToListAsync();

            var topMatchups = bestHeroes
                .Select(matchup => new OpenDotaCharacterMatchupDto
                {
                    Id = matchup.Hero_Id,
                    Name = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.Name,
                    ImagePath = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.ImagePath,
                    WinRate = (matchup.Wins * 100.0f) / matchup.Games_Played
                })
                .ToList();

            var key = characters.FirstOrDefault(x => x.OpenDotaId == myTeamHero);

            listBestHeroes.Add(key, topMatchups);
        }

        // Добавляет в список героев против которых герой слаб
        private async Task AddWorstHeroesToPick(
            Dictionary<Character, List<OpenDotaCharacterMatchupDto>> listWortstHeroes,
            List<OpenDotaMatchup> heroeMatchups,
            int myTeamHero)
        {
            var worstHeroes = heroeMatchups
                .OrderBy(matchup => (matchup.Wins * 100.0f) / matchup.Games_Played)
                .Take(3)
                .ToList();

            var worstHeroeIds = worstHeroes.Select(x => x.Hero_Id).ToList();
            worstHeroeIds.Add(myTeamHero);

            var characters = await _characterRepository.Read()
                 .Where(x => worstHeroeIds.Contains(x.OpenDotaId))
                 .ToListAsync();

            var worstMatchups = worstHeroes
                .Select(matchup => new OpenDotaCharacterMatchupDto
                {
                    Id = matchup.Hero_Id,
                    Name = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.Name,
                    ImagePath = characters.FirstOrDefault(x => x.OpenDotaId == matchup.Hero_Id)?.ImagePath,
                    WinRate = (matchup.Wins * 100.0f) / matchup.Games_Played
                })
                .ToList();

            var key = characters.FirstOrDefault(x => x.OpenDotaId == myTeamHero);

            listWortstHeroes.Add(key, worstMatchups);
        }

        private float GetHeroeWinRate(List<OpenDotaMatchup> heroeMatchup, List<int> enemyTeamHeroes)
        {
            var prepareDataCalculation = heroeMatchup.Where(x => enemyTeamHeroes.Contains(x.Hero_Id)).ToList();

            float winRate = 0f;
            int count = 0;
            foreach (var data in prepareDataCalculation)
            {
                float winRateOneHeroe = ((float)data.Wins * 100) / (float)data.Games_Played;
                count++;
                winRate = (winRate + winRateOneHeroe) / count;
            }
            return winRate;
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

        private IActionResult ValidationData(AnalyzePickViewModel model)
        {
            var validModel1 = model.MyTeamHeroes.Where(x => x != 0).ToList();
            var validModel2 = model.EnemyTeamHeroes.Where(x => x != 0).ToList();

            if (model == null || model.MyTeamHeroes == null || model.EnemyTeamHeroes == null || !validModel1.Any())
            {
                return BadRequest("Invalid data");
            }

            if (!validModel2.Any() && validModel1.Any())
            {
                return Json(
                    new
                    {
                        Success = true,
                        Result = 100,
                        Message = "Analysis complete."
                    }
                );
            }
            return null;
        }
    }
}
