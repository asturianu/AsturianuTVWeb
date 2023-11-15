using AsturianuTV.Dto.OpenDotaSync;
using AsturianuTV.Infrastructure.Data.Enums;
using System.Collections.Generic;

namespace AsturianuTV.Dto
{
    public class HeroeDetailsResultDto
    {
        public int Id { get; set; }
        public int OpenDotaId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public CharacterAttribute Attribute { get; set; }
        public RangeType RangeType { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int HealthRegeneration { get; set; }
        public int ManaRegeneration { get; set; }
        public int MagicResist { get; set; }
        public int MoveSpeed { get; set; }
        public int Damage { get; set; }
        public List<OpenDotaCharacterMatchupDto> Best { get; set; }
        public List<OpenDotaCharacterMatchupDto> Worst { get; set; }
        public List<PopularItemsResultDto> StartGameItems { get; set; }
        public List<PopularItemsResultDto> EarlyGameItems { get; set; }
        public List<PopularItemsResultDto> MidGameItems { get; set; }
        public List<PopularItemsResultDto> LateGameItems { get; set; }
    }
}
