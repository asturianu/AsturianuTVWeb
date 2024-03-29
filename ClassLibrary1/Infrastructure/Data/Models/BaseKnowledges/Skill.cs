﻿using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.Infrastructure.Data.Models.BaseKnowledges
{
    public class Skill : BaseEntity
    {
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
        public Character Character { get; set; }
    }
}
