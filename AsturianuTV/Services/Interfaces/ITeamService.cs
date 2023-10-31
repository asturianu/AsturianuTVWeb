using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeams(CancellationToken cancellationToken = default);
    }
}
