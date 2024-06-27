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
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SoftwareSystem> SoftwareSystems { get; set; }
    public DbSet<ContractsSoftwareSystems> ContractsSoftwareSystemsEnumerable { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Address>().HasData(new List<Address>
        {
            new Address
            {
                Id = 1,
                City = "Warsaw",
                Country = "Poland",
                Street = "Nizinna",
                HouseNumber = "2",
            },new Address
            {
                Id = 2,
                City = "Warsaw",
                Country = "Poland",
                Street = "Fabryczna",
                HouseNumber = "1",
            }
        });
        modelBuilder.Entity<Category>().HasData(new List<Category>
        {
            new Category
            {
                Id = 1,
                Description = "Education",
                Name = "Educational purposes only."
            }
        });
        modelBuilder.Entity<ClientCompany>().HasData(new List<ClientCompany>
        {
            new ClientCompany
            {
                Id = 1,
                Email = "example@example.com",
                PhoneNumber = "661354887",
                IdAddress = 1,
                CompanyName = "CompanyName",
                KRS = "12345678"
            }
        });modelBuilder.Entity<ClientNatural>().HasData(new List<ClientNatural>
        {
            new ClientNatural
            {
                Id = 2,
                Email = "example@example2.com",
                PhoneNumber = "887345645",
                IdAddress = 2,
                FristName = "Jan",
                LastName = "Kowalski",
                PESEL = "031276398"
            }
        });
        modelBuilder.Entity<Discount>().HasData(new List<Discount>
        {
            new Discount
            {
                Id = 1,
                Name = "Black Friday",
                Value = 20,
                DateFrom = DateTime.Now.AddDays(-1),
                DateTo = DateTime.Now.AddDays(10)
            }
        });
    }
    
}