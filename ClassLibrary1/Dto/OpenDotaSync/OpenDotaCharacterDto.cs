namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaCharacterDto
    {
        public int Id { get; set; }
        public string Localized_Name { get; set; }
        public string Primary_Attr { get; set; }
        public string Attack_Type { get; set; }
        public string Img { get; set; }
        public float Base_Health { get; set; }
        public float Base_Health_Regen { get; set; }
        public float Base_Mana { get; set; }
        public float Base_Mana_Regen { get; set; }
        public float Base_Armor { get; set; }
        public float Move_Speed { get; set; }
        public float Base_Attack_Max { get; set; }
    }
}
