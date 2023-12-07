using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.App_Start
{
    public class Consultas
    {
        private string connectionString;
        public Consultas()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
        }

        public bool UsuarioCorrecto(string nombre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM PUsuario WHERE Nombre = @nombre; ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Devuelve true si el usuario existe, de lo contrario, devuelve false
                }
            }
        }
    }

}