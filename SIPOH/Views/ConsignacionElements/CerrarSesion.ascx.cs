using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views.ConsignacionElements
{
    public partial class CerrarSesion : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnCerrarSesion(object sender, EventArgs e)
        {
            // Limpia la variable ListEnlace para cierre de sessiones
            
                      
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