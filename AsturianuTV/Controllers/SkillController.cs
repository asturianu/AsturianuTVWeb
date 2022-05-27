using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.SkillViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SkillController : Controller
    {
        private readonly IRepository<Skill> _skillRepository;
        private readonly IRepository<Character> _characterRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        public SkillController(
            IRepository<Skill> skillRepository,
            IRepository<Character> characterRepository,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _skillRepository = skillRepository;
            _characterRepository = characterRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellation) => 
            View(await _skillRepository.Read().AsNoTracking().Include(x => x.Character).ToListAsync(cancellation));

        [HttpGet]
        public async Task<ActionResult<SkillDto>> Create(CancellationToken cancellationToken) =>
             View(new SkillDto { Characters = await _characterRepository.ListAsync(cancellationToken) });

        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillViewModel createSkillViewModel)
        {
            if (createSkillViewModel != null)
            {
                string path = null;
                if (createSkillViewModel.Image != null)
                {
                    path = "/Files/Skills/" + createSkillViewModel.Image.FileName;
                    var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
                    await createSkillViewModel.Image.CopyToAsync(fileStream);
                }
                var skill = _mapper.Map<Skill>(createSkillViewModel);
                skill.ImagePath = path;
                await _skillRepository.AddAsync(skill);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Skill");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var skill = await _skillRepository
                    .Read()
                    .AsNoTracking()
                    .Include(x => x.Character)
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (skill != null)
                    return View(skill);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var skill = await _skillRepository
                    .Read()
                    .AsNoTracking()
                    .Include(x => x.Character)
                    .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (skill != null)
                {
                    var skillDto = _mapper.Map<SkillDto>(skill);
                    skillDto.Characters = await _characterRepository.ListAsync(cancellationToken);
                    return View(skillDto);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSkillViewModel updateSkillViewModel, CancellationToken cancellationToken)
        {
            if (updateSkillViewModel != null)
            {
                var skill = _mapper.Map<Skill>(updateSkillViewModel);
                await _skillRepository.UpdateAsync(skill);
            }
            else
                NotFound();
            
            return RedirectToAction("Index", "Skill");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var skill = await _skillRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (skill != null)
                    return View(skill);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var skill = await _skillRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (skill != null)
                {
                    await _skillRepository.DeleteAsync(skill);
                    return RedirectToAction("Index", "Skill");
                }
            }
            return NotFound();
        }
    }
}
