using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.MatchViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class MatchController : Controller
    {
        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<PlayerMatchStats> _matchPlayerStatsRepository;
        private readonly IMapper _mapper;

        public MatchController(
            IRepository<Match> matchRepository,
            IRepository<PlayerMatchStats> matchPlayerStatsRepository,
            IMapper mapper)
        {
            _matchRepository = matchRepository;
            _matchPlayerStatsRepository = matchPlayerStatsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateMatchViewModel createMatchViewModel,
            CancellationToken cancellationToken)
        {
            if (createMatchViewModel != null)
            {
                var match = _mapper.Map<Match>(createMatchViewModel);
                await _matchRepository.AddAsync(match, cancellationToken);
            }

            else { NotFound(); }
            return RedirectToAction("Matches", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.Read()
                .AsNoTracking()
                .Include(x => x.RadiantTeam)
                .Include(x => x.DireTeam)
                .Include(x => x.League)
                .Include(x => x.Stats)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(match);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
          UpdateMatchViewModel updateMatchViewModel,
          CancellationToken cancellationToken)
        {
            if (updateMatchViewModel != null)
            {
                var match = await _matchRepository.Read()
                    .FirstOrDefaultAsync(x => x.Id == updateMatchViewModel.Id, cancellationToken);

                _mapper.Map(updateMatchViewModel, match);

                await _matchRepository.UpdateAsync(match);
               
                if (updateMatchViewModel?.Stats?.Any() ?? false)
                {
                    var deleteStats = await _matchPlayerStatsRepository.Read()
                        .Where(x => x.MatchId == match.Id)
                        .ToListAsync();

                    await _matchPlayerStatsRepository.DeleteRangeAsync(deleteStats, cancellationToken);

                    var listOfStats = new List<PlayerMatchStats>();

                    foreach (var item in updateMatchViewModel.Stats)
                    {
                        listOfStats.Add(new PlayerMatchStats
                        {
                            MatchId = match.Id,
                            HeroId = item.HeroId,
                            PlayerId = item.PlayerId,
                            Kills = item.Kills,
                            Deaths = item.Deaths,
                            Assists = item.Assists,
                            GoldEarned = item.GoldEarned,
                            ExperienceEarned = item.ExperienceEarned
                        });
                    }

                    await _matchPlayerStatsRepository.AddRangeAsync(listOfStats);
                }
            }

            else { NotFound(); }
            return RedirectToAction("Matches", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            await _matchRepository.DeleteAsync(match);
            return RedirectToAction("Matches", "Admin");
        }
    }
}
