using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Dto.Cybersports
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string PriceFound { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
