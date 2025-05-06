using FSBR_AgendaSalas.Domain.Entities;

namespace FSBR_AgendaSalas.Application.Interfaces
{
    public interface IReservaService
    {
        Task CriarReservaAsync(Guid salaId, Guid usuarioId, DateTime dataHora);
        Task CancelarReservaAsync(Guid reservaId);
        Task DeletarReservaAsync(Guid reservaId);

    }
}
