using Xunit;
using Moq;
using FSBR_AgendaSalas.Application.Services;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Domain.Entities;
using System;
using System.Threading.Tasks;

public class SalaServiceTests
{
    private readonly Mock<ISalaRepository> _salaRepositoryMock;
    private readonly SalaService _salaService;

    public SalaServiceTests()
    {
        _salaRepositoryMock = new Mock<ISalaRepository>();
        _salaService = new SalaService(_salaRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarSala()
    {
        // Arrange
        var salaId = 1;
        var salaNome = "Sala Reunião";
        var Capacidade = 10;
        var sala = new Sala(salaId,salaNome, Capacidade);
        _salaRepositoryMock.Setup(r => r.ObterPorIdAsync(salaId)).ReturnsAsync(sala);

        // Act
        var resultado = await _salaService.ObterPorIdAsync(salaId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Sala Reunião", resultado.Nome);
    }
}
