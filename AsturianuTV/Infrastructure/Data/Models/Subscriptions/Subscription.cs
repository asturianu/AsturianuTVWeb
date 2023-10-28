using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.Infrastructure.Data.Models.Subscriptions
{
    public class Subscription : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SubscriptionLevel Level { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public int Price { get; set; }
        public bool IsReady { get; set; }
    }
}
