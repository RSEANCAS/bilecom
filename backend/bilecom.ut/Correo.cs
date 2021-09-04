using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.ut
{
    public class Correo
    {
        public bool EnviarCorreo(string correoCliente, string asunto, string html)
        {
            bool seEnvio = true;
            try
            {
                
                string CorreoEmisor = "rseancas@gmail.com";
                //string CorreoEmisor = "antony.lazaroyepez@gmail.com";
                string password = "**S34nc4sB**";
                //string password = "laureanoyepez";
                MailMessage mailMessage = new MailMessage(CorreoEmisor, correoCliente, asunto, html);
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(CorreoEmisor, password);
                smtp.Send(mailMessage);
                smtp.Dispose();

            }
            catch (Exception ex)
            {
                seEnvio = false;
                //throw ex;
            }
            return seEnvio;
        }
    }
}
