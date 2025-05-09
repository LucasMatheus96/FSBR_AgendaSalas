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
        public int Id { get;  set; }
        public int SalaId { get;  set; }
        public int UsuarioId { get;  set; }
        public DateTime DataHoraReserva { get;  set; }
       
        public StatusReserva Status { get;  set; }

        public Sala Sala { get;  set; }
        public Usuario Usuario { get;  set; }

        public DateTime DataHoraFimReserva { get;  set; }



        public Reserva(int id , int salaId, int usuarioId, DateTime dataHoraReserva , DateTime dataHoraFimReserva)
        {
            Id = id;
            SalaId = salaId;
            UsuarioId = usuarioId;
            DataHoraReserva = dataHoraReserva;
            Status = StatusReserva.Confirmada;
            DataHoraFimReserva = dataHoraFimReserva;
        }

      


        public void CancelarReserva()
        {
            if (DataHoraReserva < DateTime.Now.AddHours(24))
                throw new InvalidOperationException("Não é possível cancelar a reserva com menos de 24 horas de antecedência.");

            Status = StatusReserva.Cancelada;
        }
     
    }

}
