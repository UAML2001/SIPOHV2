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
    public partial class InicialBusNoCausa : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tituloPartesCausa3.Visible = false;
                tituloDetalles3.Visible = false;
                CargarDistritos();
            }
        }
        private void CargarDistritos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);

                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Distritos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCircuito", Circuito);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        InputDistritoProcedencia.Items.Clear();
                        InputDistritoProcedencia.Items.Add(new ListItem("Seleccionar", ""));

                        while (dr.Read())
                        {
                            ListItem listItem = new ListItem(dr["nombre"].ToString(), dr["IdDistrito"].ToString());
                            InputDistritoProcedencia.Items.Add(listItem);
                        }
                    }
                }
            }
        }
        protected void InputDistritoProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDistrito;
            if (int.TryParse(InputDistritoProcedencia.SelectedValue, out idDistrito) && idDistrito > 0)
            {
                CargarJuzgados(idDistrito);
            }
            else
            {
                inputJuzgadoProcedencia.Items.Clear();
                inputJuzgadoProcedencia.Items.Add(new ListItem("Seleccionar", ""));
            }
        }
        private void CargarJuzgados(int idDistrito)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Juzgados", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDistrito", idDistrito);
                    cmd.Parameters.AddWithValue("@Opcion", 1);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        inputJuzgadoProcedencia.Items.Clear();
                        inputJuzgadoProcedencia.Items.Add(new ListItem("Seleccionar", ""));

                        while (dr.Read())
                        {
                            ListItem listItem = new ListItem(dr["Nombre"].ToString(), dr["IdJuzgado"].ToString());
                            inputJuzgadoProcedencia.Items.Add(listItem);
                        }
                    }
                }
            }
        }
        protected void btnBuscarPCausa3_Click(object sender, EventArgs e)
        {
            try
            {
                string idJuzgado = inputJuzgadoProcedencia.SelectedValue;
                string numeroCausa = inputNucBusqueda.Value;
                int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                int opcion = 3;

                DataTable dt = BindDataToGridView(Circuito, opcion, idJuzgado, numeroCausa);

                if (dt.Rows.Count > 0)
                {
                    tituloPartesCausa3.Visible = true;
                    GridViewPCausa3.DataSource = dt;
                    GridViewPCausa3.DataBind();
                    string mensajeExito = "Se encontraron resultados para tu consulta.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
                }
                else
                {
                    tituloPartesCausa3.Visible = false;
                    GridViewPCausa3.DataSource = null;
                    GridViewPCausa3.DataBind();
                    string mensajeNoDatos = "No se encontraron resultados para la búsqueda, verifica el distrito y juzgado de ser necesario.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                }
            }
            catch (Exception ex)
            {
                string mensajeError = "Ocurrió un error al realizar la búsqueda.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", $"toastError('{mensajeError}');", true);
            }
        }

        private DataTable BindDataToGridView(int circuito, int opcion, string idJuzgado, string numeroCausa)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCircuito", circuito);
                        cmd.Parameters.AddWithValue("@opcion", opcion);
                        cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                        cmd.Parameters.AddWithValue("@numero", numeroCausa);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }

            return dt;
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
                            tituloPartesCausa3.Visible = true;
                            tituloDetalles3.Visible = true;
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

            detallesConsulta3.InnerHtml = htmlTable.ToString();
        }
        protected void btnLimpiar3_Click(object sender, EventArgs e)
        {
            tituloPartesCausa3.Visible = false;
            tituloDetalles3.Visible = false;
            GridViewPCausa3.DataSource = null;
            inputNucBusqueda.Value = "";
            GridViewPCausa3.DataBind();
            detallesConsulta3.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        protected void GridViewPCausa3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int IdEjecucion = Convert.ToInt32(e.CommandArgument);
                VerDetalles(IdEjecucion);
            }
        }
        protected void GridViewPCausa3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPCausa3.PageIndex = e.NewPageIndex;
            string idJuzgado = inputJuzgadoProcedencia.SelectedValue;
            string numeroCausa = inputNucBusqueda.Value;
            int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            int opcion = 3;

            // Vuelve a obtener los datos con los mismos parámetros, pero ahora con la nueva página
            DataTable dt = BindDataToGridView(Circuito, opcion, idJuzgado, numeroCausa);
            GridViewPCausa3.DataSource = dt;
            GridViewPCausa3.DataBind();
        }
        protected void GridViewPCausa3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Lógica para RowDataBound si es necesario
        }
   
        //
    }
}