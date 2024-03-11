using SIPOH.App_Start;
using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class DigitalizarIniciales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarIdJuzgadoPorSesion idJuzgado = new GenerarIdJuzgadoPorSesion();
                int id = idJuzgado.ObtenerIdJuzgadoDesdeSesion();

                ConsultaCargaInicial consultaCarga = new ConsultaCargaInicial();
                // Supongamos que el método ConsultarAsuntosNoDigitalizados() de la clase ConsultaCarga devuelve los datos que quieres mostrar en la tabla.
                DataTable datos = consultaCarga.ConsultaCargaDigitalizacion(id.ToString()); // Reemplaza idJuzgado con el valor real

                PDigitalizar.DataSource = datos;
                PDigitalizar.DataBind();

                ShowToastr($"Carpeta '' creada con éxito", "success");
            }

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            ElegirInicial elegirInicial = new ElegirInicial();
            elegirInicial.ElejirInicial(sender, e, PDigitalizar, descripNum, delitos, lblPartes, lblVictima, lblImputado, infoImputado, infoVictima, noDigit, lblInicialInfo, lblDocsNoDigit, lblinfo, lblAdjuntar, UploadFileDigit, btnDigitalizar);
        }


        protected void PDigitalizar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginacionInicial paginacion = new PaginacionInicial();
            paginacion.CambioIndicePagina(sender, e, PDigitalizar);
        }

        protected void btnDigitalizar_Click(object sender, EventArgs e)
        {
            try
            {
                string noDistrito = Session["IdDistrito"]?.ToString();
                string idJuzgado = Session["IdJuzgado"]?.ToString();
                string idAsunto = Session["IdAsunto"]?.ToString();
                string tipoAsunto = Session["TipoAsunto"]?.ToString();
                string folio = Session["Folio"]?.ToString();

                // Crear las carpetas
                string[] carpetas = { noDistrito, idJuzgado, tipoAsunto, idAsunto };
                string rutaDestino = "";

                foreach (string carpeta in carpetas)
                {
                    rutaDestino = Path.Combine(rutaDestino, carpeta);
                    bool carpetaExistente = ArchivosFTP.VerificarDirectorioFTP(rutaDestino);

                    if (!carpetaExistente)
                    {
                        bool isCreated = ArchivosFTP.CrearDirectorioFTP(rutaDestino);

                        if (isCreated)
                        {
                            ShowToastr($"Carpeta '{rutaDestino}' creada con éxito", "success");
                        }
                        else
                        {
                            ShowToastr($"Error al crear la carpeta '{rutaDestino}'", "error");
                            return; // Si hay algún error al crear una carpeta, se detiene el proceso.
                        }
                    }
                    else
                    {
                        ShowToastr($"La carpeta '{rutaDestino}' ya existe", "info");
                    }
                }

                ShowToastr("Carpetas creadas con éxito", "success");

                // Subir el archivo PDF
                if (UploadFileDigit.HasFile)
                {
                    Stream fileStream = UploadFileDigit.FileContent;
                    string guid = Guid.NewGuid().ToString(); // Genera un GUID.
                    string nuevoNombre = $"I_{folio}_{DateTime.Now.ToString("yyyyMMdd")}.pdf"; // Agrega el GUID al nombre del archivo.
                    string fileName = Path.Combine(rutaDestino, nuevoNombre);

                    if (!ArchivosFTP.VerificarArchivoFTP(fileName))
                    {
                        bool isUploaded = ArchivosFTP.CrearArchivoPDF(fileStream, fileName);

                        if (isUploaded)
                        {
                            ShowToastr($"Archivo '{fileName}' subido con éxito", "success");
                        }
                        else
                        {
                            ShowToastr($"Error al subir el archivo '{fileName}'", "error");
                        }
                    }
                    else
                    {
                        ShowToastr($"El archivo '{fileName}' ya existe", "info");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowToastr($"Error inesperado: {ex.Message}", "error");
            }
        }

        private void ShowToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}');", true);
        }


    }
}
