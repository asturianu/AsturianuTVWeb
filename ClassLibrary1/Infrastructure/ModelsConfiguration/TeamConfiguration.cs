using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(x => x.News)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);

            builder.HasMany(x => x.LeagueTeams)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Players)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
