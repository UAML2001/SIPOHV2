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
               
            }
        }
        protected void btnAgregarClasiDelito_Click(object sender, EventArgs e)
        {
            divAgregarClasificacion.Style["display"] = "block";
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
            int idJuzgado = 205;
            if (string.IsNullOrWhiteSpace(tipoAsunto) || string.IsNullOrWhiteSpace(numeroAsunto))
            {
                MensajeError("Por favor, selecciona un tipo de asunto e ingresa un número de asunto.");
                return;
            }
            JUC_ClasificacionDelitoController controller = new JUC_ClasificacionDelitoController();
            DataTable dt = controller.BuscarDelitos(tipoAsunto, numeroAsunto, idJuzgado);
            // Manejar el caso cuando el DataTable es nulo debido a un error
            if (dt == null)
            {
                MensajeError("No se encontraron delitos para el tipo de asunto y número proporcionados, verifica el tipo asunto y el número ingresado.");
                GridViewClasificacionDelitos.DataSource = InicializaTablaVaciaDelitos();
                GridViewClasificacionDelitos.DataBind();
                return;
            }
            // Si no se encuentran datos, mostrar advertencia
            if (dt.Rows.Count == 0)
            {
                MensajeAdvertencia("No se encontraron delitos para el tipo de asunto y número proporcionados.");
                return;
            }
            // Si se encuentran datos, enlazarlos al GridView
            GridViewClasificacionDelitos.DataSource = dt;
            GridViewClasificacionDelitos.DataBind();
            MensajeExito("Delitos encontrados con éxito.");
        }


        protected void GridViewClasificacionDelitos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridViewClasificacionDelitos.SelectedRow;
                if (row != null)
                {
                    int idDeliAsunto = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDeliAsunto"]);
                    System.Diagnostics.Debug.WriteLine("IdDeliAsunto: " + idDeliAsunto);

                    // Utilizar el controlador correcto para cargar los datos del delito
                    JUC_CatDelitosController controller = new JUC_CatDelitosController();
                    var delito = JUC_CatDelitosController.GetDelitoByDeliAsunto(idDeliAsunto);

                    if (delito != null)
                    {
                        // Cargar el delito en el DropDownList y habilitarlo
                        controller.LoadDelitoByDeliAsunto(ddDelitos, idDeliAsunto);
                        ddDelitos.Enabled = true;
                        System.Diagnostics.Debug.WriteLine("Delito cargado en ddDelitos");
                        // Mostrar u ocultar el DropDownList de Modalidad según el IdDelito
                        if (delito.IdDelito == 405)
                        {
                            ddlModalidad.Visible = true;
                            divModalidad.Style["display"] = "block";
                            // Cargar datos en el DropDownList de Modalidad si es necesario
                            GeneralesyWebUi.CargarCatalogos cargarCatalogos = new GeneralesyWebUi.CargarCatalogos();
                            cargarCatalogos.LoadModalidades(ddlModalidad);
                        }
                        else
                        {
                            ddlModalidad.Visible = false;
                            divModalidad.Style["display"] = "none";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en la selección del GridView: " + ex.Message);
                MensajeError("Error al actualizar la selección: " + ex.Message);
            }
        }




        //
    }
}