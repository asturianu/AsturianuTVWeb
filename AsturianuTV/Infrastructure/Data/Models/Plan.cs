using System.Collections;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public ICollection<Blog> Blogs { get; set; } 
    }
}
