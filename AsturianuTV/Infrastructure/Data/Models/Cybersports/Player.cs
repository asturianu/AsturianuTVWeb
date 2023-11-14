using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class Player : BaseEntity
    {
        public string FullName { get; set; }
        public string Logo { get; set; }
        public DateTime Bday { get; set; }
        public string Country { get; set; }
        public string FullPrice { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; } 
        public Team Team { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
        public ICollection<PlayerMatchStats> Stats { get; set; }
        public ICollection<PlayerHistory> PlayerHistories { get; set; }
        public string OpenDotaId { get; set; }
    }
}
