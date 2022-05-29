using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasMany(x => x.NewsMaterials)
                .WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);

            builder.HasMany(x => x.BlogMaterials)
                .WithOne(x => x.Material)
                .HasForeignKey(x => x.MaterialId);

            builder.HasMany(x => x.NewsMaterials)
                .WithOne(x => x.Material)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.BlogMaterials)
                .WithOne(x => x.Material)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
