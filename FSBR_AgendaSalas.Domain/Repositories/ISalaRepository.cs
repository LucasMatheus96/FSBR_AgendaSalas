using FSBR_AgendaSalas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSBR_AgendaSalas.Domain.Repositories
{
    public interface ISalaRepository
    {
        Task<Sala?> ObterPorIdAsync(int id);
        Task<List<Sala>> ObterTodasAsync();
        Task AdicionarAsync(Sala sala);
        Task AtualizarAsync(Sala sala);
        Task DeletarAsync(int id);
    }
}
