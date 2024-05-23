using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Data;
using System.Data.SqlClient;

public class BusquedaImputado
{
    public (string mensaje, DataTable dt) BuscarImputados(string tipoAsunto, string numeroExpediente)
    {
        if (string.IsNullOrWhiteSpace(tipoAsunto) || tipoAsunto == "SO" || string.IsNullOrWhiteSpace(numeroExpediente))
            return ("Por favor, selecciona un asunto válido y proporciona un número de expediente.", null);

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[8] { new DataColumn("IdAsunto"), new DataColumn("IdPartes"), new DataColumn("APaterno"), new DataColumn("AMaterno"), new DataColumn("Nombre"), new DataColumn("Delitos"), new DataColumn("Edad"), new DataColumn("Genero") });

        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("ConsultarImputados", conn))
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
                            row.ItemArray = new object[] { reader["IdAsunto"], reader["IdPartes"], reader["APaterno"], reader["AMaterno"], reader["Nombre"], reader["Delitos"], reader["Edad"], reader["Genero"] };
                            dt.Rows.Add(row);
                        }
                        return ("Se encontraron registros de los imputados.", dt);
                    }
                    else
                    {
                        // Si no hay registros, devolvemos la tabla vacía en lugar de null
                        return ("No se encontraron Imputados.", dt);
                    }
                }
            }
        }
    }
}
