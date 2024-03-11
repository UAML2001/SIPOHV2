using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SIPOH.Models
{
    public class MensajeAlerta
    {
        public static void AlertaAviso(Page page, string mensaje)
        {
            string script = $"toastInfo('{mensaje}');";
            page.ClientScript.RegisterStartupScript(page.GetType(), "mostrar", script, true);

        }
        public static void AlertaValidacion(Page page, string mensaje)
        {
            string msj = "Valida los siguientes datos: " + mensaje;
            string script = $"toastInfo('{msj}');";
            page.ClientScript.RegisterStartupScript(page.GetType(), "mostrar", script, true);

        }

        public static void AlertaSatisfactorioRedireccion(Page page, string mensaje, string paginaRedireccion)
        {
            string script = $"toastRedireccionPagina('{mensaje}', '{paginaRedireccion}');";
            page.ClientScript.RegisterStartupScript(page.GetType(), "mostrarToastScript", script, true);
        }

        public static void AlertaError(Page page, string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            page.ClientScript.RegisterStartupScript(page.GetType(), "mostrar", script, true);

        }

        public static void AlertaSatisfactorioRedireccionPanel(Page page, string mensaje, string paginaRedireccion)
        {
            string script = $"toastRedireccionPagina('{mensaje}', '{paginaRedireccion}');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "popupScript", script, true);
        }

        public static void AlertaErrorPanel(Page page, string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "popupScript", script, true);


        }

    }
}