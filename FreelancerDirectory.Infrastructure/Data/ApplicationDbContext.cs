using Microsoft.EntityFrameworkCore;
using FreelancerDirectory.Domain.Entities;

namespace FreelancerDirectory.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Skillset> Skillsets { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<FreelancerSkillset> FreelancerSkillsets { get; set; }
        public DbSet<FreelancerHobby> FreelancerHobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationships
            modelBuilder.Entity<FreelancerSkillset>()
                .HasKey(fs => new { fs.FreelancerId, fs.SkillsetId });

            modelBuilder.Entity<FreelancerHobby>()
                .HasKey(fh => new { fh.FreelancerId, fh.HobbyId });

            // Configure unique constraints
            modelBuilder.Entity<Freelancer>()
                .HasIndex(f => f.Username)
                .IsUnique();

            modelBuilder.Entity<Freelancer>()
                .HasIndex(f => f.Email)
                .IsUnique();
        }
    }
}