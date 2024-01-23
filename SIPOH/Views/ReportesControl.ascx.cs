using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH.ReportesEjecucion;
using System.Configuration.Provider;
using CrystalDecisions.ReportAppServer;
using System.Globalization;
using System.Configuration;


namespace SIPOH.Views
{
    public partial class ReportesControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            fechas.Visible = false;
            RIniciales.Visible = false;
            RPromociones.Visible = false;
            //PDFViewer.Visible = false;
        }

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            fechas.Visible = ddlTipoReporte.SelectedValue == "F";
        }

        protected void ddlFormatoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormatoReporte.SelectedValue == "I")
            {
                if (ddlTipoReporte.SelectedValue == "F")
                {
                    fechas.Visible = true;
                }
                RIniciales.Visible = true;
                RPromociones.Visible = false;
            }
            else if (ddlFormatoReporte.SelectedValue == "P")
            {
                if (ddlTipoReporte.SelectedValue == "F")
                {
                    fechas.Visible = true;
                }
                RIniciales.Visible = false;
                RPromociones.Visible = true;
            }
            else
            {
                RIniciales.Visible = false;
                RPromociones.Visible = false;
            }
        }


        // Empieza el back del informe

        public static class StorageReporteInicial
        {
            public static DataTable EjecutarReporteInicial(SqlTransaction transaction, int idJuzgado, DateTime fechaInicial, DateTime fechaFinal)
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ReporteInicial", transaction.Connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = idJuzgado;
                    cmd.Parameters.Add("@FechaInicial", SqlDbType.DateTime).Value = fechaInicial;
                    cmd.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = fechaFinal;

                    // Ejecutar el procedimiento almacenado y capturar los resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;  // Devuelve los resultados como un DataTable
                    }
                }
            }
        }


        protected void btnMostrarInforme_Click(object sender, EventArgs e)
        {
            // Configura la ruta del informe Crystal Reports (.rpt)
            string rutaInforme = Server.MapPath("~/ReportesEjecucion/InicialEjecucion.rpt");

            // Crea el informe
            ReportDocument reporte = new ReportDocument();
            reporte.Load(rutaInforme);


            // Convertir las fechas a formato dd/mm/yyyy
            DateTime fechaDesdeDate;
            string fechaDesde;

            if (DateTime.TryParseExact(calFechaDesde.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaDesdeDate))
            {
                fechaDesde = fechaDesdeDate.ToString("dd/MM/yyyy");
            }
            else
            {
                // Manejar el caso en que la fecha no está en el formato esperado
                fechaDesde = "Formato de fecha no válido";
            }


            DateTime fechaHastaDate;
            string fechaHasta;

            if (DateTime.TryParseExact(calFechaHasta.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHastaDate))
            {
                fechaHasta = fechaHastaDate.ToString("dd/MM/yyyy");
            }
            else
            {
                // Manejar el caso en que la fecha no está en el formato esperado
                fechaHasta = "Formato de fecha no válido";
            }




            // Obtener el nombre del juzgado desde la sesión
            var nombreJuzgado = ObtenerNombreJuzgadoDesdeSesion();


            reporte.SetParameterValue("NombreJuzgado", nombreJuzgado);

            // Verificar el valor seleccionado en el DropDownList
            if (ddlTipoReporte.SelectedValue == "D")
            {
                // Si el valor es "D", establecer la fecha de hoy
                reporte.SetParameterValue("FechaDesde", DateTime.Now.ToString("d"));
                reporte.SetParameterValue("FechaHasta", DateTime.Now.ToString("d"));
            }
            else if (ddlTipoReporte.SelectedValue == "F")
            {
                // Configurar los valores de los campos de parámetros
                reporte.SetParameterValue("FechaDesde", fechaDesde);
                reporte.SetParameterValue("FechaHasta", fechaHasta);
            }
            else
            {
                // Si no se seleccionó ninguna opción, mostrar un mensaje de error con Toastr
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Error", "EjemploErrorFechaReporte()", true);
                return;
            }

            // Configura el formato de salida como PDF
            reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            string rutaArchivoPDF = Server.MapPath("~/ReportesEjecucion/InicialEjecucion.pdf");
            reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

            // Exporta el informe a PDF
            reporte.Export();

            // Muestra el archivo PDF en el iframe
            iframePDF.Src = "~/ReportesEjecucion/InicialEjecucion.pdf";
            iframePDF.Visible = true;
        }


        private string ObtenerNombreJuzgadoDesdeSesion()
        {
            // Asegúrate de que la clave de sesión para el nombre del juzgado sea la correcta
            if (HttpContext.Current.Session["NombreJuzgado"] != null)
            {
                return HttpContext.Current.Session["NombreJuzgado"].ToString();
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return "Nombre de juzgado no disponible"; // O un valor por defecto según tu lógica de negocio
        }

    }
}