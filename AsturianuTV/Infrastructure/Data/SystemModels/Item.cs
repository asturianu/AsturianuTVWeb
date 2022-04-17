using AsturianuTV.Infrastructure.Models;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
