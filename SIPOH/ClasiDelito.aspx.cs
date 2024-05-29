using SIPOH.Controllers.AC_JefeUnidadCausa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class clasidelito : GeneralesyWebUi
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int sessionTimeout = 1 * 60;
                Session.Timeout = sessionTimeout;
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                string circuito = HttpContext.Current.Session["TCircuito"] as string;
                List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;
                bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/clasideli")) : false;
                if (enlaces == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if ((circuito == "c" || circuito == "e") && tienePermiso)
                {
                    Visible = true;
                }
                else
                {
                    Visible = false;
                    Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
                }
                GridViewClasificacionDelitos.DataSource = InicializaTablaVaciaDelitos();
                GridViewClasificacionDelitos.DataBind();
                CargarCatalogos loader = new CargarCatalogos();
                loader.LoadGradosConsumacion(ddlGradoConsumacion);
                loader.LoadConcursos(ddlConcurso);
                loader.LoadFormasAccion(ddlFormaAccion);
                loader.LoadCalificaciones(ddlCalificacion);
                loader.LoadClasificaciones(ddlOrdenResultado);
                loader.LoadElementosComision(ddlComision);
                loader.LoadFormasComision(ddlFormaComision);
                loader.LoadModalidades(ddlModalidad);
                loader.LoadMunicipios(ddlCatMunicipios);
                loader.LoadDelitos(ddDelitos);
                CargarCatalogos cargador = new CargarCatalogos();
                //checar esto que inicialice en invisible
                divFechaReclasificacion.Style["display"] = "none";
                divCheckReclasificar.Style["display"] = "none";
            }
        }
        protected void checkNoIdentificado_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNoIdentificado.Checked)
            {
                FechaDelito.Text = "1899-09-09";
            }
            else
            {
                FechaDelito.Text = "";
            }
        }
        protected void reclasificacionSi_CheckedChanged(object sender, EventArgs e)
        {
            divFechaReclasificacion.Style["display"] = reclasificacionSi.Checked ? "" : "none";
            divAgregarClasificacion.Style["display"] = reclasificacionSi.Checked ? "" : "none";
            CargarCatDelitos();
            ddDelitos.SelectedIndex = 0;
            ddlModDelito.Items.Clear();
            ddlModDelito.Items.Insert(0, new ListItem("-- SELECCIONAR --", "0"));
            divModalidad.Style["display"] = "none";
        }
        protected void CargarCatDelitos()
        {
            CargarCatalogos cargarCatalogos = new CargarCatalogos();
            int idDelitoActual = Convert.ToInt32(ddDelitos.SelectedValue);
            ddDelitos.Items.Clear();
            cargarCatalogos.LoadDelitos(ddDelitos);
            ddDelitos.SelectedValue = idDelitoActual.ToString();
            if (ddDelitos.SelectedIndex == -1)
                ddDelitos.SelectedIndex = 0;
            ddDelitos.AutoPostBack = true;
            ddDelitos.SelectedIndexChanged += ddDelitos_SelectedIndexChanged;
        }
        protected void ddlTipoAsunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoAsunto.SelectedValue == "JO")
            {
                btnAgregarClasiDelito.Enabled = false;
                btnAgregarClasiDelito.Visible = false;
            }
            else
            {
                btnAgregarClasiDelito.Enabled = true;
                btnAgregarClasiDelito.Visible = true;
            }
        }
        protected void ddDelitos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDelito = Convert.ToInt32(ddDelitos.SelectedValue);
            if (idDelito == 405)
            {
                ddlModDelito.Visible = true;
                divModalidad.Style["display"] = "block";
                ddlModDelito.Items.Clear();
                ddlModDelito.Items.Insert(0, new ListItem("-- SELECCIONAR --", "0"));
                new CargarCatalogos().LoadModalidadPorIdDelDetalle(ddlModDelito, idDelito, null);
                ddlModDelito.SelectedIndex = 0;
            }
            else
            {
                ddlModDelito.Visible = false;
                divModalidad.Style["display"] = "none";
                ddlModDelito.Items.Clear();
                ddlModDelito.Items.Insert(0, new ListItem("-- SELECCIONAR --", "0"));
            }
        }
        protected void Limpiar()
        {
            DropDownList[] dropdowns = new DropDownList[] {
                ddlModDelito, ddlCatMunicipios, ddlGradoConsumacion, ddlConcurso,
                ddlFormaAccion, ddlCalificacion, ddlOrdenResultado,
                ddlComision, ddlFormaComision, ddlModalidad, ddlModDelito
            };
            foreach (var ddl in dropdowns)
            {
                if (ddl.Items.Count > 0)
                    ddl.SelectedIndex = 0;
            }
            ddlModDelito.Visible = false;
            divModalidad.Style["display"] = "none";
            ddDelitos.Enabled = true;
            rbQuerella.Checked = false;
            rbDenuncia.Checked = false;
            rbNoIdentificado.Checked = false;
            txtLocalidad.Text = "";
            CargarCatalogos cargarCatalogos = new CargarCatalogos();
            cargarCatalogos.LoadDelitos(ddDelitos);
            FechaDelito.Text = "";

            // Limpiar la selección del GridView usando CSS
            if (ViewState["SelectedRowIndex"] != null)
            {
                int previousIndex = (int)ViewState["SelectedRowIndex"];
                if (previousIndex > -1 && previousIndex < GridViewClasificacionDelitos.Rows.Count)
                {
                    GridViewClasificacionDelitos.Rows[previousIndex].CssClass = "";
                }
                ViewState["SelectedRowIndex"] = -1; // Resetear el índice seleccionado
            }
        }

        protected void btnAgregarClasiDelito_Click(object sender, EventArgs e)
        {
            if (GridViewClasificacionDelitos.Rows.Count == 0)
            {
                MensajeAdvertencia("Necesitas buscar un asunto para poder agregar más delitos.");
                return;
            }
            Limpiar();
            divAgregarClasificacion.Style["display"] = "block";
            divCheckReclasificar.Style["display"] = "none";
            divFechaReclasificacion.Style["display"] = "none";
        }
        private DataTable InicializaTablaVaciaDelitos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Consumacion", typeof(string));
            dt.Columns.Add("Calificacion", typeof(string));
            dt.Columns.Add("Concurso", typeof(string));
            dt.Columns.Add("Clasificacion", typeof(string));
            dt.Columns.Add("Comision", typeof(string));
            dt.Columns.Add("Accion", typeof(string));
            dt.Columns.Add("Modalidad", typeof(string));
            dt.Columns.Add("ElemComision", typeof(string));
            dt.Columns.Add("Municipio", typeof(string));
            dt.Columns.Add("Persecucion", typeof(string));
            dt.Columns.Add("FeDelito", typeof(string));
            dt.Columns.Add("Domicilio", typeof(string));
            return dt;
        }
        protected void btnBuscarClasiDelito_Click(object sender, EventArgs e)
        {
            Limpiar();
            ddDelitos.Enabled = false;
            divFechaReclasificacion.Style["display"] = "none";
            divCheckReclasificar.Style["display"] = "none";
            divAgregarClasificacion.Style["display"] = "none";
            string tipoAsunto = ddlTipoAsunto.SelectedValue;
            string numeroAsunto = txtNumeroAsunto.Text;
            // Recuperar el ID del juzgado desde la sesión
            int idJuzgado;
            if (HttpContext.Current.Session["IDJuzgado"] != null && int.TryParse(HttpContext.Current.Session["IDJuzgado"].ToString(), out idJuzgado))
            {
                // Continúa con la lógica si el IDJuzgado es válido
                if (string.IsNullOrWhiteSpace(tipoAsunto) || string.IsNullOrWhiteSpace(numeroAsunto))
                {
                    MensajeError("Por favor, selecciona un tipo de asunto e ingresa un número de asunto.");
                    return;
                }
                JUC_ClasificacionDelitoController controller = new JUC_ClasificacionDelitoController();
                DataTable dt = controller.BuscarDelitos(tipoAsunto, numeroAsunto, idJuzgado);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MensajeAdvertencia("No se encontraron resultados para el tipo de asunto y número proporcionados.");
                    GridViewClasificacionDelitos.DataSource = InicializaTablaVaciaDelitos();
                    GridViewClasificacionDelitos.DataBind();
                    return;
                }
                GridViewClasificacionDelitos.DataSource = dt;
                GridViewClasificacionDelitos.DataBind();
                MensajeExito("Delitos encontrados con éxito.");
                divCheckReclasificar.Style["display"] = "none";
            }
            else
            {
                MensajeError("No se ha podido identificar el juzgado correspondiente. Por favor, inicie sesión nuevamente.");
            }
        }
        protected void GridViewClasificacionDelitos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["SelectedRowIndex"] != null)
                {
                    int previousIndex = (int)ViewState["SelectedRowIndex"];
                    if (previousIndex > -1 && previousIndex < GridViewClasificacionDelitos.Rows.Count)
                    {
                        GridViewClasificacionDelitos.Rows[previousIndex].CssClass = "";
                    }
                }
                divCheckReclasificar.Style["display"] = "block";
                GridViewRow row = GridViewClasificacionDelitos.SelectedRow;
                if (row != null)
                {
                    row.CssClass = "table-success";
                    ViewState["SelectedRowIndex"] = row.RowIndex;
                    // Carga detalles del delito seleccionado
                    int idDeliAsunto = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDeliAsunto"]);
                    JUC_CatDelitosController controller = new JUC_CatDelitosController();
                    var delito = JUC_CatDelitosController.GetDelitoByDeliAsunto(idDeliAsunto);
                    if (delito != null)
                    {
                        controller.LoadDelitoByDeliAsunto(ddDelitos, idDeliAsunto);
                        ddDelitos.Enabled = true;
                        ddDelitos.SelectedValue = delito.IdDelito.ToString();
                        // Manejo específico para delitos con IdDelito 405
                        if (delito.IdDelito == 405)
                        {
                            ddlModDelito.Visible = true;
                            divModalidad.Style["display"] = "block";
                            int idDelDetalle = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelDetalle"] ?? 0);
                            CargarCatalogos cargarCatalogos = new CargarCatalogos();
                            cargarCatalogos.LoadModalidadPorIdDelDetalle(ddlModDelito, delito.IdDelito, idDelDetalle);
                            ddlModDelito.SelectedValue = idDelDetalle.ToString();
                        }
                        else
                        {
                            ddlModDelito.Visible = false;
                            divModalidad.Style["display"] = "none";
                        }
                    }
                    // Verificar si hay datos esenciales vacíos
                    if (GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdMunicipio"] == DBNull.Value ||
                        GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Persecucion"] == DBNull.Value)
                    {
                        MensajeAdvertencia("Hay datos vacíos en la tabla, te recomendamos actualizar el delito con su información");
                        divAgregarClasificacion.Style["display"] = "block";
                        return;
                    }
                    // Actualización de DropDownList y detalles de localización
                    UpdateDropDownLists(row);
                    SetLocationDetails(row);
                    string tipoPersecucion = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Persecucion"].ToString();
                    SeleccionarTipoPersecucion(tipoPersecucion);
                    divAgregarClasificacion.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en la selección del GridView: " + ex.Message);
                MensajeError("Error al actualizar la selección: " + ex.Message);
                divAgregarClasificacion.Style["display"] = "none";
            }
        }
        //Actualiza los dropdown 
        private void UpdateDropDownLists(GridViewRow row)
        {
            SetDropDownValue(ddlCatMunicipios, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdMunicipio"]));
            SetDropDownValue(ddlGradoConsumacion, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatConsumacion"]));
            SetDropDownValue(ddlConcurso, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatConcurso"]));
            SetDropDownValue(ddlFormaAccion, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatAccion"]));
            SetDropDownValue(ddlCalificacion, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatCalificacion"]));
            SetDropDownValue(ddlOrdenResultado, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatClasificacion"]));
            SetDropDownValue(ddlComision, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatComision"]));
            SetDropDownValue(ddlFormaComision, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatElemComision"]));
            SetDropDownValue(ddlModalidad, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatModalidad"]));
        }
        //Agrega un valor a el item de un dll por parametro
        private void SetDropDownValue(DropDownList ddl, int value)
        {
            if (ddl.Items.FindByValue(value.ToString()) != null)
            {
                ddl.SelectedValue = value.ToString();
            }
        }
        //Actualiza los campos de texto relacionados con la localización y fecha del delito basado en la fila seleccionada del GridView.
        private void SetLocationDetails(GridViewRow row)
        {
            string fechaDelito = Convert.ToDateTime(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["FeDelito"]).ToString("yyyy-MM-dd");
            string domicilio = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Domicilio"].ToString();
            txtLocalidad.Text = domicilio;
            FechaDelito.Text = fechaDelito;
        }
        //Configura el estado de los radio buttons relacionados con el tipo de persecución del delito.
        private void SeleccionarTipoPersecucion(string tipoPersecucion)
        {
            rbQuerella.Checked = false;
            rbDenuncia.Checked = false;
            rbNoIdentificado.Checked = false;
            switch (tipoPersecucion)
            {
                case "Q":
                    rbQuerella.Checked = true;
                    break;
                case "D":
                    rbDenuncia.Checked = true;
                    break;
                case "N":
                    rbNoIdentificado.Checked = true;
                    break;
                default:
                    break;
            }
        }
        //
    }
}