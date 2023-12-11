using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VPN.Domain.Entities;

namespace VPN.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Key> Keys { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
