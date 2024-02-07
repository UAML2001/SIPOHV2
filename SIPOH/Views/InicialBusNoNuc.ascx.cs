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
    public partial class InicialBusNoNuc : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarJuzgados();
                tituloPartesCausa4.Visible = false;
                tituloDetalles4.Visible = false;
            }
        }
        private void CargarJuzgados()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int idDistrito = 11;

                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Juzgados", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDistrito", idDistrito);
                    cmd.Parameters.AddWithValue("@Opcion", 1);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Nombre"].ToString();
                        listItem.Value = dr["IdJuzgado"].ToString();
                        InputDistritoProcedencia.Items.Add(listItem);
                    }
                }
            }
        }
        protected void btnBuscarPCausa4_Click(object sender, EventArgs e)
        {
            string nuc = inputNucBusqueda.Value;
            string idJuzgado = InputDistritoProcedencia.Value;

            if (idJuzgado == "Seleccionar") // Reemplaza "ValorPorDefecto" con el valor real que representa la selección por defecto
            {
                string mensajeError = "Por favor, selecciona un juzgado válido.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", $"toastError('{mensajeError}');", true);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NUC", nuc);
                    cmd.Parameters.AddWithValue("@idJuzgado", idJuzgado);
                    int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                    cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                    cmd.Parameters.AddWithValue("@opcion", 4);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }

            GridViewPCausa4.DataSource = dt;
            GridViewPCausa4.DataBind();

            // Mostrar los títulos si hay datos
            if (dt.Rows.Count > 0)
            {
                tituloPartesCausa4.Visible = true;
                GridViewPCausa4.DataSource = dt;
                GridViewPCausa4.DataBind();
                detallesConsulta4.InnerHtml = "";
                string mensajeExito = "Se encontraron resultados de tu consulta por detalle de solicitante.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
            }
            else
            {
                tituloPartesCausa4.Visible = false;
                tituloDetalles4.Visible = false;
                string mensajeNoDatos = "No se encontro resultado de la busqueda.";
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
                            tituloPartesCausa4.Visible = true;
                            tituloDetalles4.Visible = true;
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

            detallesConsulta4.InnerHtml = htmlTable.ToString();
        }
       
        protected void btnLimpiar4_Click(object sender, EventArgs e)
        {
            tituloPartesCausa4.Visible = false;
            tituloDetalles4.Visible = false;
            inputNucBusqueda.Value = "";
            GridViewPCausa4.DataSource = null;
            GridViewPCausa4.DataBind();
            detallesConsulta4.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        protected void GridViewPCausa4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int IdEjecucion = Convert.ToInt32(e.CommandArgument);
                VerDetalles(IdEjecucion);
            }
        }
        protected void GridViewPCausa4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPCausa4.PageIndex = e.NewPageIndex;
            BindDataToGridView(); // Método para recargar los datos
        }
        protected void GridViewPCausa4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Lógica para RowDataBound si es necesario
        }
        private void BindDataToGridView()
        {
            try
            {
                string nuc = inputNucBusqueda.Value;
                string idJuzgado = InputDistritoProcedencia.Value;

                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NUC", nuc);
                        cmd.Parameters.AddWithValue("@idJuzgado", idJuzgado);
                        int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                        cmd.Parameters.AddWithValue("@idCircuito", Circuito);
                        cmd.Parameters.AddWithValue("@opcion", 4); // Usamos la opción 4

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }

                GridViewPCausa4.DataSource = dt;
                GridViewPCausa4.DataBind();

                // Actualizar visibilidad de los títulos
                tituloPartesCausa4.Visible = dt.Rows.Count > 0;
                tituloDetalles4.Visible = dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                // Implementar manejo de errores aquí
                // Por ejemplo: Mostrar un mensaje de error
            }
        }
        //
    }
}