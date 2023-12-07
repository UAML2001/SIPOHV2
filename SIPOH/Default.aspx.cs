using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SIPOH
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                // Verifica si el usuario ya está autenticado y redirige si es necesario
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }
        protected void BotonLogin_Click(object sender, EventArgs e)
        {
            string Usuario = txtUsuario.Text;
            string Password = txtPass.Text;
            //auth login
            if (Autenticacion.AutenticarUsuario(Usuario, Password))
            {
                string perfil = HttpContext.Current.Session["Perfil"] as string;
                FormsAuthentication.RedirectFromLoginPage(Usuario, true);                
                MensajeSuccessLabel.Text = "Bienvenid@" + Usuario ;
                Debug.WriteLine("El valor de la variable es este: " + perfil);
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                MensajeErrorLabel.Text = "¡Credenciales incorrectas!, Intenta nuevamente";                 
            }
            
        }

    }
}