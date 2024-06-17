using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.ExpedienteDigital.Victimas.CSVictimas
{
    public class IdUsuarioPorSesion
    {
        public static int ObtenerIdUsuario()
        {
            // Asegúrate de que la sesión contenga el IdUsuario antes de intentar acceder a él
            if (HttpContext.Current.Session["IdUsuario"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            }
            else
            {
                // Manejar el caso en que la sesión no contenga un IdUsuario
                throw new InvalidOperationException("El IdUsuario no está presente en la sesión actual.");
            }
        }

        public static string ObtenerNombreCompletoUsuario(int idUsuario)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT APaterno, AMaterno, Nombre
            FROM [SIPOH].[dbo].[P_Usuarios]
            WHERE IdUsuario = @IdUsuario";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string aPaterno = reader["APaterno"].ToString();
                        string aMaterno = reader["AMaterno"].ToString();
                        string nombre = reader["Nombre"].ToString();

                        return $"{aPaterno} {aMaterno} {nombre}";
                    }
                    else
                    {
                        throw new InvalidOperationException("No se encontró el usuario en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones de conexión o de consulta
                    throw new InvalidOperationException("Error al consultar la base de datos: " + ex.Message);
                }
            }
        }

    }
}