﻿using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Infrastructure.Data
{
    public class User : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public int? SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}