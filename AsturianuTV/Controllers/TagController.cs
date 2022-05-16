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
        public async Task<IActionResult> Index(CancellationToken cancellation) =>
            View(await _tagRepository.ListAsync(cancellation));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var tag = await _tagRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (tag != null)
                    return View(tag);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagViewModel tagViewModel)
        {
            if (tagViewModel != null)
            {
                var tag = _mapper.Map<Tag>(tagViewModel);
                await _tagRepository.AddAsync(tag);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Tag");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var tag = await _tagRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
                if (tag != null)
                    return View(tag);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTagViewModel newsViewModel, CancellationToken cancellationToken)
        {
            if (newsViewModel != null)
            {
                var tag = _mapper.Map<Tag>(newsViewModel);
                await _tagRepository.UpdateAsync(tag);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Tag");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var tag = await _tagRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (tag != null)
                    return View(tag);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var tag = await _tagRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (tag != null)
                {
                    await _tagRepository.DeleteAsync(tag);
                    return RedirectToAction("Index", "Tag");
                }
            }
            return NotFound();
        }
    }
}
