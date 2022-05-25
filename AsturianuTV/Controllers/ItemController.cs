using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsturianuTV.Infrastructure.Data.Models;
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
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;

        public ItemController(
            IRepository<Item> itemRepository,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _itemRepository = itemRepository;
            _appEnvironment = appEnvironment;
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
            if (itemViewModel != null)
            {
                string path = null;
                if (itemViewModel.ItemImage != null)
                {
                    path = "/Files/Item/" + itemViewModel.ItemImage.FileName;
                    var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
                    {
                        await itemViewModel.ItemImage.CopyToAsync(fileStream);
                    }
                }
                var item = _mapper.Map<Item>(itemViewModel);
                item.ImagePath = path;
                await _itemRepository.AddAsync(item);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Item");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var item = await _itemRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (item != null)
                    return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateItemViewModel itemViewModel)
        {
            if (itemViewModel != null)
            {
                var item = _mapper.Map<Item>(itemViewModel);
                await _itemRepository.UpdateAsync(item);
            }
            else
                NotFound();

            return RedirectToAction("Index", "Item");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var item = await _itemRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (item != null)
                    return View(item);
            }

            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var item = await _itemRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (item != null)
                    return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                var item = await _itemRepository
                    .Read()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (item != null)
                {
                    await _itemRepository.DeleteAsync(item);
                    return RedirectToAction("Index", "Item");
                }
            }
            return NotFound();
        }
    }
}
