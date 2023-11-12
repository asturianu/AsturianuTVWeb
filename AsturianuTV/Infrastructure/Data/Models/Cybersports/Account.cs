namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public int MMR { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int HoursPlayed { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
