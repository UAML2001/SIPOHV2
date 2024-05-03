using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.Models;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class DigitalizarPosteriores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarIdJuzgadoPorSesion idJuzgado = new GenerarIdJuzgadoPorSesion();
                int id = idJuzgado.ObtenerIdJuzgadoDesdeSesion();

                ConsultaCargaPosterior consultaCarga = new ConsultaCargaPosterior();
                // Supongamos que el método ConsultarAsuntosNoDigitalizados() de la clase ConsultaCarga devuelve los datos que quieres mostrar en la tabla.
                DataTable datos = consultaCarga.ConsultaCargaDigitalizacion(id.ToString()); // Reemplaza idJuzgado con el valor real

                PDigitalizar.EmptyDataText = "No se encontraron posteriores para digitalizar.";
                PDigitalizar.DataSource = datos.Rows.Count > 0 ? datos : null;
                PDigitalizar.DataBind();

                if (Session["ToastrMessage"] != null && Session["ToastrType"] != null)
                {
                    ShowToastr((string)Session["ToastrMessage"], (string)Session["ToastrType"]);
                    Session["ToastrMessage"] = null;
                    Session["ToastrType"] = null;
                }

            }

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            ElegirPosterior elegirposterior = new ElegirPosterior();
            elegirposterior.ElejirInicial(sender, e, PDigitalizar, descripNum, delitos, lblPartes, lblVictima, lblImputado, infoImputado, infoVictima, noDigit, lblInicialInfo, lblDocsNoDigit, lblinfo, lblAdjuntar, UploadFileDigit, btnDigitalizar);
        }


        protected void PDigitalizar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginacionPosterior paginacion = new PaginacionPosterior();
            paginacion.CambioIndicePagina(sender, e, PDigitalizar);
        }

        protected void btnDigitalizar_Click(object sender, EventArgs e)
        {
            DigitalizarPosterior digitalizador = new DigitalizarPosterior
            {
                UploadFileDigit = this.UploadFileDigit,
                noDigit = this.noDigit,
                Page = this.Page
            };

            digitalizador.btnDigitalizar_Click(sender, e);
        }

        private void ShowToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}');", true);
        }

    }
}