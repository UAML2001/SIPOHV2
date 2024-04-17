using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
            MensajeErrorLabel.Visible = false;
        }
        protected void BotonLogin_Click(object sender, EventArgs e)
        {
            string Usuario = txtUsuario.Text;
            string Password = txtPass.Text;
            //Encryting
            string contraseñaCifrada = CryptographyController.EncryptString(Password);            
            
                if (AutenticacionController.AutenticarUsuarioSeguridad(Usuario, contraseñaCifrada))
                {
                    string perfil = HttpContext.Current.Session["Perfil"] as string;
                    FormsAuthentication.RedirectFromLoginPage(Usuario, true);
                    //MensajeSuccessLabel.Text = "Bienvenid@" + Usuario ;

                    MensajeErrorLabel.Visible = false;
                    Debug.WriteLine("El valor de la variable es este: " + perfil);
                    Response.Redirect("Inicio.aspx");
                    // Usuario autenticado correctamente
                }
            
            else
            {
                // Usuario no autenticado
                MensajeErrorLabel.Text = "¡Credenciales incorrectas!, Intenta nuevamente";
                MensajeErrorLabel.Visible = true;
            }
            
            

        }

    }
}