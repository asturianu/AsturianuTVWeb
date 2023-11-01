using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.TagViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TagController : Controller
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public TagController(
            IRepository<Tag> tagRepository,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagViewModel tagViewModel)
        {
            var tag = _mapper.Map<Tag>(tagViewModel);
            await _tagRepository.AddAsync(tag);

            return RedirectToAction("Tags", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.Read()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
               
            await _tagRepository.DeleteAsync(tag);
            return RedirectToAction("Tags", "Admin");
        }
    }
}
