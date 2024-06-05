using SIPOH.Controllers.AC_RegistroInicialJuicioOral;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatAnexosController;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatTipoSolicitudController;
using static SIPOH.Controllers.AC_CatalogosCompartidos.P_CatSolicitanteController;
using static SIPOH.Controllers.AC_RegistroInicialJuicioOral.InicialJuicioOralController;
using static SIPOH.Views.CustomRegistroIniciales;

namespace SIPOH.Views

{
    public partial class CustomJuicio : System.Web.UI.UserControl
    {

        DataTable relacionesVI = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatAnexosEnDropDownList();
                CargarCatTipoAudienciaDropDownList();
                CargarCatSolicitante();
                lblIdSeleccionado.Visible = false;
                
                LimpiarRegistroDatosPrimeraParte();
                LimpiarRegistroAnexosItmes();
                LimpiarRegistroDatosSegundaParte();
                LimpiarRegistroDatosTerceraParte();
                LimpiarRegistroDatosSegundaParte();
                

            }
        }
        
        protected void RepeaterListaPersonas_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void btnGenerarRelacion_Click(object sender, EventArgs e)
        {
            
            DataTable relacionesVI;
            if (Session["RelacionesVI"] == null)
            {
                relacionesVI = new DataTable();
                relacionesVI.Columns.Add("Inculpado", typeof(string));
                relacionesVI.Columns.Add("Delitos", typeof(string));
                relacionesVI.Columns.Add("Victimas", typeof(string));
                relacionesVI.Columns.Add("Acciones", typeof(string));
                relacionesVI.Columns.Add("IdDelitosInculpado", typeof(int));
                relacionesVI.Columns.Add("IdDelitosVictima", typeof(int));
                Session["RelacionesVI"] = relacionesVI;
            }
            else
            {
                relacionesVI = (DataTable)Session["RelacionesVI"];
            }

            DataRelacionesVI infoRelacion = new DataRelacionesVI();
            List<DataRelacionesVI> listaRelacionesVI = new List<DataRelacionesVI>();
            foreach (RepeaterItem item in RepeaterTraerDelitosID.Items)
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

                        DataRelacionesVI infoRelacionTemporal = new DataRelacionesVI();
                        foreach (RepeaterItem itemV in RepeaterVictimasJO.Items)
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

                                    if (Session["Partes"] == null)
                                    {
                                        listaRelacionesVI = new List<DataRelacionesVI>();
                                    }
                                    else
                                    {
                                        // Si la lista ya existe en la sesión, recuperarla
                                        listaRelacionesVI = (List<DataRelacionesVI>)Session["Partes"];
                                    }

                                    // Verificar si el elemento ya existe en la lista antes de agregarlo
                                    bool existeRelacion = listaRelacionesVI.Any(relacion =>
                                        relacion.idInculpado == infoRelacionTemporal.idInculpado &&
                                        relacion.idVictima == infoRelacionTemporal.idVictima);

                                    if (!existeRelacion)
                                    {
                                        // Agregar el nuevo elemento a la lista solo si no existe
                                        DataRelacionesVI relacion = new DataRelacionesVI
                                        {
                                            idInculpado = infoRelacionTemporal.idInculpado,
                                            idVictima = infoRelacionTemporal.idVictima,
                                            // Añade otras propiedades que desees aquí
                                        };
                                        MensajeExito("Se añadieron las partes al Juicio Oral");
                                        relacionesVI.Rows.Add(newRow);
                                        listaRelacionesVI.Add(relacion);
                                        Session["Partes"] = listaRelacionesVI;
                                        //chkVictima.Enabled = false;
                                        
                                    }
                                    else
                                    {
                                        string mensajeError ="En tu actual petición solo se añadio los que no fueron repetidos";
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
            JuicioOralPanel.Update();
        
    }
        protected void GridRelaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;

            DataTable dtRelaciones = (DataTable)Session["RelacionesVI"];
            if (dtRelaciones != null && dtRelaciones.Rows.Count > rowIndex)
            {
                dtRelaciones.Rows.RemoveAt(rowIndex);

                GridRelaciones.DataSource = dtRelaciones;
                GridRelaciones.DataBind();
            }
            // Obtener el ID de la fila que se va a eliminar
            List<DataRelacionesVI> listaDatos = (List<DataRelacionesVI>)Session["Partes"];
            if (listaDatos != null && listaDatos.Count > rowIndex)
            {
                //string id = GridRelaciones.DataKeys[rowIndex].Value.ToString();
                // Eliminar la fila de tu origen de datos (por ejemplo, una lista, datatable, etc.)
                // Supongamos que tienes una lista llamada "listaDatos" que contiene tus datos

                listaDatos.RemoveAt(rowIndex);

                // Volver a enlazar el GridView
                
            }

            JuicioOralPanel.Update();
        }





        public static int IdAsunto;
        protected void btnGuardarJO_Click(object sender, EventArgs e)
        {
            try
            {
                // Recolección de datos del formulario
                string observacionesJO = observacionesIncial.Text;
                string quienIngresaJO = inputQuienIngresa.SelectedValue;
                string nombreParticular = inputNombreParticular.Text;
                string fojas = numeroFojas.Text;
                string tipoAudiencia = inputTipoAudiencia.SelectedValue;
                string idCausa = lblIdSeleccionado.Text;
                string numeroCausa = inputNumero.Text;

                // Validación de campos obligatorios

                if (string.IsNullOrEmpty(quienIngresaJO) ||
                    string.IsNullOrEmpty(nombreParticular) ||
                    string.IsNullOrEmpty(fojas) ||
                    string.IsNullOrEmpty(tipoAudiencia) ||
                    string.IsNullOrEmpty(idCausa) ||
                    string.IsNullOrEmpty(numeroCausa))
                {
                    

                    // Determinar cuál campo está vacío
                    if (string.IsNullOrEmpty(idCausa))                        
                        throw new Exception("Ingresa un número de causa y genera tu relación de partes");                    
                    else if (string.IsNullOrEmpty(nombreParticular))
                        throw new Exception("Nombre de persona que ingresa el documento");
                    else if (string.IsNullOrEmpty(fojas))
                        throw new Exception("Número de fojas");
                    else if (string.IsNullOrEmpty(tipoAudiencia))
                        throw new Exception("Tipo de audiencia");
                    else if (string.IsNullOrEmpty(quienIngresaJO))
                        throw new Exception("Campo quién ingresa, esta incompleto");
                                          
                }

                // Obtención de datos de sesión
                int idJuzgado = int.Parse(Session["IDJuzgado"] as string);
                int idCircuitos = int.Parse(Session["IdCircuito"] as string);
                int idUsuario = int.Parse(Session["IdUsuario"] as string);
                int idPerfil = int.Parse(Session["IdPerfil"] as string);

                // Obtención de la fecha y hora actual formateada
                string fechaActualFormateada = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Creación del objeto de inserción de juicio oral
                DataInsertJuicioOral infoJO = new DataInsertJuicioOral
                {
                    IdCircuitoFolio = idCircuitos,
                    dIdJuzgado = idJuzgado,
                    dFeIngreso = fechaActualFormateada,
                    dTipoAsunto = "JO",
                    dDigitalizado = "N",
                    dFeCaptura = fechaActualFormateada,
                    dIdUsuario = idUsuario,
                    dIdAudiencia = int.Parse(tipoAudiencia),
                    Observaciones = observacionesJO,
                    QuienIngresa = quienIngresaJO,
                    dMP = nombreParticular,
                    dPrioridad = "N",
                    dFojas = int.Parse(fojas),
                    dtipo = "I",
                    dEstado = "N",
                    dIdPerfil = idPerfil,
                    dIdActividad = 1,
                    dIdPosterior = 0,
                    IdAsuntoCausa = int.Parse(idCausa),
                    NumCausa = numeroCausa
                };

                // Preparación de datos para la inserción
                List<DataInsertJuicioOral> listaInfoJO = new List<DataInsertJuicioOral> { infoJO };
                List<DataRegistroAnexos> listaAnexos = Session["AnexosJO"] as List<DataRegistroAnexos> ?? new List<DataRegistroAnexos>();
                List<DataRelacionesVI> listaPartes = Session["Partes"] as List<DataRelacionesVI> ?? new List<DataRelacionesVI>();
                
                    DatosJO infoJOs = InsertJuicioOral(listaInfoJO, listaAnexos, listaPartes);
                // Inserción del juicio oral y sus datos relacionados
                IdAsunto = infoJOs.IdAsunto;
                    if (infoJOs.HayError)
                    {
                    
                        if (listaPartes.Count == 0)
                        {
                            MensajeError("Lo siento, Falta generar la relación de partes.");
                        }
                        else
                        {

                            MensajeExito(infoJOs.MensajeError);
                            string ticket = CrearTicketSELLO(infoJOs.NumeroJO);
                            TicketJO.Style["display"] = "block";
                            ocultarAGuardar.Style["display"] = "none !important";
                            TicketJO.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");
                            ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicketJOIniciales();", true);
                            tituloSelloJOIniciales.Style["display"] = "block";
                            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarTituloSello", "imprimirTicketJOIniciales();", true);
                            // Limpiar campos después de la inserción exitosa
                            LimpiarRegistroDatosTerceraParte();
                            LimpiarRegistroDatosSegundaParte();
                            LimpiarRegistroDatosPrimeraParte();

                            string mensajeRespuesta = "Envío exitoso. Tu registro se ha hecho correctamente.";
                            MensajeExito(mensajeRespuesta);
                        }



                    }
                    else if(!infoJOs.HayError)
                    {
                    // Mostrar el mensaje de error en una ventana emergente
                    // Hubo un error en el proceso de inserción
                    
                    MensajeError("Error en inserción de datos, no se pudo generar tu relación de partes, verifica tu consulta.");
                    
                    
                    //JuicioOralPanel.Update();
                    
                    }
                    
                

            }
            catch (Exception ex)
            {
                string mensajeError = $"¡{ex.Message}!";
                MensajeError(mensajeError);
            }
            JuicioOralPanel.Update();
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
        private void ImprimirCentrado(StringBuilder ticket, string texto)
        {
            int maxLength = 37;
            int totalPadding = maxLength - texto.Length;
            int padLeft = totalPadding / 2 + texto.Length;
            string centeredLine = texto.PadLeft(padLeft).PadRight(maxLength);
            ticket.AppendLine(centeredLine);
        }
       
        public string CrearTicketSELLO(string NumeroJO)
        {
            StringBuilder ticket = new StringBuilder();
            
            List<DataRegistroAnexos> anexosJO = Session["AnexosJO"] as List<DataRegistroAnexos>;
            string TipoAsunto = "JUICIO ORAL";
            string Causa = NumeroJO;
            string nombreJuzgado = Session["NombreJuzgado"] as string;
            
            

            List<string> lineasNombreJuzgado = DividirTextoEnLineas(nombreJuzgado, 32);

            int cantidadAnexos = CantidadAnexos(anexosJO);

           
            //string Causa = inputNumeroDocumento.Text;


            ImprimirCentrado(ticket, "TRIBUNAL SUPERIOR");
            ImprimirCentrado(ticket, "DE JUSTICIA");
            ImprimirCentrado(ticket, "DEL ESTADO DE HIDALGO");
            ImprimirCentrado(ticket, "ATENCION CIUDADANA");
            ImprimirCentrado(ticket, ".........");
            foreach (string linea in lineasNombreJuzgado)
            {
                ImprimirCentrado(ticket, linea);
            }
            ImprimirCentrado(ticket, "INICIAL");
            ImprimirCentrado(ticket, ".........");
            
            
                ticket.AppendLine($"{TipoAsunto}:{Causa}");
            ticket.AppendLine($"FOLIO: {IdAsunto}");
            
            ticket.AppendLine($"FECHA RECEPCIÒN:{GetFechaYHora()}");
            //ticket.AppendLine($"NUC:{NUC.ToUpper()}");

            int maxLength = 36;
            int maxLengthT = 30;

            foreach (var anexo in anexosJO)
            {
                int espacioEntreColumnas = 3; // Puedes ajustar este valor según tus necesidades
                int longitudTotal = maxLength - espacioEntreColumnas;

                string linea = $"{anexo.Descripcion.ToUpper()}".PadRight(longitudTotal, '.') + $"{anexo.cantidad}";
                ticket.AppendLine(linea);
            }


            int espacioEntreColumnasT = 3; // Puedes ajustar este valor según tus necesidades
            int longitudTotalT = maxLengthT - espacioEntreColumnasT;

            string separador = new string('.', longitudTotalT);
            if (cantidadAnexos != 0)
            {
                ticket.AppendLine($"TOTAL:{separador}{cantidadAnexos}");
            }else
            {
                Debug.WriteLine("No hay anexos");
            }
            





            return ticket.ToString();
        }
        protected void btnAñadirAnexo(object sender, EventArgs e)
        {
            string tipoAnexo = txtAnexosTipoJuicio.SelectedValue;
            string cantidadAnexos = txtCantidadAnexos.Text;
            if (txtCantidadAnexos.Text == "0" || txtCantidadAnexos.Text =="")
            {

                string MensajeCantidad = "Por favor, selecciona una cantidad válida.";
                MensajeError(MensajeCantidad);
                return;
            }


            // Crear un nuevo objeto DataRegistroAnexos con la información del anexo
            DataRegistroAnexos infoAnexos = new DataRegistroAnexos
            {
                IDPosterior = 0,
                Digitalizado = "N",
                Descripcion = tipoAnexo == "Otro" ? txtDescripcionAnexos.Text : tipoAnexo,
                cantidad = int.Parse(cantidadAnexos)
            };
            List<DataRegistroAnexos> listaAnexos = Session["AnexosJO"] as List<DataRegistroAnexos> ?? new List<DataRegistroAnexos>();
            if (listaAnexos.Any(v => v.Descripcion == tipoAnexo))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                string mensaje = "Este anexo ya existe en la lista.";
                MensajeError(mensaje);
                return;
            }
            if (listaAnexos.Any(v => v.Descripcion == txtDescripcionAnexos.Text))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                string mensaje = "Este anexo ya existe en la lista.";
                MensajeError(mensaje);
                return;
                
            }
            if(tipoAnexo == "Otro" && txtDescripcionAnexos.Text == "")
            {
                string mensaje = "Este anexo no es posible añadirlo.";
                MensajeError(mensaje);
                return;
            }
            listaAnexos.Add(infoAnexos);
            LimpiarRegistroAnexosItmes();
            Session["AnexosJO"] = listaAnexos;
            CopyAnexos.DataSource = listaAnexos;
            CopyAnexos.DataBind();

            RepeaterAnexos.DataSource = listaAnexos;
            RepeaterAnexos.DataBind();
            JuicioOralPanel.Update();
        }

        private int CantidadAnexos(List<DataRegistroAnexos> anexos)
        {
            return anexos?.Sum(a => a.cantidad) ?? 0;
        }
        protected string GetFechaYHora()
        {
            string formatoPersonalizado = "yyyy-MM-dd HH:mm:ss";
            string fechaYHoraFormateada = DateTime.Now.ToString(formatoPersonalizado);
            return fechaYHoraFormateada;
        }
        
        protected void btnConsultaCausa(object sender, EventArgs e)
        {
            
            //el boton de mostrar tiket no desaparece
            Session["Partes"] = new List<DataRelacionesVI>();
            LimpiarTablaRelaciones();
            LimpiarDatosEspecifico();
            try
            {

                var NumeroCausa = inputNumero.Text;
                int IdJuzgado = int.Parse(Session["IDJuzgado"] as string);
                List<DataGetCausaImputadoJuicioOral> infoCausa = GetCausa(NumeroCausa, IdJuzgado);

                if (infoCausa != null && infoCausa.Any())
                {
                    // Si hay resultados, los mostramos
                    ConsultaCausa.Visible = true;
                    TicketJO.Style["display"] = "none";
                    tituloSelloJOIniciales.Style["display"] = "none !important";
                    ocultarAGuardar.Style["display"] = "block";
                    string mensajeExito = "¡Búsqueda exitosa!, Este es tu resultado.";
                    MensajeExito(mensajeExito);
                    RepeaterListaPersonas.DataSource = infoCausa;
                    RepeaterListaPersonas.DataBind();
                }
                else
                {
                    ConsultaCausa.Visible = false; 
                    throw new Exception("No se encontró ningún resultado, busque un número de causa válido.");
                }

            }
            catch (Exception ex)
            {
                string mensajeError = $"¡{ex.Message}!";
                MensajeError(mensajeError);

            }


            JuicioOralPanel.Update();

        }
        protected void ObtenerDelitos_Click(object sender, EventArgs e)
        {
            Button btnw = (Button)sender;
            foreach (RepeaterItem item in RepeaterListaPersonas.Items)
            {
                Button btnItem = (Button)item.FindControl("controlSelected"); // Reemplaza 'tuBotonID' con el ID de tu botón
                btnItem.CssClass = btnItem.CssClass.Replace("bg-success", "");
            }

            // Agrega la clase 'bg-succes' al botón que recibió el clic
            btnw.CssClass += " bg-success";

            try
            {
                string idAsuntoCausa = HiddenIdAsuntoCausa.Value;
                lblIdSeleccionado.Text = idAsuntoCausa;

                Button btn = (Button)sender;
                
                string idPartes = btn.CommandArgument;
                int idPartesInt = int.Parse(idPartes);
                
                List<DataGetCausaDelitosJuicioOral> infoDelito = GetDelitos(idPartesInt);
                if (infoDelito != null && infoDelito.Any())
                {
                    DelitosInculpado.Visible = true;
                    string Mensaje = "Se encontró delitos de tu inculpado.";
                    MensajeExito(Mensaje);
                    RepeaterTraerDelitosID.DataSource = infoDelito;
                    RepeaterTraerDelitosID.DataBind();

                }
                else
                {
                    LimpiarDatosEspecifico();
                    VictimasDelDelito.Visible = false;
                    throw new Exception("No se encontró ningún delito asociado a tus inculpados, busca un número de causa válido o elige otro inculpado.");
                }
            }catch(Exception ex)
            {
                string mensajeError = $"¡{ex.Message}!";
                MensajeError(mensajeError);
            }
            JuicioOralPanel.Update();
        }
        protected void ObtenerVictimas_Click(object sender, EventArgs e)
        {
        }

       

        protected void chkDelito_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chkDelito = (CheckBox)sender;

            if (chkDelito.Checked)
            {
                // Desmarcar todos los demás CheckBox dentro del Repeater
                foreach (RepeaterItem item in RepeaterTraerDelitosID.Items)
                {
                    if (item.FindControl("chkDelito1") is CheckBox otherCheckBox && otherCheckBox != chkDelito)
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
               
                List<DataGetCausaVictimaJuicioOral> infoVictima = GetVictima(idDeliAsuntoInt);
                if (infoVictima != null && infoVictima.Any())
                {
                    VictimasDelDelito.Visible = true;
                    string Mensaje = "¡Los inculpados son los siguientes!";
                    MensajeExito(Mensaje);
                    RepeaterVictimasJO.DataSource = infoVictima;
                    RepeaterVictimasJO.DataBind();
                }
                else
                {
                    VictimasDelDelito.Visible = false;
                    throw new Exception("No se encontró victimas asociadas al delito");
                }
                
            }
            catch (Exception ex)
            {
                string mensajeError = $"¡{ex.Message}!";
                MensajeError(mensajeError);
            }



            //lblIdDeliAsunto.Text = "IdDeliAsunto Seleccionado: " + idDeliAsunto;
            JuicioOralPanel.Update();
        }
        protected void chkVictimaDel_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chkVictima = (CheckBox)sender;

            if (chkVictima.Checked)
            {
                // Desmarcar todos los demás CheckBox dentro del Repeater
                foreach (RepeaterItem item in RepeaterTraerDelitosID.Items)
                {
                    if (item.FindControl("chkVictima1") is CheckBox otherCheckBox && otherCheckBox != chkVictima)
                    {
                        otherCheckBox.Checked = false;
                    }
                }
            }
           
            JuicioOralPanel.Update();
        }






        protected void GetLabelPrioridad(object sender, EventArgs e)
        {


        }


        protected void btnEliminarAnexo(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;

            // Obtener la fila del Repeater que contiene el botón
            RepeaterItem item = (RepeaterItem)btnEliminar.NamingContainer;

            // Obtener el índice de la fila en el Repeater
            int indice = item.ItemIndex;

            // Obtener la lista de la sesión
            List<DataRegistroAnexos> listaAnexos = (List<DataRegistroAnexos>)Session["AnexosJO"];

            // Verificar si la lista no es nula y tiene elementos
            if (listaAnexos != null && listaAnexos.Count > indice)
            {
                // Eliminar el elemento correspondiente del origen de datos
                listaAnexos.RemoveAt(indice);

                // Actualizar el Repeater con la lista actualizada
                RepeaterAnexos.DataSource = listaAnexos;
                RepeaterAnexos.DataBind();
            }

            // Actualizar el UpdatePanel si es necesario
            JuicioOralPanel.Update();
        }

        protected void inputQuienIngresa_SelectedIndexChanged(object sender, EventArgs e)
        {

            string quienIngresa = inputQuienIngresa.SelectedItem.Text;
            string TextquienIngresa = inputQuienIngresa.SelectedItem.Text;
            //if (quienIngresa == "M")
            //{
            //    quienIngresa = "MP";
            //}
            //else if (quienIngresa == "P")
            //{
            //    quienIngresa = "particular";
            //}
            //else if (quienIngresa == "O")
            //{
            //    quienIngresa = "otra persona";
            //}
            //else
            //{
            //    quienIngresa = "";
            //}
            copyQuienIngresa.Text = TextquienIngresa.ToUpper();
            lblTipoPersona.Text = quienIngresa;

            //ScriptManager.RegisterStartupScript(this, GetType(), "mostrarValor", "mostrarValorSeleccionado();", true);
            JuicioOralPanel.Update();

        }
        protected void inputRadicacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            string valorSeleccionado = inputTipoAudiencia.SelectedItem.Text;
            copyTipoSolicitud.Text = valorSeleccionado.ToUpper();
            JuicioOralPanel.Update();
        }
        
        private void CargarCatAnexosEnDropDownList()
        {
            //Mostrar catalogo de anexos en value como descripcion 
            List<DataCatAnexos> listaAnexos = GetCatAnexos();
            txtAnexosTipoJuicio.DataSource = listaAnexos;
            txtAnexosTipoJuicio.DataTextField = "descripcionAnexo";
            txtAnexosTipoJuicio.DataValueField = "descripcionAnexo";
            txtAnexosTipoJuicio.DataBind();

        }
        private void CargarCatTipoAudienciaDropDownList()
        {
            List<DataTipoAudiencia> listaTipoAudiencias = GetCatTipoSolicitud();
            inputTipoAudiencia.DataSource = listaTipoAudiencias;
            inputTipoAudiencia.DataTextField = "CAA_Descripcion";
            inputTipoAudiencia.DataValueField = "CAA_IdAudi";
            inputTipoAudiencia.DataBind();
        }
        private void CargarCatSolicitante()
        {
            List<DataTipoSolicitante> listaTipoSolicitante = GetCatSolicitante();
            inputQuienIngresa.DataSource = listaTipoSolicitante;
            inputQuienIngresa.DataTextField = "CS_Solicitante";
            //inputQuienIngresa.DataValueField = "CS_Tipo";
            inputQuienIngresa.DataBind();

        }
        private void LimpiarRegistroDatosPrimeraParte()
        {
            inputNumero.Text = "";
            inputTipoAudiencia.SelectedIndex = 0;
            numeroFojas.Text = "";
            inputQuienIngresa.SelectedIndex = 0;
            inputNombreParticular.Text = "";
            observacionesIncial.Text = "";
            lblTipoPersona.Text = "";
            lblIdSeleccionado.Text = "";
            //JuicioOralPanel.Update();
        }
        private void LimpiarRegistroDatosSegundaParte()
        {
            Session["AnexosJO"] = new List<DataRegistroAnexos>();
            Session["Partes"] = new List<DataRelacionesVI>();

        }
        private void LimpiarRegistroAnexosItmes()
        {
            txtAnexosTipoJuicio.SelectedIndex= 0;
            txtDescripcionAnexos.Text = "";
            txtCantidadAnexos.Text = "";
        }
        private void LimpiarRegistroDatosTerceraParte()
        {
            RepeaterAnexos.DataSource = null;
            RepeaterAnexos.DataBind();
            RepeaterListaPersonas.DataSource = null;
            RepeaterListaPersonas.DataBind();
            RepeaterTraerDelitosID.DataSource = null;
            RepeaterTraerDelitosID.DataBind();
            RepeaterVictimasJO.DataSource = null;
            RepeaterVictimasJO.DataBind();
            LimpiarTablaRelaciones();
        }
        protected void LimpiarTablaRelaciones()
        {
            // Crear una nueva DataTable vacía
            DataTable relacionesVI = new DataTable();
            relacionesVI.Columns.Add("Inculpado", typeof(string));
            relacionesVI.Columns.Add("Delitos", typeof(string));
            relacionesVI.Columns.Add("Victimas", typeof(string));
            relacionesVI.Columns.Add("Acciones", typeof(string));
            relacionesVI.Columns.Add("IdDelitosInculpado", typeof(int));
            relacionesVI.Columns.Add("IdDelitosVictima", typeof(int));

            // Asignar la nueva DataTable como valor para la sesión
            Session["RelacionesVI"] = relacionesVI;

            // Asignar la DataTable actualizada como origen de datos para el Grid
            GridRelaciones.DataSource = relacionesVI;
            GridRelaciones.DataBind();
            JuicioOralPanel.Update();
        }
        protected void LimpiarDatosEspecifico()
        {
            RepeaterTraerDelitosID.DataSource = null;
            RepeaterTraerDelitosID.DataBind();
            RepeaterVictimasJO.DataSource = null;
            RepeaterVictimasJO.DataBind();
        }
        protected void MensajeExito(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastInfo('{Mensaje}');", true);
        }
        protected void MensajeError(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastError('{Mensaje}');", true);
        }
    }
}