using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSBR_AgendaSalas.Domain.Entities
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nome { get;  set; }
        public int Capacidade { get;  set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        // Constructor
        public Sala(int id ,string nome, int capacidade)
        {
            Id = id;
            Nome = nome;
            Capacidade = capacidade;
        }
       
    }
}
