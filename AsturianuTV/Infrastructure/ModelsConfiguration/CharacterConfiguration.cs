using AsturianuTV.Infrastructure.Data.SystemModels;
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
        }
    }
}
