using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Data.Enums;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;

namespace AsturianuTV.ViewModels.System.SubscriptionViewModels
{
    public class UpdateSubscriptionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SubscriptionLevel Level { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
    }
}
