namespace AsturianuTV.Infrastructure.Data.Models.BaseKnowledges
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int? CoolDown { get; set; }
        public int? Price { get; set; }
        public int OpenDotaId { get; set; }
    }
}
