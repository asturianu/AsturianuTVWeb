using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasMany(x => x.News)
                .WithOne(x => x.League)
                .HasForeignKey(x => x.LeagueId);

            builder.HasMany(x => x.LeagueTeams)
                .WithOne(x => x.League)
                .HasForeignKey(x => x.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
