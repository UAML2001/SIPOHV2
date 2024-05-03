using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Data;
using System.Data.SqlClient;

public class BusquedaVictimas
{
    public (string mensaje, DataTable dt) BuscarVictimas(string tipoAsunto, string numeroExpediente)
    {
        if (string.IsNullOrWhiteSpace(tipoAsunto) || tipoAsunto == "SO" || string.IsNullOrWhiteSpace(numeroExpediente))
            return ("Por favor, selecciona un asunto válido y proporciona un número de expediente.", null);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[6] { new DataColumn("APaterno"), new DataColumn("AMaterno"), new DataColumn("Nombre"), new DataColumn("Delitos"), new DataColumn("Edad"), new DataColumn("Genero") });

        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("ConsultarVictimas", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TipoAsunto", tipoAsunto));
                cmd.Parameters.Add(new SqlParameter("@Numero", numeroExpediente));
                cmd.Parameters.Add(new SqlParameter("@IdJuzgado", new GenerarIdJuzgadoPorSesion().ObtenerIdJuzgadoDesdeSesion()));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            row.ItemArray = new object[] { reader["APaterno"], reader["AMaterno"], reader["Nombre"], reader["Delitos"], "22", reader["Genero"] };
                            dt.Rows.Add(row);
                        }
                        return ("Se encontraron registros de las víctimas.", dt);
                    }
                    else
                    {
                        return ("No se encontraron registros que coincidan con la búsqueda.", null);
                    }
                }
            }
        }
    }
}



