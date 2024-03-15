using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class UpdateDigitalizadoPosterior
    {
        // Conectar con la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        public void Update(int idAsunto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"EXEC UpdateDigitalizadoPosterior @Id_Asunto";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id_Asunto", idAsunto);

                // Abrir la conexión
                conn.Open();

                // Ejecutar el procedimiento almacenado
                cmd.ExecuteNonQuery();
            }
        }
    }
}