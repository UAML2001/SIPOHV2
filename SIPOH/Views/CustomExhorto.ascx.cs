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
                        ddlDelitos1.Items.Add(item);
                        ddlDelitos2.Items.Add(item);
                        ddlDelitos3.Items.Add(item);
                        //inputDelitos.Items.Add(item);
                        //inputDelitos2.Items.Add(item);
                        //Juzgados.Items.Add(item);
                    }
                }
            }
        }

        //Delito Exhorto
        protected void btnAgregarDelito_Click(object sender, EventArgs e)
        {
            string nombreDelito = ddlDelitos1.SelectedItem.Text;
            
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
        //Fin Delito Exhorto

        //Delito Despacho
        protected void btnAgregarDelito_Click2(object sender, EventArgs e)
        {
            string nombreDelito2 = ddlDelitos2.SelectedItem.Text;

            DataTable dt = GetDataTableRe();
            DataRow newRow = dt.NewRow();
            newRow["NombreDelito2"] = nombreDelito2;
            dt.Rows.Add(newRow);

            gvDelitos2.DataSource = dt;
            gvDelitos2.DataBind();
        }

        private DataTable GetDataTableRe()
        {
            DataTable dt;
            if (ViewState["Delitos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("NombreDelito2", typeof(string));
                ViewState["Delitos"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Delitos"];
            }

            return dt;
        }

        protected void gvDelitos_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDelito")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTableRe();
                dt.Rows[rowIndex].Delete();

                gvDelitos2.DataSource = dt;
                gvDelitos2.DataBind();
            }
        }

        //Fin Delito Despacho



        //Delito Requisitoria
        protected void btnAgregarDelito_Click3(object sender, EventArgs e)
        {
            string nombreDelito3 = ddlDelitos3.SelectedItem.Text;

            DataTable dt = GetDataTableDe();
            DataRow newRow = dt.NewRow();
            newRow["NombreDelito3"] = nombreDelito3;
            dt.Rows.Add(newRow);

            gvDelitos3.DataSource = dt;
            gvDelitos3.DataBind();
        }

        private DataTable GetDataTableDe()
        {
            DataTable dt;
            if (ViewState["Delitos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("NombreDelito3", typeof(string));
                ViewState["Delitos"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Delitos"];
            }

            return dt;
        }

        protected void gvDelitos_RowCommand3(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDelito")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTableDe();
                dt.Rows[rowIndex].Delete();

                gvDelitos3.DataSource = dt;
                gvDelitos3.DataBind();
            }
        }

        //Fin Delito Requisitoria





        //Back para agragar partes exhorto

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

        //Fin back para agragar partes Exhorto


        //Back para agragar partes Despacho

        private DataTable GetDataTable3()
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
        protected void btnAgregarParte_Click2(object sender, EventArgs e)
        {
            string nom = nom3.Text;
            string ap = ap3.Text;
            string am = am3.Text;
            string partes = parte2.SelectedItem.Text;
            string sexox = sexo2.SelectedValue;
            string espePartes = espeParte2.Text;
            string espeSexox = espeSexo2.Text;

            // Verifica si la opción seleccionada en los DropDownList es "Otro"
            if (partes == "Otro" && sexox == "Otro")
            {
                // Asigna los valores de los TextBox de "espeParte" y "espeSexo"
                partes = espePartes;
                sexox = espeSexox;
            }

            DataTable dt = GetDataTable3();
            DataRow newRow = dt.NewRow();

            // Corrige la asignación para concatenar los valores con un espacio
            newRow["Nombre"] = nom + " " + ap + " " + am;
            newRow["Parte"] = partes;
            newRow["Genero"] = sexox;
            dt.Rows.Add(newRow);

            gvPartes2.DataSource = dt;
            gvPartes2.DataBind();

            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalGuardarDatos2();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario2();", true);

        }

        protected void gvPartes_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarParte")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTable3();
                dt.Rows[rowIndex].Delete();

                gvPartes2.DataSource = dt;
                gvPartes2.DataBind();
            }
        }

        //Fin back para agragar partes Despacho


        //Back para agragar partes requisitoria

        private DataTable GetDataTable4()
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
        protected void btnAgregarParte_Click3(object sender, EventArgs e)
        {
            string nom = nom4.Text;
            string ap = ap4.Text;
            string am = am4.Text;
            string partes = parte3.SelectedItem.Text;
            string sexox = sexo3.SelectedValue;
            string espePartes = espeParte3.Text;
            string espeSexox = espeSexo3.Text;

            // Verifica si la opción seleccionada en los DropDownList es "Otro"
            if (partes == "Otro" && sexox == "Otro")
            {
                // Asigna los valores de los TextBox de "espeParte" y "espeSexo"
                partes = espePartes;
                sexox = espeSexox;
            }

            DataTable dt = GetDataTable4();
            DataRow newRow = dt.NewRow();

            // Corrige la asignación para concatenar los valores con un espacio
            newRow["Nombre"] = nom + " " + ap + " " + am;
            newRow["Parte"] = partes;
            newRow["Genero"] = sexox;
            dt.Rows.Add(newRow);

            gvPartes3.DataSource = dt;
            gvPartes3.DataBind();

            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalGuardarDatos3();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario3();", true);

        }

        protected void gvPartes_RowCommand3(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarParte")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTable4();
                dt.Rows[rowIndex].Delete();

                gvPartes3.DataSource = dt;
                gvPartes3.DataBind();
            }
        }

        //Fin back para agragar partes requisitoria


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

        protected void btnAbrirModal2(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "AbrirModal2();", true);
        }

        protected void btnAbrirModal3(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "AbrirModal3();", true);
        }




        protected void btnCerrarModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "cerrarModal", "CerrarModal();", true);
        }

        protected void btnCerrarModal2(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "cerrarModal", "CerrarModal2();", true);
        }

        protected void btnCerrarModal3(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "cerrarModal", "CerrarModal3();", true);
        }



        protected void ObtenerDatosYMostrarModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "obtenerDatos", "ObtenerDatosYMostrarModal();", true);
        }

        protected void ObtenerDatosYMostrarModal2(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "obtenerDatos", "ObtenerDatosYMostrarModal2();", true);
        }

        protected void ObtenerDatosYMostrarModal3(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "obtenerDatos", "ObtenerDatosYMostrarModal3();", true);
        }


    }
}