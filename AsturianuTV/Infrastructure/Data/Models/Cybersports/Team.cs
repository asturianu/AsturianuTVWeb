﻿using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get;set; }
        public string Country { get; set; }
        public int FullPrice { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<LeagueTeam> LeagueTeams { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
    }
}