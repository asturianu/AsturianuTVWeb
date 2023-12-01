using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaPlayerHeroe
    {
        public int Hero_Id { get; set; }
        public Character Character { get; set; }
        public int Last_Played { get; set; }
        public int Games { get; set; }
        public int Win { get; set; }
        public int With_Games { get; set; }
        public int With_Win { get; set; }
        public int Against_Games { get; set; }
        public int Against_Win { get; set; }
    }
}
