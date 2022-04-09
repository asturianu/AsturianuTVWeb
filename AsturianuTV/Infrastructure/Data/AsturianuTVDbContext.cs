using Microsoft.EntityFrameworkCore;

namespace AsturianuTV.Infrastructure.Data
{
    public class AsturianuTVDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
