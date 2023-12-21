using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views
{
    public partial class InicialBusNoNuc : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarJuzgados();
            }
        }
        private void CargarJuzgados()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Asumiendo que tienes una variable de sesión que contiene el ID del distrito
                int idDistrito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);

                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Juzgados", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDistrito", idDistrito);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Nombre"].ToString(); // Aquí asignas el nombre del juzgado.
                        listItem.Value = dr["IdJuzgado"].ToString(); // Aquí asignas el ID del juzgado como valor.
                        InputDistritoProcedencia.Items.Add(listItem); // Asegúrate de que este sea el ID correcto del dropdown.
                    }
                }
            }
        }

        protected void btnBuscarPCausa4_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string detalleSolicitante = inputDetalleSolicitante6.Value;
            //    string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            //    DataTable dt = new DataTable();

            //    using (SqlConnection con = new SqlConnection(connectionString))
            //    {
            //        using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultas", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.AddWithValue("@nombre", detalleSolicitante);
            //            int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            //            cmd.Parameters.AddWithValue("@idCircuito", Circuito);
            //            cmd.Parameters.AddWithValue("@opcion", 6);
            //            con.Open();
            //            using (SqlDataReader dr = cmd.ExecuteReader())
            //            {
            //                dt.Load(dr);
            //            }
            //        }
            //    }

            //    if (dt.Rows.Count > 0)
            //    {
            //        tituloPartesCausa6.Visible = true;
            //        GridViewPCausa6.DataSource = dt;
            //        GridViewPCausa6.DataBind();
            //        detallesConsulta6.InnerHtml = "";
            //        string mensajeExito = "Se encontraron resultados de tu consulta por detalle de solicitante.";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
            //    }
            //    else
            //    {
            //        //string mensajeNoDatos = "No se encontro resultado de la busqueda.";
            //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //string mensajeNoDatos = "No se pudo realizar la busqueda de tu consulta.";
            //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
            //}
        }
        protected void btnLimpiar4_Click(object sender, EventArgs e)
        {
            //tituloPartesCausa6.Visible = false;
            //tituloDetalles6.Visible = false;
            //inputDetalleSolicitante6.Value = "";
            //GridViewPCausa6.DataSource = null;
            //GridViewPCausa6.DataBind();
            //detallesConsulta6.InnerHtml = "";
            //string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            //string script = $"toastWarning('{mensaje}');";
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        //
    }
}