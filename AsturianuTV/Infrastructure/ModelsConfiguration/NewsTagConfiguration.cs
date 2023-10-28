using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class NewsTagConfiguration : IEntityTypeConfiguration<NewsTag>
    {
        public void Configure(EntityTypeBuilder<NewsTag> builder)
        {
            builder.HasOne(x => x.News)
                .WithMany(x => x.NewsTags)
                .HasForeignKey(x => x.NewsId);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.NewsTags)
                .HasForeignKey(x => x.TagId);

            builder.HasOne(b => b.News)
                .WithMany(a => a.NewsTags)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Tag)
                .WithMany(a => a.NewsTags)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
