using AsturianuTV.Infrastructure.Data.Models;
using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(x => x.News)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.NewsId);
            
            builder.HasOne(b => b.News)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
