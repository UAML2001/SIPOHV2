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
        }
        protected void btnAgregarClasiDelito_Click(object sender, EventArgs e)
        {
            if (GridViewClasificacionDelitos.Rows.Count == 0)
            {
                MensajeAdvertencia("Necesitas buscar un asunto para poder agregar más delitos.");
                return;
            }
            divAgregarClasificacion.Style["display"] = "block";
            divCheckReclasificar.Style["display"] = "none";
            divFechaReclasificacion.Style["display"] = "none";
            Limpiar();
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
                    // En caso de que el valor no coincida, no se selecciona ningún RadioButton
                    break;
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
                    int idDeliAsunto = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDeliAsunto"]);
                    int idDelDetalle = 0;
                    if (GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelDetalle"] != DBNull.Value)
                    {
                        idDelDetalle = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelDetalle"]);
                    }
                    int idMunicipio = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdMunicipio"]);
                    if (ddlCatMunicipios.Items.FindByValue(idMunicipio.ToString()) != null)
                    {
                        ddlCatMunicipios.SelectedValue = idMunicipio.ToString();
                    }
                    int idCatConsumacion = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatConsumacion"]);
                    if (ddlGradoConsumacion.Items.FindByValue(idCatConsumacion.ToString()) != null)
                    {
                        ddlGradoConsumacion.SelectedValue = idCatConsumacion.ToString();
                    }
                    int idCatConcurso = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatConcurso"]);
                    if (ddlConcurso.Items.FindByValue(idCatConcurso.ToString()) != null)
                    {
                        ddlConcurso.SelectedValue = idCatConcurso.ToString();
                    }
                    int idCatFormaAccion = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatAccion"]);
                    if (ddlFormaAccion.Items.FindByValue(idCatFormaAccion.ToString()) != null)
                    {
                        ddlFormaAccion.SelectedValue = idCatFormaAccion.ToString();
                    }
                    int idCatCalificacion = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatCalificacion"]);
                    if (ddlCalificacion.Items.FindByValue(idCatCalificacion.ToString()) != null)
                    {
                        ddlCalificacion.SelectedValue = idCatCalificacion.ToString();
                    }
                    int idCatOrdenResult = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatClasificacion"]);
                    if (ddlOrdenResultado.Items.FindByValue(idCatOrdenResult.ToString()) != null)
                    {
                        ddlOrdenResultado.SelectedValue = idCatOrdenResult.ToString();
                    }
                    int idCatComision = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatComision"]);
                    if (ddlComision.Items.FindByValue(idCatComision.ToString()) != null)
                    {
                        ddlComision.SelectedValue = idCatComision.ToString();
                    }
                    int idCatFormaComision = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatElemComision"]);
                    if (ddlFormaComision.Items.FindByValue(idCatFormaComision.ToString()) != null)
                    {
                        ddlFormaComision.SelectedValue = idCatFormaComision.ToString();
                    }
                    int idCatModalidad = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatModalidad"]);
                    if (ddlModalidad.Items.FindByValue(idCatModalidad.ToString()) != null)
                    {
                        ddlModalidad.SelectedValue = idCatModalidad.ToString();
                    }
                    string tipoPersecucion = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Persecucion"].ToString();
                    string fechaDelito = Convert.ToDateTime(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["FeDelito"]).ToString("yyyy-MM-dd");
                    string domicilio = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Domicilio"].ToString();
                    txtLocalidad.Text = domicilio;
                    System.Diagnostics.Debug.WriteLine("IdDeliAsunto: " + idDeliAsunto);
                    System.Diagnostics.Debug.WriteLine("IdDelDetalle: " + idDelDetalle);
                    System.Diagnostics.Debug.WriteLine("TipoPersecucion: " + tipoPersecucion);
                    System.Diagnostics.Debug.WriteLine("IdMunicipio: " + idMunicipio);
                    divAgregarClasificacion.Style["display"] = "block";
                    FechaDelito.Text = fechaDelito;
                    JUC_CatDelitosController controller = new JUC_CatDelitosController();
                    var delito = JUC_CatDelitosController.GetDelitoByDeliAsunto(idDeliAsunto);
                    if (delito != null)
                    {

                        controller.LoadDelitoByDeliAsunto(ddDelitos, idDeliAsunto);
                        ddDelitos.Enabled = true;
                        ddDelitos.SelectedValue = delito.IdDelito.ToString();
                        System.Diagnostics.Debug.WriteLine("Delito cargado en ddDelitos");
                        if (delito.IdDelito == 405)
                        {
                            ddlModDelito.Visible = true;
                            divModalidad.Style["display"] = "block";
                            CargarCatalogos cargarCatalogos = new CargarCatalogos();
                            cargarCatalogos.LoadModalidadPorIdDelDetalle(ddlModDelito, delito.IdDelito, idDelDetalle);
                            ddlModDelito.SelectedValue = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelDetalle"].ToString();
                        }
                        else
                        {
                            ddlModDelito.Visible = false;
                            divModalidad.Style["display"] = "none";

                        }
                    }
                    SeleccionarTipoPersecucion(tipoPersecucion);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en la selección del GridView: " + ex.Message);
                //MensajeError("Error al actualizar la selección: " + ex.Message);
                MensajeAdvertencia("Hay datos vacios en la tabla, te recomendamos actualizar el delito con su información");
                divAgregarClasificacion.Style["display"] = "none";
            }
        }

 

        //
    }
}