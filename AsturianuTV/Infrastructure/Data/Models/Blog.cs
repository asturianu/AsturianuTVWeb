using System.Collections;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Material> Materials { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
    }
}
