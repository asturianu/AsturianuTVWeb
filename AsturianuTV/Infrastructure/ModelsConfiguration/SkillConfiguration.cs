using AsturianuTV.Infrastructure.Data.Models;
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

            builder.Property(x => x.DamageType)
                .HasColumnType("int");

            builder.Property(x => x.SkillType)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
