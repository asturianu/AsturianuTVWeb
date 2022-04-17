using AsturianuTV.Infrastructure.Data.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class TeamMatchConfiguration : IEntityTypeConfiguration<TeamMatch>
    {
        public void Configure(EntityTypeBuilder<TeamMatch> builder)
        {
            builder.HasKey(p => new { p.Id, p.MatchId, p.TeamId });

            builder.HasOne(p => p.Team)
                   .WithMany(p => p.TeamMatches)
                   .HasForeignKey(p => p.TeamId);

            builder.HasOne(p => p.Match)
                   .WithMany(p => p.TeamMatches)
                   .HasForeignKey(p => p.MatchId);
        }
    }
}
