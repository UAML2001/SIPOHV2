using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class NotificacionCorreo
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Correos { get; set; }
        public string NumExpediente { get; set; }
        public string NombreJuicio { get; set; }
        public string NombresCompletosActores { get; set; }
        public string NombresCompletosDemandados { get; set; }
        public string NombreJuzgado { get; set; }
        public string FechaAcuerdo { get; set; }
        public string CorreoEnviado { get; set; }
        public string dia { get; set; }
        public string mes { get; set; }
        public string año { get; set; }
        public string hora { get; set; }
        public string ruta { get; set; }
        public string NombreCompleto { get; set; }
        public string Sala { get; set; }
        public string CausaPenal { get; set; }
        public string tipoCausaControl { get; set; }
        public string tipoToca { get; set; }

        public string TipoDocumento { get; set; }
        public string NombreDistrito { get; set; }
        public string TipoAcuerdo { get; set; }

        public string NombreNotificado { get; set; }
        public string Folio { get; set; }
        public DateTime FechaActivacion { get; set; }

        public string RutaRedireccion { get; set; }

        public string GenerarContenidoActivarNotificacion()
        {
            Contenido = "<!DOCTYPE html> " +
                        "<html>" +
                        "<head>" +
                        "</head>" +
                        "<body>" +
                        $"<font color='Black' size='3' face='Arial'><p style='text-align:justify'><b> Estimado(a) {NombreCompleto}</b> <br/>" +
                        $"Usted ha solicitado satisfactoriamente la activación de la recepción de notificaciones por la plataforma del Sistema de <a href='{RutaRedireccion}', target='_blank'>Notificaciones Electrónicas Judiciales (SINEJ)</a>.<br/><br/> " +
                        $"<strong>Folio:</strong> {Folio} <br/> " +
                        $"<strong>Fecha:</strong> {FechaActivacion.ToShortDateString()}. <br/><br/> " +
                        $"Se hace constar mediante este correo que usted a aceptado los<a href='{ruta}', target='_blank'> lineamientos, términos y condiciones </a>de la notificación por correo electrónico.<br><br>" +
                         $"<h3>Favor de no remitir contestaciones a este correo ya que la bandeja de entrada no está monitoreada.</h3></p></font>" +
                        "<br/>" +
                        "</body>" +
                        "</html>";

            return Contenido;

        }



    }
}