using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete;

namespace DataAccess.Context
{
    public class CoinoCaseDbContext : IdentityDbContext<IdentityUser>
    {
        public CoinoCaseDbContext(DbContextOptions<CoinoCaseDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<RegisterModel> RegisterModels { get; set; }
    }
}
