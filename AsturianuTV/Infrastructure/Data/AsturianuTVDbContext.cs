using AsturianuTV.Infrastructure.Data.Models;
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
        public DbSet<Skill> Skills { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Material> Materials { get; set; }

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
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new NewsTagConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new PlanConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
