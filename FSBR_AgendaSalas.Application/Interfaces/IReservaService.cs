using FSBR_AgendaSalas.Domain.Entities;

namespace FSBR_AgendaSalas.Application.Interfaces
{
    public interface IReservaService
    {
        Task CriarReservaAsync(int id, int salaId, int usuarioId, DateTime dataHora, string emailUsuario);
        Task CancelarReservaAsync(int reservaId);
        Task DeletarReservaAsync(int reservaId);

        Task<Reserva> ObterPorIdAsync(int id);
        Task<List<Reserva>> ObterReservasPorSalaAsync(int salaId, DateTime data);
        Task<List<Reserva>> ObterTodasAsync();
    }
}
