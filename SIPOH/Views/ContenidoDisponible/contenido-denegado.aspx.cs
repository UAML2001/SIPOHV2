using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views.ContendoDisponible
{
    public partial class contenido_denegado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        protected void BotonLogin_Click(object sender, EventArgs e)
        {
            
            
            Response.Redirect("~/Inicio.aspx");
        }
    }
}