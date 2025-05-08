using FSBR_AgendaSalas.Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;


namespace FSBR_AgendaSalas.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task EnviarEmailAsync(string destinatario, string assunto, string menssagem)
        {
            var mensagem = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = assunto,
                Body = menssagem,
                IsBodyHtml = true
            };
            mensagem.To.Add(destinatario);

            using var smtp = new SmtpClient(_settings.SmtpServer, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mensagem);
        }
    }
}
