using SIPOH.Controllers.AC_CatalogosCompartidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class GeneralesyWebUi : Page
    {
        //mensajes de alerta
        protected void MensajeExito(string mensaje)
        {
            string script = $"toastExito('{HttpUtility.JavaScriptStringEncode(mensaje)}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarToastScript", script, true);
        }

        protected void MensajeError(string mensaje)
        {
            string script = $"toastError('{HttpUtility.JavaScriptStringEncode(mensaje)}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "toastErrorScript", script, true);
        }

        protected void MensajeAdvertencia(string mensaje)
        {
            string script = $"toastWarning('{HttpUtility.JavaScriptStringEncode(mensaje)}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "toastWarningScript", script, true);
        }
        //dropdowns catalogos
        public class CargarCatalogos
        {
            public void LoadGradosConsumacion(DropDownList ddl)
            {
                var grados = JUC_CatGradoConsumacionController.GetGradosConsumacion();
                ddl.DataSource = grados;
                ddl.DataTextField = "Consumacion";
                ddl.DataValueField = "Id_CatConsumacion";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadConcursos(DropDownList ddl)
            {
                var concursos = JUC_CatConcursoController.GetConcursos();
                ddl.DataSource = concursos;
                ddl.DataTextField = "NombreConcurso";
                ddl.DataValueField = "Id_CatConcurso";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadFormasAccion(DropDownList ddl)
            {
                var formasAccion = JUC_CatFormaAccionController.GetFormasAccion();
                ddl.DataSource = formasAccion;
                ddl.DataTextField = "Accion";
                ddl.DataValueField = "Id_CatAccion";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadCalificaciones(DropDownList ddl)
            {
                var calificaciones = JUC_CatCalificacionController.GetCalificaciones();
                ddl.DataSource = calificaciones;
                ddl.DataTextField = "CalificacionNombre";
                ddl.DataValueField = "Id_CatCalificacion";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadClasificaciones(DropDownList ddl)
            {
                var clasificaciones = JUC_CatClasificacionController.GetClasificaciones();
                ddl.DataSource = clasificaciones;
                ddl.DataTextField = "ClasificacionNombre";
                ddl.DataValueField = "Id_CatClasificacion";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadElementosComision(DropDownList ddl)
            {
                var elementosComision = JUC_CatElementosComisionController.GetElementosComision();
                ddl.DataSource = elementosComision;
                ddl.DataTextField = "ElemComision";
                ddl.DataValueField = "Id_CatElemComision";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadFormasComision(DropDownList ddl)
            {
                var formasComision = JUC_CatFormaComisionController.GetFormasComision();
                ddl.DataSource = formasComision;
                ddl.DataTextField = "Comision";
                ddl.DataValueField = "Id_CatComision";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadModalidades(DropDownList ddl)
            {
                var modalidades = JUC_CatModalidadController.GetModalidades();
                ddl.DataSource = modalidades;
                ddl.DataTextField = "ModalidadNombre";
                ddl.DataValueField = "Id_CatModalidad";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            public void LoadMunicipios(DropDownList ddl)
            {
                var municipios = JUC_CatMunicipiosController.GetMunicipios();
                ddl.DataSource = municipios;
                ddl.DataTextField = "MunicipioNombre";
                ddl.DataValueField = "IdMunicipio";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            // Método para cargar datos en DropDownList8
            public void LoadDelitos(DropDownList ddl)
            {
                var delitos = CatDelitosController.GetCatDelitos();
                ddl.DataSource = delitos;
                ddl.DataTextField = "Delito";
                ddl.DataValueField = "IdDelito";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }

            //
        }
        //
    }
}