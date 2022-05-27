using System.Collections.Generic;
using AsturianuTV.Infrastructure.Data.Models;

namespace AsturianuTV.Dto
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public int? NewsId { get; set; }
        public ICollection<News> News{ get; set; }
        public string FilePath { get; set; }
    }
}
