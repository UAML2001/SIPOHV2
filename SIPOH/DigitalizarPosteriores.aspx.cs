using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class DigitalizarPosteriores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarIdJuzgadoPorSesion idJuzgado = new GenerarIdJuzgadoPorSesion();
                int id = idJuzgado.ObtenerIdJuzgadoDesdeSesion();

                ConsultaCargaPosterior consultaCarga = new ConsultaCargaPosterior();
                DataTable datos = consultaCarga.ConsultaCargaDigitalizacion(id.ToString());

                PDigitalizar.EmptyDataText = "No se encontraron posteriores para digitalizar.";
                PDigitalizar.DataSource = datos.Rows.Count > 0 ? datos : null;
                PDigitalizar.DataBind();

                if (Session["ToastrMessage"] != null && Session["ToastrType"] != null)
                {
                    ShowToastr((string)Session["ToastrMessage"], (string)Session["ToastrType"]);
                    Session["ToastrMessage"] = null;
                    Session["ToastrType"] = null;
                }
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            // Encuentra la fila del GridView que contiene el CheckBox que activó el evento
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;

            // Encuentra el Label lblIdPosterior en la fila y obtiene su valor
            Label lblIdPosterior = (Label)row.FindControl("lblIdPosterior");
            string idPosteriorValue = lblIdPosterior.Text;

            // Almacena el valor de IdPosterior en la sesión
            Session["IdPosteriorSeleccionado"] = idPosteriorValue;

            // Llama al método ElejirInicial pasando el valor de IdPosterior
            ElegirPosterior elegirposterior = new ElegirPosterior();
            elegirposterior.ElejirInicial(sender, e, PDigitalizar, descripNum, delitos, lblPartes, lblVictima, lblImputado, infoImputado, infoVictima, noDigit, lblInicialInfo, lblDocsNoDigit, lblinfo, lblAdjuntar, UploadFileDigit, btnDigitalizar, MostrarPort);
        }


        protected void PDigitalizar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginacionPosterior paginacion = new PaginacionPosterior();
            paginacion.CambioIndicePagina(sender, e, PDigitalizar);
        }

        protected void btnDigitalizar_Click(object sender, EventArgs e)
        {
            // Recupera el valor de IdPosterior de la sesión
            string idPosteriorSeleccionado = Session["IdPosteriorSeleccionado"]?.ToString();

            // Verifica si el valor está presente y conviértelo a entero
            if (int.TryParse(idPosteriorSeleccionado, out int idPosterior))
            {
                DigitalizarPosterior digitalizador = new DigitalizarPosterior
                {
                    UploadFileDigit = this.UploadFileDigit,
                    noDigit = this.noDigit,
                    Page = this.Page
                };

                // Llama al método btnDigitalizar_Click de DigitalizarPosterior, pasando el valor de IdPosterior
                digitalizador.btnDigitalizar_Click(sender, e, idPosterior);
            }
            else
            {
                ShowToastr("Error: No se pudo obtener el IdPosterior de la sesión.", "error");
            }
        }


        private void ShowToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}');", true);
        }

        protected void MostrarPort_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand comando = new SqlCommand("PortadaDigitalizacion", conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    string idAsunto = HttpContext.Current.Session["IdAsunto"]?.ToString();
                    string idJuzgado = HttpContext.Current.Session["IdJuzgado"]?.ToString();

                    comando.Parameters.Add("@IdAsunto", SqlDbType.VarChar).Value = idAsunto;
                    comando.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = idJuzgado;

                    DataTable dt = new DataTable();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    var valorAsunto = dt.Rows[0]["Asunto"].ToString();
                    var valorNoAsunto = dt.Rows[0]["Numero"].ToString();
                    var valorDelitos = dt.Rows[0]["Delitos"].ToString();
                    var valorImputados = dt.Rows[0]["Imputados"].ToString();
                    var valorVictimas = dt.Rows[0]["Victimas"].ToString();
                    var valorFojas = dt.Rows[0]["NumeroFojas"].ToString();

                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.rpt");

                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(rutaInforme);

                    reporte.SetDataSource(dt);

                    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    reporte.Export();

                    portInicial.Src = "~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.pdf";
                    panelPdfPortadas.Style["display"] = "block";
                }
            }
        }
    }
}