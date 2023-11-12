using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsturianuTV.Services
{
    public class HeroeService : IHeroeService
    {
        private readonly IRepository<Character> _heroeRepository;

        public HeroeService(IRepository<Character> heroeRepository)
        {
            _heroeRepository = heroeRepository;
        }

        public Task<List<Character>> GetAllCharacters()
        {
            return _heroeRepository.Read().ToListAsync();
        }
    }
}
