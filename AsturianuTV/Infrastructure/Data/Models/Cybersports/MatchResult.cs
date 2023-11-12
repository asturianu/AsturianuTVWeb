using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class MatchResult : BaseEntity
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public string Duration { get; set; }
        public int WinningTeamId { get; set; }
        public Team WinningTeam { get; set; }
        public ICollection<PlayerMatchStats> Stats { get; set; }
    }
}
