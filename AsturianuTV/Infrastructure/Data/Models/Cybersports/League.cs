using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class League : BaseEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string PriceFound { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<LeagueTeam> LeagueTeams { get; set; }
        public ICollection<News> News { get; set; }
    }
}
