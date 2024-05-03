﻿using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class DigitalizarIniciales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarIdJuzgadoPorSesion idJuzgado = new GenerarIdJuzgadoPorSesion();
                int id = idJuzgado.ObtenerIdJuzgadoDesdeSesion();

                ConsultaCargaInicial consultaCarga = new ConsultaCargaInicial();
                DataTable datos = consultaCarga.ConsultaCargaDigitalizacion(id.ToString());

                PDigitalizar.EmptyDataText = "No se encontraron iniciales para digitalizar.";
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
            ElegirInicial elegirInicial = new ElegirInicial();
            elegirInicial.ElejirInicial(sender, e, PDigitalizar, descripNum, delitos, lblPartes, lblVictima, lblImputado, infoImputado, infoVictima, noDigit, lblInicialInfo, lblDocsNoDigit, lblinfo, lblAdjuntar, UploadFileDigit, btnDigitalizar, PortadaInicial, VPPortada);

        }

        protected void PDigitalizar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginacionInicial paginacion = new PaginacionInicial();
            paginacion.CambioIndicePagina(sender, e, PDigitalizar);
        }


        protected void btnDigitalizar_Click(object sender, EventArgs e)
        {
            DigitalizarInicial digitalizador = new DigitalizarInicial
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