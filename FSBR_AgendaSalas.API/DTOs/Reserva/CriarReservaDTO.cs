namespace FSBR_AgendaSalas.API.DTOs.Reserva
{
    public class CriarReservaDTO
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHoraReserva { get; set; }
        public DateTime DataHoraFimReserva { get; set; }
        public string? EmailUsuario { get; set; }
    }
}
