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
using System.Configuration;


namespace SIPOH.Views
{
    public partial class ReportesControl : System.Web.UI.UserControl
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                ConfigureDropDownVisibility();
            }
        }

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            fechas.Visible = ddlTipoReporte.SelectedValue == "F";
        }

        protected void ddlFormatoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigureDropDownVisibility();
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
            try
            {

                // Inicializar fDesde y fHasta con un valor por defecto
                DateTime fDesde = DateTime.MinValue;
                DateTime fHasta = DateTime.MaxValue;

                // Recoger los valores de entrada
                string tipoReporte = ddlTipoReporte.SelectedValue;
                string formatoReporte = ddlFormatoReporte.SelectedValue;
                string opcionInicial = OpcionInicial.SelectedValue;
                string opcionPoste = OpcionPoste.SelectedValue;

                // Si no se ha seleccionado nada en el dropdown de Tipo de Reporte
                if (tipoReporte == "S")
                {
                    // Mostrar mensaje de error
                    Toastr("No se pudo generar el reporte, por favor seleccione el tipo de fecha del reporte", "error");
                    return;
                }

                // Si no se ha seleccionado nada en el dropdown de Formato de Reporte
                if (formatoReporte == "S")
                {
                    // Mostrar mensaje de error
                    Toastr("No se pudo generar el reporte, por favor seleccione el formato del reporte", "error");
                    return;
                }

                // Si se seleccionó "I" en el dropdown de Formato de Reporte y no se seleccionó nada en el dropdown de OpcionInicial
                if (formatoReporte == "I" && opcionInicial == "S")
                {
                    // Mostrar mensaje de error
                    Toastr("No se pudo generar el reporte, por favor seleccione el tipo de inicial", "error");
                    return;
                }

                // Si se seleccionó "P" en el dropdown de Formato de Reporte y no se seleccionó nada en el dropdown de OpcionPoste
                if (formatoReporte == "P" && opcionPoste == "S")
                {
                    // Mostrar mensaje de error
                    Toastr("No se pudo generar el reporte, por favor seleccione su tipo de posterior", "error");
                    return;
                }

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


                // Ajustar fechaDesde y fechaHasta para incluir todo el día
                fDesde = fDesde.Date;
                fHasta = fHasta.Date.AddHours(23).AddMinutes(59).AddSeconds(59);


                // Conectar con la base de datos
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Llamar al procedimiento almacenado
                    string storedProcedure = formatoReporte == "P" ? "Control_ReportePosterior" : "Control_ReporteInicial";
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Pasar los valores a los parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = ObtenerIdJuzgadoDesdeSesion();
                        cmd.Parameters.Add("@FechaInicial", SqlDbType.DateTime).Value = fDesde;
                        cmd.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = fHasta;
                        cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 2).Value = formatoReporte == "P" ? opcionPoste : opcionInicial;

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
                                var valorAsunto = dt.Rows[0]["Asunto"].ToString();
                                var valorNoAsunto = dt.Rows[0]["Numero"].ToString();
                                var valorFecha = dt.Rows[0]["FechaIngreso"].ToString();
                                var valorPromovente = dt.Rows[0]["Promovente"].ToString();
                                var valorTAnexos = dt.Rows[0]["Total"].ToString();
                            }
                            else
                            {
                                // Asegúrate de que los nombres de los campos coincidan con los de tu procedimiento almacenado
                                var valorAsunto = dt.Rows[0]["Asunto"].ToString();
                                var valorNoAsunto = dt.Rows[0]["Numero"].ToString();
                                var valorFecha = dt.Rows[0]["FeIngreso"].ToString();
                                var valorImputado = dt.Rows[0]["Imputado"].ToString();
                                var valorVictima = dt.Rows[0]["Victimas"].ToString();
                                var valorDelito = dt.Rows[0]["Delitos"].ToString();
                                var valorTAnexos = dt.Rows[0]["Total"].ToString();
                            }

                            // Configura la ruta del informe Crystal Reports (.rpt)
                            string rutaInforme = ddlFormatoReporte.SelectedValue == "P" ? Server.MapPath("~/Reportes/PosteriorControl.rpt") : Server.MapPath("~/Reportes/InicialControl.rpt");

                            // Crea el informe
                            ReportDocument reporte = new ReportDocument();
                            reporte.Load(rutaInforme);

                            // Asignar el DataTable como fuente de datos del informe
                            reporte.SetDataSource(dt);

                            // Configurar los parámetros del informe
                            reporte.SetParameterValue("NombreJuzgado", ObtenerNombreJuzgadoDesdeSesion());
                            reporte.SetParameterValue("FechaDesde", fDesde.ToString("d"));
                            reporte.SetParameterValue("FechaHasta", fHasta.ToString("d"));

                            // Configurar el formato de salida como PDF
                            reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            string rutaArchivoPDF = ddlFormatoReporte.SelectedValue == "P" ? Server.MapPath("~/Reportes/PosteriorControl.pdf") : Server.MapPath("~/Reportes/InicialControl.pdf");
                            reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                            // Exportar el informe a PDF
                            reporte.Export();

                            // Mostrar el archivo PDF en el iframe
                            iframePDF.Src = ddlFormatoReporte.SelectedValue == "P" ? "~/Reportes/PosteriorControl.pdf" : "~/Reportes/InicialControl.pdf";
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
                //Toastr("No se pudo generar el reporte, por favor seleccione el juzgado del reporte", "error");
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


        private int ObtenerIdCircuitoDesdeSesion()
        {
            int IdCircuito = 0;
            if (HttpContext.Current.Session["IdCircuito"] != null)
            {
                IdCircuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            }
            return IdCircuito;
        }

        private void ConfigureDropDownVisibility()
        {
            // Ocultar ambos dropdowns secundarios al principio
            divOpcionInicial.Visible = false;
            divOpcionPoste.Visible = false;

            // Obtener el valor seleccionado en el dropdown principal
            string formatoSeleccionado = ddlFormatoReporte.SelectedValue;

            // Configurar la visibilidad del dropdown secundario correspondiente
            if (formatoSeleccionado == "I")
            {
                divOpcionInicial.Visible = true;
                divOpcionPoste.Visible = false;
            }
            else if (formatoSeleccionado == "P")
            {
                divOpcionInicial.Visible = false;
                divOpcionPoste.Visible = true;
            }
            else
            {
                // Ocultar ambos dropdowns si no se selecciona un formato específico
                divOpcionInicial.Visible = false;
                divOpcionPoste.Visible = false;
            }
        }


        private int ObtenerIdJuzgadoDesdeSesion()
        {
            int IdJuzgado = 0;
            if (HttpContext.Current.Session["IdJuzgado"] != null)
            {
                IdJuzgado = Convert.ToInt32(HttpContext.Current.Session["IdJuzgado"]);
            }
            return IdJuzgado;
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