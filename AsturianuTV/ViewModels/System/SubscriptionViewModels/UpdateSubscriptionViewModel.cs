using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.ViewModels.System.SubscriptionViewModels
{
    public class UpdateSubscriptionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public SubscriptionLevel Level { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
    }
}
