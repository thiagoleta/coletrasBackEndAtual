using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Projeto.Services.Util
{
    public class MailService
    {
        //atributo
        private readonly MailSettings mailSettings;

        //construtor para injeção de dependência
        public MailService(MailSettings mailSettings)
        {
            this.mailSettings = mailSettings;
        }

        //método para enviar o email
        public void SendMail(string email, string subject, string body)
        {
            //montando a mensagem de email..
            var mail = new MailMessage(mailSettings.EmailAddress, email);
            mail.Subject = subject; //assunto
            mail.Body = body; //corpo do email
            mail.IsBodyHtml = true;

            //enviando o email..
            var client = new SmtpClient(mailSettings.Smtp, mailSettings.Port);
            client.EnableSsl = mailSettings.EnableSsl;
            client.UseDefaultCredentials = mailSettings.UseDefaultCredentials;
            client.Credentials = new NetworkCredential
            (mailSettings.EmailAddress, mailSettings.Password);
            client.Send(mail);
        }
    }
}
