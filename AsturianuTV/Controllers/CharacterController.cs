using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.CharacterViewModels;
using AutoMapper;
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
        public async Task<IActionResult> Index(CancellationToken cancellationToken) 
            => View(await _characterRepository.ListAsync(cancellationToken));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCharacterViewModel characterViewModel)
        {
            if (characterViewModel != null)
            {
                var character = _mapper.Map<Character>(characterViewModel);
                await _characterRepository.AddAsync(character);
            }
            else
                NotFound();
            
            return RedirectToAction("Index", "Character");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var character = await _characterRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (character != null)
                    return RedirectToAction("Index", "Character");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCharacterViewModel characterViewModel)
        {
            if (characterViewModel != null)
            {
                var character = _mapper.Map<Character>(characterViewModel);
                await _characterRepository.UpdateAsync(character);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Character");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var character = await _characterRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (character != null)
                    return View(character);
            }

            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var character = await _characterRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (character != null)
                    return View(character);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var character = await _characterRepository
                    .Read()
                    .FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);
                
                if (character != null)
                {
                    await _characterRepository.DeleteAsync(character);
                    return RedirectToAction("Index", "Character");
                }
            }
            return NotFound();
        }
    }
}
