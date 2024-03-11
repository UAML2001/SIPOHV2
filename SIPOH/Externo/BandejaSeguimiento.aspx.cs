using DatabaseConnection;
using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Externo
{
    public partial class BandejaSeguimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int IdUsuarioExterno = int.Parse(Session["IdUsuarioExterno"].ToString());
                gridbuzon.DataSource = BandejaBuzonSolicitud.ObtenerBandejaBuzonSolicitud(IdUsuarioExterno);
                gridbuzon.DataBind();


                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatTipoAsunto").Items)
                {
                    ddlTipoAsunto.Items.Add(li);
                }
                ddlTipoAsunto.Items.Add(new ListItem { Value = "N", Text = "NUC" });
                ddlTipoAsunto.Items.Add(new ListItem { Value = "F", Text = "FOLIO" });
                ddlTipoAsunto.Items.Add(new ListItem { Value = "T", Text = "TODOS" });


            }
        }

        protected void gridbuzon_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Obtiene renglon seleccionado            
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = gridbuzon.Rows[index];
            #endregion

            string RutaDoc = gridbuzon.DataKeys[index]["RutaDoc"].ToString();
            if (e.CommandName == "Ver")
            {
                string _open = $"window.open('{ConexionBD.ObtenerRutaSIPOHDocumentos() + RutaDoc}');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), DateTime.Now.ToString(), _open, true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ddlTipoAsunto.SelectedIndex > 0)
            {
                int IdUsuarioExterno = int.Parse(Session["IdUsuarioExterno"].ToString());
                List<BandejaBuzonSolicitud> resultado;

                switch (ddlTipoAsunto.SelectedValue)
                {

                    case "T":
                        resultado = BandejaBuzonSolicitud.ObtenerBandejaBuzonSolicitud(IdUsuarioExterno);
                        if (resultado.Count > 0)
                        {
                            gridbuzon.DataSource = resultado;
                            gridbuzon.DataBind();
                        }
                        else
                            MensajeAlerta.AlertaAviso(this, "No se encontraron resultados.");


                        break;

                    case "F":
                        if (!string.IsNullOrEmpty(txtasunto.Text))
                        {
                            resultado = BandejaBuzonSolicitud.ObtenerBandejaBuzonSolicitudxFolio(txtasunto.Text.Trim().ToUpper());

                            if (resultado.Count > 0)
                            {
                                gridbuzon.DataSource = resultado;
                                gridbuzon.DataBind();
                            }
                            else
                                MensajeAlerta.AlertaAviso(this, "No se encontraron resultados.");

                        }
                        else
                            MensajeAlerta.AlertaAviso(this, "Ingrese el número de folio");
                        break;

                    case "N":
                        if (!string.IsNullOrEmpty(txtasunto.Text))
                        {
                            resultado = BandejaBuzonSolicitud.ObtenerBandejaBuzonSolicitudxNUC(txtasunto.Text.Trim().ToUpper());

                            if (resultado.Count > 0)
                            {
                                gridbuzon.DataSource = resultado;
                                gridbuzon.DataBind();
                            }
                            else
                                MensajeAlerta.AlertaAviso(this, "No se encontraron resultados.");
                        }
                        else
                            MensajeAlerta.AlertaAviso(this, "Ingrese el número de NUC");
                        break;


                    default:
                        if (!string.IsNullOrEmpty(txtasunto.Text))
                        {
                            resultado = BandejaBuzonSolicitud.ObtenerBandejaBuzonSolicitudxTipoYNumero(ddlTipoAsunto.SelectedValue, txtasunto.Text.Trim().ToUpper());

                            if (resultado.Count > 0)
                            {
                                gridbuzon.DataSource = resultado;
                                gridbuzon.DataBind();
                            }
                            else
                                MensajeAlerta.AlertaAviso(this, "No se encontraron resultados.");
                        }
                        else
                            MensajeAlerta.AlertaAviso(this, "Ingrese el número de asunto");
                        break;
                }
            }

        }
    }
}