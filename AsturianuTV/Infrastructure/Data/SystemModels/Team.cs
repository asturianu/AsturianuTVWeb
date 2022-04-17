using AsturianuTV.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }    
        public string Description { get; set; }
        public DateTime? Date { get; set; }  
        public ICollection<Player> Players { get; set; }
        public ICollection<TeamMatch> TeamMatches { get; set; }
    }
}
