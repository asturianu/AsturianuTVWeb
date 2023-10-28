using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AsturianuTV.Services
{
    public class PreloadDataService : IPreloadDataService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

       
        public PreloadDataService(
            IRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> PreloadData()
        {
            var userName = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault()?.Value;

            return await _userRepository.Read()
                .AsNoTracking()
                .Include(x => x.Role)
                .Include(x => x.Subscription)
                    .ThenInclude(x => x.Plans)
                .Where(x => x.Email == userName)
                .FirstOrDefaultAsync();
        }
    }
}
