using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasOne(x => x.Blog)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => x.BlogId);

            builder.HasOne(x => x.News)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => x.NewsId);

            builder.HasOne(x => x.News)
                .WithMany(x => x.Materials)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Blog)
                .WithMany(a => a.Materials)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
