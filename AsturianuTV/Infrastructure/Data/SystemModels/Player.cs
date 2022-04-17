using AsturianuTV.Infrastructure.Models;
using System;

namespace AsturianuTV.Infrastructure.Data.SystemModels
{
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime? Date { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; } 
    }
}
