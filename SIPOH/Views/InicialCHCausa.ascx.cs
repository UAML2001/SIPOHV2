using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Views
{
    public partial class InicialCHCausa : System.Web.UI.UserControl
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //string TipoDocumento = ;
            if (!IsPostBack)
            {
                Session["TipoDocumentoHistorico"] = null;
                
            }
                        
            MostrarCustom();
            //HistoricoCausaJuicioOral.Update();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

            if (Session["TipoDocumentoHistorico"] != null && int.TryParse(Session["TipoDocumentoHistorico"].ToString(), out int TipoDocumento))
            {

                if (TipoDocumento == 1)
                {
                    // Mostrar ControlUsuario1
                    buttonContainer.Style["display"] = "none !important";
                    CausaCustom.Visible = true;
                    JuicioOralCustom.Visible = false;
                    
                }
                else if (TipoDocumento == 3)
                {
                    // Mostrar ControlUsuario2
                    buttonContainer.Style["display"] = "none !important";
                    JuicioOralCustom.Visible = true;
                    CausaCustom.Visible = false;
                    
                }
                else if (TipoDocumento == 2)
                {
                    buttonContainer.Style["display"] = "none !important";
                    JuicioOralCustom.Visible = false;
                    CausaCustom.Visible = false;                   
                    
                    MensajeError("Seleccionaste por NUC, necesitas agregar una causa para poder agregar un juicio oral.");
                }
                else
                {
                    MensajeError("Error");
                }
            }
            else
            {
                //MensajeError("Error al obtener el tipo de documento histórico.");
                JuicioOralCustom.Visible = false;
                CausaCustom.Visible = false;

            }
        }

            protected void MostrarCustom()
        {
            //Debug.WriteLine("JUZGADO HISTORICO: " + Session["IdJuzgadoHistorico"] + " tipo archivo: " + Session["TipoDocumentoHistorico"] + " NumeroArchivo " + Session["NumeroDocumentoHistorico"] + " Sitema: " + Session["TipoSistema"]);

            CausaCustom.Controls.Clear();
            JuicioOralCustom.Controls.Clear();
            int tipoArchivo = Session["TipoDocumentoHistorico"] != null ? Convert.ToInt32(Session["TipoDocumentoHistorico"]) : 0;
            int juzgadoHistorico = Session["IdJuzgadoHistorico"] != null ? Convert.ToInt32(Session["IdJuzgadoHistorico"]) : 0;
            string numeroArchivo = Session["NumeroDocumentoHistorico"] as string;
            string tipoSistema = Session["TipoSistema"] as string;

            
            CustomHistoricosJuicioOral control2 = (CustomHistoricosJuicioOral)LoadControl("~/Views/CustomHistoricosJuicioOral.ascx");
            control2.TipoArchivo = tipoArchivo;
            control2.JuzgadoHistorico = juzgadoHistorico;
            control2.NumeroArchivo = numeroArchivo;
            control2.TipoSistema = tipoSistema;
            JuicioOralCustom.Controls.Add(control2);
            CustomHistoricosCupreCausa control1 = (CustomHistoricosCupreCausa)LoadControl("~/Views/CustomHistoricosCupreCausa.ascx");
            control1.TipoArchivo = tipoArchivo;
            control1.JuzgadoHistorico = juzgadoHistorico;
            control1.NumeroArchivo = numeroArchivo;
            control1.TipoSistema = tipoSistema;
            CausaCustom.Controls.Add(control1);
            HistoricoCausaJuicioOral.Update();
            
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //int TipoDocumento = int.Parse(numero.Text);
            
            if (Session["TipoDocumentoHistorico"] == null || Session["IdJuzgadoHistorico"] == null || Session["NumeroDocumentoHistorico"] == null)
            {
                MensajeError("No hay datos en pantalla");
            }
            string tipoArchivo = Session["TipoDocumentoHistorico"] as string;
            int juzgadoHistorico = Session["IdJuzgadoHistorico"] != null ? Convert.ToInt32(Session["IdJuzgadoHistorico"]) : 0;
            string numeroArchivo = Session["NumeroDocumentoHistorico"] as string;
            string tipoSistema = Session["TipoSistema"] as string;           
            //Session["IdJuzgadoHistorico"]
            //Session["NumeroDocumentoHistorico"]

        }
        
        protected void MensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastInfo('{mensaje}');", true);
        }
        protected void MensajeError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastError('{mensaje}');", true);
        }
        


    }
}