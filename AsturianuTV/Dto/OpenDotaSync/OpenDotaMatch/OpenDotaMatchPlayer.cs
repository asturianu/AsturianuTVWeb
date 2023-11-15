using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

namespace AsturianuTV.Dto.OpenDotaSync.OpenDotaMatch
{
    public class OpenDotaMatchPlayer
    {
        public string account_id { get; set; }
        public int assists { get; set; }
        public int deaths { get; set; }
        public int denies { get; set; }
        public int kills { get; set; }
        public int? last_hits { get; set; }
        public int? level { get; set; }
        public int? net_worth { get; set; }
        public int? tower_damage { get; set; }
        public int? xp_per_min { get; set; }
        public int? total_gold { get; set; }
        public int? total_xp { get; set; }
        public int? gold { get; set; }
        public int? gold_per_min { get; set; }
        public int? gold_spent { get; set; }
        public int? hero_damage { get; set; }
        public int? hero_healing { get; set; }
        public Character heroe { get; set; }
        public int hero_id { get; set; }
        public float kda { get; set; }
        public Item item_0obj { get; set; }
        public int item_0 { get; set; }
        public Item item_1obj { get; set; }
        public int item_1 { get; set; }
        public Item item_2obj { get; set; }
        public int item_2 { get; set; }
        public Item item_3obj { get; set; }
        public int item_3 { get; set; }
        public Item item_4obj { get; set; }
        public int item_4 { get; set; }
        public Item item_5obj { get; set; }
        public int item_5 { get; set; }
        public Item item_neutral_obj { get; set; }
        public int item_neutral { get; set; }
    }
}
