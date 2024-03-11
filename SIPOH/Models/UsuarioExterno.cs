using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class UsuarioExterno
    {
        public int IdUsuarioExterno { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Nombre { get; set; }
        public string ClavePersona { get; set; }
        public string TipoPersona { get; set; }
        public string Estado { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoE { get; set; }
        public string CURP { get; set; }
        public DateTime FechaActivacion { get; set; }



        public static UsuarioExterno ObtenerNNotificado(string CURP, ref bool resultado)
        {
            UsuarioExterno Notificado = new UsuarioExterno();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;

            resultado = false;

            cmd.Connection = new SqlConnection(ConexionBD.Obtener());
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM P_UsuarioExterno  ";
            cmd.CommandText += " WHERE CURP = @CURP ";

            cmd.Parameters.Add("@CURP", SqlDbType.NVarChar, 255).Value = CURP;

            try
            {
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Notificado.IdUsuarioExterno = BdConverter.FieldToInt(rdr["IdUsuarioExterno"]);
                    Notificado.ApPaterno = BdConverter.FieldToString(rdr["ApPaterno"]);
                    Notificado.ApMaterno = BdConverter.FieldToString(rdr["ApMaterno"]);
                    Notificado.Nombre = BdConverter.FieldToString(rdr["Nombre"]);
                    Notificado.Estado = BdConverter.FieldToString(rdr["Estado"]);
                    Notificado.NombreCompleto = BdConverter.FieldToString(rdr["NombreCompleto"]);
                    Notificado.CorreoE = BdConverter.FieldToString(rdr["CorreoE"]);
                    Notificado.CURP = BdConverter.FieldToString(rdr["CURP"]);

                }

                resultado = true;
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                resultado = false;

            }

            return Notificado;
        }

        public static UsuarioExterno ObtenerUsuarioxid(string IdUsuarioExterno, ref bool resultado)
        {
            UsuarioExterno Notificado = new UsuarioExterno();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;

            resultado = false;

            cmd.Connection = new SqlConnection(ConexionBD.Obtener());
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM P_UsuarioExterno  ";
            cmd.CommandText += " WHERE IdUsuarioExterno = @IdUsuarioExterno ";

            cmd.Parameters.Add("@IdUsuarioExterno", SqlDbType.Int).Value = IdUsuarioExterno;

            try
            {
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Notificado.IdUsuarioExterno = BdConverter.FieldToInt(rdr["IdUsuarioExterno"]);
                    Notificado.ApPaterno = BdConverter.FieldToString(rdr["ApPaterno"]);
                    Notificado.ApMaterno = BdConverter.FieldToString(rdr["ApMaterno"]);
                    Notificado.Nombre = BdConverter.FieldToString(rdr["Nombre"]);
                    Notificado.Estado = BdConverter.FieldToString(rdr["Estado"]);
                    Notificado.NombreCompleto = BdConverter.FieldToString(rdr["NombreCompleto"]);
                    Notificado.CorreoE = BdConverter.FieldToString(rdr["CorreoE"]);
                    Notificado.CURP = BdConverter.FieldToString(rdr["CURP"]);

                }

                resultado = true;
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                resultado = false;

            }

            return Notificado;
        }


        public static int InsertarUsuarioExterno(UsuarioExterno Notificado)
        {
            int IdAsignado = -1;
            object ResProcedimiento = new object();

            SqlCommand cmd = new SqlCommand("[dbo].[stp_Notificacion_UsuarioExterno]", new SqlConnection(ConexionBD.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ApPaterno", SqlDbType.VarChar, 255).Value = Notificado.ApPaterno;
            cmd.Parameters.Add("@ApMaterno", SqlDbType.VarChar, 255).Value = Notificado.ApMaterno;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 255).Value = Notificado.Nombre;
            cmd.Parameters.Add("@FechaActivacion", SqlDbType.DateTime).Value = Notificado.FechaActivacion;

            if (!string.IsNullOrEmpty(Notificado.Estado))
                cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 1).Value = Notificado.Estado;
            if (!string.IsNullOrEmpty(Notificado.NombreCompleto))
                cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 255).Value = Notificado.NombreCompleto;
            if (!string.IsNullOrEmpty(Notificado.CorreoE))
                cmd.Parameters.Add("@CorreoE", SqlDbType.VarChar, 255).Value = Notificado.CorreoE;
            if (!string.IsNullOrEmpty(Notificado.CURP))
                cmd.Parameters.Add("@CURP", SqlDbType.VarChar, 20).Value = Notificado.CURP;

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