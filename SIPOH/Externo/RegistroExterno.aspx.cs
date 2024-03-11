using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseConnection;
using Newtonsoft.Json;
using SIPOH.Firma;
using SIPOH.Models;

namespace SIPOH.Externo
{
    public partial class RegistroExterno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnActivarNE_Click(object sender, EventArgs e)
        {
            DatosFirmaUsuario DatosFirmaUsuario = JsonConvert.DeserializeObject<DatosFirmaUsuario>(HFDatosFirmaUsuario.Value);
            List<LineamientosTransfer> LTransfer = JsonConvert.DeserializeObject<List<LineamientosTransfer>>(HFDatosTransfer.Value);

            if (LTransfer.Count > 0)
            {
                foreach (LineamientosTransfer ItemTransfer in LTransfer)
                {
                    bool respuesta = false;
                    int IdLineamientos = ItemTransfer.IdLineamientos;
                    UsuarioExterno Notificado = UsuarioExterno.ObtenerNNotificado(DatosFirmaUsuario.subjectCURP.ToUpper(), ref respuesta);

                    if (Notificado.IdUsuarioExterno == 0)
                    {

                   
                        WebServiceSoapClient cliente = new WebServiceSoapClient();
                        string nombreArchivoOriginal = ItemTransfer.NombreArchivo;
                        string rutaArchivosOriginales = ConexionBD.ObtenerRutaRedLineamientos(); ;
                        string rutaArchivoOriginal = rutaArchivosOriginales + nombreArchivoOriginal;
                        string rutaPKCS7 = rutaArchivosOriginales + nombreArchivoOriginal + ".p7m";
                        AuthSoapHd auth = new AuthSoapHd();
                        string entidad = (System.Web.Configuration.WebConfigurationManager.AppSettings["entidad"]);
                        string usuario = ((System.Web.Configuration.WebConfigurationManager.AppSettings["usuario"]));
                        string password = ((System.Web.Configuration.WebConfigurationManager.AppSettings["clave"]));
                        auth.Entidad = entidad;
                        auth.Usuario = usuario;
                        auth.Clave = password;

                        var NombreArchivo = Path.GetFileName(rutaArchivoOriginal);
                        string rutaPDF = ConexionBD.ObtenerRutaRedLineamientosFirma();
                        string rutaArchivoPDFFinal = rutaPDF + DatosFirmaUsuario.subjectCURP + "_" + ItemTransfer.IdLineamientos + ".pdf";

                        var resultado = cliente.PwuObtienePkcs7Ns(auth, "Generacion pkcs7", rutaArchivoOriginal, rutaPKCS7, ItemTransfer.IdTransfer.ToString());
                        if (resultado.State == 0)
                        {
                            resultado = cliente.PwuObtienePdf(auth, "Generacion PDF", rutaPKCS7, rutaArchivoPDFFinal);
                            if (resultado.State == 0)
                            {
                                UsuarioExterno Notif = new UsuarioExterno();
                                Notif.Nombre = ItemTransfer.Nombre.ToUpper();
                                Notif.ApPaterno = ItemTransfer.ApPaterno.ToUpper();
                                Notif.ApMaterno = ItemTransfer.ApMaterno.ToUpper();
                                Notif.CorreoE = DatosFirmaUsuario.subjectEmail.ToUpper();
                                Notif.CURP = DatosFirmaUsuario.subjectCURP;
                                Notif.NombreCompleto = DatosFirmaUsuario.subjectName;
                                Notif.Estado = "3";
                                Notif.FechaActivacion = ItemTransfer.Fecha;

                                int IdUsuarioExterno = UsuarioExterno.InsertarUsuarioExterno(Notif);

                                if (IdUsuarioExterno > 0)
                                {
                                    //Almacenar Transfer Generado en la BD
                                    LineamientosTransfer lineamientosTransfer = new LineamientosTransfer();
                                    lineamientosTransfer.IdUsuarioExterno = IdUsuarioExterno;
                                    lineamientosTransfer.IdLineamientos = ItemTransfer.IdLineamientos;
                                    lineamientosTransfer.IdTransfer = ItemTransfer.IdTransfer;
                                    lineamientosTransfer.Descripcion = ItemTransfer.Descripcion;
                                    lineamientosTransfer.Fecha = ItemTransfer.Fecha;
                                    lineamientosTransfer.Evidencia = ItemTransfer.Evidencia;
                                    lineamientosTransfer.Huella = ItemTransfer.Huella;
                                    lineamientosTransfer.CN = ItemTransfer.CN;
                                    lineamientosTransfer.HexSerie = ItemTransfer.HexSerie;

                                    int Agregados = LineamientosTransfer.InsertarLineamientosTransfer(lineamientosTransfer);

                                    if (Agregados > 0)
                                    {


                                        //Enviado por medio de método C#
                                        //NotificacionCorreo Correo = new NotificacionCorreo();
                                        //Correo.RutaRedireccion = ConexionBD.ObtenerRutaSIPOH();
                                        //Correo.ruta = ConexionBD.ObtenerRutaLineamientosFirma() + DatosFirmaUsuario.subjectCURP + "_" + ItemTransfer.IdLineamientos + ".pdf";
                                        //Correo.NombreCompleto = DatosFirmaUsuario.subjectName;
                                        //Correo.Folio = ItemTransfer.Fecha.Year.ToString() + "-" + IdUsuarioExterno.ToString() + "FEJEH";
                                        //Correo.FechaActivacion = ItemTransfer.Fecha;
                                        //Correo.GenerarContenidoActivarNotificacion(); // Genera Contenido del Correo

                                        //EnviarEmail EnviarCorreo = new EnviarEmail();
                                        //EnviarCorreo.Contenido = Correo.Contenido;
                                        //EnviarCorreo.Correos = DatosFirmaUsuario.subjectEmail;
                                        //EnviarCorreo.Titulo = "Sistema de notificación electrónica judicial";

                                        //int CorreoEnviado = EnviarEmail.Envio(EnviarCorreo);



                                        Page.Session["IdUsuarioExterno"] = IdUsuarioExterno;

                                        string _open2 = $"window.open('{ConexionBD.ObtenerRutaLineamientosFirma() + DatosFirmaUsuario.subjectCURP + "_" + ItemTransfer.IdLineamientos + ".pdf"}','_blank');";
                                        //string _open2 = $"window.open('{rutaArchivoPDFFinal}','_blank');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open2, true);

                                        string _open = "window.open('LoginExterno.aspx');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
                                        //}
                                    }
                                    else
                                    {
                                        string mensaje = "Error firmar los terminos y condiciones";
                                        string script = $"toastError('{mensaje}');";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                                    }
                                     

                                }
                                else
                                {
                                    string mensaje = "Error al registar el usuario.";
                                    string script = $"toastError('{mensaje}');";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                                }
                               
                            }
                            else
                            {
                                //Mostrar error al generar el pdf
                                string mensaje = "Error firmar los terminos y condiciones";
                                string script = $"toastError('{mensaje}');";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                            }
                        }
                        else
                        {
                            //Mostrar mensaje de erroIdSerier en la generación de pkcs7
                            string mensaje = "Error firmar los terminos y condiciones";
                            string script = $"toastError('{mensaje}');";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                        }
                        //}

                    }
                    else
                    {
                        string mensaje = "El usuario ya ha solicitado previamente el registro para notificación electrónica. Se redireccionará al inicio de sesión.";
                        string script = $"toastRedireccion('{mensaje}');";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    
                    }

                }
            }
        }
    }
}