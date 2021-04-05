using Microsoft.EntityFrameworkCore;

namespace Customer.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Entities.Customer>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            
            modelBuilder.Entity<Entities.Contact>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}