using System;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class PlayerHistory : BaseEntity
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }  
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Games { get; set; }
        public int Wins { get; set; }
    }
}
