using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class ElegirPosterior
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
                        dtNoDigit.Columns.Add("IdAsunto"); // Nueva columna
                        dtNoDigit.Columns.Add("IdAnexoC"); // Nueva columna

                        HashSet<string> victimas = new HashSet<string>();
                        HashSet<string> imputados = new HashSet<string>();
                        HashSet<string> descripcionesAnexos = new HashSet<string>();

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
                                string idAsuntos = dr["IdAsunto"].ToString();
                                string idAnexoC = dr["IdAnexoC"].ToString();

                                if (!descripcionesAnexos.Contains(descripcion))
                                {
                                    dtNoDigit.Rows.Add(descripcion, cantidad, digit, idAsuntos, idAnexoC);
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
                    }


                }
            }
        }
    }
}