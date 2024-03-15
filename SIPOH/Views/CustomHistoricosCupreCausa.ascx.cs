using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatDelitosController;
using static SIPOH.Controllers.AC_RegistroHistoricos.RegistroHistoricoCausaController;
using static SIPOH.Controllers.AC_RegistroInicialJuicioOral.InicialJuicioOralController;
using static SIPOH.Views.CustomRegistroIniciales;

namespace SIPOH.Views
{
    public partial class CustomHistoricosCupreCausa : System.Web.UI.UserControl
    {

        

       public int TipoArchivo { get; set; }
       public int JuzgadoHistorico { get; set; }
        public string NumeroArchivo { get; set; }
        public string TipoSistema { get; set; }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Actualizar propiedades con los valores de las sesiones
            TipoArchivo = Session["TipoDocumentoHistorico"] != null ? Convert.ToInt32(Session["TipoDocumentoHistorico"]): 0;
            JuzgadoHistorico = Session["IdJuzgadoHistorico"] != null ? Convert.ToInt32(Session["IdJuzgadoHistorico"]) : 0;
            TipoSistema = Session["TipoSistema"] as string;
            NumeroArchivo = Session["NumeroDocumentoHistorico"] as string;
            
            if (TipoArchivo == 1 && (TipoSistema == "acusatorio" || TipoSistema == "tradicional"))
            {
                inputNumeroArchivo.Text = NumeroArchivo;
                inputNumeroArchivo.ReadOnly = true;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatDelitosDropDownList();
                
                limpiarSessions();
                limpiarCamposDatosGenerales();
                limpiarCamposDatosInculpado();
                limpiarCamposDatosVictima();
                inputDelitos.SelectedIndex = 0;
                Session.Remove("TipoDocumentoHistorico");
                Session.Remove("TipoSistema");
                Session.Remove("IdJuzgadoHistorico");
                Session.Remove("NumeroDocumentoHistorico");



                
            }
            

        }
        protected void ddlPersonaVictima_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí puedes agregar lógica basada en la selección del DropDownList
            if (ddlPersonaVictima.SelectedValue == "F")
            {
            
                containerGeneroVictima.Style["display"] = "block";
                containerNombreVictima.Style["display"] = "block";
                containerApellidoMVictima.Style["display"] = "block";
                containerApellidoPVictima.Style["display"] = "block";
                containerRazonSocialVictima.Style["display"] = "none";
                
            
            }
            else if (ddlPersonaVictima.SelectedValue == "M")
            {            
                
                // Lógica cuando se selecciona 'Moral'
                containerGeneroVictima.Style["display"] = "none";
                containerNombreVictima.Style["display"] = "none";
                containerApellidoMVictima.Style["display"] = "none";
                containerApellidoPVictima.Style["display"] = "none";
                containerRazonSocialVictima.Style["display"] = "block";
            }else{            
                
                containerRazonSocialVictima.Style["display"] = "none";
                containerGeneroVictima.Style["display"] = "none";
                containerNombreVictima.Style["display"] = "none";
                containerApellidoMVictima.Style["display"] = "none";
                containerApellidoPVictima.Style["display"] = "none";
            

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal", "$('#modalAgregarVictimas').modal('show');", true);
            
            //ScriptManager.RegisterStartupScript(this ,this.GetType(), "CerrarModalScript", "CerrarModal();", true);

            UpdatePanelModal.Update();
        }

