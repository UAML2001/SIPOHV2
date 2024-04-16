using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RegistroPerfilController;


namespace SIPOH
{
    public partial class Permisos : System.Web.UI.Page
    {
        //Registro de session con procedimientos almacenados
        protected void Page_Load(object sender, EventArgs e)
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
            CatPermisosEjecucion();
            CatPermisosControl();
            CatPermisosCompartido();
            CatTipoCircuito();

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
            btnEnviarPerfil();
        }
    }

}