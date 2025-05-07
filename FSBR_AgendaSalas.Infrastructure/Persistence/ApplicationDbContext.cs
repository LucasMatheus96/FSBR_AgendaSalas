using FSBR_AgendaSalas.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FSBR_AgendaSalas.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}