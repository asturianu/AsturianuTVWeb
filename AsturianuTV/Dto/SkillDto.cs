using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Enums;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Dto
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int? Damage { get; set; }
        public int? CoolDown { get; set; }
        public int? TimeDuration { get; set; }
        public int? ManaCost { get; set; }
        public DamageType? DamageType { get; set; }
        public SkillType SkillType { get; set; }
        public int? CharacterId { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
