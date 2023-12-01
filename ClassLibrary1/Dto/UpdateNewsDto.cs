using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Dto
{
    public class UpdateNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsReady { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Tag> NewsTags { get; set; }
        public ICollection<Material> NewsMaterials { get; set; }
    }
}
