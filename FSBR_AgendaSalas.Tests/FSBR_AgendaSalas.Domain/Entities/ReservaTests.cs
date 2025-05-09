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
            var idReserva = 1;
            var salaId = 1;
            var usuarioId = 1;
            var dataHora = DateTime.Now.AddDays(2);
            var dataHoraFim = DateTime.Now.AddDays(3);

            // Act
            var reserva = new Reserva(idReserva, salaId, usuarioId, dataHora, dataHoraFim);

            // Assert
            reserva.Status.Should().Be(StatusReserva.Confirmada);
            reserva.SalaId.Should().Be(salaId);
            reserva.UsuarioId.Should().Be(usuarioId);
            reserva.DataHoraReserva.Should().Be(dataHora);
        }

        [Fact]
        public void CancelarReserva_DeveAlterarStatusParaCancelado_QuandoTempoPermitido()
        {
            var idReserva = 1;
            var salaId = 1;
            var usuarioId = 1;
            // Arrange
            var reserva = new Reserva(idReserva, salaId, usuarioId, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2));

            // Act
            reserva.CancelarReserva();

            // Assert
            reserva.Status.Should().Be(StatusReserva.Cancelada);
        }

        [Fact]
        public void CancelarReserva_DeveLancarExcecao_QuandoFaltaremMenosDe24Horas()
        {
            // Arrange
            var idReserva = 1;
            var salaId = 1;
            var usuarioId = 1;
            var reserva = new Reserva(idReserva, salaId, usuarioId, DateTime.Now.AddHours(22), DateTime.Now.AddHours(23));

            // Act
            Action act = () => reserva.CancelarReserva();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Não é possível cancelar a reserva com menos de 24 horas de antecedência.");
        }
    }
}
