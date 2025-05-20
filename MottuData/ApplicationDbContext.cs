using MottuModel;
using Microsoft.EntityFrameworkCore;

namespace MottuData
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<MottuModel.Moto> Motos { get; set; }
        public DbSet<MottuModel.Zona> Zonas { get; set; }
        public DbSet<MottuModel.Patio> Patios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patio>()
                .HasMany(p => p.Zonas)
                .WithOne(z => z.Patio)
                .HasForeignKey(z => z.PatioId);

            modelBuilder.Entity<Zona>()
                .HasMany(z => z.Motos)
                .WithOne(m => m.Zona)
                .HasForeignKey(m => m.ZonaId);
        }
    }
}
