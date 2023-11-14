using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllPlayers();
    }
}
