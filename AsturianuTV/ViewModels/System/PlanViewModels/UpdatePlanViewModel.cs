using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.ViewModels.System.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
