﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class Agenda : System.Web.UI.Page
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
            //bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/agenda"));
            bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/agenda")) : false;

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
        }
    }
}