using System;
using System.Data;
using System.Data.SqlClient;

public class ConsultarIdAsunto
{
    private string _connectionString; // Tu cadena de conexión a la base de datos

    public ConsultarIdAsunto()
    {
        _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
    }

    public int GetIdAsunto(string tipoAsunto, string numero, int idJuzgado)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("ConsultarIdAsunto", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TipoAsunto", SqlDbType.VarChar, 255).Value = tipoAsunto;
                cmd.Parameters.Add("@Numero", SqlDbType.VarChar, 255).Value = numero;
                cmd.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = idJuzgado;

                conn.Open();
                object result = cmd.ExecuteScalar();
                int idAsunto = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);

                return idAsunto;
            }
        }
    }
}

