using Xunit;
using Moq;
using FSBR_AgendaSalas.Application.Services;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Domain.Entities;
using System;
using System.Threading.Tasks;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly UsuarioService _usuarioService;

    public UsuarioServiceTests()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarUsuario()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var usuarioNome = "Lucas";
        var usuarioEmail = "lucas@gmail.com";
        var usuario = new Usuario(usuarioId, usuarioNome, usuarioEmail);
        _usuarioRepositoryMock.Setup(r => r.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);

        // Act
        var resultado = await _usuarioService.ObterPorIdAsync(usuarioId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Lucas", resultado.Nome);
    }
}
