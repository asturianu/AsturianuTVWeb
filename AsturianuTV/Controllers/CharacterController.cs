using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
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
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;

        public CharacterController(
            IRepository<Character> characterRepository,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _characterRepository = characterRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) 
            => View(await _characterRepository.Read().Include(x => x.Skills).ToListAsync(cancellationToken));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogViewModel characterViewModel)
        {
            if (characterViewModel != null)
            {
                string path = "/Files/Character/" + characterViewModel.CharacterImage.FileName;
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await characterViewModel.CharacterImage.CopyToAsync(fileStream);
                }
                var character = _mapper.Map<Character>(characterViewModel);
                character.ImagePath = path;
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
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (character != null)
                    return View(character);
            }
            return NotFound();
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateMaterialViewModel characterViewModel)
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
        public async Task<IActionResult> Delete([FromRoute]int? id, CancellationToken cancellationToken)
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
