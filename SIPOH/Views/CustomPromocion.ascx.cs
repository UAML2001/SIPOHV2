using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
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


                if (Session["IdAsuntoPromocion"] == null || string.IsNullOrEmpty(Session["IdAsuntoPromocion"].ToString()))
                {
                    string mensaje = "No tienes un numero de documento asignado, por favor busca uno!";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", script, true);
                    return;
                }
                if (string.IsNullOrEmpty(promovente) || string.IsNullOrEmpty(fechaCaptura))
                {
                    List<string> camposFaltantes = new List<string>();

                    if (string.IsNullOrEmpty(promovente))
                    {
                        camposFaltantes.Add("promovente");
                    }

                    if (string.IsNullOrEmpty(fechaCaptura))
                    {
                        camposFaltantes.Add("fecha");
                    }

                    string mensaje = $"Campos {string.Join(" y ", camposFaltantes)} son obligatorios.";
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
                string mensajeSuccess = "Tu peticion fue correcta!, tu registro se ha hecho correctamente. ";

                string scriptToast = $"toastInfo('{mensajeSuccess}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastInfoScript", scriptToast, true);
                // CODIGOTICKET
                string ticket = CrearTicketSELLO();
                TicketDiv.Style["display"] = "block";
                ocultarBtnModal.Style["display"] = "none !important";
                TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");
                ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicket();", true);
                tituloSello.Style["display"] = "block";
                ScriptManager.RegisterStartupScript(this, GetType(), "mostrarTituloSello", "mostrarTituloSello();", true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error dentro codigo: " + ex);
                string mensaje = "Error: " + ex;
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                CleanEtiquetaFormAnexo();
                return;
            }

            CleanBusquedaPromocion();
            CleanEtiquetaInicial();
            CleanTables();
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


        private List<string> DividirTextoEnLineas(string texto, int maxCaracteresPorLinea)
        {
            List<string> lineas = new List<string>();
            string[] palabras = texto.Split(' ');
            string lineaActual = "";

            foreach (string palabra in palabras)
            {
                if ((lineaActual.Length > 0) && (lineaActual.Length + palabra.Length + 1 > maxCaracteresPorLinea))
                {
                    lineas.Add(lineaActual);
                    lineaActual = "";
                }

                if (lineaActual.Length > 0)
                    lineaActual += " ";

                lineaActual += palabra;
            }

            if (lineaActual.Length > 0)
                lineas.Add(lineaActual);

            return lineas;
        }
        private int CantidadAnexos(List<AnexosPromocion> anexos)
        {
            return anexos?.Sum(a => Convert.ToInt32(a.CantidadAnexo)) ?? 0;
        }
        public static List<string> listaDeAnexos = new List<string>();
        private void ImprimirCentrado(StringBuilder ticket, string texto)
        {
            int maxLength = 37;
            int totalPadding = maxLength - texto.Length;
            int padLeft = totalPadding / 2 + texto.Length;
            string centeredLine = texto.PadLeft(padLeft).PadRight(maxLength);
            ticket.AppendLine(centeredLine);
        }
        public string CrearTicketSELLO()
        {
            string TipoAsunto = Session["TipoAsuntoPromocion"] as string;

            StringBuilder ticket = new StringBuilder();
            string nombreJuzgado = Session["NombreJuzgado"] as string;
            List<AnexosPromocion> anexos = Session["Anexos"] as List<AnexosPromocion>;

            List<string> lineasNombreJuzgado = DividirTextoEnLineas(nombreJuzgado, 32);

            int cantidadAnexos = CantidadAnexos(anexos);

            string NUC = Session["NUCPromocion"] as string;
            string Causa = Session["NumeroPromocion"] as string;


            ImprimirCentrado(ticket, "TRIBUNAL SUPERIOR");
            ImprimirCentrado(ticket, "DE JUSTICIA");
            ImprimirCentrado(ticket, "DEL ESTADO DE HIDALGO");
            ImprimirCentrado(ticket, "ATENCION CIUDADANA");


            ImprimirCentrado(ticket, ".........");
            foreach (string linea in lineasNombreJuzgado)
            {
                ImprimirCentrado(ticket, linea);
            }

            ImprimirCentrado(ticket, "PROMOCION");


            ImprimirCentrado(ticket, ".........");


            if (TipoAsunto == "C")
            {
                var AsuntoIncial = "CAUSA";
                ticket.AppendLine($"{AsuntoIncial}: {Causa}");

            }
            else if (TipoAsunto == "CP")
            {
                var AsuntoIncial = "CUPRE";
                ticket.AppendLine($"{AsuntoIncial}:{Causa}");
            }else if(TipoAsunto == "JO")
            {
                var AsuntoIncial = "JUICIO ORAL";
                ticket.AppendLine($"{AsuntoIncial}:{Causa}");
            }
            else if(TipoAsunto == "E")
            {
                var AsuntoIncial = "EXHORTO";
                ticket.AppendLine($"{AsuntoIncial}:{Causa}");
            }
            ticket.AppendLine($"FECHA RECEPCIÒN:{GetFechaYHora()}");
            ticket.AppendLine($"NUC:{NUC.ToUpper()}");

            int maxLength = 36;
            int maxLengthT = 30;

            foreach (var anexo in anexos)
            {
                int espacioEntreColumnas = 3; // Puedes ajustar este valor según tus necesidades
                int longitudTotal = maxLength - espacioEntreColumnas;

                string linea = $"{anexo.DescripcionAnexo.ToUpper()}".PadRight(longitudTotal, '.') + $"{anexo.CantidadAnexo.ToUpper()}";
                ticket.AppendLine(linea);
            }


            int espacioEntreColumnasT = 3; // Puedes ajustar este valor según tus necesidades
            int longitudTotalT = maxLengthT - espacioEntreColumnasT;

            string separador = new string('.', longitudTotalT);
            
            if (cantidadAnexos != 0)
            {
                ticket.AppendLine($"TOTAL:{separador}{cantidadAnexos}");
            }
            else
            {
                Debug.WriteLine("No hay anexos");
            }





            return ticket.ToString();
        }

        //FIN SELLO
        protected string GetFechaYHora()
        {
            string formatoPersonalizado = "yyyy-MM-dd HH:mm:ss";
            string fechaYHoraFormateada = DateTime.Now.ToString(formatoPersonalizado);
            return fechaYHoraFormateada;
        }
        protected void btnGetConsultaPromocion(object sender, EventArgs e)
        {
           
                string numeroPromocion = inputNumero.Text;
            string tipoDocumento = DrpLstTipoDocumento.SelectedValue;
            copyNumeroDocumento.Text = numeroPromocion;


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
            lblInculpadosPromocion.Text = Session["InculpadosPromocion"] as string ;

            string imputados = lblInculpadosPromocion.Text;
            string[] imputadosArray = imputados.Split(',');
            string imputadosHtml = string.Join(", ", imputadosArray.Select(v => $"<a class='link-success' onclick='seleccionarImputado(\"{v.Trim()}\")'>{v.Trim()}</a>"));
            lblInculpadosPromocion.Text = imputadosHtml;

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
                
                ocultarBtnModal.Style["display"] = "block";
            TicketDiv.Style["display"] = "none";
            tituloSello.Style["display"] = "none !important";
            //ocultarBtnModal.Style["display"] = "block";
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
                RepeaterAnexosPrev.DataSource = listaAnexos;
                RepeaterAnexosPrev.DataBind();
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
                RepeaterAnexosPrev.DataSource = listaAnexos;
                RepeaterAnexosPrev.DataBind();
                RepeaterAnexos.DataBind();
                CleanEtiquetaFormAnexo();
                promocionPanel.Update();
            }


        }
        protected void btnEliminarAnexo(object sender, EventArgs e)
        {
            // Obtener el botón que activó el evento
            Button btnEliminar = (Button)sender;

            // Obtener la fila del Repeater que contiene el botón
            RepeaterItem item = (RepeaterItem)btnEliminar.NamingContainer;
            //item.Visible = false;
            // Obtener el índice de la fila en el Repeater
            int indice = item.ItemIndex;

            // Obtener la lista de la sesión
            List<AnexosPromocion> listaAnexos = (List<AnexosPromocion>)Session["Anexos"];

            // Verificar si la lista no es nula y tiene elementos
            if (listaAnexos != null && listaAnexos.Count > indice)
            {
                // Eliminar el elemento en la posición indicada por el índice
                //item.Visible = false;
                listaAnexos.RemoveAt(indice);
                //RepeaterAnexos.Controls.RemoveAt(indice);
                //Session["Anexos"] = listaAnexos;

                RepeaterAnexos.DataSource = listaAnexos;
                RepeaterAnexos.DataBind();
            }
            // Verificar si la lista no es nula y tiene elementos
            promocionPanel.Update();

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
        private void CleanBusquedaPromocion()
        {
            inputNumero.Text = "";
            DrpLstTipoDocumento.SelectedIndex = 0;
            copyNumeroDocumento.Text = "";
            copyFechaRecepcion.Text = "";
            copyPromovente.Text = "";
            lblVictimasPromocion.Text = "";
            lblNumeroPromocion.Text = "";
            lblInculpadosPromocion.Text = "";
            lblDelitosPromocion.Text = "";
            lblNumeroAmparoPromocion.Text = "";
            lblAutoridadResponsablePromocion.Text = "";
            lblEstatusPromocion.Text = "";
            lblEtapaPromocion.Text = "";
            ResultadoSolicitudPromociones.Text = "";

        }
        private void CleanTables()
        {
            RepeaterAnexos.DataSource = null;
            RepeaterAnexos.DataBind();
            RepeaterAnexosPrev.DataSource = null;
            RepeaterAnexosPrev.DataBind();
        }


    }
}