using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasIndex(p => p.ProjectCode)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.EmployeeCode)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}
