using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class LeagueTeamConfiguration : IEntityTypeConfiguration<LeagueTeam>
    {
        public void Configure(EntityTypeBuilder<LeagueTeam> builder)
        {
            builder.HasOne(x => x.League)
                .WithMany(x => x.LeagueTeams)
                .HasForeignKey(x => x.LeagueId);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.LeagueTeams)
                .HasForeignKey(x => x.TeamId);
        }
    }
}
