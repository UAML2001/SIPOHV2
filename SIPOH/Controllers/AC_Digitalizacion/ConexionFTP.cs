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
            return "ftp://192.168.73.7:22/";
        }
        internal static string ObtenerUsuarioFTP()
        {
            return "FTP_Sipoh24"; //"";
        }
        internal static string ObtenerClaveFTP()
        {
            return "Acu24"; //"";
        }
    }

    //public class ConexionFTP
    //{
    //    internal static string ObtenerRutaFTP()
    //    {
    //        return "ftp://SIPOHPruebas@192.168.1.5/";
    //    }
    //    internal static string ObtenerUsuarioFTP()
    //    {
    //        return "SIPOHPruebas"; //"";
    //    }
    //    internal static string ObtenerClaveFTP()
    //    {
    //        return "uriel2001"; //"";
    //    }
    //}
}