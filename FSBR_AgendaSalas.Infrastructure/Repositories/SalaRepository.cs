﻿using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSBR_AgendaSalas.Infrastructure.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Sala?> ObterPorIdAsync(int id)
        {
            return await _context.Salas
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Sala>> ObterTodasAsync()
        {
            return await _context.Salas.ToListAsync();
        }

        public async Task AdicionarAsync(Sala sala)
        {
            await _context.Salas.AddAsync(sala);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Sala sala)
        {
            _context.Salas.Update(sala);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                _context.Salas.Remove(sala);
                await _context.SaveChangesAsync();
            }
        }
    }
}