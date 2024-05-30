using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SIPOH
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //Ejecuta al inicio de la app
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        ///PERMITE SABER SI LA IP QUE HA SIDO CARGADA CUANDO EL USUARIO INGRESA AL SITIO ESTA BLOQUEDA
        private static bool IPBloqueadaUser(string ip)
        {

            using (SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AC_GetIPBloqueada", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IP", ip);
                        int count = (int)command.ExecuteScalar();
                        return count > 0; 
                    }
                    return true; 

                }catch(Exception ex)
                {
                    //PERMITE EJECUTAR ACCION SI ENCUENTRA ALGUN ERROR COMO CARGA DE SERVIDOR
                    return false;
                    //ACCIONES EXTRA DE DIRECCIONAMIENTO 
                    //Debug.WriteLine("Error: " +  ex.Message);
                    //HttpContext.Current.Response.Redirect("/Error.aspx");
                    HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.Redirect("/ErrorSystem.aspx");
                    HttpContext.Current.Response.StatusCode = 400; // Forbidden
                    HttpContext.Current.Response.End();
                    throw new Exception("Error de respueta al servidor: " + ex.Message);                    
                }
            }
        }
        //ACCIONES AL CARGAR EL SITIO
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //TRAEMOS LA IP DE USUARIO PRE CARGADA Y SI ESTAN DENTRO DE NUESTRAS IPS BLOQUEDAS AUTOMATICAMENTE HACE LA SIGUIENTE ACCION
            string ipUsuario = HttpContext.Current.Request.UserHostAddress;
            if (IPBloqueadaUser(ipUsuario))
            {
                //EJECUTA ERROR DE DIRECCIONAMIENTO FORBIDDEN
                HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Redirect("/ErrorSystem.aspx");
                HttpContext.Current.Response.StatusCode = 403; // Forbidden
                HttpContext.Current.Response.End();
            }
            else
            {                
                return ;
            }
        }
    }
}