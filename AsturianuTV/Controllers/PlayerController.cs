using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.PlayerViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IMapper _mapper;

        public PlayerController(
            IRepository<Player> playerRepository,
            IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerViewModel playerViewModel)
        {
            if (playerViewModel != null)
            {
                var player = _mapper.Map<Player>(playerViewModel);
                await _playerRepository.AddAsync(player);
            }
            else
            {
                NotFound();
            }

            return RedirectToAction("Players", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.Read()
                .AsNoTracking()
                .Include(x => x.Team)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePlayerViewModel playerViewModel)
        {
            if (playerViewModel != null)
            {
                var player = await _playerRepository.Read()
                    .FirstOrDefaultAsync(x => x.Id == playerViewModel.Id);

                _mapper.Map(player, playerViewModel);
                await _playerRepository.UpdateAsync(player);
            }
            else
            {
                NotFound();
            }

            return RedirectToAction("Players", "Admin");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id, CancellationToken cancellationToken)
        {
            var character = await _playerRepository.Read()
                .AsNoTracking()
                .Include(x => x.Team)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(character);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var character = await _playerRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            await _playerRepository.DeleteAsync(character);
            return RedirectToAction("Players", "Admin");
        }
    }
}
