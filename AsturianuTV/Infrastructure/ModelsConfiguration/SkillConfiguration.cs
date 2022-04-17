using AsturianuTV.Infrastructure.Data.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsturianuTV.Infrastructure.ModelsConfiguration
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasOne(p => p.Character)
                .WithMany(p => p.Skills)
                .HasForeignKey(p => p.CharacterId);
        }
    }
}
