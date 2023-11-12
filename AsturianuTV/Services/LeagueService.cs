using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IRepository<League> _leagueRepository;

        public LeagueService(IRepository<League> leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public async Task<List<League>> GetAllLeagues(CancellationToken cancellationToken = default)
        {
            return await _leagueRepository.Read().ToListAsync(cancellationToken);
        }
    }
}
