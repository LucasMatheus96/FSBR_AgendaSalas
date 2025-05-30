﻿using FSBR_AgendaSalas.API.DTOs.Sala;
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
        return Ok(salas.Select(s => new SalaDto { Id = s.Id, Nome = s.Nome , Capacidade = s.Capacidade}));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaDto>> GetById(int id)
    {
        var sala = await _salaService.ObterPorIdAsync(id);
        if (sala == null) return NotFound();

        return Ok(new SalaDto { Id = sala.Id, Nome = sala.Nome });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CriarSalaDto dto)
    {
        var sala = new Sala(dto.Id,dto.Nome, dto.Capacidade);
        await _salaService.AdicionarAsync(sala);
        return CreatedAtAction(nameof(GetById), new { id = sala.Id }, null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CriarSalaDto dto)
    {
        var sala = await _salaService.ObterPorIdAsync(id);
        if (sala == null) return NotFound();

        sala.Nome = dto.Nome;
        sala.Capacidade = dto.Capacidade;
        await _salaService.AtualizarAsync(sala);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _salaService.DeleteAsync(id);
        return NoContent();
    }
}
