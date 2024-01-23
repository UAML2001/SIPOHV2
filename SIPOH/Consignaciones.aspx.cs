using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class Consignaciones : System.Web.UI.Page
    {
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

            
            //bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/consignaciones"));
            bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/consignaciones")) : false;

            // Si enlaces es nulo, redirige a Default.aspx
            if (enlaces == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if ((circuito == "c" || circuito == "d" ) && tienePermiso)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
                Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
            }
            //contenido 
            
        }
        //[WebMethod]
        //public static string EnviarDato(string dato)
        //{
        //    // Aquí puedes procesar el dato recibido y realizar cualquier acción necesaria.
        //    return "Dato recibido: " + dato;
        //}






    }
}