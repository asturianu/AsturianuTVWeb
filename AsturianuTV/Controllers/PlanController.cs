using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Dto;
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
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public PlanController(
            IRepository<Plan> planRepository,
            IRepository<Subscription> subscriptionRepository,
            IMapper mapper)
        {
            _planRepository = planRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
            View(await _planRepository
                .Read()
                .AsNoTracking()
                .Include(x => x.Subscription)
                .Include(x => x.Blogs)
                .ToListAsync(cancellationToken));

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken) => 
            View(new PlanDto { Subscriptions = await _subscriptionRepository.ListAsync(cancellationToken)});

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
                {
                    var planDto = _mapper.Map<PlanDto>(plan);
                    planDto.Subscriptions = await _subscriptionRepository.ListAsync(cancellationToken);
                    return View(planDto);
                }
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
