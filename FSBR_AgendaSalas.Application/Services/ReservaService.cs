using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;

namespace FSBR_AgendaSalas.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly ISalaRepository _salaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;

        public ReservaService(IReservaRepository reservaRepository, ISalaRepository salaRepository, IUsuarioRepository usuarioRepository, IEmailService emailService)
        {
            _reservaRepository = reservaRepository;
            _salaRepository = salaRepository;
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        public async Task CriarReservaAsync(Guid salaId, Guid usuarioId, DateTime dataHora, string emailUsuario)
        {
            var sala = await _salaRepository.ObterPorIdAsync(salaId);
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

            if (sala == null) throw new Exception("Sala não encontrada.");
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            // Verificar se já existe reserva para essa sala no mesmo horário
            var reservas = await _reservaRepository.ObterReservaPorSalaAsync(salaId, dataHora.Date);
            if (reservas.Any(r => r.DataHoraReserva == dataHora))
                throw new Exception("Já existe uma reserva para esta sala neste horário.");

            var reserva = new Reserva(salaId, usuarioId, dataHora);
            await _reservaRepository.AdicionarAsync(reserva);
            await _emailService.EnviarEmailAsync(emailUsuario, "Reserva confirmada", "<h1>Sua reserva foi confirmada!</h1>");

        }

        public async Task CancelarReservaAsync(Guid reservaId)
        {
            var reserva = await _reservaRepository.ObterPorIdAsync(reservaId);
            if (reserva == null) throw new Exception("Reserva não encontrada.");

            reserva.CancelarReserva();
            await _reservaRepository.AtualizarAsync(reserva);
        }

        public async Task DeletarReservaAsync(Guid reservaId)
        {
            var reserva = await _reservaRepository.ObterPorIdAsync(reservaId);
            if (reserva == null)
                throw new Exception("Reserva não encontrada.");

            // Deletar a reserva no repositório
            await _reservaRepository.DeletarAsync(reservaId);
        }

        public async Task<Reserva> ObterPorIdAsync(Guid id)
        {
            return await _reservaRepository.ObterPorIdAsync(id);
        }

        public async Task<List<Reserva>> ObterReservasPorSalaAsync(Guid salaId, DateTime data)
        {
            return await _reservaRepository.ObterReservaPorSalaAsync(salaId, data);
        }


        public async Task<List<Reserva>> ObterTodasAsync()
        {
            return await _reservaRepository.ObterTodasAsync();
        }


    }
}