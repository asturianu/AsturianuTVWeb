﻿using AsturianuTV.Infrastructure.Data.Enums;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemCategory ItemCategory { get; set; } 
        public ItemType ItemType { get; set; }
    }
}
