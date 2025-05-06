using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;

namespace FSBR_AgendaSalas.Application.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public async Task<Sala> ObterPorIdAsync(Guid id)
        {
            var sala = await _salaRepository.ObterPorIdAsync(id);
            if (sala == null)
            {
                throw new Exception("Sala não encontrada.");
            }
            return sala;
        }

        public async Task<List<Sala>> ObterTodasAsync()
        {
            return await _salaRepository.ObterTodasAsync();
        }

        public async Task AdicionarAsync(Sala sala)
        {
            // Verificar se já existe uma sala com o mesmo nome, por exemplo
            // Se for necessário, você pode adicionar validações para garantir a integridade dos dados.
            await _salaRepository.AdicionarAsync(sala);
        }

        public async Task AtualizarAsync(Sala sala)
        {
            var salaExistente = await _salaRepository.ObterPorIdAsync(sala.Id);
            if (salaExistente == null)
            {
                throw new Exception("Sala não encontrada para atualização.");
            }

            // Se houver mais validações necessárias, faça aqui

            await _salaRepository.AtualizarAsync(sala);
        }

        public async Task DeletarAsync(Guid id)
        {
            var salaExistente = await _salaRepository.ObterPorIdAsync(id);
            if (salaExistente == null)
            {
                throw new Exception("Sala não encontrada para exclusão.");
            }

            // Pode-se adicionar uma lógica para verificar se a sala está em uso antes de excluí-la, se necessário.
            await _salaRepository.DeletarAsync(id);
        }
    }
}
