using DEVTEST_Cameron_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DEVTEST_Cameron_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Salesperson> Salespersons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<HighAchiever> HighAchievers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HighAchiever>().HasNoKey();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Salesperson)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SalesPersonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}