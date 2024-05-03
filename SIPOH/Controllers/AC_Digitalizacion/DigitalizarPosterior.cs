using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class DigitalizarPosterior
        {
            public FileUpload UploadFileDigit { get; set; }
            public GridView noDigit { get; set; }
            public Page Page { get; set; }

            public void btnDigitalizar_Click(object sender, EventArgs e)
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
                    // Nota: Necesitarás reemplazar "noDigit" y "chkSelect" con los nombres reales de tu tabla y columna
                    foreach (GridViewRow row in noDigit.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            ShowToastr("Por favor, desmarca los elementos seleccionados en la de la lista de documentos no digitalizados", "error");
                            return;
                        }
                    }

                    string noDistrito = HttpContext.Current.Session["IdDistrito"]?.ToString();
                    string idJuzgado = HttpContext.Current.Session["IdJuzgado"]?.ToString();
                    string idAsunto = HttpContext.Current.Session["IdAsunto"]?.ToString();
                    string tipoAsunto = HttpContext.Current.Session["TipoAsunto"]?.ToString();
                    string folio = HttpContext.Current.Session["Folio"]?.ToString();

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
                    string guid = Guid.NewGuid().ToString(); // Genera un GUID.
                    string nuevoNombre = $"P_{folio}_{DateTime.Now.ToString("yyyyMMdd")}.pdf"; // Agrega el GUID al nombre del archivo.
                    string fileName = Path.Combine(rutaDestino, nuevoNombre);

                    if (!ArchivosFTP.VerificarArchivoFTP(fileName))
                    {
                        bool isUploaded = ArchivosFTP.CrearArchivoPDF(fileStream, fileName);

                        if (isUploaded)
                        {
                            // Actualizar el estado de digitalización
                            UpdateDigitalizadoPosterior updateDigitalizado = new UpdateDigitalizadoPosterior();
                            updateDigitalizado.Update(int.Parse(idAsunto));

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

            private void ShowToastr(string message, string type)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Toastr", $"toastr.{type}('{message}');", true);
            }
        }
    }