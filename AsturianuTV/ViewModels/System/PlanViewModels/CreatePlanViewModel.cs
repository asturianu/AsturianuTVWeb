using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;

namespace AsturianuTV.ViewModels.System.PlanViewModels
{
    public class CreatePlanViewModel
    {
        public string Name { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
