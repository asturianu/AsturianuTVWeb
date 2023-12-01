using System;
using System.Collections.Generic;

namespace AsturianuTV.ViewModels.System.LeagueViewModels
{
    public class UpdateLeagueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string PriceFound { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<int> TeamIds { get; set; }
    }
}
