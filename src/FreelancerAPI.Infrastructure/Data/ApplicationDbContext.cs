using FreelancerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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

            modelBuilder.Entity<FreelancerSkillset>()
                .HasOne(fs => fs.Freelancer)
                .WithMany(f => f.FreelancerSkillsets)
                .HasForeignKey(fs => fs.FreelancerId);

            modelBuilder.Entity<FreelancerSkillset>()
                .HasOne(fs => fs.Skillset)
                .WithMany(s => s.FreelancerSkillsets)
                .HasForeignKey(fs => fs.SkillsetId);

            modelBuilder.Entity<FreelancerHobby>()
                .HasKey(fh => new { fh.FreelancerId, fh.HobbyId });

            modelBuilder.Entity<FreelancerHobby>()
                .HasOne(fh => fh.Freelancer)
                .WithMany(f => f.FreelancerHobbies)
                .HasForeignKey(fh => fh.FreelancerId);

            modelBuilder.Entity<FreelancerHobby>()
                .HasOne(fh => fh.Hobby)
                .WithMany(h => h.FreelancerHobbies)
                .HasForeignKey(fh => fh.HobbyId);

            // Configure indexes for search
            modelBuilder.Entity<Freelancer>()
                .HasIndex(f => f.Username);

            modelBuilder.Entity<Freelancer>()
                .HasIndex(f => f.Email);

            base.OnModelCreating(modelBuilder);
        }
    }
}