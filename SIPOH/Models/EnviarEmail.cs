using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SIPOH.Models
{
    public class EnviarEmail
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Correos { get; set; }


        public static int Envio(EnviarEmail Correo1)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConexionBD.ObtenerServidorSMTP(); //Host del servidor de correo
                smtp.Port = ConexionBD.ObtenerPuertoEmail(); //Puerto de salida
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Credentials = new NetworkCredential(ConexionBD.ObtenerEmail(), ConexionBD.ObtenerPassEmail());//Cuenta de correo

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(ConexionBD.ObtenerEmail(), "Subdirección de Sistemas del Poder Judicial del Estado de Hidalgo", System.Text.Encoding.UTF8);//Correo de salida

                string[] Destinos = Correo1.Correos.Split(',');
                foreach (string Email in Destinos)
                {
                    correo.To.Add(Email); //Correo destino?
                }

                correo.Subject = Correo1.Titulo; //Asunto
                correo.Body = Correo1.Contenido; //Mensaje del correo
                correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.High;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                smtp.SendMailAsync(correo).Wait();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}