using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.ViewModels.System.ItemViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Controllers
{
    public class ItemController : Controller
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public ItemController(
            IRepository<Item> itemRepository,
            IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken) => 
            View(await _itemRepository.ListAsync(cancellationToken));

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemViewModel itemViewModel)
        {            
            var item = _mapper.Map<Item>(itemViewModel);

            await _itemRepository.AddAsync(item);
            return RedirectToAction("Items", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateItemViewModel itemViewModel)
        {
            var item = await _itemRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == itemViewModel.Id);

            _mapper.Map(itemViewModel, item);
            await _itemRepository.UpdateAsync(item);
            return RedirectToAction("Items", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
           
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.Read()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                await _itemRepository.DeleteAsync(item);
                return RedirectToAction("Items", "Admin");
        
        }
    }
}
