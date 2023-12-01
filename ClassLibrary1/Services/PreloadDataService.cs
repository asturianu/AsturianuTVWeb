using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsturianuTV.Services
{
    public class PreloadDataService : IPreloadDataService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

       
        public PreloadDataService(
            IRepository<User> userRepository,
            IRepository<Material> materialRepository,
            IRepository<Tag> tagRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _materialRepository = materialRepository;
            _tagRepository = tagRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<List<Material>> GetMaterials()
        {
            return _materialRepository.Read().ToListAsync();
        }

        public Task<List<Tag>> GetTags()
        {
            return _tagRepository.Read().ToListAsync();
        }

        public async Task<User> PreloadData()
        {
            var userName = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault()?.Value;

            return await _userRepository.Read()
                .AsNoTracking()
                .Include(x => x.Role)
                .Where(x => x.Email == userName)
                .FirstOrDefaultAsync();
        }
    }
}
