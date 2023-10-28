using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.BlogViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class BlogController : Controller
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<Plan> _planRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<BlogMaterial> _blogMaterialRepository;
        private readonly IMapper _mapper;

        public BlogController(
            IRepository<Blog> blogRepository,
            IRepository<Plan> planRepository,
            IRepository<Material> materialRepository,
            IRepository<BlogMaterial> blogMaterialRepository,
            IMapper mapper)
        {
            _blogRepository = blogRepository;
            _planRepository = planRepository;
            _materialRepository = materialRepository;
            _blogMaterialRepository = blogMaterialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
            View(await _blogRepository.Read().AsNoTracking().Include(x=>x.Plan).ToListAsync(cancellationToken));

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken) => 
            View(new BlogDto
            {
                Plans = await _planRepository.ListAsync(cancellationToken),
                Materials = await _materialRepository.Read().AsNoTracking().Where(x => !x.IsNewsMaterial).Distinct().ToListAsync(cancellationToken)
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogViewModel blogViewModel)
        {
            if (blogViewModel != null)
            {
                var blog = _mapper.Map<Blog>(blogViewModel);
                await _blogRepository.AddAsync(blog);

                if (blogViewModel.Materials != null)
                {
                    foreach (var material in blogViewModel.Materials)
                    {
                        await _blogMaterialRepository.AddAsync(new BlogMaterial
                        {
                            MaterialId = material,
                            BlogId = blog.Id
                        });
                    }
                }
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
                    .Include(x => x.Plan)
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (blog != null)
                {
                    var blogDto = new BlogDto
                    {
                        Id = blog.Id,
                        Title = blog.Title,
                        Description = blog.Description,
                        Materials = await _materialRepository.ListAsync(cancellationToken),
                        PlanId = blog.PlanId,
                        Plans = await _planRepository.ListAsync(cancellationToken)
                    };
                    return View(blogDto);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBlogViewModel blogViewModel, CancellationToken cancellationToken)
        {
            if (blogViewModel != null)
            {
                var blog = _mapper.Map<Blog>(blogViewModel);
                await _blogRepository.UpdateAsync(blog);

                if (blogViewModel.Materials != null)
                {
                    var removeMaterials = await _blogMaterialRepository
                        .Read()
                        .AsNoTracking()
                        .Where(x => x.BlogId == blogViewModel.Id)
                        .ToListAsync(cancellationToken);

                    await _blogMaterialRepository.DeleteRangeAsync(removeMaterials, cancellationToken);

                    foreach (var material in blogViewModel.Materials)
                    {
                        await _blogMaterialRepository.AddAsync(new BlogMaterial { MaterialId = material, BlogId = blog.Id });
                    }
                }
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
                    .Include(x => x.Plan)
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
                    .Include(x => x.Plan)
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
