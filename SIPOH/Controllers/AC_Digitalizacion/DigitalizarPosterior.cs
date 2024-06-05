using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class DigitalizarPosterior
    {
        public FileUpload UploadFileDigit { get; set; }
        public GridView noDigit { get; set; }
        public Page Page { get; set; }

        public void btnDigitalizar_Click(object sender, EventArgs e, int idPosterior)
        {
            try
            {
                // Validación: No avanzar si no se selecciona ningún archivo en el FileUpload
                if (!UploadFileDigit.HasFile)
                {
                    ShowToastr("Por favor, selecciona un archivo para subir", "error");
                    return;
                }

                // Validación: No avanzar si los elementos en "chkSelect" de la tabla "noDigit" están seleccionados
                foreach (GridViewRow row in noDigit.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        ShowToastr("Por favor, desmarca los elementos seleccionados en la lista de documentos no digitalizados", "error");
                        return;
                    }
                }

                string noDistrito = HttpContext.Current.Session["IdDistrito"]?.ToString();
                string idJuzgado = HttpContext.Current.Session["IdJuzgado"]?.ToString();
                string idAsunto = HttpContext.Current.Session["IdAsunto"]?.ToString();
                string tipoAsunto = HttpContext.Current.Session["TipoAsunto"]?.ToString();

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

                // Crear las carpetas
                string[] carpetas = { "DocsDigitalizados", noDistrito, idJuzgado, tipoAsunto, idAsunto };
                string rutaDestino = "";

                foreach (string carpeta in carpetas)
                {
                    rutaDestino = Path.Combine(rutaDestino, carpeta);
                    ArchivosFTP.CrearDirectorioFTP(rutaDestino);
                }

                // Subir el archivo PDF
                Stream fileStream = UploadFileDigit.FileContent;
                string nuevoNombre = $"P_{idPosterior}_{DateTime.Now:yyyyMMdd}.pdf"; // Usar idPosterior en lugar de folio
                string fileName = Path.Combine(rutaDestino, nuevoNombre);

                if (!ArchivosFTP.VerificarArchivoFTP(fileName))
                {
                    bool isUploaded = ArchivosFTP.CrearArchivoPDF(fileStream, fileName);

                    if (isUploaded)
                    {
                        // Actualizar el estado de digitalización
                        UpdateDigitalizadoPosterior updateDigitalizado = new UpdateDigitalizadoPosterior();
                        updateDigitalizado.Update(idPosterior);

                        // Insertar registro en la tabla P_Documentos
                        InsertarDocumentoEnBaseDeDatos(idAsunto, fileName, nuevoNombre, tipoAsunto, idPosterior);

                        HttpContext.Current.Session["ToastrMessage"] = "Posterior digitalizada con éxito";
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

        private void InsertarDocumentoEnBaseDeDatos(string idAsunto, string url, string nombrePDF, string tipoAsunto, int idPosterior)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                INSERT INTO [SIPOH].[dbo].[P_Documentos]
                    ([IdAsunto], [IdPosterior], [FechaDigitaliza], [IdUsuarios], [URL], [NombrePDF], [Descripcion])
                VALUES
                    (@IdAsunto, @IdPosterior, @FechaDigitaliza, @IdUsuarios, @URL, @NombrePDF, @Descripcion)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        command.Parameters.AddWithValue("@FechaDigitaliza", DateTime.Now);
                        command.Parameters.AddWithValue("@IdPosterior", idPosterior);
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
}