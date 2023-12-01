namespace AsturianuTV.ViewModels.System.MatchViewModels
{
    public class UpdateMatchStatsViewModel
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int HeroId { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int GoldEarned { get; set; }
        public int ExperienceEarned { get; set; }
    }
}
