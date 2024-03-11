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
    public partial class PosterioresDigitales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatCircuitos").Items)
                {
                    ddlCircuito.Items.Add(li);
                }
                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatTipoAsunto").Items)
                {
                    ddlTipoAsunto.Items.Add(li);
                }


          

                 int IdUsuarioExterno = int.Parse(Session["IdUsuarioExterno"].ToString());
                bool respuesta = false;
                BuzonSolicitud solicitud = BuzonSolicitud.ObtenerbuzonSolicitud(IdUsuarioExterno, "P", ref respuesta);
                if (respuesta && solicitud.IdSolicitud > 0)
                {
                    divBusqueda.Visible = false;
                    divFormulario.Visible = false;
                    divPreguardar.Visible = true;
                    HFFolio.Value = solicitud.IdSolicitudBuzon.ToString(); ;
                    txtFolio.Text = solicitud.IdSolicitudBuzon.ToString();
                    txtTipoSolicitud.Text = solicitud.Solicitud;
                    txtFiguraSolicitante.Text = solicitud.Solicitante;
                    txtJuzgadoMostrar.Text = solicitud.NombreJuzgado;
                    txtPosiblesHechosSolicitud.Text = solicitud.PosiblesHechos;
                    txtAnexosSolicitud.Text = solicitud.DescripAnexos;
                    txtNUCSolicitud.Text = solicitud.NUC;
                    HFRuta.Value = solicitud.RutapdfOriginal;
                    HFNombre.Value = solicitud.Nombrearchivopdf_Original;

                    iframepdf.Src = $"{ConexionBD.ObtenerRutaSIPOHDocumentos()}{solicitud.RutapdfOriginal}{solicitud.Nombrearchivopdf_Original}" ;
                }
                else
                {
                    divBusqueda.Visible = true;
                    divFormulario.Visible = false;
                    divPreguardar.Visible = false;
                }




            }
        }

        protected void ddlCircuito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCircuito.SelectedIndex > 0)
            {
                ddlJuzgado.Items.Clear();
                ddlJuzgado.DataBind();
                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatJuzgadosxIdCircuito", "@IdCircuito", Convert.ToInt32(ddlCircuito.SelectedValue)).Items)
                {
                    ddlJuzgado.Items.Add(li);
                }
            }
        }
        
        string ValidacionBusqueda()
        {
            string mensaje = "";

            if (ddlCircuito.SelectedIndex < 1)
            {
                mensaje += "- Circuito -";
            }

            if (ddlJuzgado.SelectedIndex < 1)
            {
                mensaje += "- Juzgado -";
            }

            if (ddlTipoAsunto.SelectedIndex < 1)
            {
                mensaje += "- Tipo de Asunto -";
            }

            if (string.IsNullOrWhiteSpace(txtasunto.Text))
            {
                mensaje += "- Número de asunto -";
            }

            return mensaje;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string mensaje = ValidacionBusqueda();
            if (mensaje == "")
            {
                var resultado = BusquedaPromocion.ObtenerPromocion(txtasunto.Text.Trim(), ddlTipoAsunto.SelectedValue, ddlJuzgado.SelectedValue);
                if (resultado.Count > 0)
                {
                    gridResultado.DataSource = resultado;
                    gridResultado.DataBind();
                }
                else
                {
                    gridResultado.DataSource = null;
                    gridResultado.DataBind();
                    MensajeAlerta.AlertaAviso(this, "No existen datos relacionados a la busqueda realizada");
                }
            }
            else
            {
                MensajeAlerta.AlertaValidacion(this, mensaje);
            }
        }

        protected void gridResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Obtiene renglon seleccionado            
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = gridResultado.Rows[index];
            #endregion

            long IdAsunto = long.Parse(gridResultado.DataKeys[index]["IdAsunto"].ToString()); 
            string Delitos =gridResultado.DataKeys[index]["Delitos"].ToString(); 
            string NUC =gridResultado.DataKeys[index]["NUC"].ToString();
            if (e.CommandName == "Seleccionar")
            {
                if (IdAsunto > 0)
                {
                    HFIdAunto.Value = IdAsunto.ToString();
                    txtJuzgado.Text = ddlJuzgado.SelectedItem.Text;
                    txtNumero.Text = txtasunto.Text;
                    txtNUC.Text = NUC;
                    txtDelito.Text = Delitos;

                    divBusqueda.Visible = false;
                    divFormulario.Visible = true;
                    CargarDdl();
                }

            }
        }

        void CargarDdl()
        {
            foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatSolicitante").Items)
            {
                ddlFiguraSolicitante.Items.Add(li);
            }


            if ((DateTime.Now.Hour > 8 && DateTime.Now.Hour < 18))
            {
                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatSolicitudBuzon").Items)
                {
                    ddlTipoSolicitud.Items.Add(li);
                }
            }
            else
            {
                string[] parametros = { "@Horario" };
                string[] valores = { "E" };
                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatSolicitudBuzon", parametros, valores).Items)
                {
                    ddlTipoSolicitud.Items.Add(li);
                }
            }
        }

        string ValidacionPreguardar()
        {
            string mensaje = "";


            if (ddlTipoSolicitud.SelectedIndex < 1)
            {
                mensaje += "- Tipo Solicitud -";
            }

            if (ddlFiguraSolicitante.SelectedIndex < 1)
            {
                mensaje += "- Figura Solicitante -";
            }

            if (ddlJuzgado.SelectedIndex < 1)
            {
                mensaje += "- Juzgado -";
            }

            if (string.IsNullOrWhiteSpace(txtNUC.Text))
            {
                mensaje += "- NUC -";
            }

            if (string.IsNullOrWhiteSpace(txtAnexos.Text))
            {
                mensaje += "- Descripción de anexos -";
            }

            if (!filePDF.HasFile)
            {
                mensaje += "- Documento PDF -";

            }

            return mensaje;
        }


        protected void btnPreguardar_Click(object sender, EventArgs e)
        {
            string mensaje = ValidacionPreguardar();
            if (mensaje == "")
            {
                UsuarioExterno usuario = Page.Session["Usuario"] as UsuarioExterno;
                BuzonSolicitud solicitud = new BuzonSolicitud
                {
                    NUC = txtNUC.Text.ToUpper().Trim(),
                    IdSolicitante = int.Parse(ddlFiguraSolicitante.SelectedValue),
                    IdSolicitud = int.Parse(ddlTipoSolicitud.SelectedValue),
                    IdJuzgado = int.Parse(ddlJuzgado.SelectedValue),
                    DescripAnexos = txtAnexos.Text.ToUpper().Trim(),
                    PosiblesHechos = txtHechos.Text.ToUpper().Trim(),
                    IdUsuarioExterno = usuario.IdUsuarioExterno,
                    Tipo = "P",
                    Estatus = "P",
                    Promovente = usuario.NombreCompleto,
                    IdAsunto = long.Parse(HFIdAunto.Value),
                    
                };

                long Folio = 0;
                string rutaDoc = "";
                bool respuesta = BuzonSolicitud.Insertarsolicitud(solicitud, filePDF.FileBytes, ref Folio, ref rutaDoc);

                if (respuesta)
                {
                    divBusqueda.Visible = false;
                    divFormulario.Visible = false;
                    divPreguardar.Visible = true;
                    txtFolio.Text = Folio.ToString();
                    HFFolio.Value = Folio.ToString();
                    txtTipoSolicitud.Text = ddlTipoSolicitud.SelectedItem.Text;
                    txtFiguraSolicitante.Text = ddlFiguraSolicitante.SelectedItem.Text;
                    txtJuzgadoMostrar.Text = ddlJuzgado.SelectedItem.Text;
                    txtPosiblesHechosSolicitud.Text = solicitud.PosiblesHechos;
                    txtAnexosSolicitud.Text = solicitud.DescripAnexos;
                    txtNUCSolicitud.Text = solicitud.NUC;

                    iframepdf.Src = "http://nas.pjhidalgo.gob.mx/SIPOH/Solicitudes/" + rutaDoc;


                }
                else
                {
                    MensajeAlerta.AlertaError(this, "Ocurrio un error al enviar la solicitud.");
                }




            }
            else
            {
                MensajeAlerta.AlertaValidacion(this, mensaje);
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
                Page.Response.Redirect("PosterioresDigitales.aspx");

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idSolicitudBuzon = int.Parse(HFFolio.Value);
            string ruta = HFRuta.Value;
            string NombreDoc = HFNombre.Value;


            bool resultado = BuzonSolicitud.EliminarSolicitud(idSolicitudBuzon, ruta, NombreDoc);
            if (resultado)
            {
                MensajeAlerta.AlertaSatisfactorioRedireccion(this, "Se elimino correctamente su solicitud.", "InicialesDigitales.aspx");
            }
            else
            {
                MensajeAlerta.AlertaError(this, "Ocurrio un error al eliminar su solicitud");
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            long IdSolicitudBuzon = long.Parse(HFFolio.Value);

            var listaDocumentos = InfoDocumentosFirma.ObtenerdatosDocumentosDigitales(IdSolicitudBuzon);

            if (listaDocumentos.Count > 0)
            {
                Session["DocxFirmar"] = listaDocumentos;

                Page.Response.Redirect("FirmaDocumentos.aspx");
            }
            else
            {
                MensajeAlerta.AlertaAviso(this, "Ocurrio un error al obtener los datos de la solicitud.");
            }
        }
    }
}