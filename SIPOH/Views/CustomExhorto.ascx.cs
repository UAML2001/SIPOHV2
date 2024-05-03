using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
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
                ViewState["TablaDelitos"] = InicializarTabla(new string[] { "IdDelito", "NombreDelito" }, gvDelitos);
                ViewState["TablaPartes"] = InicializarTabla(new string[] { "Nombre", "Genero", "Parte" }, gvPartes);
                ViewState["tablaAnexos"] = InicializarTabla(new string[] { "descripcion", "Cantidad" }, gvAnexos);

                ViewState["TablaDelitos2"] = InicializarTabla(new string[] { "IdDelito2", "NombreDelito2" }, gvDelitos2);
                ViewState["TablaPartes2"] = InicializarTabla(new string[] { "Nombre", "Genero", "Parte" }, gvPartes2);
                ViewState["tablaAnexos2"] = InicializarTabla(new string[] { "descripcion2", "Cantidad2" }, gvAnexos2);

                ViewState["TablaDelitos3"] = InicializarTabla(new string[] { "IdDelito3", "NombreDelito3" }, gvDelitos3);
                ViewState["TablaPartes3"] = InicializarTabla(new string[] { "Nombre", "Genero", "Parte" }, gvPartes3);
                ViewState["tablaAnexos3"] = InicializarTabla(new string[] { "descripcion3", "Cantidad3" }, gvAnexos3);

                // Cargar los delitos en el DropDownList
                CargarDelitos();
                CargarAnexos();

                InsertExhorto.Style.Add("display", "none");
            }
        }

        private DataTable InicializarTabla(string[] columnas, GridView gridView)
        {
            DataTable tabla = new DataTable();
            foreach (string columna in columnas)
            {
                tabla.Columns.Add(columna, typeof(string));
            }

            // Si la tabla está vacía, agrega una fila vacía
            if (tabla.Rows.Count == 0)
            {
                tabla.Rows.Add(tabla.NewRow());
                gridView.DataSource = tabla;
                gridView.DataBind();
                gridView.Rows[0].Visible = false;
            }

            return tabla;
        }


        //Back para cargar anexos exhortos
        private void CargarAnexos()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT descripcion FROM P_CatAnexos";
                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(dr["descripcion"].ToString());
                        ddlAnexos.Items.Add(item);
                        ddlAnexos2.Items.Add(item);
                        ddlAnexos3.Items.Add(item);
                    }
                }
            }
        }

        // Back Anexo Exhorto
        // Método para verificar si el delito ya está en la tabla

        private bool AnexosYaEnTabla(string anexo)
        {
            foreach (GridViewRow row in gvAnexos.Rows)
            {
                Label lblAnexo = (Label)row.FindControl("lblAnexo");
                if (lblAnexo.Text == anexo)
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnAgregarAnexo_Click(object sender, EventArgs e)
        {
            string anexoSeleccionado = ddlAnexos.SelectedItem.Text;
            string cantidadAnexo = noAnexos.Text;

            // Verificar si el valor de "noAnexos" es 0 o mayor a 999
            if (int.Parse(cantidadAnexo) <= 0 || int.Parse(cantidadAnexo) > 999)
            {
                // Mostrar un mensaje de error específico para la cantidad de anexos
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CantAnexos", "toastr.error('La cantidad de anexos debe ser mayor a 0 y menor a 1000');", true);
                return;
            }

            // Verificar si el anexo seleccionado es "Seleccione el anexo a agregar:"
            if (anexoSeleccionado == "Seleccione el anexo a agregar:")
            {
                // Mostrar un mensaje de error específico para la selección de anexos
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "SelAnexos", "EjemploErrorSelAnexo();", true);
                return;
            }

            // Agregar el delito a la tabla
            DataTable dt = GetDataTableAnex();
            if (!AnexosYaEnTabla(anexoSeleccionado))
            {
                DataRow newRow = dt.NewRow();
                newRow["descripcion"] = anexoSeleccionado;
                newRow["Cantidad"] = cantidadAnexo;
                dt.Rows.Add(newRow);

                gvAnexos.DataSource = dt;
                gvAnexos.DataBind();

                // Si la primera fila es la fila vacía, elimínala
                if (dt.Rows.Count > 1 && dt.Rows[0][0] == DBNull.Value)
                {
                    dt.Rows[0].Delete();
                    gvAnexos.DataSource = dt;
                    gvAnexos.DataBind();
                }

                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarAnexos", "limpiarFormularioAnexos();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblExhortos", "EjemploErrorTblAnexo();", true);
            }
        }

        private DataTable GetDataTableAnex()
        {
            DataTable dt;
            if (ViewState["Anexos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("descripcion", typeof(string));
                dt.Columns.Add("Cantidad", typeof(string));
                ViewState["Anexos"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Anexos"];
            }

            return dt;
        }

        protected void gvAnexos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarAnexo")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTableAnex();
                dt.Rows[rowIndex].Delete();

                // Si la tabla está vacía después de la eliminación, agrega una fila vacía
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvAnexos.DataSource = dt;
                    gvAnexos.DataBind();
                    gvAnexos.Rows[0].Visible = false;
                }
                else
                {
                    gvAnexos.DataSource = dt;
                    gvAnexos.DataBind();
                }
            }
        }

        protected void ddlAnexos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el valor seleccionado del DropDownList
            string valorSeleccionado = ddlAnexos.SelectedValue;

            // Encuentra el índice de la columna 'Nombre' en el GridView
            int nombreIndex = 0; // Asegúrate de reemplazar esto con el índice correcto

            // Recorre cada fila del GridView y actualiza el valor de la celda
            foreach (GridViewRow row in gvAnexos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Actualiza el valor de la celda con el valor seleccionado del DropDownList
                    row.Cells[nombreIndex].Text = valorSeleccionado.ToUpper();
                }
            }

            // Vuelve a enlazar los datos si es necesario
            gvAnexos.DataBind();
        }
        //// Termina Back Anexo Exhorto






        // Back Anexo Despacho

        // Método para verificar si el anexo ya está en la tabla
        private bool AnexosYaEnTabla2(string anexo)
        {
            foreach (GridViewRow row in gvAnexos2.Rows)
            {
                Label lblAnexo2 = (Label)row.FindControl("lblAnexo2");
                if (lblAnexo2.Text == anexo)
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnAgregarAnexo_Click2(object sender, EventArgs e)
        {
            string anexoSeleccionado = ddlAnexos2.SelectedItem.Text;
            string cantidadAnexo = noAnexos2.Text;

            // Verificar si el valor de "noAnexos" es 0 o mayor a 999
            if (int.Parse(cantidadAnexo) <= 0 || int.Parse(cantidadAnexo) > 999)
            {
                // Mostrar un mensaje de error específico para la cantidad de anexos
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CantAnexos", "toastr.error('La cantidad de anexos debe ser mayor a 0 y menor a 1000');", true);
                return;
            }

            // Verificar si el anexo seleccionado es "Seleccione el anexo a agregar:"
            if (anexoSeleccionado == "Seleccione el anexo a agregar:")
            {
                // Mostrar un mensaje de error específico para la selección de anexos
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "SelAnexos", "EjemploErrorSelAnexo();", true);
                return;
            }

            // Agregar el delito a la tabla
            DataTable dt = GetDataTableAnex2();
            if (!AnexosYaEnTabla2(anexoSeleccionado))
            {
                DataRow newRow = dt.NewRow();
                newRow["descripcion2"] = anexoSeleccionado;
                newRow["Cantidad2"] = cantidadAnexo;
                dt.Rows.Add(newRow);

                gvAnexos2.DataSource = dt;
                gvAnexos2.DataBind();

                // Si la primera fila es la fila vacía, elimínala
                if (dt.Rows.Count > 1 && dt.Rows[0][0] == DBNull.Value)
                {
                    dt.Rows[0].Delete();
                    gvAnexos2.DataSource = dt;
                    gvAnexos2.DataBind();
                }

                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarAnexos", "limpiarFormularioAnexos2();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblExhortos", "EjemploErrorTblAnexo();", true);
            }
        }

        private DataTable GetDataTableAnex2()
        {
            DataTable dt;
            if (ViewState["Anexos2"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("descripcion2", typeof(string));
                dt.Columns.Add("Cantidad2", typeof(string));
                ViewState["Anexos2"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Anexos2"];
            }

            return dt;
        }


        protected void gvAnexos_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarAnexo2")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTableAnex2();
                dt.Rows[rowIndex].Delete();

                // Si la tabla está vacía después de la eliminación, agrega una fila vacía
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvAnexos2.DataSource = dt;
                    gvAnexos2.DataBind();
                    gvAnexos2.Rows[0].Visible = false;
                }
                else
                {
                    gvAnexos2.DataSource = dt;
                    gvAnexos2.DataBind();
                }
            }
        }

        protected void ddlAnexos_SelectedIndexChanged2(object sender, EventArgs e)
        {
            // Obtén el valor seleccionado del DropDownList
            string valorSeleccionado = ddlAnexos2.SelectedValue;

            // Encuentra el índice de la columna 'Nombre' en el GridView
            int nombreIndex = 0; // Asegúrate de reemplazar esto con el índice correcto

            // Recorre cada fila del GridView y actualiza el valor de la celda
            foreach (GridViewRow row in gvAnexos2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Actualiza el valor de la celda con el valor seleccionado del DropDownList
                    row.Cells[nombreIndex].Text = valorSeleccionado.ToUpper();
                }
            }
            // Vuelve a enlazar los datos si es necesario
            gvAnexos2.DataBind();
        }
        //// Termina Back Anexo Exhorto




        // Back Anexo Requisitoria
        // Método para verificar si el anexo ya está en la tabla
        private bool AnexosYaEnTabla3(string anexo)
        {
            foreach (GridViewRow row in gvAnexos3.Rows)
            {
                Label lblAnexo3 = (Label)row.FindControl("lblAnexo3");
                if (lblAnexo3.Text == anexo)
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnAgregarAnexo_Click3(object sender, EventArgs e)
        {
            string anexoSeleccionado = ddlAnexos3.SelectedItem.Text;
            string cantidadAnexo = noAnexos3.Text;

            // Verificar si el valor de "noAnexos" es 0 o mayor a 999
            if (int.Parse(cantidadAnexo) <= 0 || int.Parse(cantidadAnexo) > 999)
            {
                // Mostrar un mensaje de error específico para la cantidad de anexos
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CantAnexos3", "toastr.error('La cantidad de anexos debe ser mayor a 0 y menor a 1000');", true);
                return;
            }

            // Verificar si el anexo seleccionado es "Seleccione el anexo a agregar:"
            if (anexoSeleccionado == "Seleccione el anexo a agregar:")
            {
                // Mostrar un mensaje de error específico para la selección de anexos
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "SelAnexos3", "EjemploErrorSelAnexo3();", true);
                return;
            }

            // Obtener el DataTable de anexos
            DataTable dt = GetDataTableAnex3();

            // Verificar si el anexo ya está en la tabla
            if (!AnexosYaEnTabla3(anexoSeleccionado))
            {
                // Agregar el anexo a la tabla
                DataRow newRow = dt.NewRow();
                newRow["descripcion3"] = anexoSeleccionado;
                newRow["Cantidad3"] = cantidadAnexo;
                dt.Rows.Add(newRow);

                gvAnexos3.DataSource = dt;
                gvAnexos3.DataBind();

                // Si la primera fila es la fila vacía, elimínala
                if (dt.Rows.Count > 1 && dt.Rows[0][0] == DBNull.Value)
                {
                    dt.Rows[0].Delete();
                    gvAnexos3.DataSource = dt;
                    gvAnexos3.DataBind();
                }

                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarAnexos3", "limpiarFormularioAnexos3();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblExhortos3", "EjemploErrorTblAnexo3();", true);
            }
        }

        private DataTable GetDataTableAnex3()
        {
            DataTable dt;
            if (ViewState["Anexos3"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("descripcion3", typeof(string));
                dt.Columns.Add("Cantidad3", typeof(string));
                ViewState["Anexos3"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Anexos3"];
            }

            return dt;
        }

        protected void gvAnexos_RowCommand3(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarAnexo3")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTableAnex3();
                dt.Rows[rowIndex].Delete();

                // Si la tabla está vacía después de la eliminación, agrega una fila vacía
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvAnexos3.DataSource = dt;
                    gvAnexos3.DataBind();
                    gvAnexos3.Rows[0].Visible = false;
                }
                else
                {
                    gvAnexos3.DataSource = dt;
                    gvAnexos3.DataBind();
                }
            }
        }

        protected void ddlAnexos_SelectedIndexChanged3(object sender, EventArgs e)
        {
            // Obtén el valor seleccionado del DropDownList
            string valorSeleccionado = ddlAnexos3.SelectedValue;

            // Encuentra el índice de la columna 'Nombre' en el GridView
            int nombreIndex = 0; // Asegúrate de reemplazar esto con el índice correcto

            // Recorre cada fila del GridView y actualiza el valor de la celda
            foreach (GridViewRow row in gvAnexos3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Actualiza el valor de la celda con el valor seleccionado del DropDownList
                    row.Cells[nombreIndex].Text = valorSeleccionado.ToUpper();
                }
            }

            // Vuelve a enlazar los datos si es necesario
            gvAnexos3.DataBind();
        }
        // Termina Back Anexo Requisitoria












        //Back para cargar delitos
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
        // Método para verificar si el delito ya está en la tabla
        private bool DelitoYaEnTabla(string delito)
        {
            foreach (GridViewRow row in gvDelitos.Rows)
            {
                Label lblDelito = (Label)row.FindControl("lblNombreDelito");
                if (lblDelito.Text == delito)
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnAgregarDelito_Click(object sender, EventArgs e)
        {
            string delitoSeleccionado = ddlDelitos1.SelectedItem.Text;

            // Verificar si el valor es "Seleccione un delito:"
            if (delitoSeleccionado == "Seleccione un delito:")
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorSeleccion", "EjemploErrorSelecTblDelitos();", true);
            }
            // Verificar si el delito ya está en la tabla
            else if (!DelitoYaEnTabla(delitoSeleccionado))
            {
                // Agregar el delito a la tabla
                // Puedes agregar el código necesario para agregar el delito a tu fuente de datos

                string idDelito = ddlDelitos1.SelectedValue;
                string nombreDelito = ddlDelitos1.SelectedItem.Text;

                DataTable dt = GetDataTable();
                DataRow newRow = dt.NewRow();
                newRow["IdDelito"] = idDelito;
                newRow["NombreDelito"] = nombreDelito;
                dt.Rows.Add(newRow);

                gvDelitos.DataSource = dt;
                gvDelitos.DataBind();

                // Si la primera fila es la fila vacía, elimínala
                if (dt.Rows.Count > 1 && dt.Rows[0][0] == DBNull.Value)
                {
                    dt.Rows[0].Delete();
                    gvDelitos.DataSource = dt;
                    gvDelitos.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblDelitos", "EjemploErrorTblDelitos();", true);
            }
        }



        private DataTable GetDataTable()
        {
            DataTable dt;
            if (ViewState["Delitos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("IdDelito", typeof(string));
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

                // Si la tabla está vacía después de la eliminación, agrega una fila vacía
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvDelitos.DataSource = dt;
                    gvDelitos.DataBind();
                    gvDelitos.Rows[0].Visible = false;
                }
                else
                {
                    gvDelitos.DataSource = dt;
                    gvDelitos.DataBind();
                }
            }
        }


        //Fin Delito Exhorto

        //Delito Despacho
        // Método para verificar si el delito ya está en la tabla
        private bool DelitoYaEnTabla2(string delito)
        {
            foreach (GridViewRow row in gvDelitos2.Rows)
            {
                Label lblDelito2 = (Label)row.FindControl("lblNombreDelito2");
                if (lblDelito2.Text == delito)
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnAgregarDelito_Click2(object sender, EventArgs e)
        {
            string delitoSeleccionado = ddlDelitos2.SelectedItem.Text;

            // Verificar si el valor es "Seleccione un delito:"
            if (delitoSeleccionado == "Seleccione un delito:")
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorSeleccion", "EjemploErrorSelecTblDelitos();", true);
            }
            // Verificar si el delito ya está en la tabla
            else if (!DelitoYaEnTabla2(delitoSeleccionado))
            {
                // Agregar el delito a la tabla
                // Puedes agregar el código necesario para agregar el delito a tu fuente de datos

                string idDelito = ddlDelitos2.SelectedValue;
                string nombreDelito = ddlDelitos2.SelectedItem.Text;

                DataTable dt = GetDataTableRe();
                DataRow newRow = dt.NewRow();
                newRow["IdDelito2"] = idDelito;
                newRow["NombreDelito2"] = nombreDelito;
                dt.Rows.Add(newRow);

                gvDelitos2.DataSource = dt;
                gvDelitos2.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblDelitos", "EjemploErrorTblDelitos();", true);
            }
        }
        private DataTable GetDataTableRe()
        {
            DataTable dt;
            if (ViewState["Delitos2"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("IdDelito2", typeof(string));
                dt.Columns.Add("NombreDelito2", typeof(string));
                ViewState["Delitos2"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Delitos2"];
            }

            return dt;
        }

        protected void gvDelitos_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDelito2")
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
        // Método para verificar si el delito ya está en la tabla
        private bool DelitoYaEnTabla3(string delito)
        {
            foreach (GridViewRow row in gvDelitos3.Rows)
            {
                Label lblDelito3 = (Label)row.FindControl("lblNombreDelito3");
                if (lblDelito3.Text == delito)
                {
                    return true;
                }
            }
            return false;
        }
        protected void btnAgregarDelito_Click3(object sender, EventArgs e)
        {
            string delitoSeleccionado = ddlDelitos3.SelectedItem.Text;

            // Verificar si el valor es "Seleccione un delito:"
            if (delitoSeleccionado == "Seleccione un delito:")
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorSeleccion", "EjemploErrorSelecTblDelitos();", true);
            }
            // Verificar si el delito ya está en la tabla
            else if (!DelitoYaEnTabla3(delitoSeleccionado))
            {
                // Agregar el delito a la tabla
                // Puedes agregar el código necesario para agregar el delito a tu fuente de datos

                string idDelito = ddlDelitos3.SelectedValue;
                string nombreDelito = ddlDelitos3.SelectedItem.Text;

                DataTable dt = GetDataTableDe();
                DataRow newRow = dt.NewRow();
                newRow["IdDelito3"] = idDelito;
                newRow["NombreDelito3"] = nombreDelito;
                dt.Rows.Add(newRow);

                gvDelitos3.DataSource = dt;
                gvDelitos3.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblDelitos", "EjemploErrorTblDelitos();", true);
            }
        }

        private DataTable GetDataTableDe()
        {
            DataTable dt;
            if (ViewState["Delitos3"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("IdDelito3", typeof(string));
                dt.Columns.Add("NombreDelito3", typeof(string));
                ViewState["Delitos3"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Delitos3"];
            }

            return dt;
        }

        protected void gvDelitos_RowCommand3(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDelito3")
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
            string partes = parte.SelectedValue;
            string sexos = sexo.SelectedValue;
            string espePartes = espeParte.Text;
            string espeSexos = espeSexo.Text;

            // Verificar si los campos están vacíos
            if (nom == "")
            {
                MostrarError("El campo Nombre debe estar lleno");
                return;
            }
            if (ap == "")
            {
                MostrarError("El campo Apellido Paterno debe estar lleno");
                return;
            }
            if (am == "")
            {
                MostrarError("El campo Apellido Materno debe estar lleno");
                return;
            }
            if (partes == "Seleccionar")
            {
                MostrarError("Debe seleccionar una opcion en el campo Partes");
                return;
            }
            if (sexos == "Seleccionar")
            {
                MostrarError("Debe seleccionar una opcion en el campo Sexo");
                return;
            }

            // Verifica si la opción seleccionada en los DropDownList es "O"
            if (partes == "O" && espePartes == "")
            {
                MostrarError("El campo Especifique Parte debe estar lleno");
                return;
            }
            if (sexos == "O" && espeSexos == "")
            {
                MostrarError("El campo Especifique Sexo debe estar lleno");
                return;
            }

            // Asigna el valor del TextBox de "espeParte" y "espeSexo"
            if (partes == "O") partes = espePartes;
            if (sexos == "O") sexos = espeSexos;

            // Convierte "I" y "V" a "Imputado" y "Víctima" respectivamente
            if (partes == "I") partes = "Imputado";
            if (partes == "V") partes = "Víctima";

            // Convierte "F" y "M" a "Femenino" y "Masculino" respectivamente
            if (sexos == "F") sexos = "Femenino";
            else if (sexos == "M") sexos = "Masculino";

            DataTable dt = GetDataTable2();
            DataRow newRow = dt.NewRow();

            // Corrige la asignación para concatenar los valores con un espacio
            newRow["Nombre"] = nom + " " + ap + " " + am;
            newRow["Parte"] = partes;
            newRow["Genero"] = sexos;
            dt.Rows.Add(newRow);

            gvPartes.DataSource = dt;
            gvPartes.DataBind();

            // Si la primera fila es la fila vacía, elimínala
            if (dt.Rows.Count > 1 && dt.Rows[0][0] == DBNull.Value)
            {
                dt.Rows[0].Delete();
                gvPartes.DataSource = dt;
                gvPartes.DataBind();
            }

            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalGuardarDatos();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CamposVacios", "toastr.success('Partes Agregadas con exito');", true);


            //Obtener los valores de la fila seleccionada en gvPartes
            GridViewRow row = gvPartes.SelectedRow;

            if (row != null)
            {
                // Obtener los valores de las celdas en la fila seleccionada
                string nombre = row.Cells[0].Text; // La posición 0 corresponde a la columna de Nombre
                string genero = row.Cells[1].Text; // La posición 1 corresponde a la columna de Genero
                string tipoParte = row.Cells[2].Text; // La posición 2 corresponde a la columna de Parte

                // Obtener otros valores del formulario de "Agregar Partes"
                string especifiqueParte = espeParte.Text;
                string especifiqueSexo = espeSexo.Text;

                // Puedes desconcatenar el nombre si está en el formato "Nombre ApellidoPaterno ApellidoMaterno"
                string[] partesNombre = nombre.Split(' ');
                string nomParte = partesNombre[0];
                string apParte = partesNombre.Length > 1 ? partesNombre[1] : "";
                string amParte = partesNombre.Length > 2 ? partesNombre[2] : "";
            }
        }


        private void MostrarError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalPartes();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CamposVacios", $"toastr.error('{mensaje}');", true);
        }

        protected void gvPartes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarParte")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                DataTable dt = GetDataTable2();
                dt.Rows[rowIndex].Delete();

                // Si la tabla está vacía después de la eliminación, agrega una fila vacía
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvPartes.DataSource = dt;
                    gvPartes.DataBind();
                    gvPartes.Rows[0].Visible = false;
                }
                else
                {
                    gvPartes.DataSource = dt;
                    gvPartes.DataBind();
                }
            }
        }


        protected void gvPartes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra el índice de la columna 'Nombre'
                int nombreIndex = 0; // Asegúrate de reemplazar esto con el índice correcto
                e.Row.Cells[nombreIndex].Text = e.Row.Cells[nombreIndex].Text.ToUpper();
            }
        }


        //Fin back para agragar partes Exhorto


        //Back para agragar partes Despacho

        private DataTable GetDataTable3()
        {
            DataTable dt;
            if (ViewState["Partes2"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Parte", typeof(string));
                dt.Columns.Add("Genero", typeof(string));
                ViewState["Partes2"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Partes2"];
            }

            return dt;
        }

        protected void btnAgregarParte_Click2(object sender, EventArgs e)
        {

            string nom = nom3.Text;
            string ap = ap3.Text;
            string am = am3.Text;
            string partes = parte2.SelectedValue;
            string sexos = sexo2.SelectedValue;
            string espePartes = espeParte2.Text;
            string espeSexos = espeSexo2.Text;

            // Verificar si los campos están vacíos
            if (nom == "")
            {
                MostrarError2("El campo Nombre debe estar lleno");
                return;
            }
            if (ap == "")
            {
                MostrarError2("El campo Apellido Paterno debe estar lleno");
                return;
            }
            if (am == "")
            {
                MostrarError2("El campo Apellido Materno debe estar lleno");
                return;
            }
            if (partes == "Seleccionar")
            {
                MostrarError2("Debe seleccionar una opcion en el campo Partes");
                return;
            }
            if (sexos == "Seleccionar")
            {
                MostrarError2("Debe seleccionar una opcion en el campo Sexo");
                return;
            }

            // Verifica si la opción seleccionada en los DropDownList es "O"
            if (partes == "O" && espePartes == "")
            {
                MostrarError2("El campo Especifique Parte debe estar lleno");
                return;
            }
            if (sexos == "O" && espeSexos == "")
            {
                MostrarError2("El campo Especifique Sexo debe estar lleno");
                return;
            }

            // Asigna el valor del TextBox de "espeParte" y "espeSexo"
            if (partes == "O") partes = espePartes;
            if (sexos == "O") sexos = espeSexos;

            // Convierte "I" y "V" a "Imputado" y "Víctima" respectivamente
            if (partes == "I") partes = "Imputado";
            if (partes == "V") partes = "Víctima";

            // Convierte "F" y "M" a "Femenino" y "Masculino" respectivamente
            if (sexos == "F") sexos = "Femenino";
            else if (sexos == "M") sexos = "Masculino";

            DataTable dt = GetDataTable3();
            DataRow newRow = dt.NewRow();

            // Corrige la asignación para concatenar los valores con un espacio
            newRow["Nombre"] = nom + " " + ap + " " + am;
            newRow["Parte"] = partes;
            newRow["Genero"] = sexos;
            dt.Rows.Add(newRow);

            gvPartes2.DataSource = dt;
            gvPartes2.DataBind();

            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalGuardarDatos();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario2();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CamposVacios", "toastr.success('Partes Agregadas con exito');", true);

            //Obtener los valores de la fila seleccionada en gvPartes
            GridViewRow row = gvPartes2.SelectedRow;

            if (row != null)
            {
                // Obtener los valores de las celdas en la fila seleccionada
                string nombre = row.Cells[0].Text; // La posición 0 corresponde a la columna de Nombre
                string genero = row.Cells[1].Text; // La posición 1 corresponde a la columna de Genero
                string tipoParte = row.Cells[2].Text; // La posición 2 corresponde a la columna de Parte

                // Obtener otros valores del formulario de "Agregar Partes"
                string especifiqueParte = espeParte.Text;
                string especifiqueSexo = espeSexo.Text;

                // Puedes desconcatenar el nombre si está en el formato "Nombre ApellidoPaterno ApellidoMaterno"
                string[] partesNombre = nombre.Split(' ');
                string nomParte = partesNombre[0];
                string apParte = partesNombre.Length > 1 ? partesNombre[1] : "";
                string amParte = partesNombre.Length > 2 ? partesNombre[2] : "";
            }
        }

        private void MostrarError2(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalPartes();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario2();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CamposVacios", $"toastr.error('{mensaje}');", true);
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

        protected void GridView1_RowDataBound2(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra el índice de la columna 'Nombre'
                int nombreIndex = 0; // Asegúrate de reemplazar esto con el índice correcto
                e.Row.Cells[nombreIndex].Text = e.Row.Cells[nombreIndex].Text.ToUpper();
            }
        }

        //Fin back para agragar partes Despacho


        //Back para agragar partes requisitoria

        private DataTable GetDataTable4()
        {
            DataTable dt;
            if (ViewState["Partes3"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Parte", typeof(string));
                dt.Columns.Add("Genero", typeof(string));
                ViewState["Partes3"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Partes3"];
            }

            return dt;
        }

        protected void btnAgregarParte_Click3(object sender, EventArgs e)
        {
            string nom = nom4.Text;
            string ap = ap4.Text;
            string am = am4.Text;
            string partes = parte3.SelectedValue;
            string sexos = sexo3.SelectedValue;
            string espePartes = espeParte3.Text;
            string espeSexos = espeSexo3.Text;

            // Verificar si los campos están vacíos
            if (nom == "")
            {
                MostrarError3("El campo Nombre debe estar lleno");
                return;
            }
            if (ap == "")
            {
                MostrarError3("El campo Apellido Paterno debe estar lleno");
                return;
            }
            if (am == "")
            {
                MostrarError3("El campo Apellido Materno debe estar lleno");
                return;
            }
            if (partes == "Seleccionar")
            {
                MostrarError3("Debe seleccionar una opcion en el campo Partes");
                return;
            }
            if (sexos == "Seleccionar")
            {
                MostrarError3("Debe seleccionar una opcion en el campo Sexo");
                return;
            }

            // Verifica si la opción seleccionada en los DropDownList es "O"
            if (partes == "O" && espePartes == "")
            {
                MostrarError3("El campo Especifique Parte debe estar lleno");
                return;
            }
            if (sexos == "O" && espeSexos == "")
            {
                MostrarError3("El campo Especifique Sexo debe estar lleno");
                return;
            }

            // Asigna el valor del TextBox de "espeParte" y "espeSexo"
            if (partes == "O") partes = espePartes;
            if (sexos == "O") sexos = espeSexos;

            // Convierte "I" y "V" a "Imputado" y "Víctima" respectivamente
            if (partes == "I") partes = "Imputado";
            if (partes == "V") partes = "Víctima";

            // Convierte "F" y "M" a "Femenino" y "Masculino" respectivamente
            if (sexos == "F") sexos = "Femenino";
            else if (sexos == "M") sexos = "Masculino";

            DataTable dt = GetDataTable4();
            DataRow newRow = dt.NewRow();

            // Corrige la asignación para concatenar los valores con un espacio
            newRow["Nombre"] = nom + " " + ap + " " + am;
            newRow["Parte"] = partes;
            newRow["Genero"] = sexos;
            dt.Rows.Add(newRow);

            gvPartes3.DataSource = dt;
            gvPartes3.DataBind();

            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalGuardarDatos();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario3();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CamposVacios", "toastr.success('Partes Agregadas con exito');", true);

            //Obtener los valores de la fila seleccionada en gvPartes
            GridViewRow row = gvPartes3.SelectedRow;

            if (row != null)
            {
                // Obtener los valores de las celdas en la fila seleccionada
                string nombre = row.Cells[0].Text; // La posición 0 corresponde a la columna de Nombre
                string genero = row.Cells[1].Text; // La posición 1 corresponde a la columna de Genero
                string tipoParte = row.Cells[2].Text; // La posición 2 corresponde a la columna de Parte

                // Obtener otros valores del formulario de "Agregar Partes"
                string especifiqueParte = espeParte.Text;
                string especifiqueSexo = espeSexo.Text;

                // Puedes desconcatenar el nombre si está en el formato "Nombre ApellidoPaterno ApellidoMaterno"
                string[] partesNombre = nombre.Split(' ');
                string nomParte = partesNombre[0];
                string apParte = partesNombre.Length > 1 ? partesNombre[1] : "";
                string amParte = partesNombre.Length > 2 ? partesNombre[2] : "";
            }
        }

        private void MostrarError3(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "cerrarModal", "CerrarModalPartes();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarForm", "LimpiarFormulario3();", true);
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "CamposVacios", $"toastr.error('{mensaje}');", true);
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

        protected void GridView1_RowDataBound3(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra el índice de la columna 'Nombre'
                int nombreIndex = 0; // Asegúrate de reemplazar esto con el índice correcto
                e.Row.Cells[nombreIndex].Text = e.Row.Cells[nombreIndex].Text.ToUpper();
            }
        }
        //Fin back para agragar partes requisitoria


        protected void OpExhorto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = OpExhorto.SelectedValue == "E";
            Panel2.Visible = OpExhorto.SelectedValue == "D";
            Panel3.Visible = OpExhorto.SelectedValue == "R";

            // Ocultar todos los elementos h3
            titulo.Visible = false;
            lblExhorto.Visible = false;
            lblDespacho.Visible = false;
            lblRequisitoria.Visible = false;

            // Mostrar el elemento h3 correspondiente a la opción seleccionada
            switch (OpExhorto.SelectedValue)
            {
                case "SO":
                    titulo.Visible = true;
                    break;
                case "E":
                    lblExhorto.Visible = true;
                    break;
                case "D":
                    lblDespacho.Visible = true;
                    break;
                case "R":
                    lblRequisitoria.Visible = true;
                    break;
            }

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


        protected void formLimpio(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Limpiar", "limpiarFormularioInsert();", true);
        }

























        //AQUI EMPIEZA EL INSERT EXHORTO
        protected void btnGuardarDatosJudiciales_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Paso 0: Insertar AsignarFolio
                    using (SqlCommand command2 = new SqlCommand("AC_AsignarFolio", conn, transaction))
                    {
                        command2.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command2.Parameters.AddWithValue("@TipoDocumento", "EH");
                        command2.CommandType = CommandType.StoredProcedure;

                        using (var reader = command2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Folio nuevo 
                                var FolioNuevo = reader["FolioNuevo"];
                                HttpContext.Current.Session["IdFolioInicial"] = reader["IdFolio"];
                                HttpContext.Current.Session["FolioNuevoPInicial"] = FolioNuevo;

                                int folio = Convert.ToInt32(FolioNuevo);
                                int añoActual = DateTime.Now.Year;
                                string NumeroAsignado = folio.ToString("D4") + "/" + añoActual;
                                HttpContext.Current.Session["FolioNuevoInicial"] = NumeroAsignado;

                                Debug.WriteLine("Numero Asignado" + Session["FolioNuevoInicial"] + "IDFOLIO: " + Session["IdFolioInicial"] + "FOLIO NUEVO:" + Session["FolioNuevoPInicial"]);
                            }
                        }
                    }

                    // Confirmar la transacción si todo ha ido bien
                    using (SqlCommand command3 = new SqlCommand("AC_verificar_disponibilidad_folio_en_PAsunto", conn, transaction))
                    {
                        command3.Parameters.AddWithValue("@NuevoNumero", Session["FolioNuevoInicial"]);
                        command3.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command3.Parameters.AddWithValue("@TipoAsunto", "EH");
                        command3.CommandType = CommandType.StoredProcedure;

                        using (var reader = command3.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var IdAsuntoDuplicado = reader["IdAsunto"];

                                if (IdAsuntoDuplicado != DBNull.Value && Convert.ToInt32(IdAsuntoDuplicado) != 0)
                                {
                                    int idAsuntoDuplicado = Convert.ToInt32(IdAsuntoDuplicado);
                                    Debug.WriteLine("Error: Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);

                                    // Realizar Rollback para cancelar la transacción
                                    transaction.Rollback();
                                    throw new Exception("Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);
                                }
                            }
                        }
                    }

                    // Insertar datos en la tabla P_Asunto
                    int idAsunto = InsertarEnPAsunto(conn, transaction);

                    //// Insertar datos en la tabla P_PartesAsunto
                    InsertarEnPPartesAsunto(conn, transaction, idAsunto);

                    InsertarEnPAsuntoDelito(conn, transaction, idAsunto);

                    InsertarEnPTrayecto(conn, transaction, idAsunto);

                    InsertarEnPExhortos(conn, transaction, idAsunto);

                    InsertarEnPAnexos(conn, transaction, idAsunto);

                    // Actualizar el folio
                    using (SqlCommand command4 = new SqlCommand("AC_UpdateFolio", conn, transaction))
                    {
                        command4.Parameters.AddWithValue("@FolioNuevoI", Session["FolioNuevoPInicial"]);
                        command4.Parameters.AddWithValue("@IdFolio", Session["IdFolioInicial"]);
                        command4.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command4.CommandType = CommandType.StoredProcedure;

                        command4.ExecuteNonQuery();
                    }
                    // Si todo ha ido bien, confirmar la transacción
                    transaction.Commit();
                    InsertExhorto.Style.Add("display", "block");

                    // Generar el ticket
                    string ticket = CrearTicketSELLO();

                    // Insertar el ticket en el div
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");

                    // Ejecutar los scripts
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Exito", "EjemploExito();", true);
                    LimpiarYRestablecerPanel();
                    OpExhorto.Enabled = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ImprimirScript", "imprimirTicket();", true);


                }
                catch (Exception ex)
                {
                    // Manejar la excepción aquí
                    Debug.WriteLine("Error: " + ex.Message);

                    // Si la transacción todavía está activa, realizar Rollback
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }

                    // Manejar excepciones y realizar rollback si es necesario
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Limpiar", "limpiarFormularioInsert();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                    LimpiarYRestablecerPanel();
                }
                finally
                {
                    // Cerrar la conexión
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }


        private int InsertarEnPAsunto(SqlConnection conn, SqlTransaction transaction)
        {
            string Numero = Session["FolioNuevoInicial"] as string;
            int IdJuzgado = ObtenerIdJuzgadoDesdeSesion();

            string fIngresoTexto = fecha1.Text.ToUpper();
            DateTime fIngreso;

            if (DateTime.TryParse(fIngresoTexto, out fIngreso))
            {
                // La conversión fue exitosa, puedes usar fIngreso aquí
                string fIngresoFormateado = fIngreso.ToString("yyyy-MM-dd HH:mm:ss");
                // Utiliza fIngresoFormateado en tu consulta SQL
            }
            else
            {
                // La conversión falló, maneja el error aquí
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }

            string tAsunto = "E";
            string digital = "N";
            string fCaptura = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            int IdUsuario = ObtenerIdUsuarioDesdeSesion();
            int IdAudiencia = 0;
            string Observa = observa1.Text.ToUpper();
            string QuienIngresa = "".ToUpper();
            string MP = "".ToUpper();
            string nPrioridad = prioridad.SelectedValue.ToUpper();
            string nFojas = fojas1.Text.ToUpper();
            int idAsunto;

            // Realizar la inserción en P_Asunto
            string query = "INSERT INTO P_Asunto (Numero, IdJuzgado, FeIngreso, TipoAsunto, Digitalizado, FeCaptura, IdUsuario, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas) " +
            "VALUES (@Numero, @IdJuzgado, @FeIngreso, @TipoAsunto, @Digitalizado, @FeCaptura, @IdUsuario, @IdAudiencia, @Observaciones, @QuienIngresa, @MP, @Prioridad, @Fojas);" +
            "SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Numero", Numero);
                cmd.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                cmd.Parameters.AddWithValue("@FeIngreso", fIngreso);
                cmd.Parameters.AddWithValue("@TipoAsunto", tAsunto);
                cmd.Parameters.AddWithValue("@Digitalizado", digital);
                cmd.Parameters.AddWithValue("@FeCaptura", fCaptura);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@IdAudiencia", IdAudiencia);
                cmd.Parameters.AddWithValue("@QuienIngresa", QuienIngresa);
                cmd.Parameters.AddWithValue("@Observaciones", Observa);
                cmd.Parameters.AddWithValue("@MP", MP);
                cmd.Parameters.AddWithValue("@Prioridad", nPrioridad);
                cmd.Parameters.AddWithValue("@Fojas", nFojas);
                // Obtener el ID generado automáticamente
                idAsunto = Convert.ToInt32(cmd.ExecuteScalar());

                ProcesarDatosDeInsercion(fIngreso, Numero);


                return idAsunto;
            }
        }

        private void InsertarEnPPartesAsunto(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            foreach (GridViewRow row in gvPartes.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Obtén los datos de las celdas de la fila
                    string nombreCompleto = row.Cells[0].Text;
                    string genero = row.Cells[1].Text.Substring(0, 1).ToUpper();
                    string tipoParte = row.Cells[2].Text.Substring(0, 1).ToUpper();

                    // Desconcatena el nombre
                    string[] nombres = nombreCompleto.Split(' ');
                    string nomParte;
                    string apParte;
                    string amParte;

                    if (nombres.Length == 1)
                    {
                        nomParte = nombres[0];
                        apParte = "";
                        amParte = "";
                    }
                    else if (nombres.Length == 2)
                    {
                        nomParte = nombres[0];
                        apParte = nombres[1];
                        amParte = "";
                    }
                    else if (nombres.Length == 3)
                    {
                        nomParte = nombres[0];
                        apParte = nombres[1];
                        amParte = nombres[2];
                    }
                    else // nombres.Length > 3
                    {
                        nomParte = nombres[0] + " " + nombres[1];
                        apParte = nombres[2];
                        amParte = nombres[3];
                    }

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_PartesAsunto (IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias) VALUES (@IdAsunto, @Nombre, @APaterno, @AMaterno, @Genero, @TipoParte, NULL);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@Nombre", nomParte);
                        cmd.Parameters.AddWithValue("@APaterno", apParte);
                        cmd.Parameters.AddWithValue("@AMaterno", amParte);
                        cmd.Parameters.AddWithValue("@Genero", genero);
                        cmd.Parameters.AddWithValue("@TipoParte", tipoParte);
                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        private void InsertarEnPExhortos(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            string juzProce = procede1.Text.ToUpper();
            string asuProce = numdoc1.Text.ToUpper();
            string diligSoli = Diligencia1.Text.ToUpper();
            string tipoExor = OpExhorto.SelectedValue.ToUpper();
            string observa = observa1.Text.ToUpper();

            string query = "INSERT INTO P_Exhortos (IdAsunto, JuzgadoProce, AsuntoProce, DiligenciaSolicitada, TipoExhorto, Observación) VALUES (@IdAsunto, @JuzgadoProce, @AsuntoProce, @DiligenciaSolicitada, @TipoExhorto, @Observacion);";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@JuzgadoProce", juzProce);
                cmd.Parameters.AddWithValue("@AsuntoProce", asuProce);
                cmd.Parameters.AddWithValue("@DiligenciaSolicitada", diligSoli);
                cmd.Parameters.AddWithValue("@TipoExhorto", tipoExor);
                cmd.Parameters.AddWithValue("@Observacion", observa);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarEnPTrayecto(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            int posterior = 0;
            int actividad = 1;
            int perfil = 1;
            int IdUsuario = ObtenerIdUsuarioDesdeSesion();

            string fIngresoTexto = fecha1.Text;
            DateTime fIngreso;

            if (DateTime.TryParse(fIngresoTexto, out fIngreso))
            {
                // La conversión fue exitosa, puedes usar fIngreso aquí
                string fIngresoFormateado = fIngreso.ToString("yyyy-MM-dd HH:mm:ss");
                // Utiliza fIngresoFormateado en tu consulta SQL
            }
            else
            {
                // La conversión falló, maneja el error aquí
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }

            string tipo = "I";
            string estado = "A";

            string query = "INSERT INTO P_Trayecto (IdAsunto, IdPosterior, IdActividad, IdPerfil, IdUsuario, FeAsunto, Tipo, Estado) VALUES (@IdAsunto, @IdPosterior, @IdActividad, @IdPerfil, @IdUsuario, @FeAsunto, @Tipo, @Estado);";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@IdPosterior", posterior);
                cmd.Parameters.AddWithValue("@IdActividad", actividad);
                cmd.Parameters.AddWithValue("@IdPerfil", perfil);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@FeAsunto", fIngreso);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarEnPAsuntoDelito(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            foreach (GridViewRow row in gvDelitos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label lblIdDelito = (Label)row.FindControl("lblIdDelito");

                    // Obtén el valor de texto del control Label y conviértelo a int
                    int idDelito = int.Parse(lblIdDelito.Text);

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_AsuntoDelito (IdAsunto, IdDelito) VALUES (@IdAsunto, @IdDelito);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@IdDelito", idDelito);

                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        private void InsertarEnPAnexos(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            int idPost = 0;
            string digital = "N";

            foreach (GridViewRow row in gvAnexos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label descripcion = (Label)row.FindControl("lblAnexo");
                    Label cantidad = (Label)row.FindControl("lblCantAnexos");

                    // Convierte la descripción a mayúsculas antes de insertarla en la base de datos
                    string descripcionMayusculas = descripcion.Text.ToUpper();

                    // Verifica si la descripción y la cantidad están vacías antes de intentar insertarlas
                    if (!string.IsNullOrEmpty(descripcionMayusculas) && !string.IsNullOrEmpty(cantidad.Text))
                    {
                        // Realizar la inserción en P_PartesAsunto
                        string query = "INSERT INTO P_Anexos (IdAsunto, IdPosterior, Descripcion, Cantidad, Digitalizado) VALUES (@IdAsunto, @IdPosterior, @Descripcion, @Cantidad, @Digitalizado);";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                            cmd.Parameters.AddWithValue("@IdPosterior", idPost);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcionMayusculas); // Aquí está la corrección
                            cmd.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(cantidad.Text)); // Asegúrate de que este es el tipo correcto
                            cmd.Parameters.AddWithValue("@Digitalizado", digital);

                            ProcesarDatosDeInsercion2(descripcion, cantidad);

                            // Resto de los parámetros...

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }




        private int ObtenerIdUsuarioDesdeSesion()
        {
            int idUsuario = 0;
            if (HttpContext.Current.Session["IdUsuario"] != null)
            {
                idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            }
            return idUsuario;
        }

        private int ObtenerIdJuzgadoDesdeSesion()
        {
            // Asegúrate de que la clave de sesión sea la correcta
            if (HttpContext.Current.Session["IdJuzgado"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdJuzgado"]);
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return -1; // O un valor por defecto según tu lógica de negocio
        }

        private string ObtenerNombreJuzgadoDesdeSesion()
        {
            // Asegúrate de que la clave de sesión para el nombre del juzgado sea la correcta
            if (HttpContext.Current.Session["NombreJuzgado"] != null)
            {
                return HttpContext.Current.Session["NombreJuzgado"].ToString();
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return "Nombre de juzgado no disponible"; // O un valor por defecto según tu lógica de negocio
        }

        // Variables Globales
        public static string GlobalNumero;
        public static string GlobalFechaRecepcion;
        public static string GlobalNomJuzgado;
        public static string GlobalDescripcion;
        public static string GlobalCantidad;
        public static int GlobalExhorto = 0;

        private void ProcesarDatosDeInsercion(DateTime fIngresoFormateado, string Numero)
        {
            GlobalNumero = Numero;
            GlobalFechaRecepcion = fIngresoFormateado.ToString("yyyy-MM-dd HH:mm:ss");
            GlobalNomJuzgado = ObtenerNombreJuzgadoDesdeSesion();
        }

        private void ProcesarDatosDeInsercion2(Label descripcion, Label cantidad)
        {
            GlobalDescripcion = descripcion.Text;
            GlobalCantidad = cantidad.Text;
        }

        // Inicia desarrollo de sello
        private List<string> DividirTextoEnLineas(string texto, int maxCaracteresPorLinea)
        {
            List<string> lineas = new List<string>();
            string[] palabras = texto.Split(' ');
            string lineaActual = "";

            foreach (string palabra in palabras)
            {
                if ((lineaActual.Length > 0) && (lineaActual.Length + palabra.Length + 1 > maxCaracteresPorLinea))
                {
                    lineas.Add(lineaActual);
                    lineaActual = "";
                }

                if (lineaActual.Length > 0)
                    lineaActual += " ";

                lineaActual += palabra;
            }

            if (lineaActual.Length > 0)
                lineas.Add(lineaActual);

            return lineas;
        }


        public string CrearTicketSELLO()
        {
            StringBuilder ticket = new StringBuilder();
            string nombreJuzgado = GlobalNomJuzgado;
            List<string> lineasNombreJuzgado = DividirTextoEnLineas(nombreJuzgado, 30);
            int total = GlobalExhorto; // Inicializa total con GlobalExhorto
            int anchoLinea = 30; // Ancho de la línea

            // Encabezado del ticket
            ticket.AppendLine(CentrarTexto("TRIBUNAL SUPERIOR DE JUSTICIA", anchoLinea));
            ticket.AppendLine(CentrarTexto("DEL ESTADO DE HIDALGO", anchoLinea));
            ticket.AppendLine(CentrarTexto("ATENCION CIUDADANA", anchoLinea));
            ticket.AppendLine(CentrarTexto("EXHORTO", anchoLinea));
            ticket.AppendLine(new string('-', anchoLinea)); // Línea divisoria

            // Información del juzgado
            foreach (string linea in lineasNombreJuzgado)
            {
                ticket.AppendLine(linea);
            }
            ticket.AppendLine(new string('-', anchoLinea)); // Línea divisoria

            // Detalles del exhorto
            ticket.AppendLine($"Exhorto: {GlobalNumero}");
            ticket.AppendLine($"Fecha: {GlobalFechaRecepcion}");

            // Sección del total
            //ticket.AppendLine(AlinearTexto("EXHORTO", GlobalExhorto.ToString(), anchoLinea));

            foreach (GridViewRow row in gvAnexos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label descripcion = (Label)row.FindControl("lblAnexo");
                    Label cantidad = (Label)row.FindControl("lblCantAnexos");

                    // Convierte los textos a mayúsculas
                    string descripcionEnMayusculas = descripcion.Text.ToUpper();
                    string cantidadEnMayusculas = cantidad.Text.ToUpper();

                    // Verifica si la descripción y la cantidad están vacías antes de intentar agregarlas al ticket
                    if (!string.IsNullOrEmpty(descripcionEnMayusculas) && !string.IsNullOrEmpty(cantidadEnMayusculas))
                    {
                        ticket.AppendLine(AlinearTexto3(descripcionEnMayusculas, cantidadEnMayusculas, anchoLinea));
                        total += Convert.ToInt32(cantidad.Text);
                    }
                }
            }
            ticket.AppendLine(AlinearTexto("TOTAL", total.ToString(), anchoLinea));
            return ticket.ToString();
        }



        // Función para centrar texto
        private string CentrarTexto(string texto, int anchoLinea)
        {
            int espacios = (anchoLinea - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }

        // Función para alinear texto
        private string AlinearTexto(string textoIzquierda, string textoDerecha, int anchoLinea)
        {
            int espacios = anchoLinea - textoIzquierda.Length - textoDerecha.Length;
            return textoIzquierda + new string('.', espacios) + textoDerecha;
        }


        // Termina desarrollo de sello


        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ImprimirScript", "imprimirTicket();", true);
        }
        // Termina desarrollo de sello


        private void LimpiarYRestablecerPanel()
        {
            // Limpia y restablece los controles dentro del Panel1}
            OpExhorto.SelectedValue = "SO";
            numdoc1.Text = string.Empty;
            procede1.Text = string.Empty;
            fecha1.Text = string.Empty;
            fojas1.Text = "0";
            ddlDelitos1.SelectedIndex = 0;
            gvPartes.DataSource = null;
            gvPartes.DataBind();
            gvDelitos.DataSource = null;
            gvDelitos.DataBind();
            Diligencia1.Text = string.Empty;
            prioridad.SelectedIndex = 0;
            gvAnexos.DataSource = null;
            gvAnexos.DataBind();
            observa1.Text = string.Empty;
            // ... limpia otros controles según sea necesario ...

            // Oculta el Panel1
            Panel1.Visible = false;
        }

        //AQUI ACABA EL INSERT EXHORTO







        //AQUI EMPIEZA EL INSERT DESPACHO
        protected void btnGuardarDatosJudiciales_Click2(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Paso 0: Insertar AsignarFolio
                    using (SqlCommand command2 = new SqlCommand("AC_AsignarFolio", conn, transaction))
                    {
                        command2.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command2.Parameters.AddWithValue("@TipoDocumento", "EH");
                        command2.CommandType = CommandType.StoredProcedure;

                        using (var reader = command2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Folio nuevo 
                                var FolioNuevo = reader["FolioNuevo"];
                                HttpContext.Current.Session["IdFolioInicial"] = reader["IdFolio"];
                                HttpContext.Current.Session["FolioNuevoPInicial"] = FolioNuevo;

                                int folio = Convert.ToInt32(FolioNuevo);
                                int añoActual = DateTime.Now.Year;
                                string NumeroAsignado = folio.ToString("D4") + "/" + añoActual;
                                HttpContext.Current.Session["FolioNuevoInicial"] = NumeroAsignado;

                                Debug.WriteLine("Numero Asignado" + Session["FolioNuevoInicial"] + "IDFOLIO: " + Session["IdFolioInicial"] + "FOLIO NUEVO:" + Session["FolioNuevoPInicial"]);
                            }
                        }
                    }

                    // Confirmar la transacción si todo ha ido bien
                    using (SqlCommand command3 = new SqlCommand("AC_verificar_disponibilidad_folio_en_PAsunto", conn, transaction))
                    {
                        command3.Parameters.AddWithValue("@NuevoNumero", Session["FolioNuevoInicial"]);
                        command3.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command3.Parameters.AddWithValue("@TipoAsunto", "EH");
                        command3.CommandType = CommandType.StoredProcedure;

                        using (var reader = command3.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var IdAsuntoDuplicado = reader["IdAsunto"];

                                if (IdAsuntoDuplicado != DBNull.Value && Convert.ToInt32(IdAsuntoDuplicado) != 0)
                                {
                                    int idAsuntoDuplicado = Convert.ToInt32(IdAsuntoDuplicado);
                                    Debug.WriteLine("Error: Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);

                                    // Realizar Rollback para cancelar la transacción
                                    transaction.Rollback();
                                    throw new Exception("Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);
                                }
                            }
                        }
                    }

                    // Insertar datos en la tabla P_Asunto
                    int idAsunto = InsertarEnPAsunto2(conn, transaction);

                    //// Insertar datos en la tabla P_PartesAsunto
                    InsertarEnPPartesAsunto2(conn, transaction, idAsunto);

                    InsertarEnPAsuntoDelito2(conn, transaction, idAsunto);

                    InsertarEnPTrayecto2(conn, transaction, idAsunto);

                    InsertarEnPExhortos2(conn, transaction, idAsunto);

                    InsertarEnPAnexos2(conn, transaction, idAsunto);

                    // Actualizar el folio
                    using (SqlCommand command4 = new SqlCommand("AC_UpdateFolio", conn, transaction))
                    {
                        command4.Parameters.AddWithValue("@FolioNuevoI", Session["FolioNuevoPInicial"]);
                        command4.Parameters.AddWithValue("@IdFolio", Session["IdFolioInicial"]);
                        command4.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command4.CommandType = CommandType.StoredProcedure;

                        command4.ExecuteNonQuery();
                    }
                    // Si todo ha ido bien, confirmar la transacción
                    transaction.Commit();
                    InsertExhorto.Style.Add("display", "block");

                    // Generar el ticket
                    string ticket = CrearTicketSELLO2();

                    // Insertar el ticket en el div
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");

                    // Ejecutar los scripts
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Exito", "EjemploExito();", true);
                    LimpiarYRestablecerPanel2();
                    OpExhorto.Enabled = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ImprimirScript", "imprimirTicket();", true);


                }
                catch (Exception ex)
                {
                    // Manejar la excepción aquí
                    Debug.WriteLine("Error: " + ex.Message);

                    // Si la transacción todavía está activa, realizar Rollback
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }

                    // Manejar excepciones y realizar rollback si es necesario
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Limpiar", "limpiarFormularioInsert();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                    LimpiarYRestablecerPanel2();
                }
                finally
                {
                    // Cerrar la conexión
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }


        private int InsertarEnPAsunto2(SqlConnection conn, SqlTransaction transaction)
        {
            string Numero = Session["FolioNuevoInicial"] as string;
            int IdJuzgado = ObtenerIdJuzgadoDesdeSesion2();

            string fIngresoTexto = fecha2.Text;
            DateTime fIngreso;

            if (DateTime.TryParse(fIngresoTexto, out fIngreso))
            {
                // La conversión fue exitosa, puedes usar fIngreso aquí
                string fIngresoFormateado = fIngreso.ToString("yyyy-MM-dd HH:mm:ss");
                // Utiliza fIngresoFormateado en tu consulta SQL
            }
            else
            {
                // La conversión falló, maneja el error aquí
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }

            string tAsunto = "E";
            string digital = "N";
            string fCaptura = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string fCaptura = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            int IdUsuario = ObtenerIdUsuarioDesdeSesion2();
            int IdAudiencia = 0;
            string Observa = observa2.Text.ToUpper();
            string QuienIngresa = "";
            string MP = "";
            string nPrioridad = prioridad2.SelectedValue.ToUpper();
            string nFojas = fojas2.Text.ToUpper();
            int idAsunto;

            // Realizar la inserción en P_Asunto
            string query = "INSERT INTO P_Asunto (Numero, IdJuzgado, FeIngreso, TipoAsunto, Digitalizado, FeCaptura, IdUsuario, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas) " +
            "VALUES (@Numero, @IdJuzgado, @FeIngreso, @TipoAsunto, @Digitalizado, @FeCaptura, @IdUsuario, @IdAudiencia, @Observaciones, @QuienIngresa, @MP, @Prioridad, @Fojas);" +
            "SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Numero", Numero);
                cmd.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                cmd.Parameters.AddWithValue("@FeIngreso", fIngreso);
                cmd.Parameters.AddWithValue("@TipoAsunto", tAsunto);
                cmd.Parameters.AddWithValue("@Digitalizado", digital);
                cmd.Parameters.AddWithValue("@FeCaptura", fCaptura);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@IdAudiencia", IdAudiencia);
                cmd.Parameters.AddWithValue("@QuienIngresa", QuienIngresa);
                cmd.Parameters.AddWithValue("@Observaciones", Observa);
                cmd.Parameters.AddWithValue("@MP", MP);
                cmd.Parameters.AddWithValue("@Prioridad", nPrioridad);
                cmd.Parameters.AddWithValue("@Fojas", nFojas);
                // Obtener el ID generado automáticamente
                idAsunto = Convert.ToInt32(cmd.ExecuteScalar());

                ProcesarDatosDeInsercion3(fIngreso, Numero);

                return idAsunto;
            }
        }

        private void InsertarEnPPartesAsunto2(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            foreach (GridViewRow row in gvPartes2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Obtén los datos de las celdas de la fila
                    string nombreCompleto = row.Cells[0].Text;
                    string genero = row.Cells[1].Text.Substring(0, 1).ToUpper();
                    string tipoParte = row.Cells[2].Text.Substring(0, 1).ToUpper();

                    // Desconcatena el nombre
                    string[] nombres = nombreCompleto.Split(' ');
                    string nomParte;
                    string apParte;
                    string amParte;

                    if (nombres.Length == 1)
                    {
                        nomParte = nombres[0];
                        apParte = "";
                        amParte = "";
                    }
                    else if (nombres.Length == 2)
                    {
                        nomParte = nombres[0];
                        apParte = nombres[1];
                        amParte = "";
                    }
                    else if (nombres.Length == 3)
                    {
                        nomParte = nombres[0];
                        apParte = nombres[1];
                        amParte = nombres[2];
                    }
                    else // nombres.Length > 3
                    {
                        nomParte = nombres[0] + " " + nombres[1];
                        apParte = nombres[2];
                        amParte = nombres[3];
                    }

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_PartesAsunto (IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias) VALUES (@IdAsunto, @Nombre, @APaterno, @AMaterno, @Genero, @TipoParte, NULL);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@Nombre", nomParte);
                        cmd.Parameters.AddWithValue("@APaterno", apParte);
                        cmd.Parameters.AddWithValue("@AMaterno", amParte);
                        cmd.Parameters.AddWithValue("@Genero", genero);
                        cmd.Parameters.AddWithValue("@TipoParte", tipoParte);
                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        private void InsertarEnPExhortos2(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            string juzProce = quejoso.Text.ToUpper();
            string asuProce = numdesp.Text.ToUpper();
            string diligSoli = Diligencia2.Text.ToUpper();
            string tipoExor = OpExhorto.SelectedValue.ToUpper();
            string observa = observa2.Text.ToUpper();

            string query = "INSERT INTO P_Exhortos (IdAsunto, JuzgadoProce, AsuntoProce, DiligenciaSolicitada, TipoExhorto, Observación) VALUES (@IdAsunto, @JuzgadoProce, @AsuntoProce, @DiligenciaSolicitada, @TipoExhorto, @Observacion);";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@JuzgadoProce", juzProce);
                cmd.Parameters.AddWithValue("@AsuntoProce", asuProce);
                cmd.Parameters.AddWithValue("@DiligenciaSolicitada", diligSoli);
                cmd.Parameters.AddWithValue("@TipoExhorto", tipoExor);
                cmd.Parameters.AddWithValue("@Observacion", observa);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarEnPTrayecto2(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            int posterior = 0;
            int actividad = 1;
            int perfil = 1;
            int IdUsuario = ObtenerIdUsuarioDesdeSesion2();

            string fIngresoTexto = fecha2.Text;
            DateTime fIngreso;

            if (DateTime.TryParse(fIngresoTexto, out fIngreso))
            {
                // La conversión fue exitosa, puedes usar fIngreso aquí
                string fIngresoFormateado = fIngreso.ToString("yyyy-MM-dd HH:mm:ss");
                // Utiliza fIngresoFormateado en tu consulta SQL
            }
            else
            {
                // La conversión falló, maneja el error aquí
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }

            string tipo = "I";
            string estado = "A";

            string query = "INSERT INTO P_Trayecto (IdAsunto, IdPosterior, IdActividad, IdPerfil, IdUsuario, FeAsunto, Tipo, Estado) VALUES (@IdAsunto, @IdPosterior, @IdActividad, @IdPerfil, @IdUsuario, @FeAsunto, @Tipo, @Estado);";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@IdPosterior", posterior);
                cmd.Parameters.AddWithValue("@IdActividad", actividad);
                cmd.Parameters.AddWithValue("@IdPerfil", perfil);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@FeAsunto", fIngreso);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarEnPAsuntoDelito2(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            foreach (GridViewRow row in gvDelitos2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label lblIdDelito = (Label)row.FindControl("lblIdDelito2");

                    // Obtén el valor de texto del control Label y conviértelo a int
                    int idDelito = int.Parse(lblIdDelito.Text);

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_AsuntoDelito (IdAsunto, IdDelito) VALUES (@IdAsunto, @IdDelito);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@IdDelito", idDelito);

                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertarEnPAnexos2(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            int idPost = 0;
            string digital = "N";

            foreach (GridViewRow row in gvAnexos2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label descripcion = (Label)row.FindControl("lblAnexo2");
                    Label cantidad = (Label)row.FindControl("lblCantAnexos2");

                    // Convierte la descripción a mayúsculas antes de insertarla en la base de datos
                    string descripcionMayusculas = descripcion.Text.ToUpper();

                    // Verifica si la descripción y la cantidad están vacías antes de intentar insertarlas
                    if (!string.IsNullOrEmpty(descripcionMayusculas) && !string.IsNullOrEmpty(cantidad.Text))
                    {
                        // Realizar la inserción en P_PartesAsunto
                        string query = "INSERT INTO P_Anexos (IdAsunto, IdPosterior, Descripcion, Cantidad, Digitalizado) VALUES (@IdAsunto, @IdPosterior, @Descripcion, @Cantidad, @Digitalizado);";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                            cmd.Parameters.AddWithValue("@IdPosterior", idPost);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcionMayusculas); // Aquí está la corrección
                            cmd.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(cantidad.Text)); // Asegúrate de que este es el tipo correcto
                            cmd.Parameters.AddWithValue("@Digitalizado", digital);

                            ProcesarDatosDeInsercion4(descripcion, cantidad);

                            // Resto de los parámetros...

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        private int ObtenerIdUsuarioDesdeSesion2()
        {
            int idUsuario = 0;
            if (HttpContext.Current.Session["IdUsuario"] != null)
            {
                idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            }
            return idUsuario;
        }

        private int ObtenerIdJuzgadoDesdeSesion2()
        {
            // Asegúrate de que la clave de sesión sea la correcta
            if (HttpContext.Current.Session["IdJuzgado"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdJuzgado"]);
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return -1; // O un valor por defecto según tu lógica de negocio
        }

        private string ObtenerNombreJuzgadoDesdeSesion2()
        {
            // Asegúrate de que la clave de sesión para el nombre del juzgado sea la correcta
            if (HttpContext.Current.Session["NombreJuzgado"] != null)
            {
                return HttpContext.Current.Session["NombreJuzgado"].ToString();
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return "Nombre de juzgado no disponible"; // O un valor por defecto según tu lógica de negocio
        }

        // Variables Globales
        public static string GlobalNumero2;
        public static string GlobalFechaRecepcion2;
        public static string GlobalNomJuzgado2;
        public static string GlobalDescripcion2;
        public static string GlobalCantidad2;
        public static int GlobalExhorto2 = 0;

        private void ProcesarDatosDeInsercion3(DateTime fIngresoFormateado, string Numero)
        {
            GlobalNumero2 = Numero;
            GlobalFechaRecepcion2 = fIngresoFormateado.ToString("yyyy-MM-dd HH:mm:ss");
            GlobalNomJuzgado2 = ObtenerNombreJuzgadoDesdeSesion2();
        }

        private void ProcesarDatosDeInsercion4(Label descripcion, Label cantidad)
        {
            GlobalDescripcion2 = descripcion.Text;
            GlobalCantidad2 = cantidad.Text;
        }

        // Inicia desarrollo de sello
        private List<string> DividirTextoEnLineas2(string texto, int maxCaracteresPorLinea)
        {
            List<string> lineas2 = new List<string>();
            string[] palabras2 = texto.Split(' ');
            string lineaActual2 = "";

            foreach (string palabra in palabras2)
            {
                if ((lineaActual2.Length > 0) && (lineaActual2.Length + palabra.Length + 1 > maxCaracteresPorLinea))
                {
                    lineas2.Add(lineaActual2);
                    lineaActual2 = "";
                }

                if (lineaActual2.Length > 0)
                    lineaActual2 += " ";

                lineaActual2 += palabra;
            }

            if (lineaActual2.Length > 0)
                lineas2.Add(lineaActual2);

            return lineas2;
        }


        public string CrearTicketSELLO2()
        {
            StringBuilder ticket = new StringBuilder();
            string nombreJuzgado = GlobalNomJuzgado2;
            List<string> lineasNombreJuzgado = DividirTextoEnLineas2(nombreJuzgado, 30);
            int total = GlobalExhorto2; // Inicializa total con GlobalExhorto
            int anchoLinea = 30; // Ancho de la línea

            // Encabezado del ticket
            ticket.AppendLine(CentrarTexto2("TRIBUNAL SUPERIOR DE JUSTICIA", anchoLinea));
            ticket.AppendLine(CentrarTexto2("DEL ESTADO DE HIDALGO", anchoLinea));
            ticket.AppendLine(CentrarTexto2("ATENCION CIUDADANA", anchoLinea));
            ticket.AppendLine(CentrarTexto2("EXHORTO", anchoLinea));
            ticket.AppendLine(new string('-', anchoLinea)); // Línea divisoria

            // Información del juzgado
            foreach (string linea in lineasNombreJuzgado)
            {
                ticket.AppendLine(linea);
            }
            ticket.AppendLine(new string('-', anchoLinea)); // Línea divisoria

            // Detalles del exhorto
            ticket.AppendLine($"Exhorto: {GlobalNumero2}");
            ticket.AppendLine($"Fecha: {GlobalFechaRecepcion2}");

            // Sección del total
            //ticket.AppendLine(AlinearTexto("EXHORTO", GlobalExhorto.ToString(), anchoLinea));

            foreach (GridViewRow row in gvAnexos2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label descripcion = (Label)row.FindControl("lblAnexo2");
                    Label cantidad = (Label)row.FindControl("lblCantAnexos2");

                    // Convierte los textos a mayúsculas
                    string descripcionEnMayusculas = descripcion.Text.ToUpper();
                    string cantidadEnMayusculas = cantidad.Text.ToUpper();

                    // Verifica si la descripción y la cantidad están vacías antes de intentar agregarlas al ticket
                    if (!string.IsNullOrEmpty(descripcionEnMayusculas) && !string.IsNullOrEmpty(cantidadEnMayusculas))
                    {
                        ticket.AppendLine(AlinearTexto2(descripcionEnMayusculas, cantidadEnMayusculas, anchoLinea));
                        total += Convert.ToInt32(cantidad.Text);
                    }

                }
            }
            ticket.AppendLine(AlinearTexto2("TOTAL", total.ToString(), anchoLinea));
            return ticket.ToString();
        }

        // Función para centrar texto
        private string CentrarTexto2(string texto, int anchoLinea)
        {
            int espacios = (anchoLinea - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }

        // Función para alinear texto
        private string AlinearTexto2(string textoIzquierda, string textoDerecha, int anchoLinea)
        {
            int espacios = anchoLinea - textoIzquierda.Length - textoDerecha.Length;
            return textoIzquierda + new string('.', espacios) + textoDerecha;
        }


        // Termina desarrollo de sello


        protected void btnImprimir_Click2(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ImprimirScript", "imprimirTicket();", true);
        }
        // Termina desarrollo de sello


        private void LimpiarYRestablecerPanel2()
        {
            // Limpia y restablece los controles dentro de Panel2
            OpExhorto.SelectedValue = "SO";
            numdesp.Text = string.Empty;
            quejoso.Text = string.Empty;
            fecha2.Text = string.Empty;
            fojas2.Text = "0";
            ddlDelitos2.SelectedIndex = 0;
            gvPartes2.DataSource = null;
            gvPartes2.DataBind();
            gvDelitos2.DataSource = null;
            gvDelitos2.DataBind();
            Diligencia2.Text = string.Empty;
            prioridad2.SelectedIndex = 0;
            gvAnexos2.DataSource = null;
            gvAnexos2.DataBind();
            observa2.Text = string.Empty;

            // ... limpia otros controles según sea necesario ...

            // Oculta Panel2
            Panel2.Visible = false;
        }

        //AQUI ACABA EL INSERT DESPACHO








        //AQUI EMPIEZA EL INSERT REQUISITORIA

        protected void btnGuardarDatosJudiciales_Click3(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Paso 0: Insertar AsignarFolio
                    using (SqlCommand command2 = new SqlCommand("AC_AsignarFolio", conn, transaction))
                    {
                        command2.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command2.Parameters.AddWithValue("@TipoDocumento", "EH");
                        command2.CommandType = CommandType.StoredProcedure;

                        using (var reader = command2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Folio nuevo 
                                var FolioNuevo = reader["FolioNuevo"];
                                HttpContext.Current.Session["IdFolioInicial"] = reader["IdFolio"];
                                HttpContext.Current.Session["FolioNuevoPInicial"] = FolioNuevo;

                                int folio = Convert.ToInt32(FolioNuevo);
                                int añoActual = DateTime.Now.Year;
                                string NumeroAsignado = folio.ToString("D4") + "/" + añoActual;
                                HttpContext.Current.Session["FolioNuevoInicial"] = NumeroAsignado;

                                Debug.WriteLine("Numero Asignado" + Session["FolioNuevoInicial"] + "IDFOLIO: " + Session["IdFolioInicial"] + "FOLIO NUEVO:" + Session["FolioNuevoPInicial"]);
                            }
                        }
                    }

                    // Confirmar la transacción si todo ha ido bien
                    using (SqlCommand command3 = new SqlCommand("AC_verificar_disponibilidad_folio_en_PAsunto", conn, transaction))
                    {
                        command3.Parameters.AddWithValue("@NuevoNumero", Session["FolioNuevoInicial"]);
                        command3.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command3.Parameters.AddWithValue("@TipoAsunto", "EH");
                        command3.CommandType = CommandType.StoredProcedure;

                        using (var reader = command3.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var IdAsuntoDuplicado = reader["IdAsunto"];

                                if (IdAsuntoDuplicado != DBNull.Value && Convert.ToInt32(IdAsuntoDuplicado) != 0)
                                {
                                    int idAsuntoDuplicado = Convert.ToInt32(IdAsuntoDuplicado);
                                    Debug.WriteLine("Error: Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);

                                    // Realizar Rollback para cancelar la transacción
                                    transaction.Rollback();
                                    throw new Exception("Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);
                                }
                            }
                        }
                    }


                    // Insertar datos en la tabla P_Asunto
                    int idAsunto = InsertarEnPAsunto3(conn, transaction);

                    //// Insertar datos en la tabla P_PartesAsunto
                    InsertarEnPPartesAsunto3(conn, transaction, idAsunto);

                    InsertarEnPAsuntoDelito3(conn, transaction, idAsunto);

                    InsertarEnPTrayecto3(conn, transaction, idAsunto);

                    InsertarEnPExhortos3(conn, transaction, idAsunto);

                    InsertarEnPAnexos3(conn, transaction, idAsunto);

                    // Actualizar el folio
                    using (SqlCommand command4 = new SqlCommand("AC_UpdateFolio", conn, transaction))
                    {
                        command4.Parameters.AddWithValue("@FolioNuevoI", Session["FolioNuevoPInicial"]);
                        command4.Parameters.AddWithValue("@IdFolio", Session["IdFolioInicial"]);
                        command4.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command4.CommandType = CommandType.StoredProcedure;

                        command4.ExecuteNonQuery();
                    }
                    // Si todo ha ido bien, confirmar la transacción
                    transaction.Commit();
                    InsertExhorto.Style.Add("display", "block");

                    // Generar el ticket
                    string ticket = CrearTicketSELLO3();

                    // Insertar el ticket en el div
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");

                    // Ejecutar los scripts
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Exito", "EjemploExito();", true);
                    LimpiarYRestablecerPanel3();
                    OpExhorto.Enabled = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ImprimirScript", "imprimirTicket();", true);


                }
                catch (Exception ex)
                {
                    // Manejar la excepción aquí
                    Debug.WriteLine("Error: " + ex.Message);

                    // Si la transacción todavía está activa, realizar Rollback
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }

                    // Manejar excepciones y realizar rollback si es necesario
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Limpiar", "limpiarFormularioInsert();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                    LimpiarYRestablecerPanel3();
                }
                finally
                {
                    // Cerrar la conexión
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private int InsertarEnPAsunto3(SqlConnection conn, SqlTransaction transaction)
        {
            string Numero = Session["FolioNuevoInicial"] as string;
            int IdJuzgado = ObtenerIdJuzgadoDesdeSesion3();

            string fIngresoTexto = fecha3.Text;
            DateTime fIngreso;

            if (DateTime.TryParse(fIngresoTexto, out fIngreso))
            {
                // La conversión fue exitosa, puedes usar fIngreso aquí
                string fIngresoFormateado = fIngreso.ToString("yyyy-MM-dd HH:mm:ss");
                // Utiliza fIngresoFormateado en tu consulta SQL
            }
            else
            {
                // La conversión falló, maneja el error aquí
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }

            string tAsunto = "E";
            string digital = "N";
            string fCaptura = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string fCaptura = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            int IdUsuario = ObtenerIdUsuarioDesdeSesion3();
            int IdAudiencia = 0;
            string Observa = observa3.Text.ToUpper();
            string QuienIngresa = "";
            string MP = "";
            string nPrioridad = prioridad3.SelectedValue.ToUpper();
            string nFojas = fojas3.Text.ToUpper();
            int idAsunto;

            // Realizar la inserción en P_Asunto
            string query = "INSERT INTO P_Asunto (Numero, IdJuzgado, FeIngreso, TipoAsunto, Digitalizado, FeCaptura, IdUsuario, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas) " +
            "VALUES (@Numero, @IdJuzgado, @FeIngreso, @TipoAsunto, @Digitalizado, @FeCaptura, @IdUsuario, @IdAudiencia, @Observaciones, @QuienIngresa, @MP, @Prioridad, @Fojas);" +
            "SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Numero", Numero);
                cmd.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                cmd.Parameters.AddWithValue("@FeIngreso", fIngreso);
                cmd.Parameters.AddWithValue("@TipoAsunto", tAsunto);
                cmd.Parameters.AddWithValue("@Digitalizado", digital);
                cmd.Parameters.AddWithValue("@FeCaptura", fCaptura);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@IdAudiencia", IdAudiencia);
                cmd.Parameters.AddWithValue("@QuienIngresa", QuienIngresa);
                cmd.Parameters.AddWithValue("@Observaciones", Observa);
                cmd.Parameters.AddWithValue("@MP", MP);
                cmd.Parameters.AddWithValue("@Prioridad", nPrioridad);
                cmd.Parameters.AddWithValue("@Fojas", nFojas);

                ProcesarDatosDeInsercion5(fIngreso, Numero);

                // Obtener el ID generado automáticamente
                idAsunto = Convert.ToInt32(cmd.ExecuteScalar());

                return idAsunto;

            }

        }


        private void InsertarEnPPartesAsunto3(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            foreach (GridViewRow row in gvPartes3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Obtén los datos de las celdas de la fila
                    string nombreCompleto = row.Cells[0].Text;
                    string genero = row.Cells[1].Text.Substring(0, 1).ToUpper();
                    string tipoParte = row.Cells[2].Text.Substring(0, 1).ToUpper();

                    // Desconcatena el nombre
                    string[] nombres = nombreCompleto.Split(' ');
                    string nomParte;
                    string apParte;
                    string amParte;

                    if (nombres.Length == 1)
                    {
                        nomParte = nombres[0];
                        apParte = "";
                        amParte = "";
                    }
                    else if (nombres.Length == 2)
                    {
                        nomParte = nombres[0];
                        apParte = nombres[1];
                        amParte = "";
                    }
                    else if (nombres.Length == 3)
                    {
                        nomParte = nombres[0];
                        apParte = nombres[1];
                        amParte = nombres[2];
                    }
                    else // nombres.Length > 3
                    {
                        nomParte = nombres[0] + " " + nombres[1];
                        apParte = nombres[2];
                        amParte = nombres[3];
                    }

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_PartesAsunto (IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias) VALUES (@IdAsunto, @Nombre, @APaterno, @AMaterno, @Genero, @TipoParte, NULL);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@Nombre", nomParte);
                        cmd.Parameters.AddWithValue("@APaterno", apParte);
                        cmd.Parameters.AddWithValue("@AMaterno", amParte);
                        cmd.Parameters.AddWithValue("@Genero", genero);
                        cmd.Parameters.AddWithValue("@TipoParte", tipoParte);
                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertarEnPExhortos3(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            string juzProce = salaproc.Text.ToUpper();
            string asuProce = numtoca.Text.ToUpper();
            string diligSoli = Diligencia3.Text.ToUpper();
            string tipoExor = OpExhorto.SelectedValue.ToUpper();
            string observa = observa3.Text.ToUpper();

            string query = "INSERT INTO P_Exhortos (IdAsunto, JuzgadoProce, AsuntoProce, DiligenciaSolicitada, TipoExhorto, Observación) VALUES (@IdAsunto, @JuzgadoProce, @AsuntoProce, @DiligenciaSolicitada, @TipoExhorto, @Observacion);";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@JuzgadoProce", juzProce);
                cmd.Parameters.AddWithValue("@AsuntoProce", asuProce);
                cmd.Parameters.AddWithValue("@DiligenciaSolicitada", diligSoli);
                cmd.Parameters.AddWithValue("@TipoExhorto", tipoExor);
                cmd.Parameters.AddWithValue("@Observacion", observa);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarEnPTrayecto3(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            int posterior = 0;
            int actividad = 1;
            int perfil = 1;
            int IdUsuario = ObtenerIdUsuarioDesdeSesion3();

            string fIngresoTexto = fecha3.Text;
            DateTime fIngreso;

            if (DateTime.TryParse(fIngresoTexto, out fIngreso))
            {
                // La conversión fue exitosa, puedes usar fIngreso aquí
                string fIngresoFormateado = fIngreso.ToString("yyyy-MM-dd HH:mm:ss");
                // Utiliza fIngresoFormateado en tu consulta SQL
            }
            else
            {
                // La conversión falló, maneja el error aquí
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }

            string tipo = "I";
            string estado = "A";

            string query = "INSERT INTO P_Trayecto (IdAsunto, IdPosterior, IdActividad, IdPerfil, IdUsuario, FeAsunto, Tipo, Estado) VALUES (@IdAsunto, @IdPosterior, @IdActividad, @IdPerfil, @IdUsuario, @FeAsunto, @Tipo, @Estado);";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@IdPosterior", posterior);
                cmd.Parameters.AddWithValue("@IdActividad", actividad);
                cmd.Parameters.AddWithValue("@IdPerfil", perfil);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@FeAsunto", fIngreso);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarEnPAsuntoDelito3(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            foreach (GridViewRow row in gvDelitos3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label lblIdDelito = (Label)row.FindControl("lblIdDelito3");

                    // Obtén el valor de texto del control Label y conviértelo a int
                    int idDelito = int.Parse(lblIdDelito.Text);

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_AsuntoDelito (IdAsunto, IdDelito) VALUES (@IdAsunto, @IdDelito);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@IdDelito", idDelito);

                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        private void InsertarEnPAnexos3(SqlConnection conn, SqlTransaction transaction, int idAsunto)
        {
            int idPost = 0;
            string digital = "N";

            foreach (GridViewRow row in gvAnexos3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label descripcion = (Label)row.FindControl("lblAnexo3");
                    Label cantidad = (Label)row.FindControl("lblCantAnexos3");

                    // Convierte la descripción a mayúsculas antes de insertarla en la base de datos
                    string descripcionMayusculas = descripcion.Text.ToUpper();

                    // Verifica si la descripción y la cantidad están vacías antes de intentar insertarlas
                    if (!string.IsNullOrEmpty(descripcionMayusculas) && !string.IsNullOrEmpty(cantidad.Text))
                    {
                        // Realizar la inserción en P_PartesAsunto
                        string query = "INSERT INTO P_Anexos (IdAsunto, IdPosterior, Descripcion, Cantidad, Digitalizado) VALUES (@IdAsunto, @IdPosterior, @Descripcion, @Cantidad, @Digitalizado);";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                            cmd.Parameters.AddWithValue("@IdPosterior", idPost);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcionMayusculas); // Aquí está la corrección
                            cmd.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(cantidad.Text)); // Asegúrate de que este es el tipo correcto
                            cmd.Parameters.AddWithValue("@Digitalizado", digital);

                            ProcesarDatosDeInsercion6(descripcion, cantidad);

                            // Resto de los parámetros...

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private int ObtenerIdUsuarioDesdeSesion3()
        {
            int idUsuario = 0;
            if (HttpContext.Current.Session["IdUsuario"] != null)
            {
                idUsuario = Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            }
            return idUsuario;
        }

        private int ObtenerIdJuzgadoDesdeSesion3()
        {
            // Asegúrate de que la clave de sesión sea la correcta
            if (HttpContext.Current.Session["IdJuzgado"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdJuzgado"]);
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return -1; // O un valor por defecto según tu lógica de negocio
        }

        private string ObtenerNombreJuzgadoDesdeSesion3()
        {
            // Asegúrate de que la clave de sesión para el nombre del juzgado sea la correcta
            if (HttpContext.Current.Session["NombreJuzgado"] != null)
            {
                return HttpContext.Current.Session["NombreJuzgado"].ToString();
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return "Nombre de juzgado no disponible"; // O un valor por defecto según tu lógica de negocio
        }


        // Variables Globales
        public static string GlobalNumero3;
        public static string GlobalFechaRecepcion3;
        public static string GlobalNomJuzgado3;
        public static string GlobalDescripcion3;
        public static string GlobalCantidad3;
        public static int GlobalExhorto3 = 0;

        private void ProcesarDatosDeInsercion5(DateTime fIngresoFormateado, string Numero)
        {
            GlobalNumero3 = Numero;
            GlobalFechaRecepcion3 = fIngresoFormateado.ToString("yyyy-MM-dd HH:mm:ss");
            GlobalNomJuzgado3 = ObtenerNombreJuzgadoDesdeSesion3();
        }

        private void ProcesarDatosDeInsercion6(Label descripcion, Label cantidad)
        {
            GlobalDescripcion3 = descripcion.Text;
            GlobalCantidad3 = cantidad.Text;
        }

        // Inicia desarrollo de sello
        private List<string> DividirTextoEnLineas3(string texto, int maxCaracteresPorLinea)
        {
            List<string> lineas3 = new List<string>();
            string[] palabras3 = texto.Split(' ');
            string lineaActual3 = "";

            foreach (string palabra in palabras3)
            {
                if ((lineaActual3.Length > 0) && (lineaActual3.Length + palabra.Length + 1 > maxCaracteresPorLinea))
                {
                    lineas3.Add(lineaActual3);
                    lineaActual3 = "";
                }

                if (lineaActual3.Length > 0)
                    lineaActual3 += " ";

                lineaActual3 += palabra;
            }

            if (lineaActual3.Length > 0)
                lineas3.Add(lineaActual3);

            return lineas3;
        }


        public string CrearTicketSELLO3()
        {
            StringBuilder ticket = new StringBuilder();
            string nombreJuzgado = GlobalNomJuzgado3;
            List<string> lineasNombreJuzgado = DividirTextoEnLineas3(nombreJuzgado, 30);
            int total = GlobalExhorto3; // Inicializa total con GlobalExhorto
            int anchoLinea = 30; // Ancho de la línea

            // Encabezado del ticket
            ticket.AppendLine(CentrarTexto3("TRIBUNAL SUPERIOR DE JUSTICIA", anchoLinea));
            ticket.AppendLine(CentrarTexto3("DEL ESTADO DE HIDALGO", anchoLinea));
            ticket.AppendLine(CentrarTexto3("ATENCION CIUDADANA", anchoLinea));
            ticket.AppendLine(CentrarTexto3("EXHORTO", anchoLinea));
            ticket.AppendLine(new string('-', anchoLinea)); // Línea divisoria

            // Información del juzgado
            foreach (string linea in lineasNombreJuzgado)
            {
                ticket.AppendLine(linea);
            }
            ticket.AppendLine(new string('-', anchoLinea)); // Línea divisoria

            // Detalles del exhorto
            ticket.AppendLine($"Exhorto: {GlobalNumero3}");
            ticket.AppendLine($"Fecha: {GlobalFechaRecepcion3}");

            // Sección del total
            //ticket.AppendLine(AlinearTexto3("EXHORTO", GlobalExhorto3.ToString(), anchoLinea));

            foreach (GridViewRow row in gvAnexos3.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Encuentra el control Label dentro de la celda
                    Label descripcion = (Label)row.FindControl("lblAnexo3");
                    Label cantidad = (Label)row.FindControl("lblCantAnexos3");

                    // Convierte los textos a mayúsculas
                    string descripcionEnMayusculas = descripcion.Text.ToUpper();
                    string cantidadEnMayusculas = cantidad.Text.ToUpper();

                    // Verifica si la descripción y la cantidad están vacías antes de intentar agregarlas al ticket
                    if (!string.IsNullOrEmpty(descripcionEnMayusculas) && !string.IsNullOrEmpty(cantidadEnMayusculas))
                    {
                        ticket.AppendLine(AlinearTexto3(descripcionEnMayusculas, cantidadEnMayusculas, anchoLinea));
                        total += Convert.ToInt32(cantidad.Text);
                    }
                }
            }

            ticket.AppendLine(AlinearTexto3("TOTAL", total.ToString(), anchoLinea));

            return ticket.ToString();
        }

        // Función para centrar texto
        private string CentrarTexto3(string texto, int anchoLinea)
        {
            int espacios = (anchoLinea - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }

        // Función para alinear texto
        private string AlinearTexto3(string textoIzquierda, string textoDerecha, int anchoLinea)
        {
            int espacios = anchoLinea - textoIzquierda.Length - textoDerecha.Length;
            return textoIzquierda + new string('.', espacios) + textoDerecha;
        }


        // Termina desarrollo de sello


        protected void btnImprimir_Click3(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicket();", true);
        }
        // Termina desarrollo de sello

        private void LimpiarYRestablecerPanel3()
        {
            // Limpia y restablece los controles dentro del Panel3
            OpExhorto.SelectedValue = "SO";
            numtoca.Text = string.Empty;
            salaproc.Text = string.Empty;
            fecha3.Text = string.Empty;
            fojas3.Text = "0";
            ddlDelitos3.SelectedIndex = 0;
            gvPartes3.DataSource = null;
            gvPartes3.DataBind();
            gvDelitos3.DataSource = null;
            gvDelitos3.DataBind();
            Diligencia3.Text = string.Empty;
            prioridad3.SelectedIndex = 0;
            gvAnexos3.DataSource = null;
            gvAnexos3.DataBind();
            observa3.Text = string.Empty;
            // ... limpia otros controles según sea necesario ...

            // Oculta el Panel3
            Panel3.Visible = false;
        }
        //AQUI ACABA EL INSERT REQUISITORIA


        private void ReestablecerTodo()
        {
            // Limpia y restablece los controles dentro del Panel3
            OpExhorto.SelectedValue = "SO";
            numtoca.Text = string.Empty;
            salaproc.Text = string.Empty;
            fecha3.Text = string.Empty;
            fojas3.Text = "0";
            ddlDelitos3.SelectedIndex = 0;
            gvPartes3.DataSource = null;
            gvPartes3.DataBind();
            gvDelitos3.DataSource = null;
            gvDelitos3.DataBind();
            Diligencia3.Text = string.Empty;
            prioridad3.SelectedIndex = 0;
            gvAnexos3.DataSource = null;
            gvAnexos3.DataBind();
            observa3.Text = string.Empty;

            numdesp.Text = string.Empty;
            quejoso.Text = string.Empty;
            fecha2.Text = string.Empty;
            fojas2.Text = "0";
            ddlDelitos2.SelectedIndex = 0;
            gvPartes2.DataSource = null;
            gvPartes2.DataBind();
            gvDelitos2.DataSource = null;
            gvDelitos2.DataBind();
            Diligencia2.Text = string.Empty;
            prioridad2.SelectedIndex = 0;
            gvAnexos2.DataSource = null;
            gvAnexos2.DataBind();
            observa2.Text = string.Empty;

            numdoc1.Text = string.Empty;
            procede1.Text = string.Empty;
            fecha1.Text = string.Empty;
            fojas1.Text = "0";
            ddlDelitos1.SelectedIndex = 0;
            gvPartes.DataSource = null;
            gvPartes.DataBind();
            gvDelitos.DataSource = null;
            gvDelitos.DataBind();
            Diligencia1.Text = string.Empty;
            prioridad.SelectedIndex = 0;
            gvAnexos.DataSource = null;
            gvAnexos.DataBind();
            observa1.Text = string.Empty;
            // ... limpia otros controles según sea necesario ...
        }

        protected void GenerarOtro_Click(object sender, EventArgs e)
        {
            ReestablecerTodo();
            InsertExhorto.Style.Add("display", "none");
            OpExhorto.Enabled = true;
        }

        public static void ConvertirAMayusculas(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(obj);
                    if (value != null)
                    {
                        property.SetValue(obj, value.ToUpper());
                    }
                }
            }
        }


    }
}