        private bool CamposFaltantes(out string[] camposFaltantes)
        {
            string NumeroCausa = inputNumeroArchivo.Text;
            string NucHistorico = inputNUCHistorico.Text;
            int TipoSolicitud = int.Parse(inputTipoSolicitudHistorico.SelectedValue);
            string FechaRecepcion = inputFechaRecepcionC.Text;
            int NumeroFojas = int.Parse(inputNumeroFojasCHistorico.Text);
            string TipoRadicacion = inpuTipoRadicacion.SelectedValue;
            string ObservacionesHistorico = inputObservaciones.Text;
            camposFaltantes = new string[]
            {
                string.IsNullOrWhiteSpace(NumeroCausa) ? "Número Causa" : "",
                string.IsNullOrWhiteSpace(NucHistorico) ? "NUC" : "",
                TipoSolicitud <= 0 ? "Tipo de solicitud" : "",
                string.IsNullOrWhiteSpace(FechaRecepcion) ? "Fecha de recepción" : "",
                NumeroFojas <=0 ?"Número de fojas" : "",
                string.IsNullOrWhiteSpace(TipoRadicacion) ? "Tipo de radicación" : "",
            };
            return camposFaltantes.Any(campo => !string.IsNullOrWhiteSpace(campo));
        }
        private void ProcesarDatos()
        {
            string NumeroCausa = inputNumeroArchivo.Text;
            string NucHistorico = inputNUCHistorico.Text;
            int TipoSolicitud = int.Parse(inputTipoSolicitudHistorico.SelectedValue);
            string FechaRecepcion = inputFechaRecepcionC.Text;
            int NumeroFojas = int.Parse(inputNumeroFojasCHistorico.Text);
            string TipoRadicacion = inpuTipoRadicacion.SelectedValue;
            string ObservacionesHistorico = inputObservaciones.Text;
            int IdUsuario = int.Parse(Session["IdUsuario"] as string);
            int IdJuzgado;
            string TipoAsuntoH;
            if (TipoSistema == "acusatorio" || TipoSistema == "tradicional")
            {
                IdJuzgado = JuzgadoHistorico;
                
            }
            else
            {
                IdJuzgado = int.Parse(Session["IDJuzgado"] as string);

            }
            TipoAsuntoH = (TipoSistema == "acusatorico") ? "C" : (TipoSistema == "tradicional") ? "T" : "C";

            int IdPefilA = int.Parse(Session["IdPerfil"] as string);


            DataInsertHistoricoAsunto infoInsert = new DataInsertHistoricoAsunto
            {
                NumeroDocumento = NumeroCausa,
                IdJuzgado = IdJuzgado,
                FeIngreso = FechaRecepcion,
                TipoAsunto = TipoAsuntoH,
                Digitalizado = "S",
                IdUsuario = IdUsuario,
                IdAudiencia = TipoSolicitud,
                Observaciones = ObservacionesHistorico,
                QuienIngresa = "O",
                MP = "Registro Histórico",
                Prioridad = "N",
                Fojas = NumeroFojas,
                NUC = NucHistorico,
                TipoRadicacion = TipoRadicacion,
                IdPerfil = IdPefilA
            };
            try {
                List<DataInsertHistoricoAsunto> listaInfoInsert = new List<DataInsertHistoricoAsunto>();
                listaInfoInsert.Add(infoInsert);
                List<DataInsertHistoricoVictimas> listaVictimas = Session["VictimasHistorico"] as List<DataInsertHistoricoVictimas>;
                if (listaVictimas == null || listaVictimas.Count == 0)
                {
                    throw new InvalidOperationException("La lista de víctimas está vacía.");
                }

                List<DataInsertHistoricoInculpados> listaInculpados = Session["InculpadosHistorico"] as List<DataInsertHistoricoInculpados>;
                if (listaInculpados == null || listaInculpados.Count == 0)
                {
                    throw new InvalidOperationException("La lista de inculpados está vacía");
                }

                List<DataCatDelitos> listaDelitos = Session["DelitosHistorico"] as List<DataCatDelitos>;
                if (listaDelitos == null || listaDelitos.Count == 0)
                {
                    throw new InvalidOperationException("La lista de delitos está vacía.");
                }

                List<DataResultadoHistorico> datos = InsertHistoricoCausa(listaInfoInsert, listaVictimas, listaInculpados, listaDelitos);
                foreach (var d in datos)
                {
                    if (d.HayError)
                    {
                        //Debug.WriteLine("Error query I CAN SEE IT: " + d.MensajeResultado);
                        //Debug.WriteLine("ICANSEEIT: " + d.HayError);
                       throw new Exception($"Lo siento, {d.MensajeResultado}.");
                    }
                    else
                    {
                        limpiarCamposDatosGenerales();
                        limpiarCamposDatosInculpado();
                        limpiarCamposDatosVictima();
                        inputDelitos.SelectedIndex = 0;
                        limpiarSessions();
                        limpiarRepeaters();
                        string mensajeExito = $"Tu petición fue correctamente enviada. {d.MensajeResultado}";
                        MensajeExito(mensajeExito);
                        HistoricosCausaPanel.Update();
                    }
                    

                }
            
            }
            catch (Exception ex){
                MensajeError(ex.Message);
            }
            
        }
        protected void btnGuardarInicial_Click(object sender, EventArgs e)
        {
            try
            {
                string[] camposFaltantes;
                if(CamposFaltantes(out camposFaltantes))
                {
                    string camposMensaje = string.Join(", ", camposFaltantes.Where(campo => !string.IsNullOrEmpty(campo)));
                    string mensaje = $"Los siguientes campos son obligatorios: {camposMensaje}";
                   throw new Exception($"{mensaje}");

                }
                ProcesarDatos();
                

            }
            catch (Exception ex)
            {
                
                MensajeError(ex.Message);
            }
        }
        
        
        protected void ddlQuienIngresa_Selected(object sender, EventArgs e)
        {

        }
        protected void ddlPrioridadSelected(object sender, EventArgs e)
        {

        }

