using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.ItemViewModels;
using AsturianuTV.ViewModels.System.MaterialViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<News> _newsRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;

        public MaterialController(
            IRepository<Material> materialRepository,
            IRepository<Blog> blogRepository,
            IRepository<News> newsRepository,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _materialRepository = materialRepository;
            _blogRepository = blogRepository;
            _newsRepository = newsRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
             View(await _materialRepository
                 .Read()
                 .ToListAsync(cancellationToken));

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken) =>
            View(new MaterialDto
            {
                Blogs = await _blogRepository.ListAsync(cancellationToken),
                News = await _newsRepository.ListAsync(cancellationToken)
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaterialViewModel materialViewModel)
        {
            if (materialViewModel != null)
            {
                if (materialViewModel.FilePaths != null)
                {
                    foreach (var file in materialViewModel.FilePaths)
                    {
                        var path = "/Files/Materials/" + file.FileName;
                        await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        var material = _mapper.Map<Material>(materialViewModel);
                        material.FilePath = path;
                        material.ContentType = file.ContentType;
                        await _materialRepository.AddAsync(material);
                    }
                }
            }
            else
                NotFound();

            return RedirectToAction("Index", "Material");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var material = await _materialRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (material != null)
                {
                    var materialDto = _mapper.Map<MaterialDto>(material);
                    materialDto.Blogs = await _blogRepository.ListAsync(cancellationToken);
                    return View(material);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateMaterialViewModel materialViewModel)
        {
            if (materialViewModel != null)
            {
                foreach (var file in materialViewModel.FilePaths)
                {
                    var path = "/Files/Material/" + file.FileName;
                    var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
                    await file.CopyToAsync(fileStream);

                    var material = _mapper.Map<Material>(materialViewModel);
                    material.FilePath = path;
                    await _materialRepository.UpdateAsync(material);
                }
            }
            else
                NotFound();

            return RedirectToAction("Index", "Material");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var material = await _materialRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (material != null)
                    return View(material);
            }

            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var material = await _materialRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (material != null)
                    return View(material);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var material = await _materialRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (material != null)
                {
                    await _materialRepository.DeleteAsync(material);
                    return RedirectToAction("Index", "Material");
                }
            }
            return NotFound();
        }
    }
}
