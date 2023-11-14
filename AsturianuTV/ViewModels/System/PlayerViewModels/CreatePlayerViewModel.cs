using System;

namespace AsturianuTV.ViewModels.System.PlayerViewModels
{
    public class CreatePlayerViewModel
    {
        public string FullName { get; set; }
        public string Logo { get; set; }
        public DateTime Bday { get; set; }
        public string Country { get; set; }
        public string FullPrice { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; }
    }
}
