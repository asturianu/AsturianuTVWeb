using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class NewsMaterial : BaseEntity
    {
        public int? NewsId { get; set; }
        public News News { get; set; }
        public int? MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
