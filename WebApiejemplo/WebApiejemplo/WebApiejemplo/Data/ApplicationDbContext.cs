using Microsoft.EntityFrameworkCore;
using WebApiejemplo.Models;

namespace WebApiejemplo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoMantenimiento> TiposMantenimiento { get; set; }
        public DbSet<ZonaComun> ZonasComunes { get; set; }
        public DbSet<Conjunto> Conjuntos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Torre> Torres { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<BitacoraVigilancia> BitacorasVigilancia { get; set; }
        public DbSet<Apartamentos> Apartamentos { get; set; }
        public DbSet<ResidenteUnidad> ResidentesUnidad { get; set; }
        public DbSet<Parqueadero> Parqueaderos { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Mensajeria> Mensajerias { get; set; }
        public DbSet<ParqueaderoVisitante> ParqueaderosVisitantes { get; set; }
        public DbSet<CuartoUtil> CuartosUtil { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índices únicos
            modelBuilder.Entity<Rol>().HasIndex(r => r.Nombre).IsUnique().HasDatabaseName("UQ_Rol_Nombre");
            modelBuilder.Entity<TipoMantenimiento>().HasIndex(t => t.Nombre).IsUnique().HasDatabaseName("UQ_TipoMantenimiento_Nombre");
            modelBuilder.Entity<ResidenteUnidad>().HasIndex(r => new { r.UsuarioId, r.UnidadId }).IsUnique().HasDatabaseName("UQ_Residente_Unidad");

            // Valores por defecto
            modelBuilder.Entity<Usuario>().Property(u => u.Activo).HasDefaultValue(true);
            modelBuilder.Entity<Usuario>().Property(u => u.FechaCreacion).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<BitacoraVigilancia>().Property(b => b.FechaHora).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Ingreso>().Property(i => i.FechaHoraIngreso).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Mensajeria>().Property(m => m.FechaRecepcion).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<ParqueaderoVisitante>().Property(p => p.FechaHoraIngreso).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<CuartoUtil>()
                .HasIndex(c => c.Numero)
                .IsUnique()
                .HasDatabaseName("UQ_CuartoUtil_Numero");
            // Comportamiento de delete: FK opcionales -> SetNull; obligatorias -> Restrict
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = relationship.IsRequired ? DeleteBehavior.Restrict : DeleteBehavior.SetNull;
            }
        }
    }
}
