using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System;
using AsturianuTV.ViewModels.System.SkillViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SkillController : Controller
    {
        private readonly IRepository<Skill> _skillRepository;
        private readonly IMapper _mapper;
        public SkillController(
            IRepository<Skill> skillRepository,
            IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellation) => 
            View(await _skillRepository.ListAsync(cancellation));

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillViewModel createSkillViewModel)
        {
            if (createSkillViewModel != null)
            {
                var skill = _mapper.Map<Skill>(createSkillViewModel);
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
                var skill = await _skillRepository.Read()
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
                var skill = await _skillRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
                if (skill != null)
                    return View(skill);
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
