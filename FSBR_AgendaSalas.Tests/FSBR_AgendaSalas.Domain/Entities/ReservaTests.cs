using System;
using FluentAssertions;
using FSBR_AgendaSalas.Domain.Entities;
using Xunit;

namespace FSBR_AgendaSalas.Tests.FSBR_AgendaSalas.Domain.Entities
{
    public class ReservaTests
    {
        [Fact]
        public void Constructor_DeveCriarReservaComStatusConfirmado()
        {
            // Arrange
            var salaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var dataHora = DateTime.Now.AddDays(2);

            // Act
            var reserva = new Reserva(salaId, usuarioId, dataHora);

            // Assert
            reserva.Status.Should().Be(StatusReserva.Confirmada);
            reserva.SalaId.Should().Be(salaId);
            reserva.UsuarioId.Should().Be(usuarioId);
            reserva.DataHoraReserva.Should().Be(dataHora);
        }

        [Fact]
        public void CancelarReserva_DeveAlterarStatusParaCancelado_QuandoTempoPermitido()
        {
            // Arrange
            var reserva = new Reserva(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(2));

            // Act
            reserva.CancelarReserva();

            // Assert
            reserva.Status.Should().Be(StatusReserva.Cancelada);
        }

        [Fact]
        public void CancelarReserva_DeveLancarExcecao_QuandoFaltaremMenosDe24Horas()
        {
            // Arrange
            var reserva = new Reserva(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddHours(23));

            // Act
            Action act = () => reserva.CancelarReserva();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Não é possível cancelar a reserva com menos de 24 horas de antecedência.");
        }
    }
}
