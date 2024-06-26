using InsuranceWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.ID);
            modelBuilder.Entity<InsurancePolicy>().HasKey(ip => ip.ID);

            modelBuilder.Entity<InsurancePolicy>()
                .HasOne(ip => ip.User)
                .WithMany(u => u.InsurancePolicies)
                .HasForeignKey(ip => ip.UserID);

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new User { ID = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            );

            modelBuilder.Entity<InsurancePolicy>().HasData(
                new InsurancePolicy { ID = 1, PolicyNumber = "POL123456", InsuranceAmount = 50000, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), UserID = 1 },
                new InsurancePolicy { ID = 2, PolicyNumber = "POL654321", InsuranceAmount = 75000, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), UserID = 2 }
            );
        }
    }
}
