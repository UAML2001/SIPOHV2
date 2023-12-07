using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views
{
    public partial class CustomExhorto : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializar la tabla en ViewState
                DataTable tablaDelitos = new DataTable();
                tablaDelitos.Columns.Add("Nombre", typeof(string));
                ViewState["TablaDelitos"] = tablaDelitos;

                DataTable tablaPartes = new DataTable();
                tablaPartes.Columns.Add("Nombre", typeof(string));
                tablaPartes.Columns.Add("Genero", typeof(string));
                tablaPartes.Columns.Add("Parte", typeof(string));
                ViewState["TablaPartes"] = tablaPartes;

                // Cargar los delitos en el DropDownList
                CargarDelitos();
            }


        }

        //Back para agregar delitos
        private void CargarDelitos()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT IdDelito, Nombre FROM P_CatDelitos";
                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(dr["Nombre"].ToString(), dr["IdDelito"].ToString());
                        ddlDelitos.Items.Add(item);
                        //inputDelitos.Items.Add(item);
                        //inputDelitos2.Items.Add(item);
                        //Juzgados.Items.Add(item);
                    }
                }
            }
        }

        protected void btnAgregarDelito_Click(object sender, EventArgs e)
        {
            string nombreDelito = ddlDelitos.SelectedItem.Text;

            DataTable dt = GetDataTable();
            DataRow newRow = dt.NewRow();
            newRow["NombreDelito"] = nombreDelito;
            dt.Rows.Add(newRow);

            gvDelitos.DataSource = dt;
            gvDelitos.DataBind();
        }

        private DataTable GetDataTable()
        {
            DataTable dt;
            if (ViewState["Delitos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("NombreDelito", typeof(string));
                ViewState["Delitos"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Delitos"];
            }

            return dt;
        }

        protected void gvDelitos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDelito")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTable();
                dt.Rows[rowIndex].Delete();

                gvDelitos.DataSource = dt;
                gvDelitos.DataBind();
            }
        }

        //Back para agragar partes

        private DataTable GetDataTable2()
        {
            DataTable dt;
            if (ViewState["Partes"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Parte", typeof(string));
                dt.Columns.Add("Genero", typeof(string));
                ViewState["Partes"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Partes"];
            }

            return dt;
        }
        protected void btnAgregarParte_Click(object sender, EventArgs e)
        {
            string nom = nom2.Text;
            string ap = ap2.Text;
            string am = am2.Text;
            string partes = parte.SelectedItem.Text;
            string sexox = sexo.SelectedValue;
            string espePartes = espeParte.Text;
            string espeSexox = espeSexo.Text;

            // Verifica si la opción seleccionada en los DropDownList es "Otro"
            if (partes == "Otro" && sexox == "Otro")
            {
                // Asigna los valores de los TextBox de "espeParte" y "espeSexo"
                partes = espePartes;
                sexox = espeSexox;
            }

            DataTable dt = GetDataTable2();
            DataRow newRow = dt.NewRow();

            // Corrige la asignación para concatenar los valores con un espacio
            newRow["Nombre"] = nom + " " + ap + " " + am;
            newRow["Parte"] = partes;
            newRow["Genero"] = sexox;
            dt.Rows.Add(newRow);

            gvPartes.DataSource = dt;
            gvPartes.DataBind();

            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalGuardarDatos();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario();", true);

        }

        protected void gvPartes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarParte")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTable2();
                dt.Rows[rowIndex].Delete();

                gvPartes.DataSource = dt;
                gvPartes.DataBind();
            }
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = DropDownList1.SelectedValue == "Exhorto";
            Panel2.Visible = DropDownList1.SelectedValue == "Despacho";
            Panel3.Visible = DropDownList1.SelectedValue == "Requesitoria";

        }


        protected void btnAbrirModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "AbrirModal();", true);
        }

        protected void btnCerrarModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "cerrarModal", "CerrarModal();", true);
        }

        protected void ObtenerDatosYMostrarModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "obtenerDatos", "ObtenerDatosYMostrarModal();", true);
        }


    }
}