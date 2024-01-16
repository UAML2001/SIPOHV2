using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Views.CustomRegistroIniciales;

namespace SIPOH.Views
{
    //public class AnexosPromocion { 
        
    //}
    public class DataPromocion
    {
        public string Promovente { get; set; }
        public string Digitalizado { get; set; }
        public string TipoPromocion { get; set; }
        public string FechaRecepcion { get; set; }
        public string Tipo { get; set; }

        //MORE
        public int IdActividad { get; set; }
        

        public string FeAsunto { get; set; }
        
        
        public string EstadoPromocion { get; set; }

    }
    public class AnexosPromocion
    {
        public string DescripcionAnexo { get; set; }
        public string CantidadAnexo { get; set; }
        public string DigitalizadoAnexo { get; set; }
    }

    public class BusquedaPromocion
    {
        public string DataNumero { get; set; }
        public string DataTipoAsunto { get; set; }        
    }
    public partial class CustomPromocion : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarCatAnexosDropDownList();
                Session["Anexos"] = new List<AnexosPromocion>();
                CleanEtiquetaFormAnexo();
                CleanEtiquetaInicial();
                Session.Remove("TipoAsuntoPromocion");
                Session.Remove("NumeroPromocion");                
                Session.Remove("DelitosPromocion");
                Session.Remove("InculpadosPromocion");
                Session.Remove("VictimasPromocion");
                Session.Remove("IdAsuntoPromocion");
                Session.Remove("NumeroAmparoPromocion");
                Session.Remove("AutoridadResponsablePromocion");
                Session.Remove("EstatusPromocion");
                Session.Remove("EtapaPromocion");
            }
        }
        protected void DrpLstObtenerTipoDocumento(object sender, EventArgs e)
        {
            string tipoDocumento = DrpLstTipoDocumento.SelectedValue;
            if (tipoDocumento == "A") 
            {
                tipoDocumento = "Amparo";                
            }else if (tipoDocumento == "C")
            {
                tipoDocumento = "Causa";
            }else if(tipoDocumento == "JO")
            {
                tipoDocumento = "Juicio Oral";
            }else if(tipoDocumento == "E")
            {
                tipoDocumento = "Exhorto";
            }else if(tipoDocumento == "CP")
            {
                tipoDocumento = "Cupre";
            }
            else
            {
                tipoDocumento = "";
            }
            itemNombre.Text = tipoDocumento;
            promocionPanel.Update();
        }
        protected void btnEnviarPromocion(object sender, EventArgs e)
        {
            try
            {
            string promovente = inputPromovente.Text;
            string fechaCaptura = inputFechaRecepcion.Text;                

                if (string.IsNullOrEmpty(promovente) || string.IsNullOrEmpty(fechaCaptura))
                {
                    string mensaje = "Los campos promovente y fecha son obligatorios.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", script, true);
                    return;
                }
                if (Session["IdAsuntoPromocion"] == null || string.IsNullOrEmpty(Session["IdAsuntoPromocion"].ToString()))
                {
                    string mensaje = "No tienes un numero de documento asignado, por favor busca uno!";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", script, true);
                    return;
                }
                DataPromocion registro = new DataPromocion
                {
                    Promovente = promovente,
                    Digitalizado = "N",
                    TipoPromocion = "P",
                    FechaRecepcion = fechaCaptura,
                    Tipo = "P",
                    IdActividad = 1,
                    FeAsunto = fechaCaptura,                
                    EstadoPromocion = "A"
                };
                    List<DataPromocion> listaPromocion = Session["promocion"] as List<DataPromocion> ?? new List<DataPromocion>();
                    listaPromocion.Add(registro);
                    Session["RegistroPromocion"] = listaPromocion;
                //verificar si el metodo funciona 
                    List<AnexosPromocion> listaAnexos = Session["Anexos"] as List<AnexosPromocion> ?? new List<AnexosPromocion>();
                    RegistroPromociones.SendRegistroPromocion(listaPromocion, listaAnexos);

            }catch (Exception ex)
            {
                Debug.WriteLine("Error dentro codigo: " + ex);
                string mensaje = "Error: " + ex;
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                CleanEtiquetaFormAnexo();
                return;
            }


            CleanEtiquetaInicial();
            Session.Remove("Anexos");
            Session["Anexos"] = new List<AnexosPromocion>();
            CleanEtiquetaFormAnexo();
            Session.Remove("TipoAsuntoPromocion");            
            Session.Remove("NumeroPromocion");            
            Session.Remove("DelitosPromocion");
            Session.Remove("InculpadosPromocion");
            Session.Remove("VictimasPromocion");
            Session.Remove("IdAsuntoPromocion");
            Session.Remove("NumeroAmparoPromocion");
            Session.Remove("AutoridadResponsablePromocion");
            Session.Remove("EstatusPromocion");
            Session.Remove("EtapaPromocion");
            promocionPanel.Update();

        }
        protected void btnGetConsultaPromocion(object sender, EventArgs e)
        {
           
                string numeroPromocion = inputNumero.Text;
            string tipoDocumento = DrpLstTipoDocumento.SelectedValue;


            switch (tipoDocumento)
            {
                case "C":
                    RegistroPromociones.GetDataPromocion(numeroPromocion, tipoDocumento = "C");                   
                    break;
                case "JO":
                    RegistroPromociones.GetDataPromocion(numeroPromocion, tipoDocumento = "JO");                    
                    break;
                case "E":
                    RegistroPromociones.GetDataPromocion(numeroPromocion, tipoDocumento = "E");                    
                    break;
                case "CP":
                    RegistroPromociones.GetDataPromocion(numeroPromocion, tipoDocumento = "CP");                    
                    break;
                default:
                    string mensaje = "Selecciona un filtrado";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
            }
            //string tipoDocumento = "C";
            RegistroPromociones.GetDataPromocion(numeroPromocion, tipoDocumento);
            lblVictimasPromocion.Text = Session["VictimasPromocion"] as string;
            // Dividir las víctimas por comas
            string victimas = lblVictimasPromocion.Text;
            string[] victimasArray = victimas.Split(',');

            // Construir la representación HTML de las víctimas como botones o con efecto hover
            string victimasHtml = string.Join(", ", victimasArray.Select(v => $"<a class='link-success' onclick='seleccionarVictima(\"{v.Trim()}\")'>{v.Trim()}</a>"));

            // Asignar el HTML generado a la etiqueta lblVictimasPromocion
            lblVictimasPromocion.Text = victimasHtml;

            lblNumeroPromocion.Text = Session["NumeroPromocion"] as string;
                lblDelitosPromocion.Text = Session["DelitosPromocion"] as string;
                lblInculpadosPromocion.Text = Session["InculpadosPromocion"] as string;
                lblNumeroAmparoPromocion.Text = Session["NumeroAmparoPromocion"] as string;

                if (lblNumeroAmparoPromocion.Text == "")
                {
                    string Mensaje = "Este documento no tiene un Amparo";
                    ResultadoSolicitudPromociones.Text = Mensaje;
                }
                else
                {
                    ResultadoSolicitudPromociones.Visible = false;
                }
                lblAutoridadResponsablePromocion.Text = Session["AutoridadResponsablePromocion"] as string;
                lblEstatusPromocion.Text = Session["EstatusPromocion"] as string;
                lblEtapaPromocion.Text = Session["EtapaPromocion"] as string;
                promocionPanel.Update();

        }

        private void CargarCatAnexosDropDownList()
        {
            List<string> catAnexos = RegistroPromociones.GetCatAnexo();
            //Elementos
            txtAnexosTipo.DataSource = catAnexos;
            txtAnexosTipo.DataBind();
        }
        protected void btnAñadirAnexo(object sender, EventArgs e)
        {
            string inputTipoAnexo = txtAnexosTipo.SelectedValue;
            string inputCantidadAnexo = txtCantidadAnexos.Text;
            if (inputTipoAnexo == "Otro")
            {
                string inputDescripcionAnexos = txtDescripcionAnexos.Text;


                AnexosPromocion anexo = new AnexosPromocion
                {
                    //TipoAnexo = inputTipoAnexo,
                    DescripcionAnexo = inputDescripcionAnexos,
                    CantidadAnexo = inputCantidadAnexo,
                    DigitalizadoAnexo = "N"

                };
                List<AnexosPromocion> listaAnexos = Session["Anexos"] as List<AnexosPromocion> ?? new List<AnexosPromocion>();

                if (listaAnexos.Any(v => v.DescripcionAnexo == inputDescripcionAnexos))
                {
                    // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                    string mensaje = "Este anexo ya existe en la lista.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }

                listaAnexos.Add(anexo);
                Session["Anexos"] = listaAnexos;
                RepeaterAnexos.DataSource = listaAnexos;
                RepeaterAnexos.DataBind();
                CleanEtiquetaFormAnexo();
                promocionPanel.Update();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(inputTipoAnexo) ||
                    string.IsNullOrWhiteSpace(inputCantidadAnexo))
                {
                    // Mostrar mensaje toast de error o lanzar una excepción según tus necesidades
                    // Por ejemplo, puedes mostrar un mensaje y no continuar con el proceso

                    string mensaje = "Ingresa la cantidad de anexos.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }
                AnexosPromocion anexo = new AnexosPromocion
                {
                    //TipoAnexo = inputTipoAnexo,
                    DescripcionAnexo = inputTipoAnexo,
                    CantidadAnexo = inputCantidadAnexo,
                    DigitalizadoAnexo = "N"

                };
                List<AnexosPromocion> listaAnexos = Session["Anexos"] as List<AnexosPromocion> ?? new List<AnexosPromocion>();

                if (listaAnexos.Any(v => v.DescripcionAnexo == inputTipoAnexo))
                {
                    // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                    string mensaje = "Este anexo ya existe en la lista.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }

                listaAnexos.Add(anexo);
                Session["Anexos"] = listaAnexos;
                RepeaterAnexos.DataSource = listaAnexos;
                RepeaterAnexos.DataBind();
                CleanEtiquetaFormAnexo();
                promocionPanel.Update();
            }


        }

    private void CleanEtiquetaFormAnexo()
    {
        txtDescripcionAnexos.Text = "";
        txtCantidadAnexos.Text = "";
        txtAnexosTipo.SelectedIndex = 0;


    }
        private void CleanEtiquetaInicial()
        {
            inputPromovente.Text = "";
            inputFechaRecepcion.Text = "";
        }
    }
}