namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaCharacterMatchupDto
    {
        public int Id { get; set; }
        public int MainId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public float WinRate { get; set; }
        public int Games { get; set; }
        public int Wins { get; set; }
    }
}
