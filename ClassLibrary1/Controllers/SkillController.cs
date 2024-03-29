﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
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
            var skill = _mapper.Map<Skill>(createSkillViewModel);
            await _skillRepository.AddAsync(skill);

            return RedirectToAction("Skills", "Admin");
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
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.Read()
                .AsNoTracking()
                .Include(x => x.Character)
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

                var skillDto = _mapper.Map<SkillDto>(skill);
                skillDto.Characters = await _characterRepository.ListAsync(cancellationToken);
                return View(skillDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSkillViewModel updateSkillViewModel, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == updateSkillViewModel.Id, cancellationToken);

            _mapper.Map(updateSkillViewModel, skill);
            await _skillRepository.UpdateAsync(skill);
            return RedirectToAction("Skills", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.Read()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

                await _skillRepository.DeleteAsync(skill);
                return RedirectToAction("Skills", "Admin");
        }
    }
}
