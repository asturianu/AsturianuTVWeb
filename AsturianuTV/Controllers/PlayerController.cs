using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.PlayerViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Transfer> _transferRepository;
        private readonly IMapper _mapper;

        public PlayerController(
            IRepository<Player> playerRepository,
            IRepository<Transfer> transferRepository,
            IMapper mapper)
        {
            _playerRepository = playerRepository;
            _transferRepository = transferRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerViewModel playerViewModel)
        {
            _playerRepository.BeginTransaction();

            try
            {
                var player = _mapper.Map<Player>(playerViewModel);

                var result = await _playerRepository.AddAsync(player);

                if (playerViewModel.TeamId.HasValue)
                {
                    var countPlayersInTeam = await _playerRepository.Read()
                        .Where(x => x.TeamId == playerViewModel.TeamId.Value
                                 && x.Id != result.Id)
                        .CountAsync();

                    if (countPlayersInTeam >= 5)
                    {
                        _playerRepository.RollbackTransaction();
                        ModelState.AddModelError("", "You cant add the player to this Team");
                        return View(playerViewModel);
                    }
                    else
                    {
                        var newTransfer = new Transfer
                        {
                            PlayerId = result.Id,
                            NewTeamId = playerViewModel.TeamId.Value,
                            TransferDate = DateTime.Today
                        };

                        await _transferRepository.AddAsync(newTransfer);
                    }
                }

                _playerRepository.CommitTransaction();
                return RedirectToAction("Players", "Admin");
            }
            catch
            {
                _playerRepository.RollbackTransaction();
                throw;
            }
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
            var player = await _playerRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == playerViewModel.Id);

            if (player?.TeamId != playerViewModel?.TeamId)
            {
                var newTransfer = new Transfer
                {
                    PlayerId = player.Id,
                    TransferDate = DateTime.Today
                };

                var countPlayersInTeam = await _playerRepository.Read()
                       .Where(x => x.TeamId == playerViewModel.TeamId.Value)
                       .CountAsync();

                // Transfer from Team A to Team B
                if (player.TeamId.HasValue && playerViewModel.TeamId.HasValue)
                {
                    if (countPlayersInTeam >= 5)
                    {
                        ModelState.AddModelError("", "You cant add the player to this Team");
                        return View(playerViewModel);
                    }

                    newTransfer.OldTeamId = player.TeamId.Value;
                    newTransfer.NewTeamId = playerViewModel.TeamId.Value;
                }
                // Transfer to the new Team
                else if (!player.TeamId.HasValue && playerViewModel.TeamId.HasValue)
                {
                    if (countPlayersInTeam >= 5)
                    {
                        ModelState.AddModelError("", "You cant add the player to this Team");
                        return View(playerViewModel);
                    }

                    newTransfer.NewTeamId = playerViewModel.TeamId.Value;
                }
                // Leave from team
                else if (player.TeamId.HasValue && !playerViewModel.TeamId.HasValue)
                {
                    newTransfer.OldTeamId = player.TeamId.Value;
                }
                await _transferRepository.AddAsync(newTransfer);
            }

            _mapper.Map(playerViewModel, player);
            await _playerRepository.UpdateAsync(player);
            return RedirectToAction("Players", "Admin");
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
