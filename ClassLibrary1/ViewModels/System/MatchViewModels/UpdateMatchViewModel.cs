﻿using System;
using System.Collections.Generic;

namespace AsturianuTV.ViewModels.System.MatchViewModels
{
    public class UpdateMatchViewModel
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public DateTime MatchTime { get; set; }
        public int? RadiantTeamId { get; set; }
        public int? DireTeamId { get; set; }
        public bool IsRadiantWin { get; set; }
        public List<UpdateMatchStatsViewModel> Stats { get; set; }
    }
}
