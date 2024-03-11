using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Externo
{
    public partial class LoginExterno : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["IdUsuarioExterno"] != null)
            {
                Response.Redirect("Inicio.aspx");

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Page.Session["IdUsuarioExterno"] = HFIdUsuarioExterno.Value;

            bool respuesta = false;
            Page.Session["Usuario"] = UsuarioExterno.ObtenerUsuarioxid(HFIdUsuarioExterno.Value,ref respuesta);

            //Page.Response.Redirect("Inicio.aspx");
            Page.Response.Redirect("InicialesDigitales.aspx");
        }
    }
}