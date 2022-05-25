using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.BlogViewModels;
using AsturianuTV.ViewModels.System.ItemViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class BlogController : Controller
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;

        public BlogController(
            IRepository<Blog> blogRepository,
            IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
            View(await _blogRepository.ListAsync(cancellationToken));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogViewModel blogViewModel)
        {
            if (blogViewModel != null)
            {
                var blog = _mapper.Map<Blog>(blogViewModel);
                await _blogRepository.AddAsync(blog);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Blog");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var blog = await _blogRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (blog != null)
                    return View(blog);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogViewModel blogViewModel)
        {
            if (blogViewModel != null)
            {
                var blog = _mapper.Map<Blog>(blogViewModel);
                await _blogRepository.UpdateAsync(blog);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Blog");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var blog = await _blogRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (blog != null)
                    return View(blog);
            }

            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var blog = await _blogRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (blog != null)
                    return View(blog);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var blog = await _blogRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (blog != null)
                {
                    await _blogRepository.DeleteAsync(blog);
                    return RedirectToAction("Index", "Blog");
                }
            }
            return NotFound();
        }
    }
}
