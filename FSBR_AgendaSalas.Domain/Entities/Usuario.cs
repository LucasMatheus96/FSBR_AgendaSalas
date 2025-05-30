﻿

namespace FSBR_AgendaSalas.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public ICollection<Reserva> Reservas { get; private set; } = new List<Reserva>();      

        public Usuario(int id,string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;

        }

        public void AtualizarUsuario(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do usuário não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email do usuário não pode ser vazio.");

            if (!IsValidEmail(email))
                throw new ArgumentException("O email fornecido não é válido.");

            Nome = nome;
            Email = email;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    
}
