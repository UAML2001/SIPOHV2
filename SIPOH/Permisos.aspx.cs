using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static PersonalUsuariosController;
using static RegistroPerfilController;

using System.Web.Services;


namespace SIPOH
{

    public partial class Permisos : System.Web.UI.Page
    {
        //Registro de session con procedimientos almacenados
        protected  void Page_Load(object sender, EventArgs e)
        {            
            int sessionTimeout = 1 * 60; // 20 minutos
            Session.Timeout = sessionTimeout;
            
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Default.aspx");
                return;
            }
            string circuito = HttpContext.Current.Session["TCircuito"] as string;
            List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;
            //bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/permisos"));
            bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/permisos")) : false;

            // Si enlaces es nulo, redirige a Default.aspx
            if (enlaces == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (circuito == "a" && tienePermiso)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
                Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
            }
            if (!IsPostBack)
            {
                CatPermisosEjecucion();
                CatPermisosControl();
                CatPermisosCompartido();
                CatTipoCircuito();
                obtenerPerfiles();                                

                LimpiarInputCrearPerfil();
                LimpiarChecks();
            }
        }
        [WebMethod]
        public static string ProcesarDatos(string data)
        {
            // Procesa los datos aquí
            return "¡Datos recibidos: " + data + "!";
        }
        protected void RepeaterPermisoAsociado_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtener el valor de IdAsuntoCausa del item actual del repeater<<
                string idPermisoAsociado = DataBinder.Eval(e.Item.DataItem, "IdPerfil").ToString();
                string NombreInculpado = DataBinder.Eval(e.Item.DataItem, "Perfil").ToString();
                lblIdSeleccionado.Text = NombreInculpado;
                HiddenIdPermisoAsociado.Value = idPermisoAsociado;

            }

        }
        protected void btnGuardarSubPermiso_Click(object sender, EventArgs e)
        {
           //probar funcionalidad modal subpermisos
            Button btn = (Button)sender;
            string SubPermiso = btn.CommandArgument;
            int idSubpermiso = int.Parse(SubPermiso);           
            
            ObtenerSubpermisosCompartidos(idSubpermiso);
            ObtenerSubpermisosControl(idSubpermiso);
            ObtenerSubpermisosEjecucion(idSubpermiso);
            PanelPermisosAsociados.Update();

        }



        private void ObtenerSubpermisosCompartidos(int perfil)
        {
            
            DataTable data = ObtenerPermisosAsociados(perfil, "CO");
            
            RepeaterGetSubpermisoAsociadoCompartidos.DataSource = data;
            RepeaterGetSubpermisoAsociadoCompartidos.DataBind();
            
        }
       


        private void ObtenerSubpermisosControl(int perfil)
        {
            DataTable data = ObtenerPermisosAsociados(perfil, "C");
            RepeaterGetSubpermisoAsociadoControl.DataSource = data;
            RepeaterGetSubpermisoAsociadoControl.DataBind();
        }

        private void ObtenerSubpermisosEjecucion(int perfil)
        {
            DataTable data = ObtenerPermisosAsociados(perfil, "E");
            RepeaterGetSubpermisoAsociadoEjecucion.DataSource = data;
            RepeaterGetSubpermisoAsociadoEjecucion.DataBind();
        }

        protected void CatalogoTipoCircuito(object sender, EventArgs e)
        {
        }
        private void CatPermisosEjecucion()
        {
            DataTable dt = ObtenerCatPermisos("E");
            // Enlazar el Repeater con los datos obtenidos
            CatSubpermisosEjecucion.DataSource = dt;
            CatSubpermisosEjecucion.DataBind();
        }
        private void CatPermisosControl()
        {
            DataTable dt = ObtenerCatPermisos("C");
            CatSubpermisosControl.DataSource = dt;
            CatSubpermisosControl.DataBind();
        }
        private void CatPermisosCompartido()
        {
            DataTable dt = ObtenerCatPermisos("CO");
            CatSubpermisosCompartidos.DataSource = dt;
            CatSubpermisosCompartidos.DataBind();
        }
        private void CatTipoCircuito()
        {
            
            List<TipoCircuito> info = ObtenerTipoCircuito();
            inputTipoCircuito.DataSource = info;
            inputTipoCircuito.DataTextField = "Circuito";
            inputTipoCircuito.DataValueField = "Circuito";
            inputTipoCircuito.DataBind();
        }
        protected void btnEnviarPerfil(object sender, EventArgs e)
        {            
            string perfil = inputNombrePerfil.Text;
            string tipoCircuito = inputTipoCircuito.SelectedValue;

            List<string> IdsAsignados = new List<string>();

            foreach (RepeaterItem item in CatSubpermisosCompartidos.Items)
            {
                CheckBox chkIdPermisoCompartido = (CheckBox)item.FindControl("chkIdPermiso");

                string PermisosSelected = (chkIdPermisoCompartido.Attributes["data-IdPermiso"]);
                // Verificar si el CheckBox está marcado
                if (chkIdPermisoCompartido.Checked)
                {
                    IdsAsignados.Add(PermisosSelected);


                }
            }
            foreach (RepeaterItem item in CatSubpermisosControl.Items)
            {
                CheckBox chkIdPermisoControl = (CheckBox)item.FindControl("chkIdPermisoControl");

                string PermisosSelected = (chkIdPermisoControl.Attributes["data-IdPermisoControl"]);
                // Verificar si el CheckBox está marcado
                if (chkIdPermisoControl.Checked)
                {

                    IdsAsignados.Add(PermisosSelected);

                }
            }
            foreach (RepeaterItem item in CatSubpermisosEjecucion.Items)
            {
                CheckBox chkIdPermisoEjecucion = (CheckBox)item.FindControl("chkIdPermisoEjecucion");

                string PermisosSelected = (chkIdPermisoEjecucion.Attributes["data-IdPermisoEjecucion"]);
                // Verificar si el CheckBox está marcado
                if (chkIdPermisoEjecucion.Checked)
                {

                    IdsAsignados.Add(PermisosSelected);

                }
            }

            List<DataPermisoAsociado> DatapermisoAsociado = new List<DataPermisoAsociado>();

            foreach (string valor in IdsAsignados)
            {
                int idPermiso;
                if (int.TryParse(valor, out idPermiso))
                {
                    DatapermisoAsociado.Add(new DataPermisoAsociado { IdPermiso = idPermiso });
                }
            }


            if (string.IsNullOrWhiteSpace(perfil) ||
                string.IsNullOrWhiteSpace(tipoCircuito) ||
                 DatapermisoAsociado.Count == 0)
            {

                // Determinar cuál campo está vacío
                if (string.IsNullOrWhiteSpace(perfil))
                {
                    MensajeError("Ingresa nombre de perfil");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(tipoCircuito))
                {
                    MensajeError("Selecciona tu circuito");
                    return;
                }
                else if (DatapermisoAsociado.Count == 0)
                {
                    MensajeError("No ha eligido ningùn enlace para su perfil");
                    return;
                }


            }
            DataPerfil dataperfil = new DataPerfil
            {
                Perfil = perfil,
                TipoCircuito = tipoCircuito,
            };
            List<DataPerfil> Dataperfil = new List<DataPerfil> { dataperfil };
            ResultadoInsertCreacionPerfil respuesta = InsertCreacionPerfil(Dataperfil, DatapermisoAsociado);
            if (respuesta.hayError)
            {
                MensajeError("Surgio un proplema con: " + respuesta.mensaje);
                
                PermisosPanel.Update();

            }
            else
            {
                MensajeExito("Su registro fue exitoso");
                LimpiarChecks();
                LimpiarInputCrearPerfil();
               
                PermisosPanel.Update();
            }

        }
        protected void btnBuscarPerfil(object sender, EventArgs e)
        {

            string nombrePerfil = txtbxBusquedaPerfil.Text;
            
            if (string.IsNullOrEmpty(nombrePerfil) )
               
            {

                // Determinar cuál campo está vacío
                if (string.IsNullOrEmpty(nombrePerfil))
                {
                    MensajeError("Ingresa nombre de perfil");
                    return;

                }

            }
            List<DataPermisosAsociados> data = ObtenerPermisosAsociadosToPerfil(nombrePerfil);
            busquedaPerfilAsociado.DataSource = data;
            busquedaPerfilAsociado.DataBind();
            UpdateTablaPermisos.Update();
        }
        protected void obtenerPerfiles()
        {
            List<DataPermisosAsociados> data =  ObtenerPermisosAsociados();
            busquedaPerfilAsociado.DataSource = data;
            busquedaPerfilAsociado.DataBind();
            UpdateTablaPermisos.Update();
        }
        protected void MensajeExito(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastInfo('{Mensaje}');", true);
        }
        protected void MensajeError(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastError('{Mensaje}');", true);
        }        
        protected void LimpiarChecks()
        {
            // Limpiar los CheckBox del primer Repeater (CatSubpermisosCompartidos)
            foreach (RepeaterItem item in CatSubpermisosCompartidos.Items)
            {
                CheckBox chkIdPermisoCompartido = (CheckBox)item.FindControl("chkIdPermiso");
                chkIdPermisoCompartido.Checked = false;
            }

            // Limpiar los CheckBox del segundo Repeater (CatSubpermisosControl)
            foreach (RepeaterItem item in CatSubpermisosControl.Items)
            {
                CheckBox chkIdPermisoControl = (CheckBox)item.FindControl("chkIdPermisoControl");
                chkIdPermisoControl.Checked = false;
            }

            // Limpiar los CheckBox del tercer Repeater (CatSubpermisosEjecucion)
            foreach (RepeaterItem item in CatSubpermisosEjecucion.Items)
            {
                CheckBox chkIdPermisoEjecucion = (CheckBox)item.FindControl("chkIdPermisoEjecucion");
                chkIdPermisoEjecucion.Checked = false;
            }

        }
        protected void LimpiarInputCrearPerfil()
        {
            inputNombrePerfil.Text = "";
            inputTipoCircuito.SelectedIndex = 0;
        }

        //Formato de enlaces
        protected string GenerarListaEnlaces(string enlaces)
        {
            // Dividir la cadena de enlaces en una matriz de enlaces individuales
            string[] enlacesArray = enlaces.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Generar elementos de lista para cada enlace
            StringBuilder listaEnlacesHtml = new StringBuilder();
            foreach (string enlace in enlacesArray)
            {
                listaEnlacesHtml.Append("<li>" + enlace.Trim() + "</li>");
            }

            // Devolver la lista de enlaces como una cadena HTML
            return listaEnlacesHtml.ToString();
        }

    }

}