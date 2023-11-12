using System;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class Match : BaseEntity
    {
        public int LeagueId { get; set; }
        public League League { get; set; }
        public DateTime MatchTime { get; set; }
        public int? RadiantTeamId { get; set; }
        public Team RadiantTeam { get; set; }
        public int? DireTeamId { get; set; }
        public Team DireTeam { get; set; }
        public MatchResult Result { get; set; }
    }
}
