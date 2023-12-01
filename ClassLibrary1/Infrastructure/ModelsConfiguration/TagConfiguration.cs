using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x=>x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.NewsTags)
                .WithOne(x => x.Tag)
                .HasForeignKey(x => x.TagId);

            builder.HasMany(b => b.NewsTags)
                .WithOne(a => a.Tag)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
