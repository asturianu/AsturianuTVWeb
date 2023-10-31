using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Cybersports
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public Task<List<Team>> GetAllTeams(CancellationToken cancellationToken = default)
        {
            return _teamRepository.Read().ToListAsync(cancellationToken);
        }
    }
}
