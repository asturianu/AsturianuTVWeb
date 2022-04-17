using AsturianuTV.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Match : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public string Type { get; set; }
        public int? FirstTeamId { get; set; }
        public int? SecondTeamId { get; set; }
        public ICollection<TeamMatch> TeamMatches { get; set; }
    }
}
