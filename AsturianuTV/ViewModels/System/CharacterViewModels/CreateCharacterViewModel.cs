using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.ViewModels.System.CharacterViewModels
{
    public class CreateCharacterViewModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string History { get; set; }
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
    }
}
