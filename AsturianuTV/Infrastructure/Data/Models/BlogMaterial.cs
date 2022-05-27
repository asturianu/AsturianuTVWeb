namespace AsturianuTV.Infrastructure.Data.Models
{
    public class BlogMaterial : BaseEntity
    {
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
