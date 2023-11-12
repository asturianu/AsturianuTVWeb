namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaWinLose
    {
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Games
        {
            get { return Win + Lose; }
        }
        public float WinRate
        {
            get { return (Win * 100) / Games; }
        }
    }
}
