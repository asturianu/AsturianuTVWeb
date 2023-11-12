using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;

namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaPlayerMatch
    {
        public string Match_Id { get; set; }
        public int? Player_Slot { get; set; }
        public bool Radiant_Win { get; set; }
        public string Duration { get; set; }
        public int? Game_Mode { get; set; }
        public int? Lobby_Type { get; set; }
        public int Hero_Id { get; set; }
        public Character Character { get; set; }
        public int? Start_Time { get; set; }
        public string Version { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int? Average_Rank { get; set; }
        public int? Leaver_Status { get; set; }
        public int? Party_Size { get; set; }
    }
}
