using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.NewsViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class NewsController : Controller
    {
        private readonly IRepository<News> _newsRepository;
        private readonly IMapper _mapper;

        public NewsController(
            IRepository<News> newsRepository,
            IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellation) =>
            View(await _newsRepository.ListAsync(cancellation));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var news = await _newsRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (news != null)
                    return View(news);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel newsViewModel)
        {
            if (newsViewModel != null)
            {
                var news = _mapper.Map<News>(newsViewModel);
                await _newsRepository.AddAsync(news);
            }
            else
                NotFound();

            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var news = await _newsRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
                if (news != null)
                    return View(news);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateNewsViewModel newsViewModel, CancellationToken cancellationToken)
        {
            if (newsViewModel != null)
            {
                var skill = _mapper.Map<News>(newsViewModel);
                await _newsRepository.UpdateAsync(skill);
            }
            else
                NotFound();

            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var news = await _newsRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (news != null)
                    return View(news);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var news = await _newsRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (news != null)
                {
                    await _newsRepository.DeleteAsync(news);
                    return RedirectToAction("Index", "News");
                }
            }
            return NotFound();
        }
    }
}
