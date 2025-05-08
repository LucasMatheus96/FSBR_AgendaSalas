using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.API.DTOs.Reserva;
using Microsoft.AspNetCore.Mvc;

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
            await _reservaService.CriarReservaAsync(dto.SalaId, dto.UsuarioId, dto.DataHoraReserva,dto.EmailUsuario);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _reservaService.DeletarReservaAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
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
        public async Task<IActionResult> ObterPorSalaEData(Guid salaId, DateTime data)
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
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _reservaService.CancelarReservaAsync(id);
            return NoContent();
        }
    }
}
