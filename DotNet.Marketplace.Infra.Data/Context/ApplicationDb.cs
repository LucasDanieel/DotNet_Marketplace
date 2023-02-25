
using DotNet.Marketplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Context
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        { }

        public DbSet<Person> Peoples { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PersonImage> PersonImages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDb).Assembly);
        }
    }
}
