using System.Net.Mime;
using AsturianuTV.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace AsturianuTV.ViewModels.System.ItemViewModels
{
    public class CreateItemViewModel
    {
        public string Name { get; set; }
        public IFormFile ItemImage { get; set; }
        public string Description { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public ItemType ItemType { get; set; }
        public int? CoolDown { get; set; }
        public int Price { get; set; }
    }
}
