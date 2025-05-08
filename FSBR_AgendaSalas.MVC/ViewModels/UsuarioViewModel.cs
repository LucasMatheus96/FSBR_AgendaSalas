using System.ComponentModel.DataAnnotations;

namespace FSBR_AgendaSalas.MVC.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A capacidade é obrigatória")]        
        public string Email { get; set; }
    }
}
