namespace FSBR_AgendaSalas.API.DTOs.Reserva
{
    public class ReservaDTO
    {
        public Guid Id { get; set; }
        public Guid SalaId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataHoraReserva { get; set; }
    }
}
