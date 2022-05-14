using System;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int? LastModifiedBy { get; set; }
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }
    }
}
