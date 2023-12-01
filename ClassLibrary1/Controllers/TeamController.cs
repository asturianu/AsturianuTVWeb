using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.TeamViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class TeamController : Controller
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public TeamController(
            IRepository<Team> teamRepository,
            IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamViewModel createTeamViewModel)
        {
            if (createTeamViewModel != null)
            {
                var team = _mapper.Map<Team>(createTeamViewModel);
                await _teamRepository.AddAsync(team);
            }
            else { NotFound(); }
            return RedirectToAction("Teams", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
           UpdateTeamViewModel updateTeamViewModel,
           CancellationToken cancellationToken)
        {
            if (updateTeamViewModel != null)
            {
                var team = await _teamRepository.Read()
                    .FirstOrDefaultAsync(x => x.Id == updateTeamViewModel.Id, cancellationToken);

                _mapper.Map(updateTeamViewModel, team);
                await _teamRepository.UpdateAsync(team);
            }

            else { NotFound(); }
            return RedirectToAction("Teams", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            await _teamRepository.DeleteAsync(team);
            return RedirectToAction("Teams", "Admin");
        }
    }
}
