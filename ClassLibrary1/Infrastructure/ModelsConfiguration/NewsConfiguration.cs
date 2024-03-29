﻿using AsturianuTV.Infrastructure.Data.Models.ContentNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasMany(x => x.NewsTags)
                .WithOne(x => x.News)
                .HasForeignKey(x=>x.NewsId);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.News)
                .HasForeignKey(x => x.NewsId);

            builder.HasMany(b => b.NewsTags)
                .WithOne(a => a.News)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.News)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.NewsMaterials)
                .WithOne(x => x.News)
                .HasForeignKey(x => x.NewsId);

            builder.HasMany(x => x.NewsMaterials)
                .WithOne(x => x.News)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.League)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.LeagueId);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.TeamId);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.News)
                .HasForeignKey(x => x.PlayerId);
        }
    }
}
