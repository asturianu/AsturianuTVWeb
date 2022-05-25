namespace AsturianuTV.Infrastructure.Data.Models
{
    public class Material : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string FilePath { get; set; }
    }
}
