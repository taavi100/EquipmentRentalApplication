using Microsoft.EntityFrameworkCore;
using EquipmentRentalApplication.Models;

namespace EquipmentRentalApplication.Dal
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EquipmentRental;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

    }
}
