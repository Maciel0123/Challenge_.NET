using Microsoft.EntityFrameworkCore;
using MottuModel;

namespace MottuData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets mapeando para Oracle
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Moto> Motos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define os nomes exatos das tabelas no Oracle
            modelBuilder.Entity<Patio>().ToTable("PATIOS");
            modelBuilder.Entity<Zona>().ToTable("ZONAS");
            modelBuilder.Entity<Moto>().ToTable("MOTOS");

            // Chave primária (se não estiver por data annotation)
            modelBuilder.Entity<Patio>().HasKey(p => p.Id);
            modelBuilder.Entity<Zona>().HasKey(z => z.Id);
            modelBuilder.Entity<Moto>().HasKey(m => m.Id);

            // Relacionamento 1:N → Patio -> Zonas
            modelBuilder.Entity<Patio>()
                .HasMany(p => p.Zonas)
                .WithOne(z => z.Patio)
                .HasForeignKey(z => z.PatioId)
                .HasConstraintName("FK_ZONA_PATIO");

            // Relacionamento 1:N → Zona -> Motos
            modelBuilder.Entity<Zona>()
                .HasMany(z => z.Motos)
                .WithOne(m => m.Zona)
                .HasForeignKey(m => m.ZonaId)
                .HasConstraintName("FK_MOTO_ZONA");

            // (Opcional) Mapeia nomes de colunas explícitos
            modelBuilder.Entity<Patio>().Property(p => p.Nome).HasColumnName("NOME");
            modelBuilder.Entity<Zona>().Property(z => z.Nome).HasColumnName("NOME");
            modelBuilder.Entity<Moto>().Property(m => m.Placa).HasColumnName("PLACA");
            modelBuilder.Entity<Moto>().Property(m => m.Modelo).HasColumnName("MODELO");
        }
    }
}
