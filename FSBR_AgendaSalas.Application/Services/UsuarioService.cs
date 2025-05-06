using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;

namespace FSBR_AgendaSalas.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            return usuario;
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _usuarioRepository.ObterTodosAsync();
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
        
            await _usuarioRepository.AdicionarAsync(usuario);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(usuario.Id);
            if (usuarioExistente == null)
            {
                throw new Exception("Usuário não encontrado para atualização.");
            }

           

            await _usuarioRepository.AtualizarAsync(usuario);
        }

        public async Task DeletarAsync(Guid id)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuarioExistente == null)
            {
                throw new Exception("Usuário não encontrado para exclusão.");
            }

            // Caso necessário, verifique se o usuário está associado a alguma reserva ou outro processo antes de excluir.
            await _usuarioRepository.DeletarAsync(id);
        }
    }
}
