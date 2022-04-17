using AsturianuTV.Infrastructure.Data.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(p => p.Players)
                   .WithOne(p => p.Team)
                   .HasForeignKey(p => p.TeamId);

            builder.HasMany(p => p.TeamMatches)
                   .WithOne(p => p.Team)
                   .HasForeignKey(p => p.TeamId);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(p => p.Description)
                   .IsRequired();
        }
    }
}
