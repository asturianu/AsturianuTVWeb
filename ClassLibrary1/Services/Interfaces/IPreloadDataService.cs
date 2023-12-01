using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Interfaces
{
    public interface IPreloadDataService
    {
        Task<User> PreloadData();
        Task<List<Material>> GetMaterials();
        Task<List<Tag>> GetTags();
    }
}
