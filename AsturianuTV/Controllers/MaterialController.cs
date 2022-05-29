using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
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
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;

        public MaterialController(
            IRepository<Material> materialRepository,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _materialRepository = materialRepository;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
             View(await _materialRepository.ListAsync(cancellationToken));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(CreateMaterialViewModel materialViewModel)
        {
            if (materialViewModel != null)
            {
                if (materialViewModel.FilePath != null)
                {
                    var path = "/Files/Materials/" + Guid.NewGuid() + materialViewModel.FilePath.FileName;
                    await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await materialViewModel.FilePath.CopyToAsync(fileStream);
                    }

                    var material = _mapper.Map<Material>(materialViewModel);
                    material.FilePath = path;
                    material.ContentType = materialViewModel.FilePath.ContentType;
                    await _materialRepository.AddAsync(material);
                }
            }
            else
                NotFound();

            return RedirectToAction("Index", "Material");
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
