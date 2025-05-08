using System.ComponentModel.DataAnnotations;

namespace FSBR_AgendaSalas.MVC.ViewModels
{
    public class ReservaViewModel
    {
        public Guid Id { get; set; }

        public Guid SalaId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataHoraReserva { get; set; }

        public string EmailUsuario { get; set; }
    }
}
