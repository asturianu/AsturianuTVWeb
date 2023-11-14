using System;
using System.Collections.Generic;

namespace AsturianuTV.ViewModels.System.MatchViewModels
{
    public class CreateMatchViewModel
    {
        public int LeagueId { get; set; }
        public DateTime MatchTime { get; set; }
        public int? RadiantTeamId { get; set; }
        public int? DireTeamId { get; set; }
        public bool IsRadiantWin { get; set; }
        public ICollection<CreateMatchStatsViewModel> Stats { get; set; }
    }
}
