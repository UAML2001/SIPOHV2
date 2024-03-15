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
            return "ftp://192.168.73.7:24/DocsDigitalizados/"; 
        }
        internal static string ObtenerUsuarioFTP()
        {
            return "FTPDigitaSIPOH"; //"";
        }
        internal static string ObtenerClaveFTP()
        {
            return "Electro24"; //"";
        }
    }
}