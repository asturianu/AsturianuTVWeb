using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(x => x.League)
                .WithMany(x => x.Matches)
                .HasForeignKey(x => x.LeagueId);

            builder.HasOne(x => x.DireTeam)
                .WithMany(x => x.TeamDireMatches)
                .HasForeignKey(x => x.DireTeamId);

            builder.HasOne(x => x.RadiantTeam)
                .WithMany(x => x.TeamRadiantMatches)
                .HasForeignKey(x => x.RadiantTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Stats)
                .WithOne(x => x.Match)
                .HasForeignKey(x => x.MatchId);
        }
    }
}
