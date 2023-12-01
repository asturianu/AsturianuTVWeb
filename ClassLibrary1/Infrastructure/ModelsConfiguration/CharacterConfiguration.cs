using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
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


            builder.HasMany(b => b.Skills)
                .WithOne(a => a.Character)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.Attribute)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.RangeType)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
