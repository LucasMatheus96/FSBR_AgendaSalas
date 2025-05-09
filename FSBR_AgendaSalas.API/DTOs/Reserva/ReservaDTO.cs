namespace FSBR_AgendaSalas.API.DTOs.Reserva
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHoraReserva { get; set; }
        public string NomeSala { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public string EmailUsuario { get; set; } = string.Empty;
        public DateTime DataHoraFimReserva { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
