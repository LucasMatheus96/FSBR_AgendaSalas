using FSBR_AgendaSalas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSBR_AgendaSalas.Infra.Persistence.Configurations
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Salas");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.Capacidade)
                   .IsRequired();

            builder.Property(s => s.Nome)
                   .HasConversion<string>()
                   .IsRequired();

            //builder.HasOne(r => r.Sala)
            //       .WithMany(s => s.Reservas)
            //       .HasForeignKey(r => r.SalaId)
            //       .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(r => r.Usuario)
            //       .WithMany(u => u.Reservas)
            //       .HasForeignKey(r => r.UsuarioId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
