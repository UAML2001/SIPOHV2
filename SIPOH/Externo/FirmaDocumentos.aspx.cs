using DatabaseConnection;
using Newtonsoft.Json;
using SIPOH.Firma;
using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Externo
{
    public partial class FirmaDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                List<InfoDocumentosFirma> listaDocumentos =   Session["DocxFirmar"]  as List<InfoDocumentosFirma>;
                gridFirma.DataSource = listaDocumentos;
                gridFirma.DataBind();

                HFDatosNotificacion.Value = new JavaScriptSerializer().Serialize(listaDocumentos);
            }
        }

        protected void BtnGenerarArchivosFEA_Click(object sender, EventArgs e)
        {

            string DatosNotificacion = HFDatosNotificacion.Value;
            List<InfoDocumentosFirma> LDatosDocumentos = JsonConvert.DeserializeObject<List<InfoDocumentosFirma>>(DatosNotificacion);

            List<DocumentoTransfer> LTransfer = JsonConvert.DeserializeObject<List<DocumentoTransfer>>(HFDatosTransfer.Value);
            if (LTransfer.Count > 0)
            {
                foreach (DocumentoTransfer ItemTransfer in LTransfer)
                {
                    long IdSolicitudBuzon = ItemTransfer.IdSolicitudBuzon;

                    List<DocumentoTransfer> LRegistrosFirma = DocumentoTransfer.ObtenerDocumentosTransfer(IdSolicitudBuzon);

                    DocumentoTransfer FirmaExistente = (from Fir in LRegistrosFirma
                                                        where Fir.CN == ItemTransfer.CN
                                                        select Fir).FirstOrDefault();
                    int Agregados = 0;

                    if (FirmaExistente == null)
                    {
                        //Almacenar Transfer Generado en la BD
                        DocumentoTransfer NotificaTransfer = new DocumentoTransfer();
                        //NotificaTransfer.IdDocumento = ItemTransfer.IdDocumento;
                        NotificaTransfer.IdTransfer = ItemTransfer.IdTransfer;
                        NotificaTransfer.Descripcion = ItemTransfer.Descripcion;
                        NotificaTransfer.Fecha = ItemTransfer.Fecha;
                        NotificaTransfer.Huella = ItemTransfer.Huella;
                        NotificaTransfer.CN = ItemTransfer.CN;
                        NotificaTransfer.HexSerie = ItemTransfer.HexSerie;
                        NotificaTransfer.IdDocDigital = ItemTransfer.IdDocDigital;
                        //NotificaTransfer.DigestHash = HFDigestHash.Value;

                        Agregados = DocumentoTransfer.InsertarDocumentoTransfer(NotificaTransfer);
                    }
                    else
                        Agregados = 1;

                    if (Agregados > 0)
                    {
                        //string transferenciafirmausuario = HFTransfer.Value;
                        if (LTransfer.Count > 0)
                        {
                            WebServiceSoapClient cliente = new WebServiceSoapClient();
                            string nombreArchivoOriginal = ItemTransfer.Nombrearchivopdf_Original;
                            string RutaArchivoPdfOriginal = ItemTransfer.RutapdfOriginal.Replace("/", @"\");
                            string RutaArchivoPdfFirmado = ItemTransfer.Rutapdf_Firmado;

                            string rutaArchivosOriginales = ConexionBD.ObtenerRutaRedDocumentos(); // @"\\192.168.73.245\Notificaciones\";
                            string rutaArchivoOriginal = rutaArchivosOriginales + RutaArchivoPdfOriginal + nombreArchivoOriginal;
                            string rutaPKCS7 = rutaArchivoOriginal + ".p7m";
                            AuthSoapHd auth = new AuthSoapHd();
                            string entidad = (System.Web.Configuration.WebConfigurationManager.AppSettings["entidad"]);
                            string usuario = ((System.Web.Configuration.WebConfigurationManager.AppSettings["usuario"]));
                            string password = ((System.Web.Configuration.WebConfigurationManager.AppSettings["clave"]));
                            auth.Entidad = entidad;
                            auth.Usuario = usuario;
                            auth.Clave = password;

                            //var NombreArchivo = Path.GetFileName(rutaArchivoOriginal);
                            string rutaPDF = ConexionBD.ObtenerRutaRedDocumentos(); // @"\\192.168.73.245\Notificaciones\";
                            string rutaArchivoPDFFinal = rutaPDF + RutaArchivoPdfOriginal + ItemTransfer.Nombrearchivopdf_Original.Replace("_O.pdf", "_F.pdf"); // NombreArchivo;

                            LRegistrosFirma = DocumentoTransfer.ObtenerDocumentosTransfer(IdSolicitudBuzon);

                            string IdsTransfers = "";
                            for (int i = 0; i < LRegistrosFirma.Count; i++)
                            {
                                if (i == 0)
                                    IdsTransfers = LRegistrosFirma[i].IdTransfer.ToString();
                                else
                                    IdsTransfers += "," + LRegistrosFirma[i].IdTransfer.ToString();
                            }

                            var resultado = cliente.PwuObtienePkcs7Ns(auth, "Generacion pkcs7", rutaArchivoOriginal, rutaPKCS7, IdsTransfers);
                            if (resultado.State == 0)
                            {
                                resultado = cliente.PwuObtienePdf(auth, "Generacion PDF", rutaPKCS7, rutaArchivoPDFFinal);
                                if (resultado.State == 0)
                                {
                               
                                  bool respuesta =  BuzonSolicitud.modificar_doc_digital(IdSolicitudBuzon, RutaArchivoPdfOriginal.Replace(@"\", "/"), ItemTransfer.Nombrearchivopdf_Original.Replace("_O.pdf", "_F.pdf"));
                                    if (respuesta)
                                    {
                                        bool res = BuzonSolicitud.modificar_buzonSolicitud(IdSolicitudBuzon, "E");
                                        if (res)
                                        {
                                            MensajeAlerta.AlertaSatisfactorioRedireccionPanel(this,"Su solicitud fue enviada correctamente.", "Inicio.aspx");
                                        }
                                    }
                                    else
                                    {
                                        MensajeAlerta.AlertaErrorPanel(this, "Error al modificar el documento digital.");
                                    }
                                }
                                else
                                {
                                    MensajeAlerta.AlertaErrorPanel(this, resultado.Descript);
                                }
                            }
                            else
                            {
                                MensajeAlerta.AlertaErrorPanel(this, resultado.Descript);
                            }
                        }
                    }
                    else
                    {
                        MensajeAlerta.AlertaErrorPanel(this,"No se puedo agregar el registro sobre el transfer");
                    }
                }
            }
        }
    }
}