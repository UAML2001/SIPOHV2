using DatabaseConnection;
using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace SIPOH.Externo
{
    /// <summary>
    /// Descripción breve de ServicioWeb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class ServicioWeb : System.Web.Services.WebService
    {

        [WebMethod]
        public LineamientosHash GetLineamientos()
        {
            LineamientosHash Lineamientos = new LineamientosHash();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;
            cmd.Connection = new SqlConnection(ConexionBD.Obtener());

            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM dbo.P_LineamientosHash ";
            cmd.CommandText += " WHERE Vigente = 1 ";

            try
            {
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Read();
                    Lineamientos.IdNLineamientos = BdConverter.FieldToInt(rdr["IdLineamientos"]);
                    Lineamientos.Hash = BdConverter.FieldToString(rdr["Hash"]);
                    Lineamientos.Vigente = BdConverter.FieldToBoolean(rdr["Vigente"]);
                    Lineamientos.FechaCreacion = Convert.ToDateTime(rdr["FechaCreacion"]);
                    Lineamientos.NombreArchivo = Convert.ToString(rdr["NombreArchivo"]);
                }

                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }

            return Lineamientos;
        }

        [WebMethod]
        public UsuarioExterno ObtenerUsuarioExterno(string CURP)
        {
            bool realizado = false;
            return UsuarioExterno.ObtenerNNotificado(CURP, ref realizado);
        }



        
    }
}
