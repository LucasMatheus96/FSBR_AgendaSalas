using FSBR_AgendaSalas.API.DTOs.Sala;
using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SalaController : ControllerBase
{
    private readonly ISalaService _salaService;

    public SalaController(ISalaService salaService)
    {
        _salaService = salaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaDto>>> Get()
    {
        var salas = await _salaService.ObterTodasAsync();
        return Ok(salas.Select(s => new SalaDto { Id = s.Id, Nome = s.Nome }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaDto>> GetById(Guid id)
    {
        var sala = await _salaService.ObterPorIdAsync(id);
        if (sala == null) return NotFound();

        return Ok(new SalaDto { Id = sala.Id, Nome = sala.Nome });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CriarSalaDto dto)
    {
        var sala = new Sala(dto.Nome, dto.Capacidade);
        await _salaService.AdicionarAsync(sala);
        return CreatedAtAction(nameof(GetById), new { id = sala.Id }, null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] CriarSalaDto dto)
    {
        var sala = await _salaService.ObterPorIdAsync(id);
        if (sala == null) return NotFound();

        sala.Nome = dto.Nome; 
        await _salaService.AtualizarAsync(sala);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _salaService.DeletarAsync(id);
        return NoContent();
    }
}
