using DatabaseConnection;
using SIPOH.Controllers.AC_CatalogosCompartidos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class Consignacion : System.Web.UI.MasterPage
    {
        private const string ServerName = "sipoh";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            NombreJuzgado.Text = HttpContext.Current.Session["NombreJuzgado"] as string;
            LabelJuzgado.Text = HttpContext.Current.Session["NombreJuzgado"] as string;
            LabelTCircuito.Text = HttpContext.Current.Session["IdCircuito"] as string; 
            //Debug.WriteLine("Los valores de los datos en conjunto: " + Session["NombreJuzgado"] + " " + Session["IdCircuito"]);
            /// Datos usuario
            LabelProfile.Text = HttpContext.Current.Session["PerfilNombre"] as string;            
           
            
            int IdPefil = int.Parse(HttpContext.Current.Session["IdPerfil"] as string);            
            LabelAddress.Text = HttpContext.Current.Session["Address"] as string;
            LabelPhoneNum.Text = HttpContext.Current.Session["PhoneNum"] as string;
            LabelEmail.Text = HttpContext.Current.Session["Email"] as string;
            LabelName.Text = HttpContext.Current.Session["Name"] as string;
            LabelFistName.Text = HttpContext.Current.Session["FistName"] as string;
            LabelLastName.Text = HttpContext.Current.Session["LastName"] as string;
            userName.Text = HttpContext.Current.Session["Name"] as string;
            
            tipoCircuito.Text = HttpContext.Current.Session["PerfilNombre"] as string;






            //Menu dinamico:
            MostrarOpcionesMenu(IdPefil);


        }


        //public string ObtenerLinkConServidor(string enlace)
        //{
        //    // Obtiene el objeto de la URL
        //    Uri url = HttpContext.Current.Request.Url;

        //    // Obtiene el esquema (http o https)
        //    string esquema = url.Scheme;

        //    // Obtiene el nombre del servidor y el puerto si está en localhost
        //    string nombreServidor = url.IsLoopback ? $"{url.Host}:{url.Port}" : url.Host;
        //    Debug.WriteLine(nombreServidor);
        //    // Combina el esquema, el nombre del servidor y el enlace
        //    return $"{esquema}://{nombreServidor}{enlace}";
        //}
        public string ObtenerLinkConServidor(string enlace)
        {
            // Obtiene el objeto de la URL
            Uri url = HttpContext.Current.Request.Url;

            // Obtiene el esquema (http o https)
            string esquema = url.Scheme;

            // Determina el nombre del servidor y el puerto si está en localhost
            string nombreServidor = url.IsLoopback
                ? $"{url.Host}:{url.Port}"
                : $"{url.Host}:{url.Port}/{ServerName}";            

            // Combina el esquema, el nombre del servidor y el enlace
            return $"{esquema}://{nombreServidor}{enlace}";
        }


        private void MostrarOpcionesMenu(int IdPerfil)
        {
            DataTable co =   MenuDinamicoController.ObtenerCatEnlacesPorPerfilToMenuDinamico("CO", IdPerfil);
            RepeaterCatCompartidos.DataSource = co;
            RepeaterCatCompartidos.DataBind();
            if (co != null && co.Rows.Count > 0)
            {
                PanelCatCompartidos.Visible = true;
            }
            else
            {
                PanelCatCompartidos.Visible = false;
            }
            DataTable c =   MenuDinamicoController.ObtenerCatEnlacesPorPerfilToMenuDinamico("C", IdPerfil);
            RepeaterCatControl.DataSource = c;
            RepeaterCatControl.DataBind();
            if (c != null && c.Rows.Count > 0)
            {
                OpcionesControl.Visible = true;
            }
            else
            {
                OpcionesControl.Visible = false;
            }
            DataTable e = MenuDinamicoController.ObtenerCatEnlacesPorPerfilToMenuDinamico("E", IdPerfil);
            RepeaterCatEjecucion.DataSource = e;
            RepeaterCatEjecucion.DataBind();
            if (e != null && e.Rows.Count > 0)
            {
                OpcionesEjecucion.Visible = true;
            }
            else
            {
                OpcionesEjecucion.Visible = false;
            }
        }



    }
}