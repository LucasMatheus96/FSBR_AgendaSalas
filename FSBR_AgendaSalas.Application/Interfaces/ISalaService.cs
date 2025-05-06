namespace FSBR_AgendaSalas.Application.Interfaces
{
    using FSBR_AgendaSalas.Domain.Entities;

    public interface ISalaService
    {
        Task<Sala> ObterPorIdAsync(Guid id);
        Task<List<Sala>> ObterTodasAsync();
        Task AdicionarAsync(Sala sala);
        Task AtualizarAsync(Sala sala);
        Task DeletarAsync(Guid id);
    }
}
