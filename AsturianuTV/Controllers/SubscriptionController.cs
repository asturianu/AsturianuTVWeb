using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel subscriptionViewModel)
        {

            var subscription = _mapper.Map<Subscription>(subscriptionViewModel);
            await _subscriptionRepository.AddAsync(subscription);

            return RedirectToAction("Subscriptions", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.Read()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
         
            return View(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            UpdateSubscriptionViewModel updateSubscriptionViewModel)
        {
            var subscription = await _subscriptionRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == updateSubscriptionViewModel.Id);

            _mapper.Map(updateSubscriptionViewModel, subscription);
            await _subscriptionRepository.UpdateAsync(subscription);
            return RedirectToAction("Subscriptions", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.Read()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            await _subscriptionRepository.DeleteAsync(subscription);
            return RedirectToAction("Subscriptions", "Admin");
        }
    }
}
