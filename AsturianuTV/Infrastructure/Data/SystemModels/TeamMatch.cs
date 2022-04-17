using AsturianuTV.Infrastructure.Models;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class TeamMatch : BaseEntity
    {
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public int? MatchId { get; set; }  
        public Match Match { get; set; }
    }
}
