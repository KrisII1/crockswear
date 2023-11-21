using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;

namespace DataLayer
{
    public class CrockDBContext : DbContext
    {
        public CrockDBContext()
        {

        }
        public CrockDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            if (!OptionsBuilder.IsConfigured)
            {
                OptionsBuilder.UseSqlServer("Server=LAPTOP-AT94SBBO\\SQLEXPRESS;Database=CrockswearDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
            base.OnConfiguring(OptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>().Property(o => o.Status).HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Basket> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Manager> Managers { get; set; }

    }
}