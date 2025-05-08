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
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd(); ;

            builder.Property(u => u.Email)
                   .IsRequired();

            builder.Property(u => u.Nome)
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
