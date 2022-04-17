using AsturianuTV.Infrastructure.Models;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Skill : BaseEntity
    {
        public int Name { get; set; }
        public string Description { get; set; }
        public int? CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
