namespace FSBR_AgendaSalas.API.DTOs.Reserva
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHoraReserva { get; set; }
    }
}
