using System.Collections.Generic;

namespace AsturianuTV.Dto.OpenDotaSync
{
    public class OpenDotaStatisticDto
    {
        public OpenDotaWinLose OpenDotaWinLose { get; set; }
        public OpenDotaGeneralInfoUser OpenDotaGeneralInfoUser { get; set; }
        public List<OpenDotaPlayerMatch> OpenDotaPlayerMatches { get; set; }
        public List<OpenDotaPlayerHeroe> OpenDotaPlayerHeroes { get; set; }
    }
}
