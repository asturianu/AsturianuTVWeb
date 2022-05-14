using AsturianuTV.Infrastructure.Data.Models;
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
        }
    }
}
