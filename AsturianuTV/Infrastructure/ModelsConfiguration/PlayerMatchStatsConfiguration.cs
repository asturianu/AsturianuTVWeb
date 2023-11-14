using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class PlayerMatchStatsConfiguration : IEntityTypeConfiguration<PlayerMatchStats>
    {
        public void Configure(EntityTypeBuilder<PlayerMatchStats> builder)
        {
            builder.HasOne(x => x.Match)
                .WithMany(x => x.Stats)
                .HasForeignKey(x => x.MatchId);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.Stats)
                .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Character)
                .WithMany()
                .HasForeignKey(x => x.HeroId);
        }
    }
}