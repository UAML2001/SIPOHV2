using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class ConsultaCargaPosterior
    {
        // Conectar con la base de datos
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        public DataTable ConsultaCargaDigitalizacion(string idJuzgado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"EXEC ConsultaCargaDigitalizacionPosterior @IdJuzgado";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}