using SIPOH.Controllers.AC_JefeUnidadCausa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class clasidelito : System.Web.UI.Page
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

                LoadGradosConsumacion();
                LoadConcursos();
                LoadFormasAccion();
                LoadCalificaciones();
                LoadClasificaciones();
                LoadElementosComision();
                LoadFormasComision();
                LoadModalidades();
                LoadMunicipios();

            }

        }

        private void LoadGradosConsumacion()
        {
            var grados = JUC_CatGradoConsumacionController.GetGradosConsumacion();
            ddlGradoConsumacion.DataSource = grados;
            ddlGradoConsumacion.DataTextField = "Consumacion";
            ddlGradoConsumacion.DataValueField = "Id_CatConsumacion";
            ddlGradoConsumacion.DataBind();
            ddlGradoConsumacion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadConcursos()
        {
            var concursos = JUC_CatConcursoController.GetConcursos();
            ddlConcurso.DataSource = concursos;
            ddlConcurso.DataTextField = "NombreConcurso";
            ddlConcurso.DataValueField = "Id_CatConcurso";
            ddlConcurso.DataBind();
            ddlConcurso.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadFormasAccion()
        {
            var formasAccion = JUC_CatFormaAccionController.GetFormasAccion();
            ddlFormaAccion.DataSource = formasAccion;
            ddlFormaAccion.DataTextField = "Accion";
            ddlFormaAccion.DataValueField = "Id_CatAccion";
            ddlFormaAccion.DataBind();
            ddlFormaAccion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadCalificaciones()
        {
            var calificaciones = JUC_CatCalificacionController.GetCalificaciones();
            ddlCalificacion.DataSource = calificaciones;
            ddlCalificacion.DataTextField = "CalificacionNombre";
            ddlCalificacion.DataValueField = "Id_CatCalificacion";
            ddlCalificacion.DataBind();
            ddlCalificacion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadClasificaciones()
        {
            var clasificaciones = JUC_CatClasificacionController.GetClasificaciones();
            ddlOrdenResultado.DataSource = clasificaciones;
            ddlOrdenResultado.DataTextField = "ClasificacionNombre";
            ddlOrdenResultado.DataValueField = "Id_CatClasificacion";
            ddlOrdenResultado.DataBind();
            ddlOrdenResultado.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadElementosComision()
        {
            var elementosComision = JUC_CatElementosComisionController.GetElementosComision();
            ddlComision.DataSource = elementosComision;
            ddlComision.DataTextField = "ElemComision";
            ddlComision.DataValueField = "Id_CatElemComision";
            ddlComision.DataBind();
            ddlComision.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadFormasComision()
        {
            var formasComision = JUC_CatFormaComisionController.GetFormasComision();
            ddlFormaComision.DataSource = formasComision;
            ddlFormaComision.DataTextField = "Comision";
            ddlFormaComision.DataValueField = "Id_CatComision";
            ddlFormaComision.DataBind();
            ddlFormaComision.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadModalidades()
        {
            var modalidades = JUC_CatModalidadController.GetModalidades();
            ddlModalidad.DataSource = modalidades;
            ddlModalidad.DataTextField = "ModalidadNombre";
            ddlModalidad.DataValueField = "Id_CatModalidad";
            ddlModalidad.DataBind();
            ddlModalidad.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }
        private void LoadMunicipios()
        {
            var municipios = JUC_CatMunicipiosController.GetMunicipios();
            ddlCatMunicipios.DataSource = municipios;
            ddlCatMunicipios.DataTextField = "MunicipioNombre";
            ddlCatMunicipios.DataValueField = "IdMunicipio";
            ddlCatMunicipios.DataBind();
            ddlCatMunicipios.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        //
    }
}