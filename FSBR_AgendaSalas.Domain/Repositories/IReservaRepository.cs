using FSBR_AgendaSalas.Domain.Entities;

namespace FSBR_AgendaSalas.Domain.Repositories
{
    public interface IReservaRepository
    {
        Task<Reserva?> ObterPorIdAsync(Guid id);
        Task<List<Reserva>> ObterReservaPorSalaAsync(Guid salaId, DateTime data);
        Task AdicionarAsync(Reserva reserva);
        Task AtualizarAsync(Reserva reserva);
        Task DeletarAsync(Guid id);
    }
}
