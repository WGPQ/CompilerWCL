using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.Login_Resources
{
    class enviaremail
    {
        private string emailOrigen;
        private string password;
        private SmtpClient obt_smtp;

        public enviaremail()
        {
            this.emailOrigen = "sistemaVotEv4@gmail.com";
            this.password = "sistemaVE_v4";
            this.obt_smtp = conexionSmtp();
        }

        /**
        * Realizo la conexion smtp con gmail
        *
        * @return : retorno esa conexion
        */
        public SmtpClient conexionSmtp()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, password);
            return smtpClient;
        }

        /**
         * Envio el mensaje al destinatario
         */
        public void enviarEmail(string emailDestino, string asunto, string mensaje)
        {
            MailMessage mailMessage = new MailMessage("compiladorWLC@gmial.com", emailDestino, asunto, mensaje);
            mailMessage.IsBodyHtml = true;

            this.obt_smtp.Send(mailMessage);
            //this.obt_smtp.Dispose(); // para cerrar secion
        }
    }
}
