using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIPOH.Controllers.AC_CatalogosCompartidos;
using SIPOH.Controllers.EJ_Storages;

namespace SIPOH.Views
{
    public partial class InicialConsignacionesHistoricas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarJuzgados();
                LoadDistritosDDL();
                visibleAcusatorioTradicional();
                CargarSalas();
                CargarJuzgadosEjecucion();
                CargarSolicitantes();
                CargarSolicitudesCon();
                InputOtraSolicitud.Disabled = true;
                OtroAnexo.Disabled = true;
                CargarAnexosCon();
            }
        }
        //FUNCION PARA CAMBIAR DE ACUSATORIO A TRADICIONAL
        protected void ddlSistemasAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Actualizar la visibilidad cuando se cambia la selección
            visibleAcusatorioTradicional();
        }
        private void visibleAcusatorioTradicional()
        {
            // Ocultar ambos divs al inicio
            divAcusatorio.Visible = false;
            divTradicional.Visible = false;
            string sistemaSeleccionado = ddlSistemasAT.SelectedValue;
            if (sistemaSeleccionado == "acusatorio")
            {
                divAcusatorio.Visible = true;
                divTradicional.Visible = false;
                LimpiarConsignaciones();
            }
            else if (sistemaSeleccionado == "tradicional")
            {
                divAcusatorio.Visible = false;
                divTradicional.Visible = true;
                LimpiarConsignaciones();
            }
        }
        //FUNCIONES DE ERROR Y EXITO
        protected void MensajeExito(string mensaje, bool esExito)
        {
            string AlertaExito = esExito ? "mostrarAlerta" : "mostrarError";
            string script = $"toastExito('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
        }
        protected void MensajeError(string mensaje, bool esError)
        {
            string AlertaError = esError ? "mostrarAlerta" : "mostrarError";
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastErrorScript", script, true);
        }
        protected void MensajeAdvertencia(string mensaje, bool esAdvertencia)
        {
            string AlertaAdvertencia = esAdvertencia ? "mostrarAlerta" : "mostrarError";
            string script = $"toastWarning('{HttpUtility.JavaScriptStringEncode(mensaje)}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastWarningScript", script, true);
        }
        private void LimpiarConsignaciones()
        {
            GridViewCausas.DataSource = null;
            GridViewCausas.DataBind();
            selectSalas.ClearSelection();
            inputNumeroToca.Text = "";
            inputSentencia.Text = "";
            tablaSalasCon.DataSource = null;
            tablaSalasCon.DataBind();
            tablaSentenciasCon.DataSource = null;
            tablaSentenciasCon.DataBind();
            busNombreJuzEjec.ClearSelection();
            inputBusNumeroEjecucion.Text = "";
            InputNombreBusqueda.Text = "";
            InputApPaternoBusqueda.Text = "";
            inputApMaterno.Text = "";
            siInterno.Checked = false;
            noInterno.Checked = false;
            CatSolicitantesDDCon.ClearSelection();
            detalleSolicitantes.Text = "";
            CatSolicitudDDCon.ClearSelection();
            InputOtraSolicitud.Value = "";
            CatAnexosDDCon.ClearSelection();
            OtroAnexo.Value = "";
            CantidadInput.Value = "";
            tablaAnexosCon.DataSource = null;
            tablaAnexosCon.DataBind();
            divOcultarSinCausa.Style["display"] = "none";
            divResultado.Style["display"] = "none";

        }
        //FUNCION MOSTRAR MENSAJE MODAL
        protected void MostrarMensaje(string mensaje, bool esExito)
        {
            string tipoAlerta = esExito ? "mostrarAlerta" : "mostrarError";
            string script = $"<script type='text/javascript'>{tipoAlerta}('{mensaje}');</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), tipoAlerta + "Script", script, false);
        }
        //FUNCION CARGAR JUZGADOS EN EL DDL JUZGADO DE ACUSATORIO
        private void CargarJuzgados()
        {
            try
            {
                int idCircuito = Convert.ToInt32(Session["IdCircuito"]);
                var juzgados = CatJuzgadosConTipoYSubtipoController.GetJuzgadosConTipoYSubtipo(idCircuito, 'A');
                JuzgadoProcedenciaCHA.Items.Clear();
                JuzgadoProcedenciaCHA.Items.Add(new ListItem("Seleccionar", ""));

                foreach (var juzgado in juzgados)
                {
                    JuzgadoProcedenciaCHA.Items.Add(new ListItem(juzgado.NombreJuzgado, juzgado.IdJuzgado));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //debug por si no mostro los juzgados
            }

        }
        //FUNCIONES CARGAR JUZGADOS EN EL DDL JUZGADO DE TRADICIONAL 
        private void LoadDistritosDDL()
        {
            int idCircuito = Convert.ToInt32(Session["IdCircuito"]);
            var distritos = SIPOH.Controllers.AC_CatalogosCompartidos.CatEjecucion_Cat_DistritosController.GetDistritos(idCircuito);
            ddlDistritoProcedencia.DataSource = distritos;
            ddlDistritoProcedencia.DataTextField = "Nombre";
            ddlDistritoProcedencia.DataValueField = "IdDistrito";
            ddlDistritoProcedencia.DataBind();
            ddlDistritoProcedencia.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        protected void ddlDistritoProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistritoProcedencia.SelectedValue != "0")
            {
                LoadJuzgadosDDL(Convert.ToInt32(ddlDistritoProcedencia.SelectedValue));
            }
        }
        private void LoadJuzgadosDDL(int distritoId)
        {
            var juzgados = SIPOH.Controllers.AC_CatalogosCompartidos.CatEjecucion_Cat_JuzgadosController.GetJuzgados(distritoId);
            ddlJuzgadoProcedencia.DataSource = juzgados;
            ddlJuzgadoProcedencia.DataTextField = "Nombre";
            ddlJuzgadoProcedencia.DataValueField = "IdJuzgado";
            ddlJuzgadoProcedencia.DataBind();
            ddlJuzgadoProcedencia.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        //FUNCION BUSCAR CAUSA DESDE ACUSATORIO
        protected void btnBuscarAcusatorio_Click(object sender, EventArgs e)
        {
            string idjuzgadoSeleccionado = JuzgadoProcedenciaCHA.SelectedValue;
            ObtenerNombreJuzgadoPorIDController obtenerNombreJuzgado = new ObtenerNombreJuzgadoPorIDController();
            var juzgado = obtenerNombreJuzgado.ObtenerJuzgadoPorID(idjuzgadoSeleccionado);
            string numeroCausaNuc = causaNucAcusatorio.Text;
            string tipoCausa = "";
            string seleccionCausaNucCHA = CausaNucCHA.SelectedValue;
            switch (seleccionCausaNucCHA)
            {
                case "1": // Causa
                    tipoCausa = "C";
                    break;
                case "2": // NUC
                    tipoCausa = "";
                    break;
                case "3": // Juicio Oral
                    tipoCausa = "JO";
                    break;
                default:
                    break;
            }
            Ejecucion_ConsultarCausaController controller = new Ejecucion_ConsultarCausaController();
            var causas = controller.ConsultarCausa(idjuzgadoSeleccionado, numeroCausaNuc, tipoCausa);
            if (causas.Any())
            {
                GridViewCausas.DataSource = causas;
                GridViewCausas.DataBind();
                string mensajeExito = $"Se encontro la CAUSA|NUC {numeroCausaNuc} en el juzgado {juzgado.Nombre}.";
                divOcultarSinCausa.Style["display"] = "block";
                MostrarMensaje(mensajeExito, true);
            }
            else
            {
                string mensajeErrorModal = "No se encontró la CAUSA | NUC en el JUZGADO elegido, ¿Deseas registrar una nueva causa histórica? .";
                divOcultarSinCausa.Style["display"] = "none";
                MostrarMensaje(mensajeErrorModal, false);
                LimpiarConsignaciones();
              
            }
        }
        //FUNCION BUSCAR CAUSA DESDE TRADICIONAL
        protected void btnBuscarTradicional_Click(object sender, EventArgs e)
        {
            string idjuzgadoT = ddlJuzgadoProcedencia.SelectedValue;
            ObtenerNombreJuzgadoPorIDController obtenerNombreJuzgado = new ObtenerNombreJuzgadoPorIDController();
            var juzgadoT = obtenerNombreJuzgado.ObtenerJuzgadoPorID(idjuzgadoT);
            string numeroCausaT = InputCausaTradicional.Text;
            string tipoCausa = "T";
            Ejecucion_ConsultarCausaController controller = new Ejecucion_ConsultarCausaController();
            var causas = controller.ConsultarCausa(idjuzgadoT, numeroCausaT, tipoCausa);
            if (causas.Any())
            {
                GridViewCausas.DataSource = causas;
                GridViewCausas.DataBind();
                string mensajeExito = $"Se encontro la CAUSA {numeroCausaT} en el juzgado {juzgadoT.Nombre}.";
                divOcultarSinCausa.Style["display"] = "block";
                MostrarMensaje(mensajeExito, true);
            }
            else
            {
                string mensajeErrorModal = "No se encontró la CAUSA | NUC en el JUZGADO elegido, ¿Deseas registrar una nueva causa histórica?.";
                divOcultarSinCausa.Style["display"] = "none";
                MostrarMensaje(mensajeErrorModal, false);
                LimpiarConsignaciones();
            }
        }
        //------- SALAS Y TOCAS ------
        //FUNCION CARGAR SALAS EN EL DROPDOWN DE SALAS Y TOCAS
        private void CargarSalas()
        {
            List<CatEjecucion_Cat_JuzgadosEjecucionController.DataSalaEjecucion> salas = CatEjecucion_Cat_JuzgadosEjecucionController.GetSalas();
            selectSalas.Items.Clear();
            selectSalas.Items.Add(new ListItem("Seleccionar", ""));
            foreach (var sala in salas)
            {
                ListItem item = new ListItem(sala.Nombre, sala.IdJuzgado);
                selectSalas.Items.Add(item);
            }
        }
        //CLASE DONDE SE GUARDARAN LAS SALAS TEMPORAL
        [Serializable]
        public class Sala
        {
            public string NombreSala { get; set; }
            public string NumeroToca { get; set; }
        }
        //FUNCION AGREGAR SALAS Y TOCAS A LA TABLA
        protected void AgregarSalayTocaATabla(object sender, EventArgs e)
        {
            string sala = selectSalas.SelectedItem.Text;
            string valorSala = selectSalas.SelectedItem.Value; // Obtiene el valor del item seleccionado
            string numeroToca = inputNumeroToca.Text; // Asegúrate de que este ID corresponde a tu input de número de toca
            List<Sala> salas = ViewState["salas"] as List<Sala> ?? new List<Sala>();
            selectSalas.ClearSelection();
            inputNumeroToca.Text = "";
            if (valorSala.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(sala) || string.IsNullOrWhiteSpace(numeroToca))
            {
                MensajeError("Debes seleccionar una sala válida y proporcionar un número de toca.", false);
                return;
            }
            if (!salas.Any(s => s.NombreSala == sala && s.NumeroToca == numeroToca))
            {
                salas.Add(new Sala { NombreSala = sala, NumeroToca = numeroToca });
                ViewState["salas"] = salas;
                tablaSalasCon.DataSource = salas;
                tablaSalasCon.DataBind();
                //ActualizarVisibilidadTitulo();
            }
            else
            {
                MensajeError("No puedes guardar salas y tocas repetidas.", false);
            }
        }
        //FUNCION AGREGAR SENTENCIAS A LA TABLA
        protected void AgregarSentenciasATabla(object sender, EventArgs e)
        {
            string sentencia = inputSentencia.Text; // Asume un control de entrada para la sentencia
            List<string> sentencias = ViewState["sentencias"] as List<string> ?? new List<string>();
            inputSentencia.Text = "";
            if (!string.IsNullOrWhiteSpace(sentencia))
            {
                if (!sentencias.Contains(sentencia))
                {
                    sentencias.Add(sentencia);
                    ViewState["sentencias"] = sentencias;
                    tablaSentenciasCon.DataSource = sentencias.Select(x => new { Sentencia = x }).ToList();
                    tablaSentenciasCon.DataBind();
                    //ActualizarVisibilidadTitulo();
                }
                else
                {
                    MensajeError("No puedes guardar sentencias iguales.", false);
                }
            }
            else
            {
                MensajeError("No puedes dejar el campo de sentencia vacío.", false);
            }
        }
        //borrar Sala
        protected void BorrarSala(object sender, GridViewDeleteEventArgs e)
        {
            List<Sala> salas = (List<Sala>)ViewState["salas"];
            salas.RemoveAt(e.RowIndex);
            ViewState["salas"] = salas;
            tablaSalasCon.DataSource = salas;
            tablaSalasCon.DataBind();
            //ActualizarVisibilidadTitulo();
        }
        protected void BorrarSentencia(object sender, GridViewDeleteEventArgs e)
        {
            List<string> sentencias = (List<string>)ViewState["sentencias"];
            sentencias.RemoveAt(e.RowIndex);
            ViewState["sentencias"] = sentencias;
            tablaSentenciasCon.DataSource = sentencias.Select(x => new { Sentencia = x }).ToList();
            tablaSentenciasCon.DataBind();
            //ActualizarVisibilidadTitulo();
        }
        //-------------------- Numero de Ejecucion ------------------------
        //CARGAR JUZGADO DE NUMEROS DE EJECUCION
        private void CargarJuzgadosEjecucion()
        {
            int circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            List<SIPOH.Controllers.AC_CatalogosCompartidos.Cat_Ejecucion_Cat_JuzgadosPorCircuitoEController.DataJuzgadoEjecucion> juzgados =
              SIPOH.Controllers.AC_CatalogosCompartidos.Cat_Ejecucion_Cat_JuzgadosPorCircuitoEController.GetJuzgadosPorCircuito(circuito);
            foreach (var juzgado in juzgados)
            {
                ListItem listItem = new ListItem(juzgado.Nombre, juzgado.IdJuzgado);
                busNombreJuzEjec.Items.Add(listItem);
            }
        }
        //FUNCION VALIDAR EXISTE NUMERO DE EJECUCION:
        protected void btnBuscarNoEjecucion_Click(object sender, EventArgs e)
        {
            string noEjecucion = inputBusNumeroEjecucion.Text;
            int idJuzgado = Convert.ToInt32(busNombreJuzEjec.SelectedValue);
            var resultadoValidacion = EJ_ValidarNumeroEjecucionController.ValidarNumeroEjecucion(noEjecucion, idJuzgado);
            if (resultadoValidacion.ExisteNumeroEjecucion)
            {
                divResultado.Style["display"] = "none";
                MensajeAdvertencia("Se encontraron resultados de la busqueda, ya no puedes guardar mas datos.", true);
            }
            else
            {
                divResultado.Style["display"] = "block";
                MensajeExito("No se encontraron Datos relacionados, puedes continuar.", true);
            }
        }
        //-------------------- FORMULARIO DE EJECUCION ---------------------
        //FUNCION CARGAR SOLCIITANTES DROPDOWN
        private void CargarSolicitantes()
        {
            var solicitantes = SIPOH.Controllers.AC_CatalogosCompartidos.CatEjecucion_CatSolicitanteController.GetSolicitantes();
            foreach (var solicitante in solicitantes)
            {
                ListItem listItem = new ListItem(solicitante.Nombre, solicitante.Clave);
                CatSolicitantesDDCon.Items.Add(listItem);
            }
        }
        //FUNCION CARGAR SOLICITUDES DROPDOWN
        private void CargarSolicitudesCon()
        {
            var solicitudes = SIPOH.Controllers.AC_CatalogosCompartidos.CatEjecucion_CatSolicitudController.GetSolicitudes();
            foreach (var solicitud in solicitudes)
            {
                ListItem listItem = new ListItem(solicitud.Nombre, solicitud.Clave);
                CatSolicitudDDCon.Items.Add(listItem);
            }
        }
        protected void CatSolicitudDDCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esOtroSeleccionado = CatSolicitudDDCon.SelectedItem.Text == "OTRO";
            InputOtraSolicitud.Disabled = !esOtroSeleccionado;
            if (esOtroSeleccionado)
            {
                InputOtraSolicitud.Attributes["required"] = "required";
            }
            else
            {
                InputOtraSolicitud.Attributes.Remove("required");
            }
        }
        //----------- ANEXOS ------------------
        //FUNCION CARGAR ANEXOS EN EL DROPDOWN
        private void CargarAnexosCon()
        {
            // Instanciación del controlador para acceder al método
            var controller = new SIPOH.Controllers.AC_CatalogosCompartidos.CatEjecucion_CatAnexosController();
            var anexos = controller.ObtenerAnexos();
            foreach (var anexo in anexos)
            {
                ListItem listItem = new ListItem(anexo.Descripcion, anexo.Valor);
                CatAnexosDDCon.Items.Add(listItem);
            }
        }
        //FUNCION HABILITAR DESHABILITAR ANEXO OTRO:
        protected void CatAnexosDDCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            OtroAnexo.Disabled = CatAnexosDDCon.SelectedValue != "OTRO";
        }
        //FUNCION CARGAR ANEXOS A LA TABLA
        protected void AgregarATablaCon(object sender, EventArgs e)
        {
            string anexo = CatAnexosDDCon.SelectedItem.Text;
            string valorAnexo = CatAnexosDDCon.SelectedItem.Value;
            if (anexo == "OTRO")
            {
                anexo = OtroAnexo.Value;
            }
            string cantidad = CantidadInput.Value;
            List<Sala> salasCon = ViewState["anexosCon"] as List<Sala> ?? new List<Sala>();

            if (valorAnexo.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase))
            {
                MensajeError("Debes seleccionar una opción valida.", true);
                return;
            }

            if (!string.IsNullOrWhiteSpace(anexo) && int.TryParse(cantidad, out int cantidadNumerica) && cantidadNumerica > 0)
            {
                var salaExistente = salasCon.FirstOrDefault(s => s.NombreSala.Equals(anexo, StringComparison.OrdinalIgnoreCase));
                if (salaExistente != null)
                {
                    salaExistente.NumeroToca = cantidadNumerica.ToString();
                }
                else
                {
                    salasCon.Add(new Sala { NombreSala = anexo, NumeroToca = cantidad });
                }

                ViewState["anexosCon"] = salasCon;
                tablaAnexosCon.DataSource = salasCon;
                tablaAnexosCon.DataBind();
                CatAnexosDDCon.ClearSelection();
                OtroAnexo.Value = string.Empty;
                CantidadInput.Value = string.Empty;
                OtroAnexo.Disabled = true;
            }
            else
            {
                MensajeError("No puedes dejar campos vacíos y la cantidad debe ser mayor que cero.", true);
            }
        }
        //FUNCION BORRAR DATOS DE LA GRIDVIEW:
        protected void BorrarFilaCon(object sender, GridViewDeleteEventArgs e)
        {
            List<Sala> salasCon = ViewState["anexosCon"] as List<Sala>;
            if (salasCon != null && salasCon.Count > e.RowIndex)
            {
                salasCon.RemoveAt(e.RowIndex);
                ViewState["anexosCon"] = salasCon;
                tablaAnexosCon.DataSource = salasCon;
                tablaAnexosCon.DataBind();
            }
            else
            {
                MensajeError("No se puede eliminar la fila seleccionada.", true);
            }
        }
        //---------------- INICIO INSERCION -----------
        protected void btnGuardarDatosCon_Click(object sender, EventArgs e)
        {
            string noEjecucion = inputBusNumeroEjecucion.Text;
            string cveSolicitante = CatSolicitantesDDCon.SelectedValue;
            string detalleSolicitante = detalleSolicitantes.Text;
            string cveSolicitud = CatSolicitudDDCon.SelectedValue;
            string otroSolicita = InputOtraSolicitud.Value;
            string beneficiarioNombre = InputNombreBusqueda.Text;
            string beneficiarioApellidoPaterno = InputApPaternoBusqueda.Text;
            string beneficiarioApellidoMaterno = inputApMaterno.Text;
            string idJuzgadoSeleccionado = busNombreJuzEjec.SelectedValue;
            if (idJuzgadoSeleccionado == "Seleccionar" || string.IsNullOrEmpty(idJuzgadoSeleccionado))
            {
                MensajeError("Por favor, seleccione un juzgado.", false);
                return;
            }
            int idJuzgado = int.Parse(idJuzgadoSeleccionado);
            string interno = siInterno.Checked ? "S" : "N";
            string idUser = HttpContext.Current.Session["IdUsuario"]?.ToString();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int idEjecucion = InsertarEjecucion(conn, transaction, idJuzgado, noEjecucion, cveSolicitante, detalleSolicitante, cveSolicitud, otroSolicita, beneficiarioNombre, beneficiarioApellidoPaterno, beneficiarioApellidoMaterno, interno, idUser);
                        foreach (GridViewRow row in GridViewCausas.Rows)
                        {
                            HiddenField hiddenIdAsunto = (HiddenField)row.FindControl("HiddenIdAsunto");
                            if (hiddenIdAsunto != null)
                            {
                                int idAsunto = Convert.ToInt32(hiddenIdAsunto.Value);
                                bool resultado = InsertarEnEjecucionAsunto(conn, transaction, idEjecucion, idAsunto);
                                if (!resultado)
                                {
                                    transaction.Rollback();
                                    MensajeError("Error durante la inserción en P_EjecucionAsunto.", false);
                                    return;
                                }
                            }
                        }
                        transaction.Commit();
                        MensajeExito("Se ha guardado la información correctamente", true);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MensajeError("Error durante la inserción: " + ex.Message, false);
                    }
                }
            }
        }
        //INSERTAR EN P_EJECUCION
        public int InsertarEjecucion(SqlConnection conn, SqlTransaction transaction, int idJuzgado, string noEjecucion, string cveSolicitante, string detalleSolicitante, string cveSolicitud, string otroSolicita, string beneficiarioNombre, string beneficiarioApellidoPaterno, string beneficiarioApellidoMaterno, string interno, string idUser)
        {
            try
            {
                string query = @"
                INSERT INTO [SIPOH].[dbo].[P_Ejecucion]
                ([NoEjecucion], [FechaEjecucion], [CveSolicitante], [DetalleSolicitante], [CveSolicitud], [OtroSolicita], [BeneficiarioNombre], [BeneficiarioApellidoPaterno], [BeneficiarioApellidoMaterno], [IdJuzgado], [Interno], [IdUser], [Tipo])
                VALUES
                (@NoEjecucion, GETDATE(), @CveSolicitante, @DetalleSolicitante, @CveSolicitud, @OtroSolicita, @BeneficiarioNombre, @BeneficiarioApellidoPaterno, @BeneficiarioApellidoMaterno, @IdJuzgado, @Interno, @IdUser, 'Ejecución');
                SELECT CAST(scope_identity() AS int);";
                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@NoEjecucion", noEjecucion);
                    cmd.Parameters.AddWithValue("@CveSolicitante", cveSolicitante);
                    cmd.Parameters.AddWithValue("@DetalleSolicitante", detalleSolicitante);
                    cmd.Parameters.AddWithValue("@CveSolicitud", cveSolicitud);
                    cmd.Parameters.AddWithValue("@OtroSolicita", otroSolicita ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BeneficiarioNombre", beneficiarioNombre);
                    cmd.Parameters.AddWithValue("@BeneficiarioApellidoPaterno", beneficiarioApellidoPaterno);
                    cmd.Parameters.AddWithValue("@BeneficiarioApellidoMaterno", beneficiarioApellidoMaterno ?? (object)DBNull.Value); // Manejo de posible valor nulo
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                    cmd.Parameters.AddWithValue("@Interno", interno);
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    int idEjecucion = (int)cmd.ExecuteScalar();
                    return idEjecucion;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar en P_Ejecucion: " + ex.Message);
            }
        }
        //FUNCION INSERTAR EN P_EJECUCIONASUNTO
        private bool InsertarEnEjecucionAsunto(SqlConnection conn, SqlTransaction transaction, int idEjecucion, int idAsunto)
        {
            try
            {
                string query = @"
                INSERT INTO [SIPOH].[dbo].[P_EjecucionAsunto] (IdEjecucion, IdAsunto)
                VALUES (@IdEjecucion, @IdAsunto)";
                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }




        //
    }
}