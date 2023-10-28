﻿using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using System;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class Player : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime Bday { get; set; }
        public string Country { get; set; }
        public string FullPrice { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; } 
        public Team Team { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public ICollection<News> News { get; set; }
    }
}