using System.ComponentModel.DataAnnotations;

namespace FSBR_AgendaSalas.MVC.ViewModels
{
    public class ReservaViewModel
    {
        public int Id { get; set; }

        public int SalaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHoraReserva { get; set; }

        public string EmailUsuario { get; set; }
    }
}
