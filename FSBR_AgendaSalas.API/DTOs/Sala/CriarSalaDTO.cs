namespace FSBR_AgendaSalas.API.DTOs.Sala
{
    public class SalaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; } = 0;
    }

}
