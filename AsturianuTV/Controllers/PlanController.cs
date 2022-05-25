using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.PlanViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class PlanController : Controller
    {
        private readonly IRepository<Plan> _planRepository;
        private readonly IMapper _mapper;

        public PlanController(
            IRepository<Plan> planRepository,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
            View(await _planRepository.ListAsync(cancellationToken));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlanViewModel planViewModel)
        {
            if (planViewModel != null)
            {
                var plan = _mapper.Map<Plan>(planViewModel);
                await _planRepository.AddAsync(plan);
            }
            else
                NotFound();
            return RedirectToAction("Index", "Plan");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var plan = await _planRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (plan != null)
                    return View(plan);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePlanViewModel planViewModel)
        {
            if (planViewModel != null)
            {
                var plan = _mapper.Map<Plan>(planViewModel);
                await _planRepository.UpdateAsync(plan);
            }
            else
                NotFound();
            return RedirectToAction("Index", "Plan");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var plan = await _planRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (plan != null)
                    return View(plan);
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var plan = await _planRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (plan != null)
                    return View(plan);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var plan = await _planRepository
                    .Read()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (plan != null)
                {
                    await _planRepository.DeleteAsync(plan);
                    return RedirectToAction("Index", "Plan");
                }
            }
            return NotFound();
        }
    }
}
