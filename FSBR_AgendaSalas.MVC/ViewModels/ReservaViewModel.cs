using FSBR_AgendaSalas.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FSBR_AgendaSalas.MVC.ViewModels
{
    public class ReservaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione uma sala.")]
        public int SalaId { get; set; }
        [Required(ErrorMessage = "Selecione um usuário.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Informe a data e hora da reserva.")]
        public DateTime DataHoraReserva { get; set; }

        [Required(ErrorMessage = "Informe a data e hora final da reserva.")]
        public DateTime DataHoraFimReserva { get; set; }
        public string? EmailUsuario { get; set; }

        public string NomeSala { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
