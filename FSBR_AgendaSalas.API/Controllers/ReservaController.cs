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
        public async Task<IActionResult> Post([FromBody] CriarReservaDTO dto)
        {
            var resultado = await _reservaService.CriarReservaAsync(dto.Id, dto.SalaId, dto.UsuarioId, dto.DataHoraReserva, dto.DataHoraFimReserva, dto.EmailUsuario);

            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = resultado.Mensagem });

            }

            return BadRequest(new { erro = resultado.Mensagem });
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

        [HttpPost("cancelar/{id}")]
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
            return Ok(reservas.Select(s => new ReservaDTO
            {
                Id = s.Id,
                SalaId = s.SalaId,
                UsuarioId = s.UsuarioId,
                DataHoraReserva = s.DataHoraReserva,
                DataHoraFimReserva = s.DataHoraFimReserva,
                EmailUsuario = s.Usuario.Email,
                NomeSala = s.Sala.Nome,
                NomeUsuario = s.Usuario.Nome,
                Status = s.Status.ToString()
            }));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CriarReservaDTO dto)
        {
            var reserva = await _reservaService.ObterPorIdAsync(id);
            if (reserva == null) return NotFound();

            reserva.SalaId = dto.SalaId;
            reserva.DataHoraReserva = dto.DataHoraReserva;
            reserva.DataHoraFimReserva = dto.DataHoraFimReserva;
            reserva.UsuarioId = dto.UsuarioId;          
                

            var resultado = await _reservaService.AtualizarAsync(reserva);

            if (resultado.Sucesso)
            {
                return Ok(new { mensagem = resultado.Mensagem });

            }

            return BadRequest(new { erro = resultado.Mensagem });
        }



    }
}
