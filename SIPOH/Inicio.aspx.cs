using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
            string circuito = HttpContext.Current.Session["TCircuito"] as string;
            
            

            if (circuito == "c" || circuito == "d" || circuito == "e" || circuito == "a")
            {
                Visible = true;
            }
            else
            {
                List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;


                //enlaces.Clear();
                Session.Clear();
                Session.Abandon();
                // Cerrar sesión en el servidor
                FormsAuthentication.SignOut();

                // Eliminar la cookie de sesión del navegador
                HttpCookie sessionCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                sessionCookie.Expires = DateTime.Now.AddYears(-1);
                // Cerrar la sesión del usuario actual
                Response.Cookies.Add(sessionCookie);
                Response.Redirect("Default.aspx");
            }


        }
    }
}