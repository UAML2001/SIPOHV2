using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class LineamientosTransfer
    {
        public int IdRegistro { get; set; }
        public int IdLineamientos { get; set; }
        public int IdTransfer { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Evidencia { get; set; }
        public string Huella { get; set; }
        public string CN { get; set; }
        public string HexSerie { get; set; }
        public string DigestHash { get; set; }
        public string NombreArchivo { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Nombre { get; set; }
        public int IdUsuarioExterno { get; set; }



        public static  int InsertarLineamientosTransfer(LineamientosTransfer NotTransfer)
        {
            int IdAsignado = -1;
            object ResProcedimiento = new object();

            SqlCommand cmd = new SqlCommand("[dbo].[stp_AgregarLineamientosTransfer]", new SqlConnection(ConexionBD.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdLineamientos", SqlDbType.Int).Value = NotTransfer.IdLineamientos;
           
            cmd.Parameters.Add("@IdUsuarioExterno", SqlDbType.Int).Value = NotTransfer.IdUsuarioExterno;

            if (NotTransfer.IdTransfer > 0)
                cmd.Parameters.Add("@IdTransfer", SqlDbType.Int).Value = NotTransfer.IdTransfer;

            if (!string.IsNullOrEmpty(NotTransfer.Descripcion))
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 1000).Value = NotTransfer.Descripcion;

            if (NotTransfer.Fecha != null && NotTransfer.Fecha != DateTime.MinValue)
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = NotTransfer.Fecha;

            if (!string.IsNullOrEmpty(NotTransfer.Evidencia))
                cmd.Parameters.Add("@Evidencia", SqlDbType.VarChar, 1000).Value = NotTransfer.Evidencia;

            if (!string.IsNullOrEmpty(NotTransfer.Huella))
                cmd.Parameters.Add("@Huella", SqlDbType.VarChar, 1000).Value = NotTransfer.Huella;

            if (!string.IsNullOrEmpty(NotTransfer.CN))
                cmd.Parameters.Add("@Cn", SqlDbType.VarChar, 1000).Value = NotTransfer.CN;

            if (!string.IsNullOrEmpty(NotTransfer.HexSerie))
                cmd.Parameters.Add("@HexSerie", SqlDbType.VarChar, 1000).Value = NotTransfer.HexSerie;

            try
            {
                cmd.Connection.Open();
                ResProcedimiento = cmd.ExecuteScalar();
                cmd.Connection.Close();
                int.TryParse(ResProcedimiento.ToString(), out IdAsignado);
                return IdAsignado;
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();


                return -1;
            }
        }


    }
}