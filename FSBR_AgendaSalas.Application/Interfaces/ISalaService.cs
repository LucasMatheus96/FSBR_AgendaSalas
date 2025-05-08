using FSBR_AgendaSalas.Domain.Entities;

namespace FSBR_AgendaSalas.Application.Interfaces
{

    public interface ISalaService
    {
        Task<Sala> ObterPorIdAsync(int id);
        Task<List<Sala>> ObterTodasAsync();
        Task AdicionarAsync(Sala sala);
        Task AtualizarAsync(Sala sala);
        Task DeleteAsync(int id);
    }
}
