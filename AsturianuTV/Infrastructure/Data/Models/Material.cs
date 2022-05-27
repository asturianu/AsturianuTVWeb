namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Material : BaseEntity
    {
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? NewsId { get; set; }
        public News News { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
    }
}
