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
            }

        }
    }
}