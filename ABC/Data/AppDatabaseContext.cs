using ABC.Models;
using Microsoft.EntityFrameworkCore;
namespace ABC.Data;

public class AppDatabaseContext : DbContext
{
    protected AppDatabaseContext()
    {
    }

    public AppDatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<ClientCompany> ClientCompanies { get; set; }
    public DbSet<ClientNatural> ClientNaturals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<object>().HasData(new List<object>
        {
        });
    }
}