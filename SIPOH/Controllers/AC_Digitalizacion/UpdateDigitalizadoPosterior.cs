using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class UpdateDigitalizadoPosterior
    {
        public void Update(int idPosterior, List<int> desmarcados)
        {
            string desmarcadosString = string.Join(",", desmarcados);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateDigitalizadoPosterior", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPosterior", idPosterior);
                    cmd.Parameters.AddWithValue("@Desmarcados", desmarcadosString);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}