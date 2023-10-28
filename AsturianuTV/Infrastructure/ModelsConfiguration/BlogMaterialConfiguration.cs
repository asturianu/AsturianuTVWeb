using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class BlogMaterialConfiguration : IEntityTypeConfiguration<BlogMaterial>
    {
        public void Configure(EntityTypeBuilder<BlogMaterial> builder)
        {
            builder.HasOne(x => x.Blog)
                .WithMany(x => x.BlogMaterials)
                .HasForeignKey(x => x.BlogId);

            builder.HasOne(x => x.Material)
                .WithMany(x => x.BlogMaterials)
                .HasForeignKey(x => x.MaterialId);

            builder.HasOne(x => x.Blog)
                .WithMany(x => x.BlogMaterials)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Material)
                .WithMany(x => x.BlogMaterials)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
