using FSBR_AgendaSalas.Domain.Entities;

namespace FSBR_AgendaSalas.Domain.Repositories
{
    public interface IReservaRepository
    {
        Task<Reserva?> ObterPorIdAsync(int id);
        Task<List<Reserva>> ObterReservaPorSalaAsync(int salaId, DateTime data);

        Task<List<Reserva>> ObterReservaPorSalaAsync(int salaId, DateTime dataHoraInicio, DateTime dataHoraFim);
        Task<List<Reserva>> ObterTodasAsync();
        Task AdicionarAsync(Reserva reserva);
        Task AtualizarAsync(Reserva reserva);
        Task DeletarAsync(int id);
    }
}
