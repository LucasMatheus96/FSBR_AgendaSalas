using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSBR_AgendaSalas.Domain.Entities
{
    public class Sala
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Capacidade { get; private set; }

        public ICollection<Reserva> Reservas { get; private set; } = new List<Reserva>();

        // Constructor
        public Sala(string nome, int capacidade)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Capacidade = capacidade;
        }
    }
}
