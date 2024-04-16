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
            // Código que se ejecuta al iniciar la aplicación
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private static bool IPBloqueadaUser(string ip)
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("AC_GetIPBloqueada", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IP", ip);
                    int count = (int)command.ExecuteScalar();
                    return count > 0; 
                }
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string ipUsuario = HttpContext.Current.Request.UserHostAddress;
            if (IPBloqueadaUser(ipUsuario))
            {
                HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Redirect("/ErrorSystem.aspx");
                HttpContext.Current.Response.StatusCode = 403; // Forbidden
                HttpContext.Current.Response.End();


            }
        }
    }
}