using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class UpdateDigitalizado
    {
        // Conectar con la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        public void Update(int idAsunto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"EXEC UpdateDigitalizado @Id_Asunto";

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
