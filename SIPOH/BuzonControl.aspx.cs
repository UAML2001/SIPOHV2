using DatabaseConnection;
using SIPOH.Controllers.AC_CatalogosCompartidos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SIPOH
{
    public partial class BuzonControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int sessionTimeout = 1 * 60;
                Session.Timeout = sessionTimeout;
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                string circuito = HttpContext.Current.Session["TCircuito"] as string;
                List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;
                bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/promociones")) : false;
                if (enlaces == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if ((circuito == "c" || circuito == "e") && tienePermiso)
                {
                    Visible = true;
                }
                else
                {
                    Visible = false;
                    Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
                }
                //
                if (Session["IdJuzgado"] != null)
                {
                    LlenarDDL();
                    CargarDatosIniciales();
                }
            }
        }
        //conn
        private string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
        //cargar datos en grid view al cargar la pagina con parametro idjuzgado
        private void CargarDatosIniciales()
        {
            if (Session["IdJuzgado"] != null && int.TryParse(Session["IdJuzgado"].ToString(), out int idJuzgado))
            {
                AC_BandejaBuzonControlController controller = new AC_BandejaBuzonControlController();
                var bandejaSeguimiento = controller.ObtenerBandejaSeguimientoBuzon(idJuzgado);
                gridBuzonControl.DataSource = bandejaSeguimiento;
                gridBuzonControl.DataBind();
            }
            else
            {
                MensajeExito("No se encontró un juzgado logueado.", true);
            }
        }
        // FUNCIONES DE ALERTAS
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
        //llenar DDL
        private void LlenarDDL()
        {
            var tiposDeAsunto = new List<ListItem>
            {
                new ListItem("SELECCIONAR", "SELECT"),
                new ListItem("CAUSA", "C"),
                new ListItem("CUPRE", "CP"),
                new ListItem("EXHORTO", "E"),
                new ListItem("JUICIO ORAL", "JO"),
                new ListItem("TRADICIONAL", "T")
            };
            foreach (var item in tiposDeAsunto)
            {
                ddlTipoBusqueda.Items.Add(item);
            }
            // Añadir ítems estáticos
            ddlTipoBusqueda.Items.Add(new ListItem("NUC", "N"));
            ddlTipoBusqueda.Items.Add(new ListItem("FOLIO", "F"));
            ddlTipoBusqueda.Items.Add(new ListItem("TODOS", "TO"));
        }
        //FUNCION VER EL DOCUMENTO POR RUTA
        protected void gridBuzonControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gridBuzonControl.Rows[index];
                string rutaDoc = gridBuzonControl.DataKeys[index].Values["RutaDoc"].ToString();
                string baseUrl = "http://nas.pjhidalgo.gob.mx/SIPOH/Solicitudes/";
                string urlCompleta = baseUrl + rutaDoc;
                string script = $"window.open('{urlCompleta}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirDocumento", script, true);
            }
            else if (e.CommandName == "Rechazar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                long idSolicitudBuzon = Convert.ToInt64(gridBuzonControl.DataKeys[rowIndex].Values["IdSolicitudBuzon"]);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spr_AC_ActualizarEstatusBuzon", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdSolicitudBuzon", idSolicitudBuzon);
                        cmd.Parameters.AddWithValue("@Estatus", "R");
                        cmd.Parameters.AddWithValue("@FeAceptacion", DateTime.Now);
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            if (Session["BusquedaTipo"] != null && Session["BusquedaInput"] != null)
                            {
                                string seleccion = (string)Session["BusquedaTipo"];
                                string inputUsuario = (string)Session["BusquedaInput"];
                                CargarDatosGridView(seleccion, inputUsuario);
                            }
                            else
                            {
                                CargarDatosIniciales();
                            }
                        }
                        catch (Exception ex)
                        {
                            MensajeError("Se produjo un error al intentar rechazar la solicitud: " + ex.Message, true);
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }
                }
            }
            //
            else if (e.CommandName == "Guardar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                long idSolicitudBuzon = Convert.ToInt64(gridBuzonControl.DataKeys[rowIndex].Values["IdSolicitudBuzon"]);
                string tipoAsunto = gridBuzonControl.Rows[rowIndex].Cells[3].Text; // Asegúrate de reemplazar IndiceDeTuColumnaTipoAsunto por el índice real de tu columna TipoAsunto
                Session["IdSolicitudBuzon"] = idSolicitudBuzon;
                if (tipoAsunto == "INICIAL")
                {
                    Response.Redirect("Consignaciones.aspx");
                }
                else if (tipoAsunto == "POSTERIOR")
                {
                    Response.Redirect("PromocionesCtrl.aspx");
                }
                else
                {
                    // Aquí podrías manejar cualquier otro caso o mostrar un mensaje de error si es necesario
                }
            }
            //
        }
        protected void btnBuscarBuzon_Click(object sender, EventArgs e)
        {
            string seleccion = ddlTipoBusqueda.SelectedValue;
            string inputUsuario = txtNumeroAsunto.Text;
            Session["BusquedaTipo"] = seleccion;
            Session["BusquedaInput"] = inputUsuario;
            CargarDatosGridView(seleccion, inputUsuario);
        }
        // Funcion Para mostrar datos en grid view acorde a los parametros de busqueda
        private void CargarDatosGridView(string seleccion, string inputUsuario)
        {
            gridBuzonControl.DataSource = null;
            gridBuzonControl.DataBind();
            bool hayResultados = true;
            switch (seleccion)
            {
                case "TO":
                    if (Session["IdJuzgado"] != null && int.TryParse(Session["IdJuzgado"].ToString(), out int idJuzgado))
                    {
                        AC_BandejaBuzonControlController controllerTodos = new AC_BandejaBuzonControlController();
                        var resultadosTodos = controllerTodos.ObtenerBandejaSeguimientoBuzon(idJuzgado);
                        if (resultadosTodos == null || !resultadosTodos.Any()) // Asegúrate de que resultadosTodos es una colección enumerable
                        {
                            MensajeError("No se encontraron resultados.", true);
                        }
                        else
                        {
                            gridBuzonControl.DataSource = resultadosTodos;
                            gridBuzonControl.DataBind();
                        }
                    }
                    else
                    {
                        MensajeError("IdJuzgado no está disponible en la sesión.", true);
                    }
                    break;
                case "F":
                    if (long.TryParse(inputUsuario, out long folio))
                    {
                        AC_Folio_BandejaBuzonControlController folioController = new AC_Folio_BandejaBuzonControlController();
                        var resultadosFolio = folioController.ObtenerBandejaSeguimientoPorFolio(folio);
                        if (resultadosFolio == null || !resultadosFolio.Any()) // Verifica si hay resultados
                        {
                            MensajeError("No se encontraron resultados con el folio proporcionado.", true);
                        }
                        else
                        {
                            gridBuzonControl.DataSource = resultadosFolio;
                            gridBuzonControl.DataBind();
                        }
                    }
                    else
                    {
                        MensajeError("Error al convertir el folio a número.", true);
                    }
                    break;
                case "N":
                    AC_Nuc_BandejaBuzonControlController nucController = new AC_Nuc_BandejaBuzonControlController();
                    var resultadosNUC = nucController.ObtenerBandejaSeguimientoPorNUC(inputUsuario);
                    if (resultadosNUC == null || !resultadosNUC.Any()) // Verifica si hay resultados
                    {
                        MensajeError("No se encontraron resultados para el NUC proporcionado.", true);
                        hayResultados = false;
                    }
                    else
                    {
                        gridBuzonControl.DataSource = resultadosNUC;
                        gridBuzonControl.DataBind();
                    }
                    break;
                case "C":
                case "CP":
                case "E":
                case "JO":
                case "T":
                    AC_Tipo_BandejaBuzonControlController tipoController = new AC_Tipo_BandejaBuzonControlController();
                    var resultadosTipo = tipoController.ObtenerBandejaSeguimientoPorTipoYNumero(seleccion, inputUsuario);
                    if (resultadosTipo == null || !resultadosTipo.Any()) // Verifica si hay resultados
                    {
                        MensajeError($"No se encontraron resultados para el tipo seleccionado con el número proporcionado, verificalo.", true);
                        hayResultados = false;
                    }
                    else
                    {
                        gridBuzonControl.DataSource = resultadosTipo;
                        gridBuzonControl.DataBind();
                    }
                    break;
                default:
                    MensajeError("Selección de búsqueda no válida.", true);
                    hayResultados = false;
                    break;
            }
            if (hayResultados)
            {
                txtNumeroAsunto.Text = string.Empty;
            }
        }
        //FUNCION DESHABILITAR TEXTBOX AL ELEGIR OPCION TODOS
        protected void ddlTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoBusqueda.SelectedValue == "TO")
            {
                txtNumeroAsunto.ReadOnly = true;
                txtNumeroAsunto.Text = string.Empty;
            }
            else
            {
                txtNumeroAsunto.ReadOnly = false;
            }
        }
        //
    }
}