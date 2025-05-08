

using System.Threading.Tasks;

namespace FSBR_AgendaSalas.Application.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string para, string assunto, string mensagem);
    }
}
