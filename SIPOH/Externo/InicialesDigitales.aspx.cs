using DatabaseConnection;
using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Externo
{
    public partial class InicialesDigitales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (ListItem li in Generales.GenerarCatalogo("ObtenerCatCircuitos").Items)
                {
                    ddlCircuito.Items.Add(li);
                }

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

                //string embed = "<object class=\".iframe-container\" data=\"{0}\" type=\"application/pdf\"  width=\"100%\" height=\"500px\" ></object>";
                //embedPdf.Text = string.Format(embed, ResolveUrl("https://www.clickdimensions.com/links/TestPDFfile.pdf"));


                int IdUsuarioExterno = int.Parse(Session["IdUsuarioExterno"].ToString());
                bool respuesta = false;
                BuzonSolicitud solicitud = BuzonSolicitud.ObtenerbuzonSolicitud(IdUsuarioExterno, "I", ref respuesta);
                if (respuesta && solicitud.IdSolicitud > 0)
                {
                    divInicio.Visible = false;
                    divPreguardar.Visible = true;
                    HFFolio.Value = solicitud.IdSolicitudBuzon.ToString(); ;
                    txtFolio.Text = solicitud.IdSolicitudBuzon.ToString();
                    txtTipoSolicitud.Text = solicitud.Solicitud;
                    txtFiguraSolicitante.Text = solicitud.Solicitante;
                    txtJuzgado.Text = solicitud.NombreJuzgado;
                    txtPosiblesHechosSolicitud.Text = solicitud.PosiblesHechos;
                    txtAnexosSolicitud.Text = solicitud.DescripAnexos;
                    txtNUCSolicitud.Text = solicitud.NUC;
                    HFRuta.Value = solicitud.RutapdfOriginal;
                    HFNombre.Value = solicitud.Nombrearchivopdf_Original;

                    iframepdf.Src = $"{ConexionBD.ObtenerRutaSIPOHDocumentos()}{solicitud.RutapdfOriginal}{solicitud.Nombrearchivopdf_Original}" ;
                }
                else
                {
                    divPreguardar.Visible = false;
                }



            }
        }

        protected void btnPreguardar_Click(object sender, EventArgs e)
        {
            string mensaje = Validacion();
            if (mensaje == "")
            {

                int IdUsuarioExterno = int.Parse(Session["IdUsuarioExterno"].ToString());
                BuzonSolicitud solicitud = new BuzonSolicitud
                {
                    NUC = txtNUC.Text.ToUpper().Trim(),
                    IdSolicitante = int.Parse(ddlFiguraSolicitante.SelectedValue),
                    IdSolicitud = int.Parse(ddlTipoSolicitud.SelectedValue),
                    IdJuzgado = int.Parse(ddlJuzgado.SelectedValue),
                    DescripAnexos = txtAnexos.Text.ToUpper().Trim(),
                    PosiblesHechos = txtHechos.Text.ToUpper().Trim(),
                    IdUsuarioExterno = IdUsuarioExterno,
                    Tipo = "I",
                    Estatus = "P"
                };

                long Folio = 0;
                string rutaDoc = "";
                bool respuesta = BuzonSolicitud.Insertarsolicitud(solicitud, filePDF.FileBytes, ref Folio, ref rutaDoc);

                if (respuesta)
                {
                    divInicio.Visible = false;
                    divPreguardar.Visible = true;
                    txtFolio.Text = Folio.ToString();
                    HFFolio.Value = Folio.ToString();
                    txtTipoSolicitud.Text = ddlTipoSolicitud.SelectedItem.Text;
                    txtFiguraSolicitante.Text = ddlFiguraSolicitante.SelectedItem.Text;
                    txtJuzgado.Text = ddlJuzgado.SelectedItem.Text;
                    txtPosiblesHechosSolicitud.Text = solicitud.PosiblesHechos;
                    txtAnexosSolicitud.Text = solicitud.DescripAnexos;
                    txtNUCSolicitud.Text = solicitud.NUC;

                    iframepdf.Src = "http://nas.pjhidalgo.gob.mx/SIPOH/Solicitudes/"+ rutaDoc;


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



        string Validacion()
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

            if (ddlCircuito.SelectedIndex < 1)
            {
                mensaje += "- Circuito -";
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int idSolicitudBuzon = int.Parse(HFFolio.Value);
            string ruta = HFRuta.Value;
           string NombreDoc =  HFNombre.Value;


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
           long IdSolicitudBuzon =  long.Parse(HFFolio.Value );
            
            var listaDocumentos = InfoDocumentosFirma.ObtenerdatosDocumentosDigitales(IdSolicitudBuzon);

            if (listaDocumentos.Count > 0)
            {
                Session["DocxFirmar"] = listaDocumentos;

                Page.Response.Redirect("FirmaDocumentos.aspx");
            }
            else
            {
                MensajeAlerta.AlertaAviso(this,"Ocurrio un error al obtener los datos de la solicitud.");
            }
            

        }
    }
}