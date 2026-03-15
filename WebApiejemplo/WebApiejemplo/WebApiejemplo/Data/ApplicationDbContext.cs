using Microsoft.EntityFrameworkCore;
using WebApiejemplo.Models;

namespace WebApiejemplo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> CLIENTE { get; set; }
        public DbSet<Empresa> EMPRESA { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configurar la entidad Empresa como una entidad sin clave
        //    modelBuilder.Entity<Empresa>().HasNoKey();
        //}
    }
}
