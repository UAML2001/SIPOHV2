using SIPOH.Controllers.AC_JefeUnidadCausa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SIPOH.Controllers.AC_JefeUnidadCausa.JUC_CatDelitosController;

namespace SIPOH
{
    public partial class clasidelito : GeneralesyWebUi
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
                bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/clasideli")) : false;
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
                GridViewClasificacionDelitos.DataSource = InicializaTablaVaciaDelitos();
                GridViewClasificacionDelitos.DataBind();
                CargarCatalogos loader = new CargarCatalogos();
                loader.LoadGradosConsumacion(ddlGradoConsumacion);
                loader.LoadConcursos(ddlConcurso);
                loader.LoadFormasAccion(ddlFormaAccion);
                loader.LoadCalificaciones(ddlCalificacion);
                loader.LoadClasificaciones(ddlOrdenResultado);
                loader.LoadElementosComision(ddlComision);
                loader.LoadFormasComision(ddlFormaComision);
                loader.LoadModalidades(ddlModalidad);
                loader.LoadMunicipios(ddlCatMunicipios);
                loader.LoadDelitos(ddDelitos);
                CargarCatalogos cargador = new CargarCatalogos();
                //checar esto que inicialice en invisible
                divFechaReclasificacion.Style["display"] = "none";
                divCheckReclasificar.Style["display"] = "none";
            }
        }
        protected void checkNoIdentificado_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNoIdentificado.Checked)
            {
                FechaDelito.Text = "1899-09-09";
            }
            else
            {
                FechaDelito.Text = "";
            }
        }
        protected void reclasificacionSi_CheckedChanged(object sender, EventArgs e)
        {
            if (reclasificacionSi.Checked)
            {
                ViewState["Reclasificado"] = "R";
            }
            else
            {
                ViewState.Remove("Reclasificado");
            }

            divFechaReclasificacion.Style["display"] = reclasificacionSi.Checked ? "" : "none";
            divAgregarClasificacion.Style["display"] = reclasificacionSi.Checked ? "" : "none";
            CargarCatDelitos();
            ddDelitos.SelectedIndex = 0;
            ddlModDelito.Items.Clear();
            ddlModDelito.Items.Insert(0, new ListItem("-- SELECCIONAR --", "0"));
            divModalidad.Style["display"] = "none";
        }
        protected void CargarCatDelitos()
        {
            CargarCatalogos cargarCatalogos = new CargarCatalogos();
            int idDelitoActual = Convert.ToInt32(ddDelitos.SelectedValue);
            ddDelitos.Items.Clear();
            cargarCatalogos.LoadDelitos(ddDelitos);
            ddDelitos.SelectedValue = idDelitoActual.ToString();
            if (ddDelitos.SelectedIndex == -1)
                ddDelitos.SelectedIndex = 0;
            ddDelitos.AutoPostBack = true;
            ddDelitos.SelectedIndexChanged += ddDelitos_SelectedIndexChanged;
        }
        protected void ddlTipoAsunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoAsunto.SelectedValue == "JO")
            {
                btnAgregarClasiDelito.Enabled = false;
                btnAgregarClasiDelito.Visible = false;
            }
            else
            {
                btnAgregarClasiDelito.Enabled = true;
                btnAgregarClasiDelito.Visible = true;
            }
        }
        protected void ddDelitos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDelito = Convert.ToInt32(ddDelitos.SelectedValue);
            CargarCatalogos cargarCatalogos = new CargarCatalogos();

            // Limpiar el DropDownList y ocultarlo inicialmente
            ddlModDelito.Items.Clear();
            ddlModDelito.Visible = false;
            divModalidad.Style["display"] = "none";

            // Cargar modalidades (detalles del delito) para el IdDelito seleccionado
            cargarCatalogos.LoadModalidadesPorIdDelito(ddlModDelito, idDelito);

            // Verificar si hay modalidades para mostrar
            if (ddlModDelito.Items.Count > 1) // Más de un item incluyendo "-- SELECCIONAR --"
            {
                ddlModDelito.Visible = true;
                divModalidad.Style["display"] = "block";
               
                ddlModDelito.SelectedIndex = 0;
            }
        }
        protected string ConvertPersecucion(object value)
        {
            if (value == null || value == DBNull.Value)
                return string.Empty;

            string persecucion = value.ToString();
            switch (persecucion)
            {
                case "Q":
                    return "Querella";
                case "D":
                    return "Denuncia";
                case "N":
                    return "No Identificado";
                default:
                    return persecucion;
            }
        }
        protected void Limpiar()
        {
            DropDownList[] dropdowns = new DropDownList[] {
        ddlModDelito, ddlCatMunicipios, ddlGradoConsumacion, ddlConcurso,
        ddlFormaAccion, ddlCalificacion, ddlOrdenResultado,
        ddlComision, ddlFormaComision, ddlModalidad, ddlModDelito
    };
            foreach (var ddl in dropdowns)
            {
                if (ddl.Items.Count > 0)
                    ddl.SelectedIndex = 0;
            }
            ddlModDelito.Visible = false;
            divModalidad.Style["display"] = "none";
            ddDelitos.Enabled = true;
            rbQuerella.Checked = false;
            rbDenuncia.Checked = false;
            rbNoIdentificado.Checked = false;
            txtLocalidad.Text = "";
            CargarCatalogos cargarCatalogos = new CargarCatalogos();
            cargarCatalogos.LoadDelitos(ddDelitos);
            FechaDelito.Text = "";

            // Limpiar la selección del GridView usando CSS
            if (ViewState["SelectedRowIndex"] != null)
            {
                int previousIndex = (int)ViewState["SelectedRowIndex"];
                if (previousIndex > -1 && previousIndex < GridViewClasificacionDelitos.Rows.Count)
                {
                    GridViewClasificacionDelitos.Rows[previousIndex].CssClass = "";
                }
                ViewState["SelectedRowIndex"] = -1; // Resetear el índice seleccionado
            }

            // Mantener los ViewState importantes
            var accion = ViewState["Accion"];
            var idAsunto = ViewState["IdAsunto"];
            ViewState.Clear();
            ViewState["Accion"] = accion;
            ViewState["IdAsunto"] = idAsunto;
            // Limpiar el ViewState de Reclasificado
            ViewState.Remove("Reclasificado");
        }
        protected void LimpiarFormulario()
        {
            DropDownList[] dropdowns = new DropDownList[] {
                ddlModDelito, ddlCatMunicipios, ddlGradoConsumacion, ddlConcurso,
                ddlFormaAccion, ddlCalificacion, ddlOrdenResultado,
                ddlComision, ddlFormaComision, ddlModalidad, ddlModDelito
            };
            foreach (var ddl in dropdowns)
            {
                if (ddl.Items.Count > 0)
                    ddl.SelectedIndex = 0;
            }
            ddlModDelito.Visible = false;
            divModalidad.Style["display"] = "none";
            ddDelitos.Enabled = true;
            rbQuerella.Checked = false;
            rbDenuncia.Checked = false;
            rbNoIdentificado.Checked = false;
            txtLocalidad.Text = "";
            CargarCatalogos cargarCatalogos = new CargarCatalogos();
            cargarCatalogos.LoadDelitos(ddDelitos);
            FechaDelito.Text = "";
        }
        protected void btnAgregarClasiDelito_Click(object sender, EventArgs e)
        {
            if (GridViewClasificacionDelitos.Rows.Count == 0)
            {
                MensajeAdvertencia("Necesitas buscar un asunto para poder agregar más delitos.");
                return;
            }

            // Limpiar los campos del formulario
            LimpiarFormulario();

            // Establecer el valor del ViewState para la acción después de limpiar
            ViewState["Accion"] = "AG";

            divAgregarClasificacion.Style["display"] = "block";
            divCheckReclasificar.Style["display"] = "none";
            divFechaReclasificacion.Style["display"] = "none";
        }
        private DataTable InicializaTablaVaciaDelitos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Consumacion", typeof(string));
            dt.Columns.Add("Calificacion", typeof(string));
            dt.Columns.Add("Concurso", typeof(string));
            dt.Columns.Add("Clasificacion", typeof(string));
            dt.Columns.Add("Comision", typeof(string));
            dt.Columns.Add("Accion", typeof(string));
            dt.Columns.Add("Modalidad", typeof(string));
            dt.Columns.Add("ElemComision", typeof(string));
            dt.Columns.Add("Municipio", typeof(string));
            dt.Columns.Add("Persecucion", typeof(string));
            dt.Columns.Add("FeDelito", typeof(string));
            dt.Columns.Add("Domicilio", typeof(string));
            return dt;
        }
        protected void btnBuscarClasiDelito_Click(object sender, EventArgs e)
        {
            Limpiar();
            ddDelitos.Enabled = false;
            divFechaReclasificacion.Style["display"] = "none";
            divCheckReclasificar.Style["display"] = "none";
            divAgregarClasificacion.Style["display"] = "none";
            string tipoAsunto = ddlTipoAsunto.SelectedValue;
            string numeroAsunto = txtNumeroAsunto.Text;

            int idJuzgado;
            if (HttpContext.Current.Session["IDJuzgado"] != null && int.TryParse(HttpContext.Current.Session["IDJuzgado"].ToString(), out idJuzgado))
            {
                if (string.IsNullOrWhiteSpace(tipoAsunto) || string.IsNullOrWhiteSpace(numeroAsunto))
                {
                    MensajeError("Por favor, selecciona un tipo de asunto e ingresa un número de asunto.");
                    return;
                }
                JUC_ClasificacionDelitoController controller = new JUC_ClasificacionDelitoController();
                DataTable dt = controller.BuscarDelitos(tipoAsunto, numeroAsunto, idJuzgado);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MensajeAdvertencia("No se encontraron resultados para el tipo de asunto y número proporcionados.");
                    GridViewClasificacionDelitos.DataSource = InicializaTablaVaciaDelitos();
                    GridViewClasificacionDelitos.DataBind();
                    return;
                }

                GridViewClasificacionDelitos.DataSource = dt;
                GridViewClasificacionDelitos.DataBind();
                MensajeExito("Delitos encontrados con éxito.");

                // Guardar IdAsunto, de la primera fila, si hay cambios mdificar el storage y aqui
                if (dt.Rows.Count > 0)
                {
                    ViewState["IdAsunto"] = dt.Rows[0]["IdAsunto"];
                }

                divCheckReclasificar.Style["display"] = "none";
            }
            else
            {
                MensajeError("No se ha podido identificar el juzgado correspondiente. Por favor, inicie sesión nuevamente.");
            }
        }
        //FUNCION DE SELECTED GRID
        protected void GridViewClasificacionDelitos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["SelectedRowIndex"] != null)
                {
                    int previousIndex = (int)ViewState["SelectedRowIndex"];
                    if (previousIndex > -1 && previousIndex < GridViewClasificacionDelitos.Rows.Count)
                    {
                        GridViewClasificacionDelitos.Rows[previousIndex].CssClass = "";
                    }
                }
                divCheckReclasificar.Style["display"] = "block";
                GridViewRow row = GridViewClasificacionDelitos.SelectedRow;
                if (row != null)
                {
                    row.CssClass = "table-success";
                    ViewState["SelectedRowIndex"] = row.RowIndex;

                    // Carga detalles del delito seleccionado
                    int idDeliAsunto = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDeliAsunto"]);
                    JUC_CatDelitosController controller = new JUC_CatDelitosController();
                    var delito = JUC_CatDelitosController.GetDelitoByDeliAsunto(idDeliAsunto);
                    if (delito != null)
                    {
                        controller.LoadDelitoByDeliAsunto(ddDelitos, idDeliAsunto);
                        ddDelitos.Enabled = true;
                        ddDelitos.SelectedValue = delito.IdDelito.ToString();

                        // Obtener el IdDelDetalle de la fila seleccionada
                        int? idDelDetalle = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelDetalle"] as int?;

                        CargarCatalogos cargarCatalogos = new CargarCatalogos();

                        // Cargar todas las modalidades y seleccionar la específica si existe
                        cargarCatalogos.LoadModalidadPorIdDelDetalle(ddlModDelito, delito.IdDelito, idDelDetalle);

                        if (ddlModDelito.Items.Count > 1)
                        {
                            ddlModDelito.Visible = true;
                            divModalidad.Style["display"] = "block";
                        }
                        else
                        {
                            ddlModDelito.Visible = false;
                            divModalidad.Style["display"] = "none";
                        }
                    }
                    else
                    {
                        ddlModDelito.Visible = false;
                        divModalidad.Style["display"] = "none";
                    }

                    // Verificar si hay datos esenciales vacíos
                    if (GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdMunicipio"] == DBNull.Value ||
                        GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Persecucion"] == DBNull.Value)
                    {
                        ViewState["Accion"] = "I";
                        MensajeAdvertencia("Hay datos vacíos en la tabla, te recomendamos actualizar el delito con su información");
                        divFechaReclasificacion.Style["display"] = "none";
                        divCheckReclasificar.Style["display"] = "none";
                        divAgregarClasificacion.Style["display"] = "block";
                        return;
                    }
                    else
                    {
                        ViewState["Accion"] = "U";
                    }

                    UpdateDropDownLists(row);
                    SetLocationDetails(row);
                    string tipoPersecucion = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Persecucion"].ToString();
                    SeleccionarTipoPersecucion(tipoPersecucion);
                    divAgregarClasificacion.Style["display"] = "block";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en la selección del GridView: " + ex.Message);
                MensajeError("Error al actualizar la selección: " + ex.Message);
                divAgregarClasificacion.Style["display"] = "none";
            }
        }
        private void UpdateDropDownLists(GridViewRow row)
        {
            // Actualiza los dropdown 
            SetDropDownValue(ddlCatMunicipios, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdMunicipio"]));
            SetDropDownValue(ddlGradoConsumacion, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatConsumacion"]));
            SetDropDownValue(ddlConcurso, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatConcurso"]));
            SetDropDownValue(ddlFormaAccion, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatAccion"]));
            SetDropDownValue(ddlCalificacion, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatCalificacion"]));
            SetDropDownValue(ddlOrdenResultado, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatClasificacion"]));
            SetDropDownValue(ddlComision, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatElemComision"]));
            SetDropDownValue(ddlFormaComision, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatComision"]));
            SetDropDownValue(ddlModalidad, Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Id_CatModalidad"]));
        }
        private void SetDropDownValue(DropDownList ddl, int value)
        {
            // Agrega un valor a el item de un ddl por parámetro
            if (ddl.Items.FindByValue(value.ToString()) != null)
            {
                ddl.SelectedValue = value.ToString();
            }
        }
        private void SetLocationDetails(GridViewRow row)
        {
            // Actualiza los campos de texto relacionados con la localización y fecha del delito basado en la fila seleccionada del GridView.
            string fechaDelito = Convert.ToDateTime(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["FeDelito"]).ToString("yyyy-MM-dd");
            string domicilio = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["Domicilio"].ToString();
            txtLocalidad.Text = domicilio;
            FechaDelito.Text = fechaDelito;
        }
        private void SeleccionarTipoPersecucion(string tipoPersecucion)
        {
            // Configura el estado de los radio buttons relacionados con el tipo de persecución del delito.
            rbQuerella.Checked = false;
            rbDenuncia.Checked = false;
            rbNoIdentificado.Checked = false;
            switch (tipoPersecucion)
            {
                case "Q":
                    rbQuerella.Checked = true;
                    break;
                case "D":
                    rbDenuncia.Checked = true;
                    break;
                case "N":
                    rbNoIdentificado.Checked = true;
                    break;
                default:
                    break;
            }
        }
        //FUNCION DE GUARDADO
        private DateTime? ObtenerFechaIngreso(int idAsunto)
        {
            //funcion para obtener la fecha de ingreso del asunto
            DateTime? fechaIngreso = null;
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FeIngreso FROM P_Asunto WHERE IdAsunto = @IdAsunto";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAsunto", idAsunto);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        fechaIngreso = reader["FeIngreso"] as DateTime?;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al obtener FeIngreso: " + ex.Message);
                }
            }

            return fechaIngreso;
        }

        protected void btnGuardarClasiDeli_Click(object sender, EventArgs e)
        {
            string accion = ViewState["Accion"] != null ? ViewState["Accion"].ToString() : string.Empty;
            char? reclasificar = ViewState["Reclasificado"] != null ? (char?)ViewState["Reclasificado"].ToString()[0] : null;

            if (accion == string.Empty)
            {
                MensajeError("No se ha definido la acción a realizar.");
                return;
            }

            // Validaciones de campos obligatorios
            if (ddlGradoConsumacion.SelectedValue == "0")
            {
                MensajeError("El campo Grado de Consumación es obligatorio.");
                return;
            }

            if (ddlCalificacion.SelectedValue == "0")
            {
                MensajeError("El campo Calificación es obligatorio.");
                return;
            }

            if (ddlConcurso.SelectedValue == "0")
            {
                MensajeError("El campo Concurso es obligatorio.");
                return;
            }

            if (ddlOrdenResultado.SelectedValue == "0")
            {
                MensajeError("El campo Orden de Resultado es obligatorio.");
                return;
            }

            if (ddlFormaComision.SelectedValue == "0")
            {
                MensajeError("El campo Forma de Comisión es obligatorio.");
                return;
            }

            if (ddlFormaAccion.SelectedValue == "0")
            {
                MensajeError("El campo Forma de Acción es obligatorio.");
                return;
            }

            if (ddlModalidad.SelectedValue == "0")
            {
                MensajeError("El campo Modalidad es obligatorio.");
                return;
            }

            if (ddlComision.SelectedValue == "0")
            {
                MensajeError("El campo Comisión es obligatorio.");
                return;
            }

            if (ddlCatMunicipios.SelectedValue == "0")
            {
                MensajeError("El campo Municipio es obligatorio.");
                return;
            }

            if (string.IsNullOrWhiteSpace(FechaDelito.Text))
            {
                MensajeError("El campo Fecha del Delito es obligatorio.");
                return;
            }

            if (!rbQuerella.Checked && !rbDenuncia.Checked && !rbNoIdentificado.Checked)
            {
                MensajeError("Debe seleccionar un tipo de persecución del delito.");
                return;
            }

            if (ddDelitos.SelectedValue == "0")
            {
                MensajeError("El campo Delito es obligatorio.");
                return;
            }

            if (ddlModDelito.Visible && ddlModDelito.SelectedValue == "0")
            {
                MensajeError("El campo Modalidad es obligatorio.");
                return;
            }

            DateTime feDelito;
            if (!DateTime.TryParse(FechaDelito.Text, out feDelito))
            {
                MensajeError("Fecha de delito no válida.");
                return;
            }

            int idAsunto;
            if (ViewState["IdAsunto"] != null)
            {
                idAsunto = Convert.ToInt32(ViewState["IdAsunto"]);
            }
            else if (GridViewClasificacionDelitos.SelectedDataKey != null && GridViewClasificacionDelitos.SelectedDataKey.Values["IdAsunto"] != null)
            {
                idAsunto = Convert.ToInt32(GridViewClasificacionDelitos.SelectedDataKey.Values["IdAsunto"]);
            }
            else
            {
                MensajeError("No se pudo obtener el IdAsunto.");
                return;
            }

            DateTime? feIngreso = ObtenerFechaIngreso(idAsunto);

            if (feIngreso == null)
            {
                MensajeError("No se pudo obtener la fecha de ingreso.");
                return;
            }

            if (feDelito > feIngreso)
            {
                MensajeError("La fecha del delito no puede ser mayor a la fecha de captura.");
                return;
            }

            int? idDelitoC = null;
            int? idDeliAsunto = null;

            if (accion == "U")
            {
                if (GridViewClasificacionDelitos.SelectedDataKey.Values["IdDelitoC"] != null)
                {
                    idDelitoC = Convert.ToInt32(GridViewClasificacionDelitos.SelectedDataKey.Values["IdDelitoC"]);
                }
            }

            if (accion != "AG")
            {
                if (GridViewClasificacionDelitos.SelectedDataKey.Values["IdDeliAsunto"] != null)
                {
                    idDeliAsunto = Convert.ToInt32(GridViewClasificacionDelitos.SelectedDataKey.Values["IdDeliAsunto"]);
                }
            }

            int consumacion = Convert.ToInt32(ddlGradoConsumacion.SelectedValue);
            int calificacion = Convert.ToInt32(ddlCalificacion.SelectedValue);
            int concurso = Convert.ToInt32(ddlConcurso.SelectedValue);
            int clasificacion = Convert.ToInt32(ddlOrdenResultado.SelectedValue);
            int fComision = Convert.ToInt32(ddlFormaComision.SelectedValue);
            int fAccion = Convert.ToInt32(ddlFormaAccion.SelectedValue);
            int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
            int elemComision = Convert.ToInt32(ddlComision.SelectedValue);
            int idMunicipio = Convert.ToInt32(ddlCatMunicipios.SelectedValue);
            string domicilio = txtLocalidad.Text;
            string persecucion = rbQuerella.Checked ? "Q" : rbDenuncia.Checked ? "D" : rbNoIdentificado.Checked ? "N" : "";

            // Obtener el IdDelDetalle si está visible
            int idDelDetalle = ddlModDelito.Visible && ddlModDelito.SelectedValue != "0" ? Convert.ToInt32(ddlModDelito.SelectedValue) : 0;

            JUC_CrudClasiDelitosController controller = new JUC_CrudClasiDelitosController();
            bool resultado = false;

            int? idDelito = null;
            DateTime? feReclasificacion = null;
            DateTime? feCaptura = null;

            try
            {
                if (accion == "AG")
                {
                    idDelito = Convert.ToInt32(ddDelitos.SelectedValue);
                    resultado = controller.ActualizarClasificacionDelito(accion, null, null, consumacion, calificacion, concurso, clasificacion, fComision, fAccion, modalidad, elemComision, persecucion, idMunicipio, feDelito, domicilio, idDelDetalle, null, null, null, idAsunto, idDelito);
                }
                else if (accion == "U" && reclasificar == 'R')
                {
                    feReclasificacion = DateTime.Now;  // Puedes ajustar cómo obtienes esta fecha
                    feCaptura = DateTime.Now;          // Puedes ajustar cómo obtienes esta fecha
                    idDelito = Convert.ToInt32(ddDelitos.SelectedValue);
                    resultado = controller.ActualizarClasificacionDelito("U", idDelitoC, idDeliAsunto, consumacion, calificacion, concurso, clasificacion, fComision, fAccion, modalidad, elemComision, persecucion, idMunicipio, feDelito, domicilio, idDelDetalle, feReclasificacion, feCaptura, reclasificar, idAsunto, idDelito);
                }
                else // Maneja "U" e "I"
                {
                    resultado = controller.ActualizarClasificacionDelito(accion, idDelitoC, idDeliAsunto, consumacion, calificacion, concurso, clasificacion, fComision, fAccion, modalidad, elemComision, persecucion, idMunicipio, feDelito, domicilio, idDelDetalle, null, null, reclasificar, idAsunto);
                }

                if (resultado)
                {
                    MensajeExito("La clasificación del delito ha sido procesada correctamente.");
                    rbNoIdentificado.Checked = false;
                    reclasificacionSi.Checked = false;
                    btnBuscarClasiDelito_Click(sender, e);
                }
                else
                {
                    MensajeError("Error al procesar la clasificación del delito.");
                }
            }
            catch (Exception ex)
            {
                // Registro detallado del error
                System.Diagnostics.Debug.WriteLine("Error en btnGuardarClasiDeli_Click: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Accion: " + accion);
                System.Diagnostics.Debug.WriteLine("IdDelitoC: " + idDelitoC);
                System.Diagnostics.Debug.WriteLine("IdDeliAsunto: " + idDeliAsunto);
                System.Diagnostics.Debug.WriteLine("Consumacion: " + consumacion);
                System.Diagnostics.Debug.WriteLine("Calificacion: " + calificacion);
                System.Diagnostics.Debug.WriteLine("Concurso: " + concurso);
                System.Diagnostics.Debug.WriteLine("Clasificacion: " + clasificacion);
                System.Diagnostics.Debug.WriteLine("FComision: " + fComision);
                System.Diagnostics.Debug.WriteLine("FAccion: " + fAccion);
                System.Diagnostics.Debug.WriteLine("Modalidad: " + modalidad);
                System.Diagnostics.Debug.WriteLine("ElemComision: " + elemComision);
                System.Diagnostics.Debug.WriteLine("Persecucion: " + persecucion);
                System.Diagnostics.Debug.WriteLine("IdMunicipio: " + idMunicipio);
                System.Diagnostics.Debug.WriteLine("FeDelito: " + feDelito);
                System.Diagnostics.Debug.WriteLine("Domicilio: " + domicilio);
                System.Diagnostics.Debug.WriteLine("IdDelDetalle: " + idDelDetalle);
                System.Diagnostics.Debug.WriteLine("FeReclasificacion: " + feReclasificacion);
                System.Diagnostics.Debug.WriteLine("FeCaptura: " + feCaptura);
                System.Diagnostics.Debug.WriteLine("Reclasificar: " + reclasificar);
                System.Diagnostics.Debug.WriteLine("IdAsunto: " + idAsunto);
                System.Diagnostics.Debug.WriteLine("IdDelito: " + idDelito);

                MensajeError("Ocurrió un error inesperado al procesar la clasificación del delito: " + ex.Message);
            }
        }





        //FUNCION DE BORRAR
        protected void btnModalBorrar_Click(object sender, EventArgs e)
        {
             ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "abrirModalConfirmar();", true);
        }
        protected void btnBorrarClasiDelito_Click(object sender, EventArgs e)
        {
            if (GridViewClasificacionDelitos.SelectedIndex >= 0)
            {
                // Verificar si la cantidad de filas en la GridView es mayor que 1
                if (GridViewClasificacionDelitos.Rows.Count > 1)
                {
                    GridViewRow row = GridViewClasificacionDelitos.SelectedRow;
                    int idDeliAsunto = Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDeliAsunto"]);

                    // Verificar si IdDelitoC es DBNull antes de convertir
                    int idDelitoC = GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelitoC"] != DBNull.Value
                                    ? Convert.ToInt32(GridViewClasificacionDelitos.DataKeys[row.RowIndex].Values["IdDelitoC"])
                                    : 0;

                    // Verificar si el checkbox de reclasificación está marcado
                    string accion = ViewState["Reclasificado"] != null ? ViewState["Reclasificado"].ToString() : string.Empty;

                    JUC_DeleteClasiDelitosController controller = new JUC_DeleteClasiDelitosController();
                    bool resultado = controller.EliminarClasificacionDelito(accion, idDeliAsunto, idDelitoC);

                    if (resultado)
                    {
                        // Mostrar mensaje de éxito
                        MensajeExito("Se eliminó la clasificación del delito exitosamente");
                        // Refrescar el GridView después de la eliminación
                        btnBuscarClasiDelito_Click(sender, e);
                    }
                    else
                    {
                        // Mostrar mensaje de error
                        MensajeError("Error al eliminar la clasificación del delito.");
                    }
                }
                else
                {
                    // Mostrar mensaje de advertencia si solo queda una fila en la GridView
                    MensajeError("No se puede eliminar la última fila. Debe haber al menos un delito asociado al asunto.");
                }
            }
            else
            {
                // Mostrar mensaje de advertencia si no hay una fila seleccionada
                MensajeError("Por favor seleccione una fila de delito para eliminarlo.");
            }
        }
        //
    }
}