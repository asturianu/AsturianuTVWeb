using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.ViewModels.System.ItemViewModels
{
    public class CreateItemViewModel
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int? CoolDown { get; set; }
        public int Price { get; set; }
    }
}
