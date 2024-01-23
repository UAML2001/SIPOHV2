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

        //protected void btnConsultarReporte_Click(object sender, EventArgs e)
        //{
        //    PDFViewer.Visible = true;
        //}

        protected void btnMostrarInforme_Click(object sender, EventArgs e)
        {

            // Configura la ruta del informe Crystal Reports (.rpt)
            string rutaInforme = Server.MapPath("~/ReportesEjecucion/InicialEjecucion.rpt");

            // Crea el informe
            ReportDocument reporte = new ReportDocument();
            reporte.Load(rutaInforme);

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

    }
}