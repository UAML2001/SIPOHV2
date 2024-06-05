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

            //MensajeErrorLabel.Text = AutenticacionController.intentosFallidos;
        }
        private void ShowToast(string title, string message)
        {
            string script = $@"
        $(document).ready(function () {{
            $('#liveToast .toast-header strong').text('{title}');
            $('#liveToast .toast-body').text('{message}');
            $('#liveToast').toast('show');
        }});";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowToast", script, true);
        }
        //protected void BotonLogin_Click(object sender, EventArgs e)
        //{
        //    string Usuario = txtUsuario.Text;
        //    string Password = txtPass.Text;

        //    // Encriptar la contraseña
        //    string contraseñaCifrada = CryptographyController.EncryptString(Password);

        //    ShowToast("SIPOH", "Intentos fallidos: " + (AutenticacionController.intentosFallidos + 1));

        //    // Variable para almacenar el mensaje de error
        //    string mensajeError = string.Empty;

        //    // Autenticar usuario y obtener mensaje de error si falla
        //    if (AutenticacionController.AutenticarUsuarioSeguridad(Usuario, contraseñaCifrada, out mensajeError))
        //    {
        //        string perfil = HttpContext.Current.Session["Perfil"] as string;
        //        FormsAuthentication.RedirectFromLoginPage(Usuario, true);

        //        MensajeErrorLabel.Visible = false;
        //        Debug.WriteLine("El valor de la variable es este: " + perfil);
        //        Response.Redirect("Inicio.aspx");
        //    }
        //    else
        //    {
        //        // Usuario no autenticado, mostrar mensaje de error específico
        //        MensajeErrorLabel.Text = "¡Credenciales incorrectas!, Intenta nuevamente. Error: " + mensajeError;
        //        MensajeErrorLabel.Visible = true;
        //    }
        //}
        public int maxIntentosFallidos = 5;
        protected void BotonLogin_Click(object sender, EventArgs e)
        {
            string Usuario = txtUsuario.Text;
            string Password = txtPass.Text;

            // Encriptar la contraseña
            string contraseñaCifrada = CryptographyController.EncryptString(Password);

            //ShowToast("SIPOH", "Intentos fallidos: " + (AutenticacionController.intentosFallidos + 1));

            // Variable para almacenar el mensaje de error
            string mensajeError = string.Empty;

            // Autenticar usuario y obtener mensaje de error si falla
            if (AutenticacionController.AutenticarUsuarioSeguridad(Usuario, contraseñaCifrada, out mensajeError))
            {
                string perfil = HttpContext.Current.Session["Perfil"] as string;
                FormsAuthentication.RedirectFromLoginPage(Usuario, true);

                MensajeErrorLabel.Visible = false;                
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                int intentosRestantes = (maxIntentosFallidos-1) - AutenticacionController.intentosFallidos;                
                // Usuario no autenticado, mostrar mensaje de error específico
                MensajeErrorLabel.Text =  mensajeError + $", ¡{intentosRestantes} intentos restantes!";
                MensajeErrorLabel.Visible = true;
            }
        }



    }
}