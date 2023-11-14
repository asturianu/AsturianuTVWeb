using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.LeagueViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class LeagueController : Controller
    {
        private readonly IRepository<League> _legueRepository;
        private readonly IRepository<LeagueTeam> _leagueTeamRepository;
        private readonly IMapper _mapper;

        public LeagueController(
            IRepository<League> legueRepository,
            IRepository<LeagueTeam> leagueTeamRepository,
            IMapper mapper)
        {
            _legueRepository = legueRepository;
            _leagueTeamRepository = leagueTeamRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateLeagueViewModel createLegueViewModel,
            CancellationToken cancellationToken)
        {
            if (createLegueViewModel != null)
            {
                var league = _mapper.Map<League>(createLegueViewModel);
                var result = await _legueRepository.AddAsync(league);

                var listOfLeagueTeams = new List<LeagueTeam>();

                foreach (var item in createLegueViewModel.TeamIds)
                {
                    listOfLeagueTeams.Add(new LeagueTeam
                    {
                        TeamId = item,
                        LeagueId = result.Id
                    });
                }
                await _leagueTeamRepository.AddRangeAsync(listOfLeagueTeams);
            }

            else { NotFound(); }
            return RedirectToAction("Leagues", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var legue = await _legueRepository.Read()
                .AsNoTracking()
                .Include(x => x.LeagueTeams)
                .ThenInclude(x => x.Team)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(legue);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
           UpdateLeagueViewModel updateLegueViewModel,
           CancellationToken cancellationToken)
        {
            if (updateLegueViewModel != null)
            {
                var league = await _legueRepository.Read()
                    .FirstOrDefaultAsync(x => x.Id == updateLegueViewModel.Id, cancellationToken);

                _mapper.Map(league, updateLegueViewModel);

                await _legueRepository.UpdateAsync(league);

                var teamLeagues = await _leagueTeamRepository.Read()
                    .Where(x => x.LeagueId == league.Id)
                    .ToListAsync(cancellationToken);

                await _leagueTeamRepository.DeleteRangeAsync(teamLeagues, cancellationToken);

                var listOfLeagueTeams = new List<LeagueTeam>();

                foreach (var item in updateLegueViewModel.TeamIds)
                {
                    listOfLeagueTeams.Add(new LeagueTeam
                    {
                        TeamId = item,
                        LeagueId = league.Id
                    });
                }

                await _leagueTeamRepository.AddRangeAsync(listOfLeagueTeams);
            }

            else { NotFound(); }
            return RedirectToAction("Leagues", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var league = await _legueRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            await _legueRepository.DeleteAsync(league);
            return RedirectToAction("Leagues", "Admin");
        }
    }
}
