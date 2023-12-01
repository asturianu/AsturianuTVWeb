using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class PlayerMatchStats : BaseEntity
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int HeroId { get; set; }
        public Character Character { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int GoldEarned { get; set; }
        public int ExperienceEarned { get; set; }
    }
}
