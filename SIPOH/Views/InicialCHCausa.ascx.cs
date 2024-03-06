using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views
{
    public partial class InicialCHCausa : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AlgunaOperacion();
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            AlgunaOperacion();
        }

        private void AlgunaOperacion()
        {

            // Recuperar los valores almacenados en la sesión
            lblTipoSistema.Text = "Tipo de Sistema: " + (Session["TipoSistema"] as string ?? "No especificado");
            lblJuzgadoSeleccionado.Text = "Juzgado Seleccionado: " + (Session["JuzgadoSeleccionado"] as string ?? "No especificado");
            lblTipo.Text = "Tipo: " + (Session["Tipo"] as string ?? "No especificado");
            lblNumeroCausaNucJuicio.Text = "Número de Causa/NUC/Juicio Oral: " + (Session["NumeroCausaNucJuicio"] as string ?? "No especificado");

        }


    }
}