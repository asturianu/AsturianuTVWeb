using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class NewsMaterialConfiguration : IEntityTypeConfiguration<NewsMaterial>
    {
        public void Configure(EntityTypeBuilder<NewsMaterial> builder)
        {
            builder.HasOne(x => x.News)
                .WithMany(x => x.NewsMaterials)
                .HasForeignKey(x => x.NewsId);

            builder.HasOne(x => x.Material)
                .WithMany(x => x.NewsMaterials)
                .HasForeignKey(x => x.MaterialId);

            builder.HasOne(x => x.News)
                .WithMany(x => x.NewsMaterials)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Material)
                .WithMany(x => x.NewsMaterials)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
