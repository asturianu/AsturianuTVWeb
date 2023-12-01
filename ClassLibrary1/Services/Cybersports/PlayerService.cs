using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Cybersports
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepository;

        public PlayerService(IRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<List<Player>> GetAllPlayers()
        {
            return _playerRepository.Read().ToListAsync();
        }
    }
}
