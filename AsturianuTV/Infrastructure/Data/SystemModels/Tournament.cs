using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Tournament
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<Match> Mathces { get; set; }
    }
}
