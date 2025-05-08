using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.API.DTOs.Reserva;
using Microsoft.AspNetCore.Mvc;
using FSBR_AgendaSalas.API.DTOs.Sala;
using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Application.Services;

namespace FSBR_AgendaSalas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarReservaDTO dto)
        {
            await _reservaService.CriarReservaAsync(dto.Id,dto.SalaId, dto.UsuarioId, dto.DataHoraReserva,dto.EmailUsuario);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _reservaService.DeletarReservaAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var reserva = await _reservaService.ObterPorIdAsync(id);
            if (reserva == null) return NotFound();
            return Ok(new ReservaDTO
            {
                Id = reserva.Id,
                SalaId = reserva.SalaId,
                UsuarioId = reserva.UsuarioId,
                DataHoraReserva = reserva.DataHoraReserva
            });
        }

        [HttpGet("sala/{salaId}/data/{data}")]
        public async Task<IActionResult> ObterPorSalaEData(int salaId, DateTime data)
        {
            var reservas = await _reservaService.ObterReservasPorSalaAsync(salaId, data);
            return Ok(reservas.Select(r => new ReservaDTO
            {
                Id = r.Id,
                SalaId = r.SalaId,
                UsuarioId = r.UsuarioId,
                DataHoraReserva = r.DataHoraReserva
            }));
        }

        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> Cancelar(int id)
        {
            await _reservaService.CancelarReservaAsync(id);
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> Get()
        {
            var reservas = await _reservaService.ObterTodasAsync();
            if (reservas == null) return NotFound();
            return Ok(reservas.Select(s => new ReservaDTO{ Id = s.Id,SalaId = s.SalaId,UsuarioId = s.UsuarioId, DataHoraReserva = s.DataHoraReserva}));          
        }

       
     
    }
}
