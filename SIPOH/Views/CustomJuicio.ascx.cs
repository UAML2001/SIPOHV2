using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatAnexosController;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatTipoSolicitudController;
using static SIPOH.Controllers.AC_CatalogosCompartidos.P_CatSolicitanteController;

namespace SIPOH.Views
{
    public partial class CustomJuicio : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatAnexosEnDropDownList();
                CargarCatTipoAudienciaDropDownList();
                CargarCatSolicitante();

                // Ejemplo de datos de la lista
                List<string> nombres = new List<string> { "MARIA DEL CARMEN", "JOSUE","ANA KARINA" };
                List<string> apellidos = new List<string> { "CONTRERAS GARCIA", "GOMEZ OMAÑA", "JUSTO GARCIA" };
                List<string> generos = new List<string> { "F", "M", "F" };

                // Combina los datos en un formato que pueda ser utilizado por el Repeater
                List<object> datos = new List<object>();
                for (int i = 0; i < nombres.Count; i++)
                {
                    datos.Add(new
                    {
                        Nombre = nombres[i],
                        Apellido = apellidos[i],
                        Genero = generos[i]
                    });
                }

                // Enlaza la lista al control Repeater
                RepeaterListaPersonas.DataSource = datos;
                RepeaterListaPersonas.DataBind();
            }
        }
        protected void btnConsultaCausa(object sender, EventArgs e)
        {
            
        }
        protected void GetLabelPrioridad(object sender, EventArgs e)
        {
            
            
        }
        protected void btnAñadirAnexo(object sender, EventArgs e)
        {


        }
        protected void btnEliminarAnexo(object sender, EventArgs e)
        {


        }
        protected void inputQuienIngresa_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                string quienIngresa = inputQuienIngresa.SelectedValue;
                //if (quienIngresa == "M")
                //{
                //    quienIngresa = "MP";
                //}
                //else if (quienIngresa == "P")
                //{
                //    quienIngresa = "particular";
                //}
                //else if (quienIngresa == "O")
                //{
                //    quienIngresa = "otra persona";
                //}
                //else
                //{
                //    quienIngresa = "";
                //}

                lblTipoPersona.Text = quienIngresa;

                //ScriptManager.RegisterStartupScript(this, GetType(), "mostrarValor", "mostrarValorSeleccionado();", true);
                JuicioOralPanel.Update();
            
        }
        private void CargarCatAnexosEnDropDownList()
        {
           //Mostrar catalogo de anexos en value como descripcion 
            List<DataCatAnexos> listaAnexos = GetCatAnexos();
            txtAnexosTipoJuicio.DataSource = listaAnexos;
            txtAnexosTipoJuicio.DataTextField = "descripcionAnexo";
            txtAnexosTipoJuicio.DataValueField = "descripcionAnexo";
            txtAnexosTipoJuicio.DataBind();
            
        }
        private void CargarCatTipoAudienciaDropDownList()
        {
            List<DataTipoAudiencia> listaTipoAudiencias = GetCatTipoSolicitud();
            inputTipoAudiencia.DataSource = listaTipoAudiencias;
            inputTipoAudiencia.DataTextField = "CAA_Descripcion";
            inputTipoAudiencia.DataValueField = "CAA_IdAudi";
            inputTipoAudiencia.DataBind();
        }
        private void CargarCatSolicitante()
        {
            List<DataTipoSolicitante> listaTipoSolicitante = GetCatSolicitante();
            inputQuienIngresa.DataSource = listaTipoSolicitante;
            inputQuienIngresa.DataTextField = "CS_Solicitante";
            //inputQuienIngresa.DataValueField = "CS_Tipo";
            inputQuienIngresa.DataBind();

        }
    }
}