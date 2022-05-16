using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.SubscriptionViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionController(
            IRepository<Subscription> subscriptionRepository,
            IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) =>
            View(await _subscriptionRepository.ListAsync(cancellationToken));

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create() => View();
        
        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var subscription = await _subscriptionRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (subscription != null)
                    return View(subscription);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel subscriptionViewModel)
        {

            if (subscriptionViewModel != null)
            {
                var subscription = _mapper.Map<Subscription>(subscriptionViewModel);
                await _subscriptionRepository.AddAsync(subscription);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Subscription");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var subscription = await _subscriptionRepository.Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
                if (subscription != null)
                    return View(subscription);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSubscriptionViewModel updateSkillViewModel, 
            CancellationToken cancellationToken)
        {
            if (updateSkillViewModel != null)
            {
                var subscription = _mapper.Map<Subscription>(updateSkillViewModel);
                await _subscriptionRepository.UpdateAsync(subscription);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Subscription");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var subscription = await _subscriptionRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (subscription != null)
                    return View(subscription);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var subscription = await _subscriptionRepository
                    .Read()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (subscription != null)
                {
                    await _subscriptionRepository.DeleteAsync(subscription);
                    return RedirectToAction("Index", "Subscription");
                }
            }
            return NotFound();
        }
    }
}
