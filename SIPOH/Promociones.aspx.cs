using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class Promociones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sessionTimeout = 1 * 60; // 20 minutos
            Session.Timeout = sessionTimeout;

            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Default.aspx");
                return;
            }
            string circuito = HttpContext.Current.Session["TCircuito"] as string;
            List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;
            bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/promociones"));

            if ((circuito == "c" || circuito == "e") && tienePermiso)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
                Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
            }
            if (!IsPostBack)
            {
                tituloTablaPromociones.Visible = false;
                CargarJuzgados();
            }
        }
        //
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

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Nombre"].ToString();
                        listItem.Value = dr["IdJuzgado"].ToString();
                        selectBusJuzgados.Items.Add(listItem);
                    }
                }
            }
        }
        protected void btnBuscarPromocion_Click(object sender, EventArgs e)
        {
            string ejecucion = inpuBusEjecucion.Value; //inputNucBusqueda
            string idJuzgado = selectBusJuzgados.Value; //InputDistritoProcedencia

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
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ConsultaInicial", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Numero", ejecucion);
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }

            GridViewPromociones.DataSource = dt;
            GridViewPromociones.DataBind();

            // Mostrar los títulos si hay datos
            if (dt.Rows.Count > 0)
            {
                tituloTablaPromociones.Visible = true;
                GridViewPromociones.DataSource = dt;
                GridViewPromociones.DataBind();
                string mensajeExito = "Se encontraron resultados de tu consulta de promociones.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
            }
            else
            {
                tituloTablaPromociones.Visible = false;
               // tituloDetalles4.Visible = false;
                string mensajeNoDatos = "No se encontro resultado de la busqueda.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
            }
        }

        protected void btnLimpiarPromocion_Click(object sender, EventArgs e)
        {
            tituloTablaPromociones.Visible = false;
            //tituloDetalles5.Visible = false;
            GridViewPromociones.DataSource = null;
            GridViewPromociones.DataBind();
            //detallesConsulta5.InnerHtml = "";
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        //
    }
}