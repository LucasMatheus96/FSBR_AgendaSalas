using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace FSBR_AgendaSalas.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reserva?> ObterPorIdAsync(Guid id)
        {
            return await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reserva>> ObterReservaPorSalaAsync(Guid salaId, DateTime data)
        {
            return await _context.Reservas
                .Include(r => r.Sala)
                .Where(r => r.SalaId == salaId && r.DataHoraReserva.Date == data.Date)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var reserva = await ObterPorIdAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }
    }
}