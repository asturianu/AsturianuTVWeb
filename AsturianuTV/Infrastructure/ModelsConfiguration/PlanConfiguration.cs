﻿using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasOne(x => x.Subscription)
                .WithMany(x => x.Plans)
                .HasForeignKey(x => x.SubscriptionId);

            builder.HasMany(x => x.Blogs)
                .WithOne(x => x.Plan)
                .HasForeignKey(x => x.PlanId);

            builder.HasOne(b => b.Subscription)
                .WithMany(a => a.Plans)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(b => b.Blogs)
                .WithOne(a => a.Plan)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}