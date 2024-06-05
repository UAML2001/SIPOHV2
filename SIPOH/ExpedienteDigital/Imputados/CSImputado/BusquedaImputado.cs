using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

public class BusquedaImputado
{
    // Variable de estado para seguir si el toastr ya ha sido mostrado
    private bool toastrMostrado = false;

    public (string mensaje, DataTable dt) BuscarImputados(string tipoAsunto, string numeroExpediente)
    {
        if (string.IsNullOrWhiteSpace(tipoAsunto) || tipoAsunto == "SO" || string.IsNullOrWhiteSpace(numeroExpediente))
            return ("Por favor, selecciona un asunto válido y proporciona un número de expediente.", null);

        // Validar longitud y formato del número de expediente
        if (!ValidarNumeroExpediente(numeroExpediente))
        {
            return ("Número de Expediente no válido", null);
        }

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

    // Método para validar el formato del número de expediente
    private bool ValidarNumeroExpediente(string numeroExpediente)
    {
        // El formato del número de expediente es "XXXX/XXXX"
        // Donde X representa un dígito

        // Expresión regular para validar el formato
        Regex regex = new Regex(@"^\d{4}\/\d{4}$");

        return regex.IsMatch(numeroExpediente);
    }

    // Método para mostrar un toastr con mensaje de error
    private void MostrarToastrError(string mensaje)
    {
        // Crear un script que muestre un toastr con el mensaje de error
        string script = @"<script type='text/javascript'>
                            toastr.error('" + mensaje + @"');
                          </script>";

        // Obtener la página actual
        Page page = HttpContext.Current.CurrentHandler as Page;

        // Verificar si la página es válida
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("ToastrScript"))
        {
            // Registrar el script para que se ejecute en el cliente
            page.ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", script);
        }
    }
}

