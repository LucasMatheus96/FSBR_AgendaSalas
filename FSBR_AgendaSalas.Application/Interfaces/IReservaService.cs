using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Shared;

namespace FSBR_AgendaSalas.Application.Interfaces
{
    public interface IReservaService
    {
        Task<Resultado> CriarReservaAsync(int id, int salaId, int usuarioId, DateTime dataHora, DateTime dataHoraFinal, string emailUsuario);
        Task CancelarReservaAsync(int reservaId);
        Task DeletarReservaAsync(int reservaId);

        Task<Reserva> ObterPorIdAsync(int id);
        Task<List<Reserva>> ObterReservasPorSalaAsync(int salaId, DateTime data);
        Task<List<Reserva>> ObterTodasAsync();

        Task<Resultado> AtualizarAsync(Reserva reserva);
    }
}
