using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasOne(x => x.Plan)
                .WithMany(x => x.Blogs)
                .HasForeignKey(x => x.PlanId);

            builder.HasMany(x => x.Materials)
                .WithOne(x => x.Blog)
                .HasForeignKey(x => x.BlogId);

            builder.HasMany(b => b.Materials)
                .WithOne(a => a.Blog)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Plan)
                .WithMany(a => a.Blogs)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
