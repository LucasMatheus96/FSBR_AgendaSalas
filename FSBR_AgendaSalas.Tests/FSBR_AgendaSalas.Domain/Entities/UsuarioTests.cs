using System;
using FluentAssertions;
using FSBR_AgendaSalas.Domain.Entities;
using Xunit;

namespace FSBR_AgendaSalas.Tests.FSBR_AgendaSalas.Domain.Entities
{
    public class UsuarioTests
    {
        [Fact]
        public void Constructor_DeveCriarUsuario_QuandoDadosValidos()
        {
            // Arrange
            var id = 1;
            var nome = "Lucas";
            var email = "lucas@email.com";

            // Act
            var usuario = new Usuario(id,nome, email);

            // Assert
            usuario.Id.Should().NotBe(0);
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
            usuario.Reservas.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_DeveCriarUsuarioComIdInformado()
        {
            // Arrange
            var id = 1;
            var nome = "Lara";
            var email = "lara@email.com";

            // Act
            var usuario = new Usuario(id, nome, email);

            // Assert
            usuario.Id.Should().Be(id);
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
        }

        [Fact]
        public void AtualizarUsuario_DeveAtualizarNomeEEmail_QuandoValidos()
        {
            // Arrange
            var id = 1;
            var usuario = new Usuario(id,"Teste", "teste@email.com");
            var novoNome = "Lucas Atualizado";
            var novoEmail = "lucas@atualizado.com";

            // Act
            usuario.AtualizarUsuario(novoNome, novoEmail);

            // Assert
            usuario.Nome.Should().Be(novoNome);
            usuario.Email.Should().Be(novoEmail);
        }

        [Theory]
        [InlineData("", "teste@email.com")]
        [InlineData(null, "teste@email.com")]
        public void AtualizarUsuario_DeveLancarExcecao_QuandoNomeInvalido(string nome, string email)
        {
            var id = 1;
            var usuario = new Usuario(id,"Nome", "email@email.com");

            // Act
            Action act = () => usuario.AtualizarUsuario(nome, email);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("O nome do usuário não pode ser vazio.");
        }

        [Theory]
        [InlineData("Lucas", "")]
        [InlineData("Lucas", null)]
        public void AtualizarUsuario_DeveLancarExcecao_QuandoEmailInvalido(string nome, string email)
        {
            var id = 1;
            var usuario = new Usuario(id,"Lucas", "email@email.com");

            // Act
            Action act = () => usuario.AtualizarUsuario(nome, email);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("O email do usuário não pode ser vazio.");
        }

        [Fact]
        public void AtualizarUsuario_DeveLancarExcecao_QuandoEmailFormatoInvalido()
        {
            var id = 1;
            var usuario = new Usuario(id,"Lucas", "teste@email.com");

            // Act
            Action act = () => usuario.AtualizarUsuario("Lucas", "email-invalido");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("O email fornecido não é válido.");
        }

        [Theory]
        [InlineData("lucas@email.com", true)]
        [InlineData("lucas@", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValidEmail_DeveRetornarCorreto(string email, bool esperado)
        {
            var id = 1;
            var usuario = new Usuario(id,"Lucas", "email@email.com");

            var resultado = usuario.IsValidEmail(email);

            resultado.Should().Be(esperado);
        }
    }
}
