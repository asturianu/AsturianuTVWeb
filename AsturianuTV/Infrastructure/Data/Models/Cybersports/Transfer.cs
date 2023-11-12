using System;

namespace AsturianuTV.Infrastructure.Data.Models.Cybersports
{
    public class Transfer : BaseEntity
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int? OldTeamId { get; set; }
        public Team OldTeam { get; set; }
        public int? NewTeamId { get; set; }
        public Team NewTeam { get; set; }
        public DateTime TransferDate { get; set; }  
    }
}
