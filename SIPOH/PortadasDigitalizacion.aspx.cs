using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH.Controllers.EJ_Storages;

namespace SIPOH
{
    public partial class PortadasDigitalizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (TAsunto.SelectedItem == null || TAsunto.SelectedItem.Value == "SO" || string.IsNullOrWhiteSpace(numexpe.Text))
            {
                MostrarMensajeToastr("Por favor, selecciona un asunto válido y proporciona un número de expediente.", "error");
                return;
            }

            GenerarIdJuzgadoPorSesion id = new GenerarIdJuzgadoPorSesion();
            List<string> listaImputados = new List<string>(), listaVictimas = new List<string>();
            bool registrosEncontrados = false;

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ConsultarPortadas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TipoAsunto", TAsunto.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@Numero", numexpe.Text));
                    cmd.Parameters.Add(new SqlParameter("@IdJuzgado", id.ObtenerIdJuzgadoDesdeSesion()));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                descripNum.Text = reader["Numero"].ToString();
                                delitos.Text = reader["Delitos"].ToString();
                                listaImputados.AddRange(reader["Imputados"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(imputado => imputado.Trim()));
                                listaVictimas.AddRange(reader["Victimas"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(victima => victima.Trim()));
                                registrosEncontrados = true;
                            }
                        }
                        else
                        {
                            OcultarControles();
                            MostrarMensajeToastr("No se encontraron registros que coincidan con la búsqueda.", "warning");
                            return;
                        }
                    }
                }
            }

            if (registrosEncontrados)
            {
                infoImputado.DataSource = ConvertListToDataTable(listaImputados, "Imputados");
                infoImputado.DataBind();
                infoVictima.DataSource = ConvertListToDataTable(listaVictimas, "Victimas");
                infoVictima.DataBind();
                MostrarControles();
                MostrarMensajeToastr("Búsqueda completada con éxito.", "success");
            }
        }

        private void OcultarControles()
        {
            lblInicialInfo.Visible = false;
            descripNum.Visible = false;
            lblAsunto.Visible = false;
            delitos.Visible = false;
            lblDeli.Visible = false;
            lblPartes.Visible = false;
            lblVictima.Visible = false;
            infoVictima.Visible = false;
            lblImputado.Visible = false;
            infoImputado.Visible = false;
            ImpPortada.Visible = false;
            VPPortada.Visible = false;
        }

        private void MostrarControles()
        {
            lblInicialInfo.Visible = true;
            descripNum.Visible = true;
            lblAsunto.Visible = true;
            delitos.Visible = true;
            lblDeli.Visible = true;
            lblPartes.Visible = true;
            lblVictima.Visible = true;
            infoVictima.Visible = true;
            lblImputado.Visible = true;
            infoImputado.Visible = true;
            ImpPortada.Visible = true;
        }

        private DataTable ConvertListToDataTable(List<string> list, string columnName)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(columnName, typeof(string));
            foreach (string item in list)
            {
                DataRow row = dataTable.NewRow();
                row[columnName] = item;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        private void MostrarMensajeToastr(string mensaje, string tipo)
        {
            string script = $"toastr.{tipo}('{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage", script, true);
        }

        protected void ImpPortada_Click(object sender, EventArgs e)
        {
            // Instancia para obtener el ID del juzgado desde la sesión
            GenerarIdJuzgadoPorSesion id = new GenerarIdJuzgadoPorSesion();

            // Configura la conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                // Abre la conexión
                conn.Open();

                // Crea el comando para ejecutar el procedimiento almacenado
                using (SqlCommand comando = new SqlCommand("ConsultarPortadas", conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Configura los parámetros del procedimiento almacenado
                    comando.Parameters.Add("@TipoAsunto", SqlDbType.VarChar).Value = TAsunto.SelectedValue;
                    comando.Parameters.Add("@Numero", SqlDbType.VarChar).Value = numexpe.Text;
                    comando.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = id.ObtenerIdJuzgadoDesdeSesion();

                    // Crear un DataTable para almacenar los resultados
                    DataTable dt = new DataTable();

                    // Recoger los resultados
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        // Llenar el DataTable con los resultados
                        dt.Load(reader);
                    }

                    // Validar que el DataTable no esté vacío
                    if (dt.Rows.Count == 0)
                    {
                        // Mostrar una notificación Toastr y salir del método
                        ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", "toastr.error('No se encontraron datos para generar la portada.');", true);
                        return;
                    }

                    // Asegúrate de que los nombres de los campos coincidan con los de tu procedimiento almacenado
                    var valorAsunto = dt.Rows[0]["Asunto"].ToString();
                    var valorNoAsunto = dt.Rows[0]["Numero"].ToString();
                    var valorDelitos = dt.Rows[0]["Delitos"].ToString();
                    var valorImputados = dt.Rows[0]["Imputados"].ToString();
                    var valorVictimas = dt.Rows[0]["Victimas"].ToString();
                    var valorFojas = dt.Rows[0]["NumeroFojas"].ToString();

                    // Configura la ruta del informe Crystal Reports (.rpt)
                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/ConsultaPortada.rpt");

                    // Crea el informe
                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(rutaInforme);

                    // Asignar el DataTable como fuente de datos del informe
                    reporte.SetDataSource(dt);

                    // Configura el formato de salida como PDF
                    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/ConsultaPortada.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    // Exporta el informe a PDF
                    reporte.Export();

                    // Muestra el archivo PDF en el Panel            
                    VPPortada.Src = "~/Controllers/AC_Digitalizacion/ConsultaPortada.pdf";
                    VPPortada.Visible = true;

                    // Mostrar una notificación Toastr de éxito
                    ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", "toastr.success('La portada se generó correctamente.');", true);
                }
            }
        }


    }
}
