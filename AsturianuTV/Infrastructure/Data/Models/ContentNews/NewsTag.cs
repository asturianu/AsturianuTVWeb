﻿namespace AsturianuTV.Infrastructure.Data.Models.ContentNews
{
    public class NewsTag : BaseEntity
    {
        public News News { get; set; }
        public int? NewsId { get; set; }
        public Tag Tag { get; set; }
        public int? TagId { get; set; }
    }
}
