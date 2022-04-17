using AsturianuTV.Infrastructure.Data.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class MatchConfguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasMany(p => p.TeamMatches)
                   .WithOne(p => p.Match)
                   .HasForeignKey(p => p.MatchId);
        }
    }
}
