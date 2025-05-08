using FSBR_AgendaSalas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSBR_AgendaSalas.Infrastructure.Persistence.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                  .ValueGeneratedOnAdd();

            builder.Property(r => r.DataHoraReserva)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(r => r.Sala)
                   .WithMany(s => s.Reservas)
                   .HasForeignKey(r => r.SalaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Usuario)
                   .WithMany(u => u.Reservas)
                   .HasForeignKey(r => r.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
