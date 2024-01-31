using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SIPOH.Views
{
    public partial class InicialBusSolicitante : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPartesCausa5.Visible = false;
                tituloDetalles5.Visible = false;
                CargarSolicitantes();
            }
        }

        private void CargarSolicitantes()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Solicitante, CveSolicitante FROM P_EjecucionCatSolicitante ORDER BY CASE WHEN Solicitante = 'OTRO' THEN 1 ELSE 0 END, Solicitante", con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Solicitante"].ToString();
                        listItem.Value = dr["CveSolicitante"].ToString(); // Aquí asignas la clave como el valor.
                        selectDetalleSolicitante5.Items.Add(listItem); // Asegúrate de que este sea el ID correcto del dropdown.
                    }
                }
            }
        }


        protected void btnBuscarPCausa5_Click(object sender, EventArgs e)
        {
            try
            {
                
                string claveSolicitante = selectDetalleSolicitante5.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Solicitante", claveSolicitante); // Usa la clave del solicitante.
                int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                cmd.Parameters.AddWithValue("@opcion", 5); // Asegúrate de que la opción sea la correcta.
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    tituloPartesCausa5.Visible = true;
                    GridViewPCausa5.DataSource = dt;
                    GridViewPCausa5.DataBind();
                    detallesConsulta5.InnerHtml = "";
                    string mensajeExito = "Se encontraron resultados de tu consulta por detalle de solicitante.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
                }
                else
                {
                    tituloPartesCausa5.Visible = false;
                    tituloDetalles5.Visible = false;
                    GridViewPCausa5.DataSource = null;
                    GridViewPCausa5.DataBind();
                    detallesConsulta5.InnerHtml = "";
                    string mensajeNoDatos = "No se encontro resultado de la busqueda.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                }
            }
            catch (Exception ex)
            {
                string mensajeNoDatos = "No se pudo realizar la busqueda de tu consulta.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
            }
        }
        protected void VerDetalles(int idAsunto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            StringBuilder htmlTable = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultasDetalle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        htmlTable.Append("<table class='table table-sm table-striped table-hover mb-0'>");
                        htmlTable.Append("<thead>");
                        htmlTable.Append("<tr class='text-center bg-primary text-white'>");
                        htmlTable.Append("<th class='bg-success text-white'>Causa</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Juzgado</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Ofendidos</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Inculpados</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Delitos</th>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</thead>");
                        htmlTable.Append("<tbody>");

                        if (dr.HasRows)
                        {
                            tituloPartesCausa5.Visible = true;
                            tituloDetalles5.Visible = true;
                            while (dr.Read())
                            {
                                htmlTable.Append("<tr>");
                                htmlTable.Append($"<td class='text-dark'>{dr["Numero"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Juzgado"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Ofendidos"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Inculpados"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Delitos"]}</td>");
                                htmlTable.Append("</tr>");
                            }
                        }
                        else
                        {
                            htmlTable.Append("<tr><td colspan='2'>No se encontraron detalles.</td></tr>");
                        }

                        htmlTable.Append("</tbody>");
                        htmlTable.Append("</table>");
                    }
                }
            }

            detallesConsulta5.InnerHtml = htmlTable.ToString();
        }
        protected void btnLimpiar5_Click(object sender, EventArgs e)
        {
           tituloPartesCausa5.Visible = false;
           tituloDetalles5.Visible = false;
           GridViewPCausa5.DataSource = null;
           GridViewPCausa5.DataBind();
           detallesConsulta5.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        protected void GridViewPCausa5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int idAsunto = Convert.ToInt32(e.CommandArgument);
                VerDetalles(idAsunto);
            }
        }
  
        protected void GridViewPCausa5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPCausa5.PageIndex = e.NewPageIndex;
            string claveSolicitante = selectDetalleSolicitante5.Value;
            int circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            BindDataToGridView(claveSolicitante, circuito, 5);
        }

        protected void GridViewPCausa5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Lógica para RowDataBound si es necesario
        }
        private void BindDataToGridView(string claveSolicitante, int circuito, int opcion)
        {
            try
            {
                string detalleSolicitante = selectDetalleSolicitante5.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Solicitante", claveSolicitante);
                        cmd.Parameters.AddWithValue("@idCircuito", circuito);
                        cmd.Parameters.AddWithValue("@opcion", 5);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }

                GridViewPCausa5.DataSource = dt;
                GridViewPCausa5.DataBind();
            }
            catch (Exception)
            {
                // Manejar aquí cualquier excepción o error que ocurra
                // Por ejemplo, podrías mostrar un mensaje de error al usuario
            }
        }

        //
    }
}