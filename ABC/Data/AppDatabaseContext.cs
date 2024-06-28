using ABC.Models;
using GakkoHorizontalSlice.Model;
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
    public DbSet<ClientCompany> ClientsCompanies { get; set; }
    public DbSet<ClientNatural> ClientsNaturals { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SoftwareSystem> SoftwareSystems { get; set; }
    public DbSet<ContractsSoftwareSystems> ContractsSoftwareSystemsEnumerable { get; set; }

    
    public DbSet<AppUser> Users { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);
        
        
        modelBuilder.Entity<ContractsSoftwareSystems>()
            .HasKey(cs => new { cs.IdContract, cs.IdSoftwareSystem });

        modelBuilder.Entity<ContractsSoftwareSystems>()
            .HasOne(cs => cs.Contract)
            .WithMany(c => c.ContractsSoftwareSystems)
            .HasForeignKey(cs => cs.IdContract)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ContractsSoftwareSystems>()
            .HasOne(cs => cs.SoftwareSystem)
            .WithMany(s => s.ContractsSoftwareSystems)
            .HasForeignKey(cs => cs.IdSoftwareSystem)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Contract)
            .WithMany()
            .HasForeignKey(p => p.IdContract)
            .OnDelete(DeleteBehavior.NoAction);



        modelBuilder.Entity<AppUser>().HasData(new List<AppUser>
        {
            new AppUser
            {
                IdUser = 1,
                Login = "admin",
                Email = "admin@abc.pl",
                Password = "UCj/SzNluTr2O7t1unmdXEPX3VpOkxqWUUMrhfwefiA=",
                Salt = "s1q1vyXNvCGXtzXswP6GUg==",
                RefreshToken = "gdaodTn6fZrAFZnvZhKLnabnBVlyE6/lJTlXcfpR3EI="
            }
        });
        
        
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
        }); modelBuilder.Entity<SoftwareSystem>().HasData(new List<SoftwareSystem>
        {
            new SoftwareSystem
            {
                Id = 1,
                Name = "EduSoftware",
                Description = "Educational Software for students",
                VersionInformation = "Version 1.0",
                IdCategory = 1,
                PriceForYear = 2000
            }
        }); modelBuilder.Entity<Payment>().HasData(new List<Payment>
        {
            new Payment
            {
                Id = 1,
                MoneyAmount = 1000,
                Date = DateTime.Now,
                IdContract = 1,
                IdClient = 1,
            }
        }); modelBuilder.Entity<Contract>().HasData(new List<Contract>
        {
            new Contract
            {
                Id = 1,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(20),
                IsActive = false,
                Price = 3000,
                SupportUpdatePeriodInYears = 2,
                UpdateInformation = "Possible updates in future",
                IdDiscount = 1,
                IdSoftwareSystem = 1,
                IdClient = 1,
                IsSigned = false,
            }
        }); modelBuilder.Entity<ContractsSoftwareSystems>().HasData(new List<ContractsSoftwareSystems>
        {
            new ContractsSoftwareSystems
            {
                IdContract = 1,
                IdSoftwareSystem = 1,
            }
        });
    }
    
}