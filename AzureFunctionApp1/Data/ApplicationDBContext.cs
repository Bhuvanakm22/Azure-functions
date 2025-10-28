using Microsoft.EntityFrameworkCore;
using System.Data;



namespace AzureFunctionApp1.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) {
        }
       public DbSet<SalesRequest> SalesRequests { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //keys of Identity tables are mapped in OnModelCreating method of IdentityDbContext(base class)
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
                
        }
    }
}
