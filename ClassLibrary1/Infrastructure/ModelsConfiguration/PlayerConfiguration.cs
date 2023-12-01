using AsturianuTV.Infrastructure.Data.Models.Cybersports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasMany(x => x.News)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
