using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using System.Collections.Generic;

namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Material : BaseEntity
    {
        public bool IsNewsMaterial { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public ICollection<NewsMaterial> NewsMaterials { get; set; }
        public ICollection<BlogMaterial> BlogMaterials { get; set; }
    }
}
