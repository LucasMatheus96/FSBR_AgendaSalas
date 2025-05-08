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

        public async Task<Sala> ObterPorIdAsync(int id)
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
            await _salaRepository.AdicionarAsync(sala);
        }

        public async Task AtualizarAsync(Sala sala)
        {
            var salaExistente = await _salaRepository.ObterPorIdAsync(sala.Id);
            if (salaExistente == null)
            {
                throw new Exception("Sala não encontrada para atualização.");
            }

            await _salaRepository.AtualizarAsync(sala);
        }

        public async Task DeleteAsync(int id)
        {
            var salaExistente = await _salaRepository.ObterPorIdAsync(id);
            if (salaExistente == null)
            {
                throw new Exception("Sala não encontrada para exclusão.");
            }
            
            await _salaRepository.DeletarAsync(id);
        }
    }
}
