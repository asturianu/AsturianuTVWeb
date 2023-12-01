using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

namespace AsturianuTV.Dto.OpenDotaSync.OpenDotaMatch
{
    public class OpenDotaPickBan
    {
        public bool is_pick { get; set; }
        public Character Character { get; set; }
        public int hero_id { get; set; }
        public int team { get; set; }
        public int order { get; set; }
    }
}
