using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Domain.Entities;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Domain.Shared;

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

        public async Task<Resultado> CriarReservaAsync(int id ,int salaId, int usuarioId, DateTime dataHora, DateTime dataHoraFinal, string emailUsuario)
        {
            try
            {
                var sala = await _salaRepository.ObterPorIdAsync(salaId);
                var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

                if (sala == null) return Resultado.Falha("Sala não encontrada.");
                if (usuario == null) return Resultado.Falha("Usuário não encontrado.");

                // Verificar se já existe reserva para essa sala no mesmo horário
                var reservas = await _reservaRepository.ObterReservaPorSalaAsync(salaId, dataHora, dataHoraFinal);
                if (reservas.Any())return Resultado.Falha("Já existe uma reserva para esta sala neste horário.");

                var reserva = new Reserva(id, salaId, usuarioId, dataHora, dataHoraFinal);
                await _reservaRepository.AdicionarAsync(reserva);
                try
                {
                    await _emailService.EnviarEmailAsync(emailUsuario, "Reserva confirmada", "<h1>Sua reserva foi confirmada!</h1>");
                }
                catch
                {
                    //Apenas para não parar a aplicação na tentativa do envio pois os dados não estão configurados
                }

                return Resultado.Ok("Reserva criada com sucesso");
            }
            catch (Exception e)
            {
                return Resultado.Falha("Erro inesperado ao criar a reserva:" + e.Message);
            }
            

        }

        public async Task<Resultado> CancelarReservaAsync(int reservaId)
        {
            try
            {
                var reserva = await _reservaRepository.ObterPorIdAsync(reservaId);
                if (reserva == null) return Resultado.Falha("Reserva não encontrada.");

                reserva.CancelarReserva();
                await _reservaRepository.AtualizarAsync(reserva);

                return Resultado.Ok("Reserva cancelada com sucesso");
            }
            catch (Exception e)
            {
                return Resultado.Falha(e.Message);
            }
           
        }

        public async Task<Resultado> DeletarReservaAsync(int reservaId)
        {
            try
            {
                var reserva = await _reservaRepository.ObterPorIdAsync(reservaId);
                if (reserva == null)
                    return Resultado.Falha("Reserva não encontrada.");

                // Deletar a reserva no repositório
                await _reservaRepository.DeletarAsync(reservaId);

                return Resultado.Ok("Reserva excluida com sucesso.");
            }
            catch (Exception)
            {
                return Resultado.Falha("Falha ao excluir reserva");
            }
           
        }

        public async Task<Reserva> ObterPorIdAsync(int id)
        {
            return await _reservaRepository.ObterPorIdAsync(id);
        }

        public async Task<List<Reserva>> ObterReservasPorSalaAsync(int salaId, DateTime data)
        {
            return await _reservaRepository.ObterReservaPorSalaAsync(salaId, data);
        }


        public async Task<List<Reserva>> ObterTodasAsync()
        {
            return await _reservaRepository.ObterTodasAsync();
        }

        public async Task<Resultado> AtualizarAsync(Reserva reserva)
        {
            try
            {
                var reservaExistente = await _reservaRepository.ObterPorIdAsync(reserva.Id);
                if (reservaExistente == null)
                {
                    return Resultado.Falha("Reserva não encontrada para atualização.");
                }

                if (reserva.DataHoraFimReserva != reservaExistente.DataHoraFimReserva || reserva.DataHoraReserva != reservaExistente.DataHoraReserva)
                {
                    var reservas = await _reservaRepository.ObterReservaPorSalaAsync(reserva.SalaId, reserva.DataHoraReserva, reserva.DataHoraFimReserva);
                    if (reservas.Any()) return Resultado.Falha("Já existe uma reserva para esta sala neste horário.");
                }
                await _reservaRepository.AtualizarAsync(reserva);

                return Resultado.Ok("Reserva criada com sucesso");
            }
            catch (Exception)
            {
                return Resultado.Falha("Falha ao criar reserva.");
            }
         
        }


    }
}