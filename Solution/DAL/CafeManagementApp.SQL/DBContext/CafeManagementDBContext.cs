using CafeManagementApp.SQL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CafeManagementApp.SQL.DBContext
{
    public class CafeManagementDBContext : DbContext
    {
        public CafeManagementDBContext()
        {
        }

        public CafeManagementDBContext(DbContextOptions<CafeManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cafe> Cafes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<CafeEmployee> CafeEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Code here is required to be used by the EFCore Migrations step.
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("CafeManagementAppDBContext");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraint for CafeId and EmployeeId in CafeEmployee
            modelBuilder.Entity<CafeEmployee>()
                .HasIndex(ce => new { ce.CafeId, ce.EmployeeId })
                .IsUnique();

            // Disable cascading deletes globally
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
