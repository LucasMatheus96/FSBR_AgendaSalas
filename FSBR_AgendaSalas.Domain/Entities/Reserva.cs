using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSBR_AgendaSalas.Domain.Entities
{
    public enum StatusReserva
    {
        Confirmada = 1,
        Cancelada = 2
    }

    public class Reserva
    {
        public Guid Id { get; private set; }
        public Guid SalaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public DateTime DataHoraReserva { get; private set; }
        public StatusReserva Status { get; private set; }

        public Sala Sala { get; private set; }
        public Usuario Usuario { get; private set; }


        public Reserva(Guid salaId, Guid usuarioId, DateTime dataHoraReserva)
        {
            Id = Guid.NewGuid();
            SalaId = salaId;
            UsuarioId = usuarioId;
            DataHoraReserva = dataHoraReserva;
            Status = StatusReserva.Confirmada; 
        }

       
        public void CancelarReserva()
        {
            if (DataHoraReserva < DateTime.Now.AddHours(24))
                throw new InvalidOperationException("Não é possível cancelar a reserva com menos de 24 horas de antecedência.");

            Status = StatusReserva.Cancelada;
        }
     
    }

}
