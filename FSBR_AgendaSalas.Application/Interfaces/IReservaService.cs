using FSBR_AgendaSalas.Domain.Entities;

namespace FSBR_AgendaSalas.Application.Interfaces
{
    public interface IReservaService
    {
        Task CriarReservaAsync(Guid salaId, Guid usuarioId, DateTime dataHora, string emailUsuario);
        Task CancelarReservaAsync(Guid reservaId);
        Task DeletarReservaAsync(Guid reservaId);

        Task<Reserva> ObterPorIdAsync(Guid id);

        Task<List<Reserva>> ObterReservasPorSalaAsync(Guid salaId, DateTime data);
    }
}
