using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Dto
{
    public class BlogDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Material> Materials { get; set; }
        public int? PlanId { get; set; }
        public ICollection<Plan> Plans { get; set; }
    }
}
