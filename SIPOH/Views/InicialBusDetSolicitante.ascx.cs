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
    public partial class InicialBusDetSolicitante : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tituloPartesCausa6.Visible = false;
            tituloDetalles6.Visible = false;
        }
        protected void btnBuscarPCausa6_Click(object sender, EventArgs e)
        {
            try
            {
                string detalleSolicitante = inputDetalleSolicitante6.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", detalleSolicitante);
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 6);
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    tituloPartesCausa6.Visible = true;
                    GridViewPCausa6.DataSource = dt;
                    GridViewPCausa6.DataBind();
                    detallesConsulta6.InnerHtml = "";
                    string mensajeExito = "Se encontraron resultados de tu consulta por detalle de solicitante.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
                }
                else
                {
                    string mensajeNoDatos = "No se encontro resultado de la busqueda.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                    tituloPartesCausa6.Visible = false;
                    tituloDetalles6.Visible = false;
                    inputDetalleSolicitante6.Value = "";
                    GridViewPCausa6.DataSource = null;
                    GridViewPCausa6.DataBind();
                    detallesConsulta6.InnerHtml = "";
                }
            }
            catch (Exception ex)
            {
                string mensajeNoDatos = "No se pudo realizar la busqueda de tu consulta.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
            }
        }


        protected void VerDetalles(int IdEjecucion)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            StringBuilder htmlTable = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_MostrarCausasRelacionadas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEjecucion", IdEjecucion);

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
                            tituloPartesCausa6.Visible = true;
                            tituloDetalles6.Visible = true;
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

            detallesConsulta6.InnerHtml = htmlTable.ToString();
        }
     
        protected void GridViewPCausa6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int IdEjecucion = Convert.ToInt32(e.CommandArgument);
                VerDetalles(IdEjecucion);
            }
        }

        protected void btnLimpiar6_Click(object sender, EventArgs e)
        {
            tituloPartesCausa6.Visible = false;
            tituloDetalles6.Visible = false;
            inputDetalleSolicitante6.Value = "";
            GridViewPCausa6.DataSource = null;
            GridViewPCausa6.DataBind();
            detallesConsulta6.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        protected void GridViewPCausa6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPCausa6.PageIndex = e.NewPageIndex;
            BindDataToGridView(); // Asegúrate de que este método esté adaptado para recargar los datos en GridViewPCausa6
        }

        protected void GridViewPCausa6_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Lógica para RowDataBound si es necesario
        }
        private void BindDataToGridView()
        {
            try
            {
                string detalleSolicitante = inputDetalleSolicitante6.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", detalleSolicitante); // Asegúrate de que el nombre del parámetro coincida con el procedimiento almacenado
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 6); // Utilizando la opción 6

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }

                GridViewPCausa6.DataSource = dt;
                GridViewPCausa6.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar aquí cualquier excepción o error que ocurra
                // Por ejemplo, podrías mostrar un mensaje de error al usuario
            }
        }



    }
}