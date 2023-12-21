using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using WebGrease.Activities;
using System.Web.UI.HtmlControls;

namespace SIPOH.Views
{
    public partial class InicialBusPCausa : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tituloPartesCausa.Visible = false;
            tituloDetalles.Visible = false;
        }
        protected void btnBuscarPCausa_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = inputNombre.Value;
                string apellidoPaterno = inputApellidoPaterno.Value;
                string apellidoMaterno = inputApellidoMaterno.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", apellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", apellidoMaterno);
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 1);
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(
                         this.UpdateBusquedaPartesCausa,
                         this.UpdateBusquedaPartesCausa.GetType(),
                         "modalBuscarPartes",
                         "modalBuscarPartes();",
                         true
                     );

                    tituloPartesCausa.Visible = true;
                    GridViewPCausa.DataSource = dt;
                    GridViewPCausa.DataBind();
                    detallesConsulta.InnerHtml = "";
                    string mensajeExito = "Se encontraron resultados de tu consulta.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
                }
                else
                {
                    tituloPartesCausa.Visible = false;
                    tituloDetalles.Visible = false;
                    GridViewPCausa.DataSource = null;
                    GridViewPCausa.DataBind();
                    detallesConsulta.InnerHtml = "";
                    string mensajeNoDatos = "No se encontro resultado de la busqueda.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                }
            }
            catch (Exception ex)
            {
                tituloPartesCausa.Visible = false;
                tituloDetalles.Visible = false;
                detallesConsulta.InnerHtml = "";
                string mensajeError = "Error al realizar la búsqueda: " + ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", $"toastError('{mensajeError}');", true);
            }
        }
        protected void GridViewPCausa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int idAsunto = Convert.ToInt32(e.CommandArgument);
                VerDetalles(idAsunto);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            tituloPartesCausa.Visible = false;
            tituloDetalles.Visible = false;
            GridViewPCausa.DataSource = null;
            GridViewPCausa.DataBind();
            detallesConsulta.InnerHtml = "";
            string mensaje = "se ha cancelado la consulta";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            tituloPartesCausa.Visible = false;
            tituloDetalles.Visible = false;
            inputNombre.Value = "";
            inputApellidoPaterno.Value = "";
            inputApellidoMaterno.Value = "";
            GridViewPCausa.DataSource = null;
            GridViewPCausa.DataBind();
            detallesConsulta.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buacar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);

        }
        protected void GridViewPCausa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPCausa.PageIndex = e.NewPageIndex;
            BindDataToGridView();
        }

        private void BindDataToGridView()
        {
            try
            {
                string nombre = inputNombre.Value;
                string apellidoPaterno = inputApellidoPaterno.Value;
                string apellidoMaterno = inputApellidoMaterno.Value;

                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", apellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", apellidoMaterno);
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 1); // Asegúrate de establecer la opción correcta

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }
                tituloPartesCausa.Visible = true;
            
                GridViewPCausa.DataSource = dt;
                GridViewPCausa.DataBind();
            }
            catch (Exception ex)
            {
                // Maneja el error como consideres apropiado
            }
        }
        //termina busqueda de tabla
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
                            tituloPartesCausa.Visible = true;
                            tituloDetalles.Visible = true;
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
                            htmlTable.Append("<tr><td colspan='5'>No se encontraron detalles.</td></tr>");
                        }
                        htmlTable.Append("</table>");
                    }
                }
            }
            detallesConsulta.InnerHtml = htmlTable.ToString();
        }
        //funcion para diseño de la tabla
        protected void GridViewPCausa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.CssClass = "custom-gridview-header";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "text-secondary";
            }
        }
        //fin funcion diseño de la tabla
    }
}