        protected void btnAgregarVictima_Click(object sender, EventArgs a)
        {
            
            string nombreVictima = txtNombreVictima.Text;
            string apellidoPaterno = txtAPVictima.Text;
            string razonSocial = txtRazonSocialVictima.Text;
            string apellidoMaterno =txtAMVictima.Text;
            string genero = ddlSexoVictima.SelectedValue;
            List<DataInsertHistoricoVictimas> listaVictimas = Session["VictimasHistorico"] as List<DataInsertHistoricoVictimas> ?? new List<DataInsertHistoricoVictimas>();
            if (listaVictimas.Any(v => string.Equals(v.NombreParte, nombreVictima, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoMaterno, apellidoMaterno, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoPaterno, apellidoPaterno, StringComparison.OrdinalIgnoreCase)
                                           
                                          ))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades
                limpiarCamposDatosVictima();
                HistoricosCausaPanel.Update();
                ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
                string mensaje = "Esta víctima ya existe en la lista.";
                MensajeError(mensaje);
                return;
            }      
            if (ddlPersonaVictima.SelectedValue == "F")
            {
                limpiarCamposDatosVictima();
                HistoricosCausaPanel.Update();
                if (string.IsNullOrWhiteSpace(nombreVictima) ||
                    string.IsNullOrWhiteSpace(apellidoMaterno) ||
                    string.IsNullOrWhiteSpace(apellidoPaterno) ||
                    string.IsNullOrWhiteSpace(genero))
                {
                    MensajeError("Todos los campos para registrar una víctima son obligatorios.");
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
                    containerRazonSocialVictima.Style["display"] = "none";
                    containerGeneroVictima.Style["display"] = "none";
                    containerNombreVictima.Style["display"] = "none";
                    containerApellidoMVictima.Style["display"] = "none";
                    containerApellidoPVictima.Style["display"] = "none";
                    
                    limpiarCamposDatosVictima();
                    HistoricosCausaPanel.Update();
                    return;
                }
                
                    DataInsertHistoricoVictimas nuevaVictima = new DataInsertHistoricoVictimas
                {
                    NombreParte = nombreVictima,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Genero = genero
                };
                listaVictimas.Add(nuevaVictima);
                Session["VictimasHistorico"] = listaVictimas;
                RepeaterVictimas.DataSource = listaVictimas;
                RepeaterVictimas.DataBind();
                containerRazonSocialVictima.Style["display"] = "none";
                containerGeneroVictima.Style["display"] = "none";
                containerNombreVictima.Style["display"] = "none";
                containerApellidoMVictima.Style["display"] = "none";
                containerApellidoPVictima.Style["display"] = "none";
                ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
                limpiarCamposDatosVictima();
                HistoricosCausaPanel.Update();
            }else if(ddlPersonaVictima.SelectedValue == "M")
            {
                limpiarCamposDatosVictima();
                HistoricosCausaPanel.Update();
                if (listaVictimas.Any(v => string.Equals(v.ApellidoPaterno, razonSocial, StringComparison.OrdinalIgnoreCase)))                                                       
                {
                    // Mostrar mensaje de error o lanzar una excepción según tus necesidades
                    limpiarCamposDatosVictima();

                    string mensaje = "Esta víctima ya existe en la lista.";
                    MensajeError(mensaje);
                    containerRazonSocialVictima.Style["display"] = "none";
                    containerGeneroVictima.Style["display"] = "none";
                    containerNombreVictima.Style["display"] = "none";
                    containerApellidoMVictima.Style["display"] = "none";
                    containerApellidoPVictima.Style["display"] = "none";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
                    return;
                }
                limpiarCamposDatosVictima();
                HistoricosCausaPanel.Update();
                if (string.IsNullOrWhiteSpace(razonSocial))
                {

                    limpiarCamposDatosVictima();
                    
                    HistoricosCausaPanel.Update();
                    containerRazonSocialVictima.Style["display"] = "none";
                    containerGeneroVictima.Style["display"] = "none";
                    containerNombreVictima.Style["display"] = "none";
                    containerApellidoMVictima.Style["display"] = "none";
                    containerApellidoPVictima.Style["display"] = "none";
                    MensajeError("El campo de razón social debe estar lleno");
                    ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
                    return;
                }
                DataInsertHistoricoVictimas nuevaVictima = new DataInsertHistoricoVictimas
                {
                    NombreParte = "",
                    ApellidoPaterno = razonSocial,
                    ApellidoMaterno = "",
                    Genero = "O"
                };
                listaVictimas.Add(nuevaVictima);
                Session["VictimasHistorico"] = listaVictimas;
                RepeaterVictimas.DataSource = listaVictimas;
                RepeaterVictimas.DataBind();
                limpiarCamposDatosVictima();
            
            
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
                containerRazonSocialVictima.Style["display"] = "none";
                containerGeneroVictima.Style["display"] = "none";
                containerNombreVictima.Style["display"] = "none";
                containerApellidoMVictima.Style["display"] = "none";
                containerApellidoPVictima.Style["display"] = "none";
                //HistoricosCausaPanel.Update();

            }
            else
            {
                containerRazonSocialVictima.Style["display"] = "none";
                containerGeneroVictima.Style["display"] = "none";
                containerNombreVictima.Style["display"] = "none";
                containerApellidoMVictima.Style["display"] = "none";
                containerApellidoPVictima.Style["display"] = "none";
                //HistoricosCausaPanel.Update();
                limpiarCamposDatosVictima();
                MensajeError("Selecciona un tipo de parte");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal", "CerrarModal();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "ActivarScroll", "ActivarScroll();", true);
        }
        protected void btnEliminarVictimaList(object sender, EventArgs e)
        {
            Button btnEliminarVictima =(Button)sender;
            RepeaterItem item = (RepeaterItem)btnEliminarVictima.NamingContainer;
            int indice = item.ItemIndex;
            List<DataInsertHistoricoVictimas> listaVictimas = (List<DataInsertHistoricoVictimas>)Session["VictimasHistorico"];
            if(listaVictimas != null & listaVictimas.Count > indice)
            {
                listaVictimas.RemoveAt(indice);
                RepeaterVictimas.DataSource = listaVictimas;
                RepeaterVictimas.DataBind();

            }
            HistoricosCausaPanel.Update();

        }
        public void btnAgregarImputado_Click(object sender, EventArgs e)
        {
            string nombreInculpado = txtNombreImputado.Text;
            string apellidoMaterno = txtAMaternoImputado.Text;
            string apellidoPaterno = txtAPaternoImputado.Text;
            string genero = txtGeneroImputado.Text;
            string alias = txtAliasImputado.Text;
            if (string.IsNullOrWhiteSpace(nombreInculpado) ||
                string.IsNullOrWhiteSpace(apellidoPaterno) ||
                string.IsNullOrWhiteSpace(apellidoMaterno) ||
                string.IsNullOrWhiteSpace(genero))
            {
                MensajeError("Todos los campos para agregar un inculpado son necesarios");
                return;
            }

            List<DataInsertHistoricoInculpados> listaDeImputados = Session["InculpadosHistorico"] as List<DataInsertHistoricoInculpados> ?? new List<DataInsertHistoricoInculpados>();

            if (listaDeImputados.Any(v => string.Equals(v.NombreParte, nombreInculpado, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoPaterno, apellidoPaterno, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoMaterno, apellidoMaterno, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.Alias, alias, StringComparison.OrdinalIgnoreCase)))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades
                limpiarCamposDatosInculpado();
                MensajeError("Este imputado ya ha sido registrado");
                return;
            }

            DataInsertHistoricoInculpados inculpado = new DataInsertHistoricoInculpados
            {
                NombreParte = nombreInculpado,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Genero = genero,
                Alias = alias
            };

            listaDeImputados.Add(inculpado);
            Session["InculpadosHistorico"] = listaDeImputados;

            RepeaterInputados.DataSource = listaDeImputados;
            RepeaterInputados.DataBind();
            limpiarCamposDatosInculpado();
           
            HistoricosCausaPanel.Update();

        }
        protected void btnEliminarCulpadoList(object sender, EventArgs e)
        {
            Button btnEliminarInculpado = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnEliminarInculpado.NamingContainer;
            int indice = item.ItemIndex;
            List<DataInsertHistoricoInculpados> listaInculpados = (List<DataInsertHistoricoInculpados>)Session["InculpadosHistorico"];
            if (listaInculpados != null & listaInculpados.Count > indice)
            {
                listaInculpados.RemoveAt(indice);
                RepeaterInputados.DataSource = listaInculpados;
                RepeaterInputados.DataBind();

            }
            HistoricosCausaPanel.Update();
        }

        protected void btnAgregarDelito_Click(object sender, EventArgs e)
        {
            copyNumeroArchivo.Text = inputNumeroArchivo.Text;
            copyTextBoxNUC.Text = inputNUCHistorico.Text;
            copyDropDownTipoAsunto.Text = inputTipoSolicitudHistorico.SelectedItem.Text.ToUpper();
            copyFechaRecepcionC.Text = inputFechaRecepcionC.Text.ToUpper();
            copyNumeroFojasCHistorico.Text = inputNumeroFojasCHistorico.Text;
            copyTipoRadicacion.Text = inpuTipoRadicacion.SelectedItem.Text.ToUpper();
            copyObservaciones.Text = inputObservaciones.Text.ToUpper();

            if (string.IsNullOrEmpty(inputDelitos.SelectedValue))
            {
                MensajeError("Por favor, selecciona un delito.");
                return;
            }

            int IdDelitoSelected;
            if (!int.TryParse(inputDelitos.SelectedValue, out IdDelitoSelected))
            {
                MensajeError("El valor seleccionado no es válido.");
                return;
            }

            string DelitoSelected = inputDelitos.SelectedItem.Text;

            DataCatDelitos delito = new DataCatDelitos
            {
                IdDelito = IdDelitoSelected,
                Delito = DelitoSelected
            };

            List<DataCatDelitos> listaDelitos = Session["DelitosHistorico"] as List<DataCatDelitos> ?? new List<DataCatDelitos>();

            if (listaDelitos.Any(v => v.IdDelito == IdDelitoSelected))
            {
                MensajeError("Este delito ya existe en la lista");
                return;
            }

            listaDelitos.Add(delito);
            Session["DelitosHistorico"] = listaDelitos;
            RepeaterDelitos.DataSource = listaDelitos;
            RepeaterDelitos.DataBind();
            inputDelitos.SelectedIndex = 0;
            HistoricosCausaPanel.Update();
        }

        protected void btnEliminarDelitoList(object sender, EventArgs e)
        {
            Button btnDelitos = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnDelitos.NamingContainer;
            int indice = item.ItemIndex;
            List<DataCatDelitos> listaDelitos = (List<DataCatDelitos>)Session["DelitosHistorico"];
            if (listaDelitos != null & listaDelitos.Count > indice)
            {
                listaDelitos.RemoveAt(indice);
                RepeaterDelitos.DataSource = listaDelitos;
                RepeaterDelitos.DataBind();

            }
            HistoricosCausaPanel.Update();
        }
        private void CargarCatDelitosDropDownList()
        {
            List<DataCatDelitos> catDelitos = GetCatDelitos();
            inputDelitos.DataSource = catDelitos;
            inputDelitos.DataTextField = "Delito";
            inputDelitos.DataValueField = "IdDelito";
            inputDelitos.DataBind();

        }
        protected void MensajeExito(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastInfo('{Mensaje}');", true);
        }
        protected void MensajeError(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastError('{Mensaje}');", true);
        }
        
        
     
        private void limpiarCamposDatosGenerales()
        {
            inputNumeroArchivo.Text =   "";
            inputNUCHistorico.Text = "";
            inputTipoSolicitudHistorico.SelectedIndex = 0;
            inputFechaRecepcionC.Text = "";
            inputNumeroFojasCHistorico.Text = "";
            inpuTipoRadicacion.SelectedIndex = 0;
            inputObservaciones.Text = "";
            copyTextBoxNUC.Text = "";
            copyDropDownTipoAsunto.Text = "";            
            copyFechaRecepcionC.Text = "";
            copyNumeroFojasCHistorico.Text = "";
           
           
            copyTipoRadicacion.Text = "";
           
            copyObservaciones.Text = "";
            

        }
        private void limpiarCamposDatosInculpado()
        {
            txtAPaternoImputado.Text = "";
            txtAMaternoImputado.Text = "";
            txtNombreImputado.Text = "";
            txtGeneroImputado.SelectedIndex = 0;
            txtAliasImputado.Text = "";
        }
        private void limpiarCamposDatosVictima()
        {
            ddlPersonaVictima.SelectedIndex = 0;
            txtRazonSocialVictima.Text = "";
            txtAPVictima.Text = "";
            txtAMVictima.Text = "";
            txtNombreVictima.Text = "";
            ddlSexoVictima.SelectedIndex = 0;
        }
        private void limpiarSessions()
        {
            Session["VictimasHistorico"] = new List<DataInsertHistoricoVictimas>();
            Session["DelitosHistorico"] = new List<DataCatDelitos>();
            Session["InculpadosHistorico"] = new List<DataInsertHistoricoInculpados>();
            

        }
        private void limpiarRepeaters()
        {
            RepeaterVictimas.DataSource = null;
            RepeaterVictimas.DataBind();
            RepeaterInputados.DataSource = null;
            RepeaterInputados.DataBind();
            RepeaterDelitos.DataSource = null;
            RepeaterDelitos.DataBind();
        }

    }
}