using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views.ContenidoExpediente
{
    public partial class InformacionExpediente : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnBuscarExpediente(object sender, EventArgs e)
        {
            Debug.WriteLine("Buscando expediente...");
        }
    }
}