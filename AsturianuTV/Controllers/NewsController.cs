using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;
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
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<NewsMaterial> _newsMaterialRepository;
        private readonly IRepository<NewsTag> _newsTagRepository;
        private readonly IMapper _mapper;

        public NewsController(
            IRepository<News> newsRepository,
            IRepository<Tag> tagRepository,
            IRepository<Material> materialRepository,
            IRepository<NewsMaterial> newsMaterialRepository,
            IRepository<NewsTag> newsTagRepository,
            IMapper mapper)
        {
            _newsRepository = newsRepository ?? throw new System.ArgumentNullException(nameof(newsRepository));
            _tagRepository = tagRepository;
            _materialRepository = materialRepository;
            _newsMaterialRepository = newsMaterialRepository;
            _newsTagRepository = newsTagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellation) =>
            View(await _newsRepository.ListAsync(cancellation));

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var news = await _newsRepository
                    .Read()
                    .AsNoTracking()
                    .Include(x => x.NewsMaterials)
                        .ThenInclude(x => x.Material)
                    .Include(x => x.NewsTags)
                        .ThenInclude(x => x.Tag)
                    .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (news != null)
                    return View(news);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken) => 
            View(new NewsDto
            {
                Tags = await _tagRepository.ListAsync(cancellationToken),
                Materials = await _materialRepository.Read().AsNoTracking().Where(x => x.IsNewsMaterial).Distinct().ToListAsync(cancellationToken)
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel newsViewModel)
        {
            if (newsViewModel != null)
            {
                var news = _mapper.Map<News>(newsViewModel);
                await _newsRepository.AddAsync(news);

                if (newsViewModel.Tags != null)
                {
                    foreach (var tag in newsViewModel.Tags)
                    {
                        await _newsTagRepository.AddAsync(new NewsTag { TagId = tag,  NewsId = news.Id });
                    }
                }

                if (newsViewModel.Materials != null)
                {
                    foreach (var material in newsViewModel.Materials)
                    {
                        await _newsMaterialRepository.AddAsync(new NewsMaterial
                        {
                            MaterialId = material, 
                            NewsId = news.Id
                        });
                    }
                }
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
                var news = await _newsRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (news != null)
                {
                    var newsDto = new UpdateNewsDto
                    {
                        Id = news.Id,
                        Title = news.Title,
                        Description = news.Description,
                        IsReady = news.IsReady,
                        Tags = await _tagRepository.ListAsync(cancellationToken),
                        Materials = await _materialRepository.Read().AsNoTracking().Where(x => x.IsNewsMaterial).ToListAsync(cancellationToken)
                    };
                    return View(newsDto);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateNewsViewModel newsViewModel, CancellationToken cancellationToken)
        {
            if (newsViewModel != null)
            {
                var news = _mapper.Map<News>(newsViewModel);
                await _newsRepository.UpdateAsync(news);

                if (newsViewModel.Tags != null)
                {
                    var removeTags = await _newsTagRepository
                        .Read()
                        .AsNoTracking()
                        .Where(x => x.NewsId == newsViewModel.Id)
                        .ToListAsync(cancellationToken);

                    await _newsTagRepository.DeleteRangeAsync(removeTags, cancellationToken);

                    foreach (var tag in newsViewModel.Tags)
                    {
                        await _newsTagRepository.AddAsync(new NewsTag { TagId = tag, NewsId = news.Id });
                    }
                }

                if (newsViewModel.Materials != null)
                {
                    var removeMaterials = await _newsMaterialRepository
                        .Read()
                        .AsNoTracking()
                        .Where(x => x.NewsId == newsViewModel.Id)
                        .ToListAsync(cancellationToken);

                    await _newsMaterialRepository.DeleteRangeAsync(removeMaterials, cancellationToken);

                    foreach (var material in newsViewModel.Materials)
                    {
                        await _newsMaterialRepository.AddAsync(new NewsMaterial { MaterialId = material, NewsId = news.Id });
                    }
                }
            }
            else
                NotFound();

            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var news = await _newsRepository.Read()
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

            await _newsRepository.DeleteAsync(news);
            return RedirectToAction("Index", "News");
        }
    }
}
