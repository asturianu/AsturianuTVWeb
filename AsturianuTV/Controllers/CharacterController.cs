using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.CharacterViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IRepository<Character> _characterRepository;
        private readonly IMapper _mapper;

        public CharacterController(
            IRepository<Character> characterRepository,
            IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterViewModel characterViewModel)
        {
            var character = _mapper.Map<Character>(characterViewModel);
            await _characterRepository.AddAsync(character);
            return RedirectToAction("Heroes", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
                
                return View(character);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCharacterViewModel characterViewModel)
        {
            var character = await _characterRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == characterViewModel.Id);

            _mapper.Map(characterViewModel, character);
            await _characterRepository.UpdateAsync(character);

            return RedirectToAction("Heroes", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(x=>x.Id == id, cancellationToken);
                
            await _characterRepository.DeleteAsync(character);
            return RedirectToAction("Heroes", "Admin");
        }
    }
}
