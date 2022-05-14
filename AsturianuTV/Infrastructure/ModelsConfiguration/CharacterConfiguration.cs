using AsturianuTV.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasMany(p => p.Skills)
                .WithOne(p => p.Character)
                .HasForeignKey(p => p.CharacterId);

            builder.Property(x => x.Attribute)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.RangeType)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
