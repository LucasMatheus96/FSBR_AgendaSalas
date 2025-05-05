using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AdicionarAsync(Usuario Usuario)
    {
        await _context.Usuarios.AddAsync(Usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario Usuario)
    {
        _context.Usuarios.Update(Usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Guid id)
    {
        var Usuario = await _context.Usuarios.FindAsync(id);
        if (Usuario != null)
        {
            _context.Usuarios.Remove(Usuario);
            await _context.SaveChangesAsync();
        }
    }

  
}