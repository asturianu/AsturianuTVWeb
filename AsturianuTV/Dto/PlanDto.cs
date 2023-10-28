using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;

namespace AsturianuTV.Dto
{
    public class PlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SubscriptionId { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
