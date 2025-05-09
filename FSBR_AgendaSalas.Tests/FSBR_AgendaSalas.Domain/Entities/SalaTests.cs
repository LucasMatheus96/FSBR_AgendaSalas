using System;
using FluentAssertions;
using FSBR_AgendaSalas.Domain.Entities;
using Xunit;

namespace FSBR_AgendaSalas.Tests.FSBR_AgendaSalas.Domain.Entities
{
    public class SalaTests
    {
        [Fact]
        public void Constructor_DeveCriarSalaComIdGerado_QuandoNomeCapacidadeValidos()
        {
            // Arrange
            var id = 2;
            var nome = "Sala de Reunião";
            var capacidade = 10;

            // Act
            var sala = new Sala( id , nome, capacidade);

            // Assert
            sala.Nome.Should().Be(nome);
            sala.Capacidade.Should().Be(capacidade);
            sala.Id.Should().NotBe(0);
            sala.Reservas.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_DeveCriarSalaComIdInformado()
        {
            // Arrange
            var id =3;
            var nome = "Sala 2";
            var capacidade = 20;

            // Act
            var sala = new Sala(id, nome, capacidade);

            // Assert
            sala.Id.Should().Be(id);
            sala.Nome.Should().Be(nome);
            sala.Capacidade.Should().Be(capacidade);
        }
    }
}
