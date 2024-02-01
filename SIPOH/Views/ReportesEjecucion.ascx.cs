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
using SIPOH.Reportes;
using System.Configuration.Provider;
using CrystalDecisions.ReportAppServer;
using System.Globalization;
using System.Web.DynamicData;
using System.Configuration;

namespace SIPOH.Views
{
    public partial class ReportesEjecucion : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JuzgadoPorCircuito();
                // Si el usuario ha seleccionado un valor en el dropdown de intervalo de fecha
                if (ddlTipoReporte.SelectedValue != "S")
                {
                    // Hacer visible el input de fechas
                    fechas.Visible = true;
                }
                else
                {
                    // Si no se ha seleccionado un valor, ocultar el input de fechas
                    fechas.Visible = false;
                }
            }
        }

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            fechas.Visible = ddlTipoReporte.SelectedValue == "F";
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

        //protected void botonmostrarpromocion(object sender, EventArgs e)
        //{
        //    // Configura la ruta del informe Crystal Reports (.rpt)
        //    string rutaInforme = Server.MapPath("~/ReportesEjecucion/PromocionEjecucion.rpt");

        //    // Crea el informe
        //    ReportDocument reporte = new ReportDocument();
        //    reporte.Load(rutaInforme);

        //    // Asignar el DataTable como fuente de datos del informe

        //    // Configurar el formato de salida como PDF
        //    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //    string rutaArchivoPDF = Server.MapPath("~/ReportesEjecucion/PromocionEjecucion.pdf");
        //    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

        //    // Exportar el informe a PDF
        //    reporte.Export();

        //    // Mostrar el archivo PDF en el iframe
        //    iframePDF.Src = "~/ReportesEjecucion/PromocionEjecucion.pdf";
        //    iframePDF.Visible = true;
        //    GenerarOtro.Visible = true;
        //    TituloReporte.Visible = true;
        //    btnMostrarInforme.Enabled = false;
        //    // No hay datos para mostrar, mostrar mensaje Toastr
        //    Toastr("Reporte generado con exito", "success");
        //}

        protected void btnMostrarInforme_Click(object sender, EventArgs e)
        {
            try
            {
                // Recoger los valores de entrada
                string IdJuzgado = JuzgadoEjec.SelectedValue;
                string NombreJuzgado = JuzgadoEjec.SelectedItem.Text;

                string tipoReporte = ddlTipoReporte.SelectedValue;

                // Inicializar fDesde y fHasta con un valor por defecto
                DateTime fDesde = DateTime.MinValue;
                DateTime fHasta = DateTime.MaxValue;

                // Si no se ha seleccionado nada en el dropdown
                if (ddlTipoReporte.SelectedValue == "S")
                {
                    // Mostrar mensaje de error
                    Toastr("No se pudo generar el reporte, por favor seleccione el tipo de fecha del reporte", "error");
                }

                // Si no se ha seleccionado nada en el dropdown
                if (ddlFormatoReporte.SelectedValue == "S")
                {
                    // Mostrar mensaje de error
                    Toastr("No se pudo generar el reporte, por favor seleccione el formato del reporte", "error");
                }
                else
                {
                    // Si el usuario selecciona "Día" en ddlTipoReporte
                    if (tipoReporte == "D")
                    {
                        // Establecer fechaDesde y fechaHasta a la fecha de hoy
                        fDesde = DateTime.Today;
                        fHasta = DateTime.Today;
                    }
                    else if (tipoReporte == "F")
                    {
                        // Verificar si las fechas están presentes
                        if (!string.IsNullOrEmpty(calFechaDesde.Text) && !string.IsNullOrEmpty(calFechaHasta.Text))
                        {
                            fDesde = DateTime.Parse(calFechaDesde.Text);
                            fHasta = DateTime.Parse(calFechaHasta.Text);
                            fechas.Visible = true;
                        }
                        else
                        {
                            Toastr("Por favor, seleccione las fechas", "error");
                            fechas.Visible = true;
                            return;
                        }
                    }
                }


                // Ajustar fechaDesde y fechaHasta para incluir todo el día
                fDesde = fDesde.Date;
                fHasta = fHasta.Date.AddHours(23).AddMinutes(59).AddSeconds(59);




                // Conectar con la base de datos
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Llamar al procedimiento almacenado
                    string storedProcedure = ddlFormatoReporte.SelectedValue == "P" ? "Ejecucion_ReportePromociones" : "Ejecucion_ReporteInicial";
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Pasar los valores a los parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = IdJuzgado;
                        cmd.Parameters.Add("@FechaInicial", SqlDbType.DateTime).Value = fDesde;
                        cmd.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = fHasta;

                        con.Open();

                        // Crear un DataTable para almacenar los resultados
                        DataTable dt = new DataTable();

                        // Recoger los resultados
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Llenar el DataTable con los resultados
                            dt.Load(reader);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            if (ddlFormatoReporte.SelectedValue == "P")
                            {
                                // Asegúrate de que los nombres de los campos coincidan con los de tu procedimiento almacenado
                                var valorNoEjecucion = dt.Rows[0]["NoEjecucion"].ToString();
                                var valorFechaIngreso = dt.Rows[0]["FechaIngreso"].ToString();
                                var valorPromovente = dt.Rows[0]["Promovente"].ToString();
                                var valorSolicitud = dt.Rows[0]["Solicitud"].ToString();
                                var valorNoAnexo = dt.Rows[0]["NoAnexo"].ToString();
                            }
                            else
                            {
                                // Asegúrate de que los nombres de los campos coincidan con los de tu procedimiento almacenado
                                var valorNoEjecucion = dt.Rows[0]["NoEjecucion"].ToString();
                                var valorDetalleSolicitante = dt.Rows[0]["DetalleSolicitante"].ToString();
                                var valorFechaEjeucion = dt.Rows[0]["FechaEjecucion"].ToString();
                                var valorSolicitud = dt.Rows[0]["Solicitud"].ToString();
                                var valorBeneficiario = dt.Rows[0]["Beneficiario"].ToString();
                                var valorCausa = dt.Rows[0]["Causa"].ToString();
                                var valorNUC = dt.Rows[0]["NUC"].ToString();
                                var valorJuzgado = dt.Rows[0]["Juzgado"].ToString();
                            }

                            // Configura la ruta del informe Crystal Reports (.rpt)
                            string rutaInforme = ddlFormatoReporte.SelectedValue == "P" ? Server.MapPath("~/Reportes/PromocionEjecucion.rpt") : Server.MapPath("~/Reportes/InicialEjecucion.rpt");

                            // Crea el informe
                            ReportDocument reporte = new ReportDocument();
                            reporte.Load(rutaInforme);

                            // Asignar el DataTable como fuente de datos del informe
                            reporte.SetDataSource(dt);

                            // Configurar los parámetros del informe
                            reporte.SetParameterValue("NombreJuzgado", NombreJuzgado);
                            reporte.SetParameterValue("FechaDesde", fDesde.ToString("d"));
                            reporte.SetParameterValue("FechaHasta", fHasta.ToString("d"));

                            // Configurar el formato de salida como PDF
                            reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            string rutaArchivoPDF = ddlFormatoReporte.SelectedValue == "P" ? Server.MapPath("~/Reportes/PromocionEjecucion.pdf") : Server.MapPath("~/Reportes/InicialEjecucion.pdf");
                            reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                            // Exportar el informe a PDF
                            reporte.Export();

                            // Mostrar el archivo PDF en el iframe
                            iframePDF.Src = ddlFormatoReporte.SelectedValue == "P" ? "~/Reportes/PromocionEjecucion.pdf" : "~/Reportes/InicialEjecucion.pdf";
                            iframePDF.Visible = true;
                            GenerarOtro.Visible = true;
                            TituloReporte.Visible = true;
                            btnMostrarInforme.Enabled = false;
                            // No hay datos para mostrar, mostrar mensaje Toastr
                            Toastr("Reporte generado con exito", "success");
                        }
                        else
                        {
                            // No hay datos para mostrar, mostrar mensaje Toastr
                            Toastr("No hay datos disponibles para el rango de fechas seleccionado.", "info");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones y mostrar mensaje Toastr
                Toastr("Error al generar el informe: " + ex.Message, "error");
            }
        }

        private void Toastr(string mensaje, string tipo)
        {
            // Agregar referencia a Toastr en tu página ASPX o MasterPage
            // Ejemplo: <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
            //          <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

            // Mostrar mensaje Toastr
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "toastr", $"toastr.{tipo}('{mensaje}');", true);
        }

        protected void GenerarOtro_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }


        private void LimpiarFormulario()
        {
            // Limpia los campos de tu formulario
            ddlTipoReporte.SelectedValue = "S";
            ddlFormatoReporte.SelectedValue = "S";
            JuzgadoEjec.SelectedValue = "S";
            calFechaDesde.Text = null;
            calFechaHasta.Text = null;
            fechas.Visible = false;
            // Agrega más campos según sea necesario...
        }


        private void JuzgadoPorCircuito()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosPorCircuitoE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Aquí asumimos que tienes un método para obtener el IdCircuito de la sesión
                    int idCircuito = ObtenerIdCircuitoDesdeSesion();
                    cmd.Parameters.AddWithValue("@IdCircuito", idCircuito);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        JuzgadoEjec.DataSource = reader;
                        JuzgadoEjec.DataTextField = "Nombre";  // Reemplaza con el nombre de tu campo
                        JuzgadoEjec.DataValueField = "IdJuzgado";  // Reemplaza con el valor de tu campo
                        JuzgadoEjec.DataBind();
                    }
                }
            }

            // Agrega el elemento inicial al DropDownList
            ListItem itemInicial = new ListItem("Seleccione el juzgado del reporte...", "");
            JuzgadoEjec.Items.Insert(0, itemInicial);
        }


        private int ObtenerIdCircuitoDesdeSesion()
        {
            int IdCircuito = 0;
            if (HttpContext.Current.Session["IdCircuito"] != null)
            {
                IdCircuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            }
            return IdCircuito;
        }

    }

}