using System;

namespace AsturianuTV.ViewModels.System.TeamViewModels
{
    public class CreateTeamViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime StartDate { get; set; }
        public string Country { get; set; }
        public int FullPrice { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
    }
}
