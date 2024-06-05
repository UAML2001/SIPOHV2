using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class ElegirInicial
    {
        public void ElejirInicial(object sender, EventArgs e,
            GridView PDigitalizar,
            Label descripNum,
            Label delitos,
            Label lblPartes,
            Label lblVictima,
            Label lblImputado,
            GridView infoImputado,
            GridView infoVictima,
            GridView noDigit,
            Label lblInicialInfo,
            Label lblDocsNoDigit,
            Label lblinfo,
            Label lblAdjuntar,
            System.Web.UI.WebControls.FileUpload UploadFileDigit,
            Button btnDigitalizar,
            Button MostrarPort
           //Label PortadaInicial,
           //HtmlIframe VPPortada
           )
        {
            // Obtén el CheckBox que disparó el evento
            CheckBox chk = (CheckBox)sender;

            // Obtén la fila de GridView en la que se encuentra el CheckBox
            GridViewRow rows = (GridViewRow)chk.NamingContainer;

            // Encuentra los controles en la fila y obtén sus valores
            Label lblIdAsunto = (Label)rows.FindControl("lblIdAsunto");
            string idAsunto = lblIdAsunto.Text;

            // El valor de TipoAsunto se puede obtener directamente de la celda correspondiente
            string tipoAsunto = rows.Cells[3].Text;

            string folio = rows.Cells[2].Text;
            folio = folio.Replace("/", "_");

            // Ahora puedes usar estos valores donde los necesites
            // Por ejemplo, podrías almacenarlos en variables de sesión
            HttpContext.Current.Session["IdAsunto"] = idAsunto;
            HttpContext.Current.Session["TipoAsunto"] = tipoAsunto;
            HttpContext.Current.Session["Folio"] = folio;

            int IdJuzgado = new GenerarIdJuzgadoPorSesion().ObtenerIdJuzgadoDesdeSesion();
            int IdAsunto = int.Parse(((Label)((GridViewRow)((CheckBox)sender).NamingContainer).FindControl("lblIdAsunto")).Text);


            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ConsultaAlSeleccionar", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = IdAsunto;
                    cmd.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = IdJuzgado;

                    conn.Open();
                    if (((CheckBox)sender).Checked)
                    {
                        foreach (GridViewRow row in PDigitalizar.Rows)
                        {
                            if (row != ((CheckBox)sender).NamingContainer)
                            {
                                ((CheckBox)row.FindControl("chkSelect")).Checked = false;
                            }
                            else
                            {
                                //ScriptManager.RegisterStartupScript(updPanel, updPanel.GetType(), "PreviewPDF", "PreviewPDF();", true);
                            }
                        }

                        DataTable dtVictimas = new DataTable();
                        dtVictimas.Columns.Add("TipoParte");
                        dtVictimas.Columns.Add("NombreCompleto");
                        dtVictimas.Columns.Add("Genero");

                        DataTable dtImputados = new DataTable();
                        dtImputados.Columns.Add("TipoParte");
                        dtImputados.Columns.Add("NombreCompleto");
                        dtImputados.Columns.Add("Genero");

                        DataTable dtNoDigit = new DataTable();
                        dtNoDigit.Columns.Add("Descripcion");
                        dtNoDigit.Columns.Add("Cantidad");
                        dtNoDigit.Columns.Add("Digitalizado");

                        HashSet<string> victimas = new HashSet<string>();
                        HashSet<string> imputados = new HashSet<string>();
                        HashSet<string> descripcionesAnexos = new HashSet<string>(); // Nuevo HashSet para descripciones de anexos

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                descripNum.Text = $"{((GridViewRow)((CheckBox)sender).NamingContainer).Cells[3].Text}: {((GridViewRow)((CheckBox)sender).NamingContainer).Cells[2].Text}";
                                delitos.Text = $"Delito(s): {dr["NombreDelito"]}";
                                lblPartes.Text = "Parte(s):";
                                lblVictima.Text = "Victima(s):";
                                lblImputado.Text = "Imputado(s):";

                                string tipoParte = dr["TipoParte"].ToString();
                                string nombreCompleto = dr["NombreCompleto"].ToString();

                                string descripcion = dr["Descripcion"].ToString();
                                string cantidad = dr["Cantidad"].ToString();
                                string digit = dr["Digitalizado"].ToString();

                                if (!descripcionesAnexos.Contains(descripcion))
                                {
                                    dtNoDigit.Rows.Add(descripcion, cantidad, digit);
                                    descripcionesAnexos.Add(descripcion);
                                }

                                if (tipoParte == "Victima" && !victimas.Contains(nombreCompleto))
                                {
                                    dtVictimas.Rows.Add(tipoParte, nombreCompleto, dr["Genero"]);
                                    victimas.Add(nombreCompleto);
                                }
                                else if (tipoParte == "Imputado" && !imputados.Contains(nombreCompleto))
                                {
                                    dtImputados.Rows.Add(tipoParte, nombreCompleto, dr["Genero"]);
                                    imputados.Add(nombreCompleto);
                                }
                            }
                        }

                        infoVictima.EmptyDataText = "No se encontraron víctimas.";
                        infoVictima.DataSource = dtVictimas.Rows.Count > 0 ? dtVictimas : null;
                        infoVictima.DataBind();

                        infoImputado.EmptyDataText = "No se encontraron imputados.";
                        infoImputado.DataSource = dtImputados.Rows.Count > 0 ? dtImputados : null;
                        infoImputado.DataBind();

                        infoImputado.EmptyDataText = "No se encontraron anexos.";
                        noDigit.DataSource = dtNoDigit.Rows.Count > 0 ? dtNoDigit : null;
                        noDigit.DataBind();

                        lblInicialInfo.Visible = true;
                        lblDocsNoDigit.Visible = true;
                        lblinfo.Visible = true;
                        lblAdjuntar.Visible = true;
                        UploadFileDigit.Visible = true;
                        btnDigitalizar.Visible = true;
                        MostrarPort.Visible = true;
                        //PortadaInicial.Visible = true;


                        //// Crea el comando para ejecutar el procedimiento almacenado
                        //using (SqlCommand comando = new SqlCommand("PortadaDigitalizacion", conn))
                        //{
                        //    comando.CommandType = CommandType.StoredProcedure;

                        //    // Configura los parámetros del procedimiento almacenado
                        //    comando.Parameters.Add("@IdAsunto", SqlDbType.VarChar).Value = IdAsunto;
                        //    comando.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = IdJuzgado;

                        //    // Crear un DataTable para almacenar los resultados
                        //    DataTable dt = new DataTable();

                        //    // Recoger los resultados
                        //    using (SqlDataReader reader = comando.ExecuteReader())
                        //    {
                        //        // Llenar el DataTable con los resultados
                        //        dt.Load(reader);
                        //    }

                        //    // Asegúrate de que los nombres de los campos coincidan con los de tu procedimiento almacenado
                        //    var valorAsunto = dt.Rows[0]["Asunto"].ToString();
                        //    var valorNoAsunto = dt.Rows[0]["Numero"].ToString();
                        //    var valorDelitos = dt.Rows[0]["Delitos"].ToString();
                        //    var valorImputados = dt.Rows[0]["Imputados"].ToString();
                        //    var valorVictimas = dt.Rows[0]["Victimas"].ToString();
                        //    var valorFojas = dt.Rows[0]["NumeroFojas"].ToString();

                        //    // Configura la ruta del informe Crystal Reports (.rpt)
                        //    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.rpt");

                        //    // Crea el informe
                        //    ReportDocument reporte = new ReportDocument();
                        //    reporte.Load(rutaInforme);

                        //    // Asignar el DataTable como fuente de datos del informe
                        //    reporte.SetDataSource(dt);

                        //    // Configura el formato de salida como PDF
                        //    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        //    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        //    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.pdf");
                        //    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                        //    // Exporta el informe a PDF
                        //    reporte.Export();

                        //    // Muestra el archivo PDF en el Panel            
                        //    VPPortada.Src = "~/Controllers/AC_Digitalizacion/PortadaDigitalizacion.pdf";
                        //    VPPortada.Visible = true;

                        //}
                    }
                    else
                    {
                        descripNum.Text = "";
                        delitos.Text = "";
                        lblPartes.Text = "";
                        lblVictima.Text = "";
                        lblImputado.Text = "";

                        infoVictima.EmptyDataText = "";
                        infoVictima.DataSource = null;
                        infoVictima.DataBind();

                        infoImputado.EmptyDataText = "";
                        infoImputado.DataSource = null;
                        infoImputado.DataBind();

                        noDigit.EmptyDataText = "";
                        noDigit.DataSource = null;
                        noDigit.DataBind();

                        lblInicialInfo.Visible = false;
                        lblDocsNoDigit.Visible = false;
                        lblinfo.Visible = false;
                        lblAdjuntar.Visible = false;
                        UploadFileDigit.Visible = false;
                        btnDigitalizar.Visible = false;
                        MostrarPort.Visible = false;
                        //PortadaInicial.Visible = false;
                    }


                }
            }
        }
    }
}