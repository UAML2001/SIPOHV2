using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System;

namespace DatabaseConnection
{
    public class ConexionBD
    {
        
            private readonly string connectionString;
            public SqlConnection Connection { get; private set; }
            public ConexionBD()
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                Connection = new SqlConnection(connectionString);
            }
            public bool Conectar()
            {
                try
                {
                    Connection.Open();
                    Debug.WriteLine("Conexion Exitosa!");
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Hay problemas con la conexion" + ex.Message);
                    
                return false;
                }
            }
            public void Desconectar()
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }
        //conexion de firma

        internal static string Obtener()
        {
            return ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        }

        internal static string ObtenerRutaRedLineamientos()
        {
            return @"\\192.168.73.7\SIPOH\Lineamientos\";   // @"\\192.168.73.245\Notificaciones\Lineamientos\";
        }

        internal static string ObtenerRutaRedLineamientosFirma()
        {
            return @"\\192.168.73.7\SIPOH\LineamientosFirma\";    // @"\\192.168.73.245\Notificaciones\LineamientosFirma\";
        }

        internal static string ObtenerRutaRedDocumentos()
        {
            return @"\\192.168.73.7\SIPOH\Solicitudes\";    // @"\\192.168.73.245\Notificaciones\LineamientosFirma\";
        }

        internal static string ObtenerRutaSIPOH()
        {
            return "http://servernet3.pjhidalgo.gob.mx/SIPOH/";
        }
        internal static string ObtenerRutaSIPOHDocumentos()
        {
            return "http://nas.pjhidalgo.gob.mx/SIPOH/Solicitudes/";
        }


        internal static string ObtenerRutaLineamientosFirma()
        {
            return "192.168.73.7/sipoh/LineamientosFirma";//"http://nas.pjhidalgo.gob.mx/DoctosNotif/LineamientosFirma/";
        }

        internal static string ObtenerServidorSMTP()
        {
            return "smtp.office365.com";
        }
        internal static int ObtenerPuertoEmail()
        {
            return 587;
        }

        internal static string ObtenerEmail()
        {
            return "desarrollo.sistemas@pjhidalgo.gob.mx";
        }
        internal static string ObtenerPassEmail()
        {
            return "Sub20Pach";
        }
        //fin conexion de firma
    }
}