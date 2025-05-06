using FSBR_AgendaSalas.Domain.Entities;


namespace FSBR_AgendaSalas.Application.Interfaces
{
   

    public interface IUsuarioService
    {
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task<List<Usuario>> ObterTodosAsync();
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task DeletarAsync(Guid id);
    }
}
