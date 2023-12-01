namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class LeagueTeam : BaseEntity
    {
        public int TeamId { get; set; } 
        public Team Team { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
