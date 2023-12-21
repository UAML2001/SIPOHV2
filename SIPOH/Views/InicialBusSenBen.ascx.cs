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
    public partial class InicialBusSenBen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tituloPartesCausa2.Visible = false;
            tituloDetalles2.Visible = false;
        }

        protected void btnBuscarPCausa2_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreBeneficiario = inputNombreBeneficiario2.Value;
                string apellidoPaterno = inputApellidoPaterno2.Value;
                string apellidoMaterno = inputApellidoMaterno2.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", nombreBeneficiario);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", apellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", apellidoMaterno);
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 2); // Utilizando la opción 2
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    tituloPartesCausa2.Visible = true;
                    GridViewPCausa2.DataSource = dt;
                    GridViewPCausa2.DataBind();
                    detallesConsulta2.InnerHtml = "";
                    string mensajeExito = "Se encontraron resultados de tu consulta.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
                }
                else
                {
                    string mensajeNoDatos = "No se encontro resultado de la busqueda.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                }
            }
            catch (Exception ex)
            {
                string mensajeNoDatos = "No se pudo iniciar la busqueda de la consulta.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
            }
        }

      

        protected void btnLimpiar2_Click(object sender, EventArgs e)
        {
            tituloPartesCausa2.Visible = false;
            tituloDetalles2.Visible = false;
            inputNombreBeneficiario2.Value = "";
            inputApellidoPaterno2.Value = "";
            inputApellidoMaterno2.Value = "";
            GridViewPCausa2.DataSource = null;
            GridViewPCausa2.DataBind();
            detallesConsulta2.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }

        protected void GridViewPCausa2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int idAsunto = Convert.ToInt32(e.CommandArgument);
                VerDetalles(idAsunto); // Asegúrate de que este método esté adaptado para trabajar con los nuevos controles y lógica
            }
        }

        protected void GridViewPCausa2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Aplica estilos al encabezado si es necesario
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Aplica estilos a las filas de datos si es necesario
            }
        }

        protected void GridViewPCausa2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPCausa2.PageIndex = e.NewPageIndex;
            BindDataToGridView(); // Este método debería estar adaptado para recargar los datos en GridViewPCausa2
        }
        private void BindDataToGridView()
        {
            try
            {
                string nombreBeneficiario = inputNombreBeneficiario2.Value;
                string apellidoPaterno = inputApellidoPaterno2.Value;
                string apellidoMaterno = inputApellidoMaterno2.Value;
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", nombreBeneficiario);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", apellidoPaterno);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", apellidoMaterno);
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 2); // Asegúrate de que esta es la opción correcta para la consulta

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }

                tituloPartesCausa2.Visible = true;
                GridViewPCausa2.DataSource = dt;
                GridViewPCausa2.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar aquí cualquier excepción o error que ocurra
                // Por ejemplo, podrías mostrar un mensaje de error al usuario
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
                            tituloPartesCausa2.Visible = true;
                            tituloDetalles2.Visible = true;
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

            detallesConsulta2.InnerHtml = htmlTable.ToString(); // Asegúrate de que 'detallesConsulta2' sea el ID del div o control donde quieres mostrar los detalles
        }
        //
    }
}