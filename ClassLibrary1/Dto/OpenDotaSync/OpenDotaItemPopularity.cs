using System.Collections.Generic;

namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaItemPopularity
    {
        public Dictionary<string, int> start_game_items { get; set; }
        public Dictionary<string, int> early_game_items { get; set; }
        public Dictionary<string, int> mid_game_items { get; set; }
        public Dictionary<string, int> late_game_items { get; set; }
    }
}
