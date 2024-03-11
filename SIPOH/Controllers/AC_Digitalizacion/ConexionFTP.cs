using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class ConexionFTP
    {
        internal static string ObtenerRutaFTP()
        {
            return "ftp://192.168.56.1:21/DocsDigitalizados/"; 
        }
        internal static string ObtenerUsuarioFTP()
        {
            return "SIPOHPruebas"; //"";
        }
        internal static string ObtenerClaveFTP()
        {
            return "uriel2001"; //"";
        }


        internal static string ObtenerRutaFTPServer()
        {
            return "ftp://nas.pjhidalgo.gob.mx:22/Solicitudes/";
        }
        internal static string ObtenerUsuarioFTPServer()
        {
            return "FTP_Sipoh24"; //"";
        }
        internal static string ObtenerClaveFTPServer()
        {
            return "Acu24"; //"";
        }


    }
}