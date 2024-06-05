using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
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
                DataTable datos = consultaCarga.ConsultaCargaDigitalizacion(id.ToString());

                PDigitalizar.EmptyDataText = "No se encontraron iniciales para digitalizar.";
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
            ElegirInicial elegirInicial = new ElegirInicial();
            elegirInicial.ElejirInicial(sender, e, PDigitalizar, descripNum, delitos, lblPartes, lblVictima, lblImputado, infoImputado, infoVictima, noDigit, lblInicialInfo, lblDocsNoDigit, lblinfo, lblAdjuntar, UploadFileDigit, btnDigitalizar, MostrarPort);
        }

        protected void PDigitalizar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginacionInicial paginacion = new PaginacionInicial();
            paginacion.CambioIndicePagina(sender, e, PDigitalizar);
        }


        protected void btnDigitalizar_Click(object sender, EventArgs e)
        {
            DigitalizarInicial digitalizador = new DigitalizarInicial
            {
                UploadFileDigit = this.UploadFileDigit,
                noDigit = this.noDigit,
                Page = this.Page,
            };

            digitalizador.btnDigitalizar_Click(sender, e);
        }


        protected void GenerarOtro_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void ShowToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}');", true);

        }

        protected void MostrarPort_Click(object sender, EventArgs e)
        {
            // Crea el comando para ejecutar el procedimiento almacenado
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand comando = new SqlCommand("PortadaDigitalizacion", conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    string idAsunto = HttpContext.Current.Session["IdAsunto"]?.ToString();
                    string idJuzgado = HttpContext.Current.Session["IdJuzgado"]?.ToString();

                    // Configura los parámetros del procedimiento almacenado
                    comando.Parameters.Add("@IdAsunto", SqlDbType.VarChar).Value = idAsunto;
                    comando.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = idJuzgado;

                    // Crear un DataTable para almacenar los resultados
                    DataTable dt = new DataTable();

                    // Recoger los resultados
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        // Llenar el DataTable con los resultados
                        dt.Load(reader);
                    }

                    // Asegúrate de que los nombres de los campos coincidan con los de tu procedimiento almacenado
                    var valorAsunto = dt.Rows[0]["Asunto"].ToString();
                    var valorNoAsunto = dt.Rows[0]["Numero"].ToString();
                    var valorDelitos = dt.Rows[0]["Delitos"].ToString();
                    var valorImputados = dt.Rows[0]["Imputados"].ToString();
                    var valorVictimas = dt.Rows[0]["Victimas"].ToString();
                    var valorFojas = dt.Rows[0]["NumeroFojas"].ToString();

                    // Configura la ruta del informe Crystal Reports (.rpt)
                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.rpt");

                    // Crea el informe
                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(rutaInforme);

                    // Asignar el DataTable como fuente de datos del informe
                    reporte.SetDataSource(dt);

                    // Configura el formato de salida como PDF
                    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    // Exporta el informe a PDF
                    reporte.Export();

                    // Llama al método para mostrar el PDF generado
                    portInicial.Src = "~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.pdf";
                    panelPdfPortadas.Style["display"] = "block";
                }
            }
        }
    }
}