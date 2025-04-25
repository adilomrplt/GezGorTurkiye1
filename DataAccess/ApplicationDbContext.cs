using Microsoft.EntityFrameworkCore;
using GezGorTurkiye.Entities;

namespace GezGorTurkiye.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Mekan> Mekanlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mekan>()
                .HasOne(m => m.Sehir)
                .WithMany()
                .HasForeignKey(m => m.SehirId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
