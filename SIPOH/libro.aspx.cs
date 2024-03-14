using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Controllers.AC_CatalogosCompartidos.AC_ConsultaLibroIndiceController;

namespace SIPOH
{
    public partial class libroindice : System.Web.UI.Page
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
            //bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/promociones"));
            bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/libroindice")) : false;
            // Si enlaces es nulo, redirige a Default.aspx
            if (enlaces == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if ((circuito == "c" || circuito == "e") && tienePermiso)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
                Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
            }
        }

        protected void btnBuscarLibroIndice_Click(object sender, EventArgs e)
        {
            string nombre = inputNombre.Value;
            string apellidoPaterno = inputApellidoPaterno.Value;
            string apellidoMaterno = inputApellidoMaterno.Value;
            int idJuzgado;
            if (HttpContext.Current.Session["IDJuzgado"] != null && int.TryParse(HttpContext.Current.Session["IDJuzgado"].ToString(), out idJuzgado))
            {
                // Si el valor está presente y es un entero, se usa este valor.
            }
            else
            {
                // En caso de no encontrar el valor en la sesión o no ser un entero, puedes asignar un valor por defecto o manejar el error.
                idJuzgado = 0; // Valor por defecto o manejo de error.
            }
            SIPOH.Controllers.AC_CatalogosCompartidos.AC_ConsultaLibroIndiceController controller = new SIPOH.Controllers.AC_CatalogosCompartidos.AC_ConsultaLibroIndiceController();
            List<SIPOH.Controllers.AC_CatalogosCompartidos.AC_ConsultaLibroIndiceController.LibroIndice> resultados = controller.ConsultaLibroIndice(idJuzgado, nombre, apellidoPaterno, apellidoMaterno);
            gridViewResultados.DataSource = resultados;
            gridViewResultados.DataBind();
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            inputNombre.Value = "";
            inputApellidoPaterno.Value = "";
            inputApellidoMaterno.Value = "";
            gridViewResultados.DataSource = null;
            gridViewResultados.DataBind();
        }


        //
    }
}