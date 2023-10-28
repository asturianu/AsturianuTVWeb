using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasMany(x => x.Users)
                .WithOne(x => x.Subscription)
                .HasForeignKey(x => x.SubscriptionId);

            builder.HasMany(x => x.Plans)
                .WithOne(x => x.Subscription)
                .HasForeignKey(x => x.SubscriptionId);

            builder.HasMany(b => b.Plans)
                .WithOne(a => a.Subscription)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(b => b.Users)
                .WithOne(a => a.Subscription)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
