using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class UpdateDigitalizado
    {
        public void Update(int idAsunto, List<int> desmarcados)
        {
            string desmarcadosString = string.Join(",", desmarcados);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateDigitalizado", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Asunto", idAsunto);
                    cmd.Parameters.AddWithValue("@Desmarcados", desmarcadosString);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
