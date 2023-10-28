using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Interfaces
{
    public interface IPreloadDataService
    {
        Task<User> PreloadData();
    }
}
