using EfCoreDbIterceptors.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDbIterceptors
{
    public class EntitiesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.AddInterceptors(new CustomDbCommandInterceptor(new TenantDetail()));
            base.OnConfiguring(optionsBuilder);
        }
        public EntitiesContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
