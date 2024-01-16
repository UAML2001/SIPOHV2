using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Views.CustomRegistroIniciales;
using static SIPOH.Views.InicialAcusatorio;

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
                tablaDelitos.Columns.Add("IdDelito", typeof(string));
                tablaDelitos.Columns.Add("Nombre", typeof(string));
                ViewState["TablaDelitos"] = tablaDelitos;

                DataTable tablaPartes = new DataTable();
                tablaPartes.Columns.Add("Nombre", typeof(string));
                tablaPartes.Columns.Add("Genero", typeof(string));
                tablaPartes.Columns.Add("Parte", typeof(string));
                ViewState["TablaPartes"] = tablaPartes;


                DataTable tablaAnexos = new DataTable();
                tablaPartes.Columns.Add("descripcion", typeof(string));
                tablaPartes.Columns.Add("Cantidad", typeof(string));
                ViewState["tablaAnexos"] = tablaAnexos;


                // Cargar los delitos en el DropDownList
                CargarDelitos();
                CargarAnexos();
            }


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

            // Verificar si el delito ya está en la tabla
            if (!AnexosYaEnTabla(anexoSeleccionado))
            {
                // Agregar el delito a la tabla
                // Puedes agregar el código necesario para agregar el delito a tu fuente de datos

                string nombreAnexo = ddlAnexos.SelectedItem.Text;
                string cantidadAnexo = noAnexos.Text;

                DataTable dt = GetDataTableAnex();
                DataRow newRow = dt.NewRow();
                newRow["descripcion"] = nombreAnexo;
                newRow["Cantidad"] = cantidadAnexo;
                dt.Rows.Add(newRow);

                gvAnexos.DataSource = dt;
                gvAnexos.DataBind();

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

                gvAnexos.DataSource = dt;
                gvAnexos.DataBind();
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
        // Termina Back Anexo Exhorto






        // Back Anexo Despacho
        // Método para verificar si el delito ya está en la tabla
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

            // Verificar si el delito ya está en la tabla
            if (!AnexosYaEnTabla2(anexoSeleccionado))
            {
                // Agregar el delito a la tabla
                // Puedes agregar el código necesario para agregar el delito a tu fuente de datos

                string nombreAnexo = ddlAnexos2.SelectedItem.Text;
                string cantidadAnexo = noAnexos2.Text;

                DataTable dt = GetDataTableAnex2();
                DataRow newRow = dt.NewRow();
                newRow["descripcion2"] = nombreAnexo;
                newRow["Cantidad2"] = cantidadAnexo;
                dt.Rows.Add(newRow);

                gvAnexos2.DataSource = dt;
                gvAnexos2.DataBind();

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

                gvAnexos2.DataSource = dt;
                gvAnexos2.DataBind();
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

        // Termina Back Anexo Despacho



        // Back Anexo Despacho
        // Método para verificar si el delito ya está en la tabla
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

            // Verificar si el delito ya está en la tabla
            if (!AnexosYaEnTabla3(anexoSeleccionado))
            {
                // Agregar el delito a la tabla
                // Puedes agregar el código necesario para agregar el delito a tu fuente de datos

                string nombreAnexo = ddlAnexos3.SelectedItem.Text;
                string cantidadAnexo = noAnexos3.Text;

                DataTable dt = GetDataTableAnex3();
                DataRow newRow = dt.NewRow();
                newRow["descripcion3"] = nombreAnexo;
                newRow["Cantidad3"] = cantidadAnexo;
                dt.Rows.Add(newRow);

                gvAnexos3.DataSource = dt;
                gvAnexos3.DataBind();

                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "limpiarAnexos", "limpiarFormularioAnexos3();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "ErrorTblExhortos", "EjemploErrorTblAnexo();", true);
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

                gvAnexos3.DataSource = dt;
                gvAnexos3.DataBind();
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

        // Termina Back Anexo Despacho











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

            // Verificar si el delito ya está en la tabla
            if (!DelitoYaEnTabla(delitoSeleccionado))
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

                gvDelitos.DataSource = dt;
                gvDelitos.DataBind();
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

            // Verificar si el delito ya está en la tabla
            if (!DelitoYaEnTabla2(delitoSeleccionado))
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
            if (ViewState["Delitos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("IdDelito2", typeof(string));
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

            // Verificar si el delito ya está en la tabla
            if (!DelitoYaEnTabla3(delitoSeleccionado))
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
            if (ViewState["Delitos"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("IdDelito3", typeof(string));
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


            // Obtener los valores de la fila seleccionada en gvPartes
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
            string partes = parte2.SelectedValue;
            string sexox = sexo2.SelectedValue;
            string espePartes = espeParte2.Text;
            string espeSexox = espeSexo2.Text;

            // Verifica si la opción seleccionada en los DropDownList es "Otro"
            if (partes == "O" && sexox == "O")
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
            string partes = parte3.SelectedValue;
            string sexox = sexo3.SelectedValue;
            string espePartes = espeParte3.Text;
            string espeSexox = espeSexo3.Text;

            // Verifica si la opción seleccionada en los DropDownList es "Otro"
            if (partes == "O" && sexox == "O")
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

        public static class StorageFolio
        {
            public static int EjecutarControlAsignarFolio(SqlTransaction transaction)
            {
                using (SqlCommand cmd = new SqlCommand("Control_AsignarFolio", transaction.Connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@folio", SqlDbType.Int).Direction = ParameterDirection.Output; // Parámetro de salida para folio

                    cmd.ExecuteNonQuery();

                    int folio = (int)cmd.Parameters["@folio"].Value;  // Captura el valor del folio
                    Debug.WriteLine($"Folio obtenido: {folio}");
                    return folio;  // Devuelve el valor del folio
                }
            }
        }

        protected void btnGuardarDatosJudiciales_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Ejecutar el procedimiento almacenado para obtener el folio
                    int folio = StorageFolio.EjecutarControlAsignarFolio(transaction);


                    // Insertar datos en la tabla P_Asunto
                    int idAsunto = InsertarEnPAsunto(conn, transaction, folio);

                    // ActualizarFolios(conn, transaction, folio);

                    // Verificar si idAsunto es válido (no nulo o cero)
                    if (idAsunto == 0)
                    {
                        throw new Exception("No se pudo generar el idAsunto.");
                    }

                    //// Insertar datos en la tabla P_PartesAsunto
                    InsertarEnPPartesAsunto(conn, transaction, idAsunto);

                    InsertarEnPAsuntoDelito(conn, transaction, idAsunto);

                    InsertarEnPTrayecto(conn, transaction, idAsunto);

                    InsertarEnPExhortos(conn, transaction, idAsunto);

                    InsertarEnPAnexos(conn, transaction, idAsunto);

                    // Commit de la transacción
                    transaction.Commit();

                    // Mostrar mensajes y recargar la página si es necesario
                    // Mostrar un Toastr de confirmación

                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Limpiar", "limpiarFormularioInsert();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Exito", "EjemploExito();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);

                    string ticket = CrearTicketSELLO();
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicket();", true);

                }
                catch (SqlException sqlEx)
                {
                    // Revisar si la transacción está activa antes de hacer rollback
                    if (transaction != null && conn.State == System.Data.ConnectionState.Open)
                    {
                        transaction.Rollback();
                    }

                    // Manejar excepciones y realizar rollback si es necesario
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Limpiar", "limpiarFormularioInsert();", true);
                    //ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                    string errorMessage = sqlEx.Message;

                    // Registra el script de JavaScript con el mensaje de error.
                    string script = $"EjemploError('{errorMessage}');";
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Error", script, true);

                    // Registra los detalles de la excepción en la consola o en un archivo de registro
                    Console.WriteLine("Error SQL: " + sqlEx.Message);

                    foreach (SqlError error in sqlEx.Errors)
                    {
                        Console.WriteLine($"Número de error: {error.Number}");
                        Console.WriteLine($"Estado: {error.State}");
                        Console.WriteLine($"Procedimiento almacenado: {error.Procedure}");
                        Console.WriteLine($"Línea: {error.LineNumber}");
                        Console.WriteLine($"Mensaje: {error.Message}");
                        Console.WriteLine("==============================");
                    }

                    // Muestra un mensaje de error específico en el frontend
                    string mensaje = "Error SQL: " + sqlEx.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastErrorScript", script, true);

                    // Puedes decidir si lanzar una excepción aquí para manejar el rollback en el bloque try-catch
                    throw;
                }
                finally
                {
                    // Cerrar la conexión
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private int InsertarEnPAsunto(SqlConnection conn, SqlTransaction transaction, int folio)
        {
            int añoActual = DateTime.Now.Year;
            string Numero = $"{folio:0000}/{añoActual}";
            int IdJuzgado = ObtenerIdJuzgadoDesdeSesion();

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

            string tAsunto = "E";
            int digital = 0;
            string fCaptura = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            int IdUsuario = ObtenerIdUsuarioDesdeSesion();
            int IdAudiencia = 0;
            string Observa = observa1.Text;
            string QuienIngresa = "";
            string MP = "";
            string nPrioridad = prioridad.SelectedValue;
            string nFojas = fojas1.Text;
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
                    string genero = row.Cells[1].Text;
                    string tipoParte = row.Cells[2].Text;

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
            string juzProce = procede1.Text;
            string asuProce = numdoc1.Text;
            string diligSoli = Diligencia1.Text;
            string tipoExor = OpExhorto.SelectedValue;
            string observa = observa1.Text;

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
                    Label descripcion = (Label)row.FindControl("descripcion");
                    Label cantidad = (Label)row.FindControl("Cantidad");

                    // Realizar la inserción en P_PartesAsunto
                    string query = "INSERT INTO P_Anexos (IdAsunto, IdPosterior, Descripcion, Cantidad, Digitalizado) VALUES (@IdAsunto, @IdPosterior, @Descripcion, @Cantidad, @Digitalizado);";

                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@IdPosterior", idPost);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@Digitalizado", digital);

                        // Resto de los parámetros...

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //private void ActualizarFolios(SqlConnection conn, SqlTransaction transaction, int idJuzgado)
        //{
        //    string query = @"
        //    UPDATE [SIPOH].[dbo].[P_Folios]
        //    SET Folio = Folio + 1, frecuencia = frecuencia + 1
        //    WHERE IdJuzgado = @IdJuzgado AND Tipo = 'C'";

        //    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        //    {
        //        cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

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

        private void ProcesarDatosDeInsercion(DateTime fIngresoFormateado, string Numero)
        {
            GlobalNumero = Numero;
            GlobalFechaRecepcion = fIngresoFormateado.ToString("yyyy-MM-dd HH:mm:ss");
            GlobalNomJuzgado = ObtenerNombreJuzgadoDesdeSesion();
            // Nota: idEjecucion se almacena en GlobalesId.IdEjecucion AQUI ANDO
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

            // Encabezado del ticket
            ticket.AppendLine("TRIBUNAL SUPERIOR DE JUSTICIA");
            ticket.AppendLine("DEL ESTADO DE HIDALGO");
            ticket.AppendLine("ATENCION CIUDADANA");
            ticket.AppendLine("EXHORTO");
            ticket.AppendLine(new string('-', 30)); // Línea divisoria

            // Información del juzgado
            foreach (string linea in lineasNombreJuzgado)
            {
                ticket.AppendLine(linea);
            }
            ticket.AppendLine(new string('-', 30)); // Línea divisoria

            // Detalles del exhorto
            ticket.AppendLine($"Exhorto: {GlobalNumero}");
            ticket.AppendLine($"Fecha: {GlobalFechaRecepcion}");

            // Sección del total
            ticket.AppendLine("EXHORTO.................1");
            ticket.AppendLine("TOTAL..................1");

            return ticket.ToString();
        }


        // Termina desarrollo de sello


        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicket();", true);
        }
        // Termina desarrollo de sello



        //AQUI ACABA EL INSERT EXHORTO









        //AQUI EMPIEZA EL INSERT DESPACHO

        public static class StorageFolio2
        {
            public static int EjecutarControlAsignarFolio(SqlTransaction transaction)
            {
                using (SqlCommand cmd = new SqlCommand("Control_AsignarFolio", transaction.Connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@folio", SqlDbType.Int).Direction = ParameterDirection.Output; // Parámetro de salida para folio

                    cmd.ExecuteNonQuery();

                    int folio = (int)cmd.Parameters["@folio"].Value;  // Captura el valor del folio
                    Debug.WriteLine($"Folio obtenido: {folio}");
                    return folio;  // Devuelve el valor del folio
                }
            }
        }

        protected void btnGuardarDatosJudiciales_Click2(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Ejecutar el procedimiento almacenado para obtener el folio
                    int folio = StorageFolio2.EjecutarControlAsignarFolio(transaction);

                    //ActualizarFolios2(conn, transaction, idJuzgadoFolio);

                    // Insertar datos en la tabla P_Asunto
                    int idAsunto = InsertarEnPAsunto2(conn, transaction, folio);

                    //// Insertar datos en la tabla P_PartesAsunto
                    InsertarEnPPartesAsunto2(conn, transaction, idAsunto);

                    InsertarEnPAsuntoDelito2(conn, transaction, idAsunto);

                    InsertarEnPTrayecto2(conn, transaction, idAsunto);

                    InsertarEnPExhortos2(conn, transaction, idAsunto);

                    // Commit de la transacción
                    transaction.Commit();

                    // Mostrar mensajes y recargar la página si es necesario
                    // Mostrar un Toastr de confirmación

                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Exito", "EjemploExito();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                }
                catch (SqlException sqlEx)
                {

                    transaction.Rollback();

                    // Manejar excepciones y realizar rollback si es necesario
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                    string errorMessage = sqlEx.Message;

                    // Registra el script de JavaScript con el mensaje de error.
                    string script = $"EjemploError('{errorMessage}');";
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Error", script, true);

                    // Registra los detalles de la excepción en la consola o en un archivo de registro
                    Console.WriteLine("Error SQL: " + sqlEx.Message);

                    foreach (SqlError error in sqlEx.Errors)
                    {
                        Console.WriteLine($"Número de error: {error.Number}");
                        Console.WriteLine($"Estado: {error.State}");
                        Console.WriteLine($"Procedimiento almacenado: {error.Procedure}");
                        Console.WriteLine($"Línea: {error.LineNumber}");
                        Console.WriteLine($"Mensaje: {error.Message}");
                        Console.WriteLine("==============================");
                    }

                    // Muestra un mensaje de error específico en el frontend
                    string mensaje = "Error SQL: " + sqlEx.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastErrorScript", script, true);

                    // Puedes decidir si lanzar una excepción aquí para manejar el rollback en el bloque try-catch
                    throw;
                }
                finally
                {
                    // Cerrar la conexión
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private int InsertarEnPAsunto2(SqlConnection conn, SqlTransaction transaction, int folio)
        {
            int añoActual = DateTime.Now.Year;
            string Numero = $"{folio:0000}/{añoActual}";
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

            string tAsunto = "D";
            int digital = 0;
            string fCaptura = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string fCaptura = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            int IdUsuario = ObtenerIdUsuarioDesdeSesion2();
            int IdAudiencia = 0;
            string Observa = observa2.Text;
            string QuienIngresa = "";
            string MP = "";
            string nPrioridad = prioridad2.SelectedValue;
            string nFojas = fojas2.Text;
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
                    string genero = row.Cells[1].Text;
                    string tipoParte = row.Cells[2].Text;

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
            string juzProce = quejoso.Text;
            string asuProce = numdesp.Text;
            string diligSoli = Diligencia2.Text;
            string tipoExor = OpExhorto.SelectedValue;
            string observa = observa2.Text;

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
            // Obtener datos desde tu panel ASPX para P_PartesAsunto
            int idDelito;

            if (int.TryParse(ddlDelitos2.SelectedValue, out idDelito))
            {
                // Resto de los campos...

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
            else
            {
                // Manejar el caso donde ddlDelitos1.SelectedValue no es un número válido
                // Puedes mostrar un mensaje de error o tomar alguna acción específica
                Console.WriteLine($"El valor '{ddlDelitos2.SelectedValue}' no es un número válido para IdDelito.");
            }
        }

        //private void ActualizarFolios2(SqlConnection conn, SqlTransaction transaction, int idJuzgado)
        //{
        //    string query = @"
        //    UPDATE [SIPOH].[dbo].[P_Folios]
        //    SET Folio = Folio + 1, frecuencia = frecuencia + 1
        //    WHERE IdJuzgado = @IdJuzgado AND Tipo = 'C'";

        //    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        //    {
        //        cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

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

        //AQUI ACABA EL INSERT DESPACHO




















        //AQUI EMPIEZA EL INSERT REQUISITORIA
        public static class StorageFolio3
        {
            public static int EjecutarControlAsignarFolio(SqlTransaction transaction)
            {
                using (SqlCommand cmd = new SqlCommand("Control_AsignarFolio", transaction.Connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@folio", SqlDbType.Int).Direction = ParameterDirection.Output; // Parámetro de salida para folio

                    cmd.ExecuteNonQuery();

                    int folio = (int)cmd.Parameters["@folio"].Value;  // Captura el valor del folio
                    Debug.WriteLine($"Folio obtenido: {folio}");
                    return folio;  // Devuelve el valor del folio
                }
            }
        }

        protected void btnGuardarDatosJudiciales_Click3(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {

                    // Ejecutar el procedimiento almacenado para obtener el folio
                    int folio = StorageFolio3.EjecutarControlAsignarFolio(transaction);

                    //ActualizarFolios3(conn, transaction, idJuzgadoFolio);

                    // Insertar datos en la tabla P_Asunto
                    int idAsunto = InsertarEnPAsunto3(conn, transaction, folio);

                    //// Insertar datos en la tabla P_PartesAsunto
                    InsertarEnPPartesAsunto3(conn, transaction, idAsunto);

                    InsertarEnPAsuntoDelito3(conn, transaction, idAsunto);

                    InsertarEnPTrayecto3(conn, transaction, idAsunto);

                    InsertarEnPExhortos3(conn, transaction, idAsunto);

                    // Commit de la transacción
                    transaction.Commit();

                    // Mostrar mensajes y recargar la página si es necesario
                    // Mostrar un Toastr de confirmación

                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Exito", "EjemploExito();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                }
                catch (SqlException sqlEx)
                {

                    transaction.Rollback();

                    // Manejar excepciones y realizar rollback si es necesario
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Cerrar", "CerrarConfirmacion();", true);
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Recarga", "Recargar();", true);
                    string errorMessage = sqlEx.Message;

                    // Registra el script de JavaScript con el mensaje de error.
                    string script = $"EjemploError('{errorMessage}');";
                    ScriptManager.RegisterStartupScript(this.updPanel, this.updPanel.GetType(), "Error", script, true);

                    // Registra los detalles de la excepción en la consola o en un archivo de registro
                    Console.WriteLine("Error SQL: " + sqlEx.Message);

                    foreach (SqlError error in sqlEx.Errors)
                    {
                        Console.WriteLine($"Número de error: {error.Number}");
                        Console.WriteLine($"Estado: {error.State}");
                        Console.WriteLine($"Procedimiento almacenado: {error.Procedure}");
                        Console.WriteLine($"Línea: {error.LineNumber}");
                        Console.WriteLine($"Mensaje: {error.Message}");
                        Console.WriteLine("==============================");
                    }

                    // Muestra un mensaje de error específico en el frontend
                    string mensaje = "Error SQL: " + sqlEx.Message;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastErrorScript", script, true);

                    // Puedes decidir si lanzar una excepción aquí para manejar el rollback en el bloque try-catch
                    throw;
                }
                finally
                {
                    // Cerrar la conexión
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private int InsertarEnPAsunto3(SqlConnection conn, SqlTransaction transaction, int folio)
        {
            int añoActual = DateTime.Now.Year;
            string Numero = $"{folio:0000}/{añoActual}";
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

            string tAsunto = "R";
            int digital = 0;
            string fCaptura = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string fCaptura = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            int IdUsuario = ObtenerIdUsuarioDesdeSesion3();
            int IdAudiencia = 0;
            string Observa = observa3.Text;
            string QuienIngresa = "";
            string MP = "";
            string nPrioridad = prioridad3.SelectedValue;
            string nFojas = fojas3.Text;
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
                    string genero = row.Cells[1].Text;
                    string tipoParte = row.Cells[2].Text;

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
            string juzProce = salaproc.Text;
            string asuProce = numtoca.Text;
            string diligSoli = Diligencia3.Text;
            string tipoExor = OpExhorto.SelectedValue;
            string observa = observa3.Text;

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
            // Obtener datos desde tu panel ASPX para P_PartesAsunto
            int idDelito;

            if (int.TryParse(ddlDelitos3.SelectedValue, out idDelito))
            {
                // Resto de los campos...

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
            else
            {
                // Manejar el caso donde ddlDelitos1.SelectedValue no es un número válido
                // Puedes mostrar un mensaje de error o tomar alguna acción específica
                Console.WriteLine($"El valor '{ddlDelitos1.SelectedValue}' no es un número válido para IdDelito.");
            }
        }

        //private void ActualizarFolios3(SqlConnection conn, SqlTransaction transaction, int idJuzgado)
        //{
        //    string query = @"
        //    UPDATE [SIPOH].[dbo].[P_Folios]
        //    SET Folio = Folio + 1, frecuencia = frecuencia + 1
        //    WHERE IdJuzgado = @IdJuzgado AND Tipo = 'C'";

        //    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
        //    {
        //        cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

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
    }
    //AQUI ACABA EL INSERT REQUISITORIA

}