using FSBR_AgendaSalas.API.DTOs.Usuario;
using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Get()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();
        return Ok(usuarios.Select(u => new UsuarioDto { Id = u.Id, Nome = u.Nome , Email = u.Email}));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> GetById(int id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        if (usuario == null) return NotFound();

        return Ok(new UsuarioDto { Id = usuario.Id, Nome = usuario.Nome });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CriarUsuarioDto dto)
    {
        var usuario = new Usuario(dto.Id,dto.Nome,dto.Email);
        await _usuarioService.AdicionarAsync(usuario);
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CriarUsuarioDto dto)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        if (usuario == null) return NotFound();

        usuario.AtualizarUsuario(dto.Nome,dto.Email); // ou usuario.Nome = dto.Nome
        await _usuarioService.AtualizarAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _usuarioService.DeleteAsync(id);
        return NoContent();
    }
}
