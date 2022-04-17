using AsturianuTV.Infrastructure.Data.SystemModels;
using AsturianuTV.Infrastructure.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Infrastructure.Data
{
    public class AsturianuTVDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMatch> TeamMatches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        public AsturianuTVDbContext(DbContextOptions<AsturianuTVDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new Role { Id = 1, Name = "Administrator" };
            Role userRole = new Role { Id = 2, Name = "DefaultUser" };
            User adminUser = new User 
            { 
                Id = 1, 
                Email = "admin@gmail.com", 
                Password = "Asturianu#2001", 
                RoleId = adminRole.Id 
            };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            modelBuilder.ApplyConfiguration(new CharacterConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new TeamMatchConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
