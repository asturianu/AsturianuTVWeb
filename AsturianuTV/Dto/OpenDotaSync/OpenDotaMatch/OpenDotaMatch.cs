using System.Collections.Generic;

namespace AsturianuTV.Dto.OpenDotaSync.OpenDotaMatch
{
    public class OpenDotaMatch
    { 
        public int match_id { get; set; }
        public int barracks_status_dire { get; set; }
        public int barracks_status_radiant { get; set; }
        public int dire_score { get; set; }
        public int radiant_score { get; set; }
        public bool radiant_win { get; set; }
        public int tower_status_dire { get; set; }
        public int tower_status_radiant { get; set; }
        public int duration { get; set; }
        public int first_blood_time { get; set; }
        public List<OpenDotaPickBan> picks_bans { get; set; }
        public List<OpenDotaMatchPlayer> players { get; set; }
    }
}
