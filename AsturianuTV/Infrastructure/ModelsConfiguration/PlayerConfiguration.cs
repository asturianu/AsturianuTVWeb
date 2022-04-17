using AsturianuTV.Infrastructure.Data.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasOne(p => p.Team)
                   .WithMany(p => p.Players)
                   .HasForeignKey(p => p.TeamId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.MiddleName)
               .HasMaxLength(128);

            builder.Property(p => p.Surname)
               .IsRequired()
               .HasMaxLength(128);
        }
    }
}
