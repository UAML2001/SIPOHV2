using DatabaseConnection;
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
    public partial class Consignacion : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /// Datos usuario
            LabelProfile.Text = HttpContext.Current.Session["Perfil"] as string;
            LabelAddress.Text = HttpContext.Current.Session["Address"] as string;
            LabelPhoneNum.Text = HttpContext.Current.Session["PhoneNum"] as string;
            LabelEmail.Text = HttpContext.Current.Session["Email"] as string;
            LabelName.Text = HttpContext.Current.Session["Name"] as string;
            LabelFistName.Text = HttpContext.Current.Session["FistName"] as string;
            LabelLastName.Text = HttpContext.Current.Session["LastName"] as string;
            userName.Text = HttpContext.Current.Session["Name"] as string;
            NombreJuzgado.Text = Autenticacion.getJuzgado();
            LabelJuzgado.Text = Autenticacion.getJuzgado();
            string circuito = HttpContext.Current.Session["TCircuito"] as string;
            LabelTCircuito.Text = circuito; 
            tipoCircuito.Text = circuito == "c" || circuito == "d" ? "Atencion Ciudadana" : "Ejecucion";
            MostrarOpcionesConjunto(circuito);            
            MostrarOpcionesPorRol(circuito, OpcionesEjecucion, "e");
            MostrarOpcionesPorRol(circuito, OpcionesControl, "d");
            MostrarOpcionesCircuito(circuito, OpcionesControlCircuito, "c");
            
        }
        private void MostrarOpcionesPorRol(string circuito, Panel panelOpciones, string rolObjetivo)
        {
            panelOpciones.Visible = (circuito == rolObjetivo);
        }
        private void MostrarOpcionesCircuito(string circuito, Panel panelOpciones, string rolObjetivo)
        {
            panelOpciones.Visible = (circuito == rolObjetivo);
        }

        private void MostrarOpcionesConjunto(string circuito)
        {
            if (circuito == "c" || circuito == "d" || circuito =="e")
            {
                OpcionesConjunto.Visible = true;
            }
            else
            {
                OpcionesConjunto.Visible = false;
            }
        }



    }
}