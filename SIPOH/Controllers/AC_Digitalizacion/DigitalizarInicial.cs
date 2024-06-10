using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH;
using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class DigitalizarInicial
{
    public FileUpload UploadFileDigit { get; set; }
    public GridView noDigit { get; set; }
    public Page Page { get; set; }

    public void btnDigitalizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!UploadFileDigit.HasFile)
            {
                ShowToastr("Por favor, selecciona un archivo para subir", "error");
                return;
            }

            List<int> desmarcados = new List<int>();
            foreach (GridViewRow row in noDigit.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && !chkSelect.Checked)
                {
                    int idAnexoC = Convert.ToInt32(noDigit.DataKeys[row.RowIndex].Value);
                    desmarcados.Add(idAnexoC);
                }
            }

            string noDistrito = HttpContext.Current.Session["IdDistrito"]?.ToString();
            string idJuzgado = HttpContext.Current.Session["IdJuzgado"]?.ToString();
            string idAsunto = HttpContext.Current.Session["IdAsunto"]?.ToString();
            string tipoAsunto = HttpContext.Current.Session["TipoAsunto"]?.ToString();
            string folio = HttpContext.Current.Session["Folio"]?.ToString();

            string[] carpetas = { "DocsDigitalizados", noDistrito, idJuzgado, tipoAsunto, idAsunto };
            string rutaDestino = "";

            foreach (string carpeta in carpetas)
            {
                rutaDestino = Path.Combine(rutaDestino, carpeta);
                ArchivosFTP.CrearDirectorioFTP(rutaDestino);
            }

            Stream fileStream = UploadFileDigit.FileContent;
            string guid = Guid.NewGuid().ToString();
            string nuevoNombre = $"I_{idAsunto}_{DateTime.Now.ToString("yyyyMMdd")}.pdf";
            string fileName = Path.Combine(rutaDestino, nuevoNombre);

            if (!ArchivosFTP.VerificarArchivoFTP(fileName))
            {
                bool isUploaded = ArchivosFTP.CrearArchivoPDF(fileStream, fileName);

                if (isUploaded)
                {
                    UpdateDigitalizado updateDigitalizado = new UpdateDigitalizado();
                    updateDigitalizado.Update(int.Parse(idAsunto), desmarcados);

                    InsertarDocumentoEnBaseDeDatos(idAsunto, fileName, nuevoNombre, tipoAsunto);

                    HttpContext.Current.Session["ToastrMessage"] = "Inicial digitalizada con éxito";
                    HttpContext.Current.Session["ToastrType"] = "success";
                }
                else
                {
                    HttpContext.Current.Session["ToastrMessage"] = "Error al subir el archivo";
                    HttpContext.Current.Session["ToastrType"] = "error";
                }

                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
            }
            else
            {
                ShowToastr("El archivo ya existe", "info");
            }
        }
        catch (Exception ex)
        {
            ShowToastr($"Error inesperado: {ex.Message}", "error");
        }
    }

    private void InsertarDocumentoEnBaseDeDatos(string idAsunto, string url, string nombrePDF, string tipoAsunto)
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                INSERT INTO [SIPOH].[dbo].[P_Documentos]
                    ([IdAsunto], [FechaDigitaliza], [IdUsuarios], [URL], [NombrePDF], [Descripcion])
                VALUES
                    (@IdAsunto, @FechaDigitaliza, @IdUsuarios, @URL, @NombrePDF, @Descripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    command.Parameters.AddWithValue("@FechaDigitaliza", DateTime.Now);
                    command.Parameters.AddWithValue("@IdUsuarios", HttpContext.Current.Session["IdUsuario"]?.ToString());
                    command.Parameters.AddWithValue("@URL", url);
                    command.Parameters.AddWithValue("@NombrePDF", nombrePDF);
                    command.Parameters.AddWithValue("@Descripcion", tipoAsunto);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            ShowToastr($"Error al insertar en la base de datos: {ex.Message}", "error");
        }
    }

    private void ShowToastr(string message, string type)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Toastr", $"toastr.{type}('{message}');", true);
    }
}
