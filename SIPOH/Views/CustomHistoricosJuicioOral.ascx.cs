using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatTipoSolicitudController;
using static SIPOH.Controllers.AC_RegistroHistoricos.RegistroHistoricoJOController;
using static SIPOH.Controllers.AC_RegistroInicialJuicioOral.InicialJuicioOralController;

namespace SIPOH.Views
{
    public partial class CustomHistoricosJuicioOral : System.Web.UI.UserControl
    {


        public int TipoArchivo { get; set; }
        public int JuzgadoHistorico { get; set; }
        public string TipoSistema { get; set; }
        public string NumeroArchivo { get; set; }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Actualizar propiedades con los valores de las sesiones
            TipoArchivo = Session["TipoDocumentoHistorico"] != null ? Convert.ToInt32(Session["TipoDocumentoHistorico"]) : 0;
            JuzgadoHistorico = Session["IdJuzgadoHistorico"] != null ? Convert.ToInt32(Session["IdJuzgadoHistorico"]) : 0;
            TipoSistema = Session["TipoSistema"] as string;
            NumeroArchivo = Session["NumeroDocumentoHistorico"] as string;            
            if (TipoArchivo == 3 &&  TipoSistema == "acusatorio")
            {
                inputNumeroArchivo.Text = NumeroArchivo;
                inputNumeroArchivo.ReadOnly = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                
                CatAudienciasHistoricoJO();
                lblIdSeleccionado.Visible = false;
                limpiarRegistroDatosPrimeraParte();
                limpiarRegistroDatosSegundaParte();
                limpiarRegistroDatosTerceraParte();
                limpiarTablaRelaciones();
                Session.Remove("TipoDocumentoHistorico");
                Session.Remove("TipoSistema");
                Session.Remove("IdJuzgadoHistorico");
                Session.Remove("NumeroDocumentoHistorico");
            }
        }
        protected void btnConsultaHistoricoCausa(object sender, EventArgs e)
        {
            
            try
            {
                limpiarRegistroDatosTerceraParte();
                limpiarTablaRelaciones();
                limpiarRegistroDatosSegundaParte();
                string numeroArchivo = inputNumero.Text;
                int IdJuzgado;
                if (TipoSistema == "acusatorio")
                {
                    IdJuzgado = JuzgadoHistorico;
                }
                else
                {
                    IdJuzgado = int.Parse(Session["IDJuzgado"] as string);

                }
                
                List<DataGetCausaImputadoJuicioOralHistorico> infoCausa = GetCausaHistorico(numeroArchivo, IdJuzgado);
                if(infoCausa != null && infoCausa.Any())
                {
                    ConsultaCausa.Visible = true;
                    ocultarAGuardar.Style["display"] = "block";
                    RepeaterListaPartes.DataSource = infoCausa;
                    RepeaterListaPartes.DataBind();
                    MensajeExito("Causa encontrada");
                    JuicioOralHistoricoPanel.Update();
                }
                else
                {
                    throw new Exception("Esta causa no fue encontrada");
                }
            }catch(Exception ex) {
                MensajeError($"¡{ex.Message }!");
            }
        }
        protected void ObtenerDelitos_Click(object sender, EventArgs e)
        {
            Button btnw = (Button)sender;
            foreach(RepeaterItem item in RepeaterListaPartes.Items)
            {
                Button btnItem = (Button)item.FindControl("controlSelected");
                btnItem.CssClass = btnItem.CssClass.Replace("bg-success", "");
            }
            btnw.CssClass += " bg-success";
            try
            {
                string idAsuntoCausa = HiddenIdAsuntoCausa.Value;
                lblIdSeleccionado.Text = idAsuntoCausa;
                Button btn = (Button)sender;
                string idPartes = btn.CommandArgument;
                int idPartesInt = int.Parse(idPartes);
                List<DataGetCausaDelitosJuicioOralHistorico> infoDelito = GetDelitosHistorico(idPartesInt);
                if(infoDelito != null && infoDelito.Any())
                {
                    DelitosInculpado.Visible = true;
                    string Mensaje = "Se encontró delitos de tu inculpado.";
                    MensajeExito(Mensaje);
                    RepeaterListaDelitos.DataSource = infoDelito;
                    RepeaterListaDelitos.DataBind();
                    JuicioOralHistoricoPanel.Update();
                }
                else
                {
                    VictimasDelDelito.Visible = false;
                    throw new Exception("No se encontró ningún delito asociado a tus inculpados, busca un número de causa válido o elige otro inculpado");
                }
            }catch(Exception ex)
            {
                MensajeError($"¡{ex.Message}!");
            }
        }
        protected void ddlTipoAudiencia_Selected(object sender, EventArgs e)
        {
            var tipoAudi = inputTipoAudiencia.SelectedItem.Text;
            copyInputTipoAudiencia.Text = tipoAudi.ToUpper();
            JuicioOralHistoricoPanel.Update();

        }
        protected void CatAudienciasHistoricoJO()
        {
            List<DataTipoAudiencia> ListaTipoAudiencia = GetCatTipoSolicitud();
            inputTipoAudiencia.DataSource = ListaTipoAudiencia;
            inputTipoAudiencia.DataTextField = "CAA_Descripcion";
            inputTipoAudiencia.DataValueField = "CAA_IdAudi";
            inputTipoAudiencia.DataBind();
        }
        protected void ddlQuienIngresa_Selected(object sender, EventArgs e)
        {

        }
        protected void RepeaterListaPersonas_Items(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtener el valor de IdAsuntoCausa del item actual del repeater<<
                string idAsuntoCausa = DataBinder.Eval(e.Item.DataItem, "IdAsuntoCausa").ToString();
                string NombreInculpado = DataBinder.Eval(e.Item.DataItem, "Nombre").ToString();
                lblIdSeleccionado.Text = NombreInculpado;
                HiddenIdAsuntoCausa.Value = idAsuntoCausa;

            }
        }

        protected void GridRelaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;

            DataTable dtRelaciones = (DataTable)Session["RelacionesHistoricoVI"];
            if (dtRelaciones != null && dtRelaciones.Rows.Count > rowIndex)
            {
                dtRelaciones.Rows.RemoveAt(rowIndex);

                GridRelaciones.DataSource = dtRelaciones;
                GridRelaciones.DataBind();
            }
            // Obtener el ID de la fila que se va a eliminar
            List<DataRelacionesVIHistorico> listaDatos = (List<DataRelacionesVIHistorico>)Session["PartesHistorico"];
            if (listaDatos != null && listaDatos.Count > rowIndex)
            {
               

                listaDatos.RemoveAt(rowIndex);

                // Volver a enlazar el GridView

            }

            JuicioOralHistoricoPanel.Update();
        }
        protected void chkDelito_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkDelito = (CheckBox)sender;
            if (chkDelito.Checked)
            {
                foreach(RepeaterItem item in RepeaterListaDelitos.Items)
                {
                    if(item.FindControl("chkDelito1") is CheckBox otherCheckBox && otherCheckBox != chkDelito)
                    {
                        otherCheckBox.Checked = false;
                    }
                }
            }
            
            try
            {
                string idDeliAsunto = chkDelito.Attributes["data-idDeliAsunto"];
                string idInculpado = chkDelito.Attributes["data-idInculpado"];
                string delito = chkDelito.Attributes["data-DelitoSelected"];

                int idDeliAsuntoInt = int.Parse(idDeliAsunto);
                List<DataGetCausaVictimaJuicioOralHistorico> infoVictima = GetVictimaHistorico(idDeliAsuntoInt);
                if(infoVictima != null && infoVictima.Any())
                {
                    VictimasDelDelito.Visible = true;
                    MensajeExito("¡Los inculpado son los siguientes!");
                    RepeaterListaVictimasJO.DataSource = infoVictima;
                    RepeaterListaVictimasJO.DataBind();
                    JuicioOralHistoricoPanel.Update();
                }
                else
                {
                    VictimasDelDelito.Visible = false;
                    throw new Exception("No se encontró victimas asociadas al delito");
                }
            }catch(Exception ex)
            {
                MensajeError($"¡{ex.Message}!");
            }
        }
        protected void chkVictimaDel_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void btnGenerarRelacion_Click(object sender, EventArgs e)
        {
            copyInputNumero.Text = inputNumero.Text;
            copyInputNumeroArchivo.Text = inputNumeroArchivo.Text;
            copyInputTipoAudiencia.Text = inputTipoAudiencia.SelectedItem.Text.ToUpper();
            copyFechaRecepcion.Text = fechaRecepcion.Text.ToUpper();
            copyNumeroFojas.Text = numeroFojas.Text;
            copyObservacionesInicial.Text = observacionesIncial.Text.ToUpper();
            DataTable relacionesVI;
            if (Session["RelacionesHistoricoVI"] == null)
            {
                relacionesVI = new DataTable();
                relacionesVI.Columns.Add("Inculpado", typeof(string));
                relacionesVI.Columns.Add("Delitos", typeof(string));
                relacionesVI.Columns.Add("Victimas", typeof(string));
                relacionesVI.Columns.Add("Acciones", typeof(string));
                relacionesVI.Columns.Add("IdDelitosInculpado", typeof(int));
                relacionesVI.Columns.Add("IdDelitosVictima", typeof(int));
                Session["RelacionesHistoricoVI"] = relacionesVI;
            }
            else
            {
                relacionesVI = (DataTable)Session["RelacionesHistoricoVI"];
            }

            DataRelacionesVIHistorico infoRelacion = new DataRelacionesVIHistorico();
            List<DataRelacionesVIHistorico> listaRelacionesVI = new List<DataRelacionesVIHistorico>();
            foreach (RepeaterItem item in RepeaterListaDelitos.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkDelito = (CheckBox)item.FindControl("chkDelito1");
                    if (chkDelito != null && chkDelito.Checked)
                    {
                        string valorDelito = chkDelito.Attributes["Value"]; // Obtener el valor del atributo "Value"
                        infoRelacion.idDelito = int.Parse(chkDelito.Attributes["data-idDeliAsunto"]);
                        infoRelacion.idInculpado = int.Parse(chkDelito.Attributes["data-idInculpado"]);
                        infoRelacion.nombreInculpado = chkDelito.Attributes["data-NombreInculpado"];

                        infoRelacion.delito = chkDelito.Attributes["data-DelitoSelected"];

                        DataRelacionesVIHistorico infoRelacionTemporal = new DataRelacionesVIHistorico();
                        foreach (RepeaterItem itemV in RepeaterListaVictimasJO.Items)
                        {
                            if (itemV.ItemType == ListItemType.Item || itemV.ItemType == ListItemType.AlternatingItem)
                            {
                                CheckBox chkVictima = (CheckBox)itemV.FindControl("chkVictima1");
                                if (chkVictima != null && chkVictima.Checked)
                                {
                                    //DataRelacionesVI infoRelacionTemporal = new DataRelacionesVI();
                                    infoRelacionTemporal.idInculpado = infoRelacion.idInculpado;
                                    infoRelacionTemporal.delito = infoRelacion.delito;
                                    infoRelacionTemporal.nombreVictima = chkVictima.Attributes["data-NombreVictima"];
                                    infoRelacionTemporal.apellidosVictima = chkVictima.Attributes["data-ApellidosVictima"];
                                    infoRelacionTemporal.idVictima = int.Parse(chkVictima.Attributes["data-idVictimaDel"]);

                                    // Agregar una nueva fila a la DataTable relacionesVI
                                    DataRow newRow = relacionesVI.NewRow();
                                    newRow["Inculpado"] = infoRelacion.nombreInculpado;
                                    newRow["Delitos"] = infoRelacionTemporal.delito;
                                    newRow["Victimas"] = infoRelacionTemporal.nombreVictima + " " + infoRelacionTemporal.apellidosVictima;
                                    newRow["Acciones"] = "✖️";
                                    newRow["IdDelitosInculpado"] = infoRelacionTemporal.idInculpado;
                                    newRow["IdDelitosVictima"] = infoRelacionTemporal.idVictima;

                                    // Verificar si hay una lista existente en la sesión

                                    if (Session["PartesHistorico"] == null)
                                    {
                                        listaRelacionesVI = new List<DataRelacionesVIHistorico>();
                                    }
                                    else
                                    {
                                        // Si la lista ya existe en la sesión, recuperarla
                                        listaRelacionesVI = (List<DataRelacionesVIHistorico>)Session["PartesHistorico"];
                                    }

                                    // Verificar si el elemento ya existe en la lista antes de agregarlo
                                    bool existeRelacion = listaRelacionesVI.Any(relacion =>
                                        relacion.idInculpado == infoRelacionTemporal.idInculpado &&
                                        relacion.idVictima == infoRelacionTemporal.idVictima);

                                    if (!existeRelacion)
                                    {
                                        // Agregar el nuevo elemento a la lista solo si no existe
                                        DataRelacionesVIHistorico relacion = new DataRelacionesVIHistorico
                                        {
                                            idInculpado = infoRelacionTemporal.idInculpado,
                                            idVictima = infoRelacionTemporal.idVictima,
                                            // Añade otras propiedades que desees aquí
                                        };
                                        MensajeExito("Se añadieron las partes al Juicio Oral");
                                        relacionesVI.Rows.Add(newRow);
                                        listaRelacionesVI.Add(relacion);
                                        Session["PartesHistorico"] = listaRelacionesVI;
                                        //chkVictima.Enabled = false;

                                    }
                                    else
                                    {
                                        string mensajeError = "En tu actual petición solo se añadio los que no fueron repetidos";
                                        MensajeError(mensajeError);
                                    }




                                }
                            }
                        }
                    }
                }
            }


            // Asignar la DataTable actualizada como origen de datos para el Grid
            GridRelaciones.DataSource = relacionesVI;
            GridRelaciones.DataBind();
            JuicioOralHistoricoPanel.Update();
        }
        protected void btnGuardarJO_Click(object sender, EventArgs e)
        {
            try
            {
                string numeroArchivo = inputNumeroArchivo.Text;
                string observacionesJO = observacionesIncial.Text;
                string foja = numeroFojas.Text;
                string tipoAudiencia = inputTipoAudiencia.SelectedValue;
                string idCausa = lblIdSeleccionado.Text;
                string numeroCausa = inputNumero.Text;
                string fechaIngreso = fechaRecepcion.Text;
                if (string.IsNullOrEmpty(foja) ||
                     string.IsNullOrEmpty(tipoAudiencia) ||
                     string.IsNullOrEmpty(numeroArchivo) ||
                     string.IsNullOrEmpty(fechaIngreso))
                {

                    if (string.IsNullOrEmpty(foja))
                        throw new Exception("Número de fojas");               
                    else if (string.IsNullOrEmpty(tipoAudiencia))
                        throw new Exception("Tipo de audiencia");
                    else if (string.IsNullOrEmpty(numeroArchivo))
                        throw new Exception("Número de Juicio Oral");
                    else if (string.IsNullOrEmpty(fechaIngreso))
                        throw new Exception("Fecha Ingreso");
                }
                
                int IdJuzgado;                
                if (TipoSistema == "acusatorio")
                {
                    IdJuzgado = JuzgadoHistorico;
                    
                }
                else
                {
                    IdJuzgado = int.Parse(Session["IDJuzgado"] as string);
                    
                }
                int idCircuitos = int.Parse(Session["IdCircuito"] as string);
                int idUsuario = int.Parse(Session["IdUsuario"] as string);
                

                string fechaActualFormato = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
          
                DataInsertJuicioOralHistorico infoJO = new DataInsertJuicioOralHistorico
                {

                    NumeroArchivo = numeroArchivo,
                    IdCircuitoFolio = idCircuitos,
                    dIdJuzgado = IdJuzgado,
                    dFeIngreso = fechaIngreso,
                    dTipoAsunto = "JO",
                    dDigitalizado = "S",
                    dFeCaptura = fechaActualFormato,
                    dIdUsuario = idUsuario,
                    dIdAudiencia = int.Parse(tipoAudiencia),
                    Observaciones = observacionesJO,
                    QuienIngresa = "O",
                    dMP = "REGISTRO HISTORICO",
                    dPrioridad = "N",
                    dFojas = int.Parse(foja),

                    IdAsuntoCausa = int.Parse(idCausa),
                    NumCausa = numeroCausa
                };

          
        // Preparación de datos para la inserción
        List<DataInsertJuicioOralHistorico> listaInfoJO = new List<DataInsertJuicioOralHistorico> { infoJO };
                
                List<DataRelacionesVIHistorico> listaPartes = Session["PartesHistorico"] as List<DataRelacionesVIHistorico> ?? new List<DataRelacionesVIHistorico>();

                
                DatosJOHistorico data = InsertJuicioOralHistorico(listaInfoJO, listaPartes);
                // Inserción del juicio oral y sus datos relacionados
                Debug.WriteLine(data.HayError);
                Debug.WriteLine(data.MensajeError);
                

                if (data.HayError)
                {

                    if (listaPartes.Count == 0)
                    {
                        throw new Exception("Lo siento, Falta generar la relación de partes.");
                        
                    }
                    else
                    {

                        MensajeExito(data.MensajeError);
                        ocultarAGuardar.Style["display"] = "none !important";
                        limpiarRegistroDatosPrimeraParte();
                        limpiarRegistroDatosSegundaParte();
                        limpiarRegistroDatosTerceraParte();
                        limpiarTablaRelaciones();
                        JuicioOralHistoricoPanel.Update();
                        string mensajeRespuesta = "Envío exitoso. Tu registro se ha hecho correctamente.";
                        throw new Exception(mensajeRespuesta);
                    }



                }
                else if (!data.HayError)
                {
                    // Mostrar el mensaje de error en una ventana emergente
                    // Hubo un error en el proceso de inserción
                    var mensaje = $"{ data.MensajeError}";
                    
                    MensajeError(mensaje);
                    Debug.WriteLine("MAGSAGE: " +  mensaje);
                    JuicioOralHistoricoPanel.Update();
                    throw new Exception( mensaje );
                    


            }

            }
            catch(Exception ex)
            {
                MensajeError($"¡Error en tu consulta: {ex.Message}!");
            }
        }
        protected void MensajeError(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastError('{Mensaje}');", true);
        }
        protected void limpiarRegistroDatosTerceraParte()
        {
            RepeaterListaPartes.DataSource = null;
            RepeaterListaPartes.DataBind();
            RepeaterListaDelitos.DataSource = null;
            RepeaterListaDelitos.DataBind();
            RepeaterListaVictimasJO.DataSource = null;
            RepeaterListaVictimasJO.DataBind();
        }
        protected void limpiarRegistroDatosSegundaParte()
        {

            //Session["RelacionesHistoricoVI"] = new List<DataRelacionesVIHistorico>();
            Session["PartesHistorico"] = new List<DataRelacionesVIHistorico>();

        }
        protected void limpiarTablaRelaciones()
        {
            DataTable relacionesVI = new DataTable();
            relacionesVI.Columns.Add("Inculpado", typeof(string));
            relacionesVI.Columns.Add("Delitos", typeof(string));
            relacionesVI.Columns.Add("Victimas", typeof(string));
            relacionesVI.Columns.Add("Acciones", typeof(string));
            relacionesVI.Columns.Add("IdDelitosInculpado", typeof(int));
            relacionesVI.Columns.Add("IdDelitosVictima", typeof(int));

            // Asignar la nueva DataTable como valor para la sesión
            Session["RelacionesHistoricoVI"] = relacionesVI;

            // Asignar la DataTable actualizada como origen de datos para el Grid
            GridRelaciones.DataSource = relacionesVI;
            GridRelaciones.DataBind();
            JuicioOralHistoricoPanel.Update();
        }
        protected void limpiarRegistroDatosPrimeraParte()
        {
            inputNumero.Text = "";
            inputNumeroArchivo.Text = "";
            inputTipoAudiencia.SelectedIndex = 0;
            fechaRecepcion.Text = "";
            numeroFojas.Text = "";
            observacionesIncial.Text = "";

        }
        protected void MensajeExito(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastInfo('{Mensaje}');", true);

        }
        
    }
}