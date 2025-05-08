using FluentAssertions;
using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Application.Services;
using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Infrastructure.Repositories;
using FSBR_AgendaSalas.Infrastructure.Services.Email;
using Moq;

public class ReservaServiceTests
{
    private readonly Mock<IReservaRepository> _reservaRepoMock;
    private readonly Mock<ISalaRepository> _salaRepoMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly ReservaService _service;

    public ReservaServiceTests()
    {
        _reservaRepoMock = new Mock<IReservaRepository>();
        _salaRepoMock = new Mock<ISalaRepository>();
        _usuarioRepoMock = new Mock<IUsuarioRepository>();
        _emailServiceMock = new Mock<IEmailService>();

        _service = new ReservaService(
            _reservaRepoMock.Object,
            _salaRepoMock.Object,
            _usuarioRepoMock.Object,
            _emailServiceMock.Object
        );
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarReserva_QuandoExiste()
    {
        // Arrange
        var reservaId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var salaId = Guid.NewGuid();
        DateTime dataHoraReserva = DateTime.Now;

        var reservaEsperada = new Reserva(reservaId, salaId, usuarioId,dataHoraReserva);
        _reservaRepoMock.Setup(r => r.ObterPorIdAsync(reservaId))
            .ReturnsAsync(reservaEsperada);

        // Act
        var resultado = await _service.ObterPorIdAsync(reservaId);

        // Assert
        resultado.Should().Be(reservaEsperada);
    }
}
