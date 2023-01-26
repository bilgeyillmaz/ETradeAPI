using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete;
using Core.Entities;
using System.Reflection;

namespace DataAccess.Context
{
    public class CoinoCaseDbContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=MSI; database= CoinoCaseDb; integrated security=true");
        }
        public CoinoCaseDbContext(DbContextOptions<CoinoCaseDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Kalem",
                    Price = 100,
                    Quantity = 20,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Silgi",
                    Price = 200,
                    Quantity = 30,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 3,
                    CategoryId = 1,
                    Name = "Defter",
                    Price = 600,
                    Quantity = 60,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 4,
                    CategoryId = 1,
                    Name = "Kalemtraş",
                    Price = 600,
                    Quantity = 60,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 5,
                    CategoryId = 2,
                    Name = "Televizyon",
                    Price = 6600,
                    Quantity = 320,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 7,
                    CategoryId = 2,
                    Name = "Laptop",
                    Price = 6600,
                    Quantity = 320,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 8,
                    CategoryId = 2,
                    Name = "Klavye",
                    Price = 6600,
                    Quantity = 320,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                },
                new Product()
                {
                    Id = 9,
                    CategoryId = 2,
                    Name = "Monitör",
                    Price = 6600,
                    Quantity = 320,
                    UpdatedDate = DateTime.Now,
                    IsStock = true
                });

            modelBuilder.Entity<Category>().HasData(new Category() { Id = 1, Name = "Kırtasiye" },
                new Category() { Id = 2, Name = "Elektronik" },
                new Category() { Id = 3, Name = "Mobilya" });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RegisterModel> RegisterModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CartsProduct> CartsProducts { get; set; }
    }
}
