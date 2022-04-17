using AsturianuTV.Infrastructure.Models;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Character : BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string History { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
