using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsturianuTV.Services.Interfaces
{
    public interface IHeroeService
    {
        Task<List<Character>> GetAllCharacters();
    }
}
