using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

using System.Configuration;
using SIPOH.Views;


namespace SIPOH
{
    public partial class Promociones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sessionTimeout = 1 * 60; // 20 minutos
            Session.Timeout = sessionTimeout;
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
            string circuito = HttpContext.Current.Session["TCircuito"] as string;
            List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;
            //bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/promociones"));
            bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/promociones")) : false;
            // Si enlaces es nulo, redirige a Default.aspx
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
            if (!IsPostBack)
            {

                tituloTablaPromociones.Visible = false;
                tituloDetallesCausa.Visible = false;
                lbltituloRelacionCausa.Visible = false;
                TablasAnexos.Visible = false;
                insertarPromoventeAnexos.Style.Add("display", "none");
                BotonGuardarDiv.Style.Add("display", "none");
                tituloSello.Style.Add("display", "none");
                divrelacionar.Style.Add("display", "none");
                CargarJuzgados();
                CargarAnexos();
                CargarJuzgadosAcusatorio();
                LoadDistritosTradicional();
                CargarJuzgadosJuicioOral();
                OtroAnexo.Disabled = true;
                TablasAnexos.Visible = false;
                VerificarCamposYDeshabilitarBoton();
                ScriptManager.RegisterStartupScript(this, GetType(), "verificarCampos", "verificarCampos();", true);
            }
        }
        //------------------------------------------------ PRIMER GRID ---------------------------------------------------------------
        //CODIGO PARA EL DROPDOWN DE JUZGADOS
        private void CargarJuzgados()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosPorCircuitoE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCircuito", circuito);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Nombre"].ToString();
                        listItem.Value = dr["IdJuzgado"].ToString();
                        selectBusJuzgados.Items.Add(listItem);
                    }
                }
            }
        }
        //CODIGO NUMERO EJECUCION CONSUMIENDO Ejecucion_ConsultaInicial PRIMER GRID
        protected void btnBuscarPromocion_Click(object sender, EventArgs e)
        {
            string ejecucion = inpuBusEjecucion.Value;
            string idJuzgado = selectBusJuzgados.Value;

            if (idJuzgado == "Seleccionar")
            {
                string mensajeError = "Por favor, selecciona un juzgado válido.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", $"toastError('{mensajeError}');", true);
                return;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ConsultaInicial", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Numero", ejecucion);
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }
            GridViewPromociones.DataSource = dt;
            GridViewPromociones.DataBind();
            if (dt.Rows.Count > 0)
            {
                //guardo en un viestate el idejecucion
                ViewState["NumeroEjecucionSeleccionado"] = dt.Rows[0]["NoEjecucion"].ToString();
                ViewState["IdEjecucionPrimerGrid"] = dt.Rows[0]["IdEjecucion"].ToString();
                tituloTablaPromociones.Visible = true;
                GridViewPromociones.DataSource = dt;
                GridViewPromociones.DataBind();
                tituloDetallesCausa.Visible = false;
                insertarPromoventeAnexos.Style.Add("display", "none");
                string mensajeExito = "Se encontraron resultados de tu consulta de promociones.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
            }
            else
            {
                tituloTablaPromociones.Visible = false;
                tituloDetallesCausa.Visible = false;
                string mensajeNoDatos = "No se encontro resultado de la busqueda.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                insertarPromoventeAnexos.Style.Add("display", "none");
            }
        }
        // ROCOMMAND PARA BOTONES DE PRIMERA GRID OBTENER EL IDASUNTO DE MI PRIMERA TABLA 
        protected void GridViewPromociones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                if (ViewState["IdEjecucionPrimerGrid"] != null &&
                   int.TryParse(ViewState["IdEjecucionPrimerGrid"].ToString(), out int idEjecucion))
                {
                    HiddenTipo.Value = "";
                    CheckJAcusatorio.Checked = false;
                    CheckJTradicional.Checked = false;
                    CheckJuiciOral.Checked = false;
                    VerDetalles(idEjecucion);
                }
                else
                {
                    MostrarMensajeError("No es valido o no esta presente el folio asunto");
                }
            }

            if (e.CommandName == "RelacionarCausa")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lbltituloRelacionCausa.Visible = true;
                divrelacionar.Style.Add("display", "block");
            }
        }
        //CODIGO DE ALERTAS DE ERRORES
        private void MostrarMensajeError(string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
        }
        //------------------------------------RADIO BUTTONS PARA RELACIONAR--------------------------------------------------------
        //radio buttons de relacion en juzgado acusatorio o tradicional ocultar divs
        protected void juzgadosRelacionar_CheckedChanged(object sender, EventArgs e)
        {
            
            if (CheckJAcusatorio.Checked)
            {
                HiddenTipo.Value = "C";
                divAcusatorioRelacionar.Style.Add("display", "block");
                divTradicionalRelacionar.Style.Add("display", "none");
                divJuicioOralRelacionar.Style.Add("display", "none");
            }
            else if (CheckJTradicional.Checked)
            {
                HiddenTipo.Value = "T";
                divTradicionalRelacionar.Style.Add("display", "block");
                divAcusatorioRelacionar.Style.Add("display", "none");
                divJuicioOralRelacionar.Style.Add("display", "none"); 
            }
            else if (CheckJuiciOral.Checked) 
            {
                HiddenTipo.Value = "JO";
                divJuicioOralRelacionar.Style.Add("display", "block");
                divAcusatorioRelacionar.Style.Add("display", "none");
                divTradicionalRelacionar.Style.Add("display", "none");
            }

            if (ViewState["IdEjecucionPrimerGrid"] != null)
            {
                int idEjecucion = Convert.ToInt32(ViewState["IdEjecucionPrimerGrid"]);
                VerDetalles(idEjecucion);
            }
            else
            {
                MostrarMensajeToast("No se pudo obtener la informacion en la tabla");
            }

        }
        //---------------------------- INSERT DE RELACION CAUSA A NO EJECUCION ----------------------------------------------
        private bool RelacionarCausaConEjecucion(int idAsunto, int idEjecucion, string tipoAsuntoEsperado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            bool result = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_RelacionarCausaPromocion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                    cmd.Parameters.AddWithValue("@TipoAsuntoEsperado", tipoAsuntoEsperado);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
        //CODIGO PARA OBTENER EL ID  ASUNTO DEL STORAGE POR LOS INPUTS
        private int ObtenerIdAsunto(int juzgado, string numero)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            int idAsunto = -1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ConsultarCausa", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Juzgado", juzgado);
                    cmd.Parameters.AddWithValue("@Numero", numero);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            idAsunto = Convert.ToInt32(dr["IdAsunto"]);
                        }
                    }
                }
            }

            return idAsunto;
        }
        private bool ValidarExistenciaDeCausa(string numero, int juzgado)
        {
            bool existe = false;
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ValidarAsunto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Numero", numero);
                    cmd.Parameters.AddWithValue("@IdJuzgado", juzgado);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && Convert.ToInt32(result) == 1)
                    {
                        existe = true;
                    }
                }
            }

            return existe;
        }
        //----------------------------------------- ACUSATORIO -------------------------------------------------------------
        //CODIGO PARA MOSTRAR EN EL SELECT DE RELACION CAUSAS ACUSATORIO
        private void CargarJuzgadosAcusatorio()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);

                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosConTipoYSubtipo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCircuito", circuito);
                    cmd.Parameters.AddWithValue("@Tipo", 'a');
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ListItem item = new ListItem(dr["Nombre"].ToString(), dr["IdJuzgado"].ToString());
                            JuzgadoAcusatorio.Items.Add(item);
                        }
                    }
                }
            }
        }
        //BOTON DE AGREGAR A TABLA EN ACUSATORIO:
        protected void btnAgregarAcusatorio_Click(object sender, EventArgs e)
        {
            try
            {
                string juzgadoValor = Request.Form[JuzgadoAcusatorio.UniqueID];
                if (string.IsNullOrEmpty(juzgadoValor) || juzgadoValor == "Seleccionar")
                {
                    MostrarMensajeError("Por favor, selecciona un juzgado.");
                    return;
                }
                int juzgado = Convert.ToInt32(juzgadoValor);
                string numero = inputCausaNuc.Value;
                // Validar si existe la causa
                bool causaExiste = ValidarExistenciaDeCausa(numero, juzgado);
                if (!causaExiste)
                {
                    MostrarMensajeError("La CAUSA|NUC no existe en el sistema para el juzgado seleccionado, Favor de verificarlo.");
                    return;
                }
                // Obtener IdAsunto
                int idAsunto = ObtenerIdAsunto(juzgado, numero);
                if (idAsunto == -1)
                {
                    MostrarMensajeError("No se pudo obtener el folio del asunto, verifica que la causa sea correcta o pertenezca a ese juzgado");
                    return;
                }
                // Obtener IdEjecucion de ViewState
                int idEjecucion = Convert.ToInt32(ViewState["IdEjecucionPrimerGrid"]);

                // Insertar relación en la base de datos
                if (!RelacionarCausaConEjecucion(idAsunto, idEjecucion, "C"))
                {
                    MostrarMensajeError("Puede que la causa ya exista en la base de datos o verifique que sea la causa correcta en el juzgado seleccionado y el tipo de asunto.");
                    return;
                }
                // Actualizar GridView
                VerDetalles(idEjecucion);
                string mensaje = "Se relacionó la CAUSA|NUC con el número de ejecución correctamente";
                string script = $"mostrarToast('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
            catch (FormatException)
            {
                MostrarMensajeError("El valor seleccionado en el juzgado no es válido.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Ocurrió un error: " + ex.Message);
            }
        }
        //CODIGO PARA INSERTAR EN P_EJECUCIONASUNTO POR STORAGE RELACIONARCAUSAPROMOCION
        //----------------------------------------- TRADICIONAL -------------------------------------------------------------
        //CHANGE JUXGADOS TRADICIONALES 
        protected void DistritoTradicional_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDistritoId = int.Parse(ddlDistritoTradicional.SelectedValue);
            LoadJuzgados(selectedDistritoId);
        }
        //CODIGO DE CARGAR JUZGADOS EN EL DROPDOWNLIST DE JUZGADOS Y DISTRITO TRADICIONAL
        protected void LoadJuzgados(int distritoId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Juzgados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDistrito", distritoId);
                    cmd.Parameters.AddWithValue("@Opcion", 2);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlJuzgadoTradicional.DataSource = reader;
                        ddlJuzgadoTradicional.DataTextField = "Nombre";
                        ddlJuzgadoTradicional.DataValueField = "IdJuzgado";
                        ddlJuzgadoTradicional.DataBind();
                    }
                }
            }
            ddlJuzgadoTradicional.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        //CODIGO PARA CARGAR PRIMERO DISTRITOS
        protected void LoadDistritosTradicional()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Distritos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    int idCircuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                    cmd.Parameters.AddWithValue("@IdCircuito", idCircuito);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlDistritoTradicional.DataSource = reader;
                        ddlDistritoTradicional.DataTextField = "nombre";
                        ddlDistritoTradicional.DataValueField = "IdDistrito";
                        ddlDistritoTradicional.DataBind();
                    }
                }
            }
            ddlDistritoTradicional.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        //CODIGO AGREGAR CAUSAS DEL BOTON DE AGREGAR DESDE TRADICIONAL
        protected void btnAgregarTradicional_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener valores de los controles de interfaz
                int juzgado = Convert.ToInt32(ddlJuzgadoTradicional.SelectedValue);
                if (juzgado == 0)
                {
                    MostrarMensajeError("Por favor, selecciona un juzgado.");
                    return;
                }

                string numero = inputCausaTradicional.Value;

                // Validar si existe la causa
                bool causaExiste = ValidarExistenciaDeCausa(numero, juzgado);
                if (!causaExiste)
                {
                    MostrarMensajeError("La CAUSA no existe en el sistema para el juzgado seleccionado.");
                    return;
                }

                // Obtener IdAsunto
                int idAsunto = ObtenerIdAsunto(juzgado, numero);
                if (idAsunto == -1)
                {
                    MostrarMensajeError("No se pudo obtener el folio del asunto, verifica que la causa sea correcta o pertenezca a ese juzgado");
                    return;
                }

                // Obtener IdEjecucion de ViewState
                int idEjecucion = Convert.ToInt32(ViewState["IdEjecucionPrimerGrid"]);

                // Insertar relación en la base de datos
                if (!RelacionarCausaConEjecucion(idAsunto, idEjecucion, "T"))
                {
                    MostrarMensajeError("Puede que la causa ya exista en la base de datos o verifique que sea la causa correcta en el juzgado seleccionado y el tipo de asunto.");
                    return;
                }

                // Actualizar GridView
                VerDetalles(idEjecucion);
                string mensaje = "Se relacionó la CAUSA con el número de ejecución correctamente";
                string script = $"mostrarToast('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
            catch (FormatException)
            {
                MostrarMensajeError("El valor seleccionado en el juzgado no es válido.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Ocurrió un error: " + ex.Message);
            }
        }
        //------------------------------------------ JUICIO ORAL -----------------------------------------------------------
        private void CargarJuzgadosJuicioOral()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);

                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosConTipoYSubtipo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCircuito", circuito);
                    cmd.Parameters.AddWithValue("@Tipo", 'a');
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ListItem item = new ListItem(dr["Nombre"].ToString(), dr["IdJuzgado"].ToString());
                            JuzgadoJuicioOral.Items.Add(item);
                        }
                    }
                }
            }
        }
        //CODIGO BOTON DE AGREGAR JUICIO ORAL AL GRID PRINCIPAL
        protected void btnAgregarJuicioOral_Click(object sender, EventArgs e)
        {
            try
            {
                string juzgadoValor = Request.Form[JuzgadoJuicioOral.UniqueID];
                if (string.IsNullOrEmpty(juzgadoValor) || juzgadoValor == "Seleccionar")
                {
                    MostrarMensajeError("Por favor, selecciona un juzgado.");
                    return;
                }
                int juzgado = Convert.ToInt32(juzgadoValor);
                string numero = inputNumeroJuicio.Value;

                // Validar si existe la causa
                bool causaExiste = ValidarExistenciaDeCausa(numero, juzgado);
                if (!causaExiste)
                {
                    MostrarMensajeError("El Juicio Oral no existe en el sistema para el juzgado seleccionado.");
                    return;
                }

                int idAsunto = ObtenerIdAsunto(juzgado, numero);
                if (idAsunto == -1)
                {
                    // Si no se encuentra el IdAsunto después de validar su existencia, manejar el error
                    MostrarMensajeError("No se pudo obtener el folio del asunto, verifica que la causa sea correcta o pertenezca a ese juzgado");
                    return;
                }
                int idEjecucion = Convert.ToInt32(ViewState["IdEjecucionPrimerGrid"]);

                if (!RelacionarCausaConEjecucion(idAsunto, idEjecucion, "JO"))
                {
                    MostrarMensajeError("Puede que el juicio ya exista en la base de datos o verifique que sea el juicio correcto en el juzgado seleccionado y el tipo de asunto");
                    return;
                }

                VerDetalles(idEjecucion);
                string mensaje = "Se relacionó el Juicio Oral con el número de ejecución correctamente";
                string script = $"mostrarToast('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
            catch (FormatException)
            {
                MostrarMensajeError("El valor seleccionado en el juzgado no es válido.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Ocurrió un error: " + ex.Message);
            }
        }
        //------------------------------------ SEGUNDO GRID DE DETALLES -----------------------------------------------------
        //CODIGO DE VER DETALLES DE LA CAUSA CONSUMIENDO Ejecucion_MostrarCausasRelacionadas
        protected void VerDetalles(int idEjecucion)
        {
            // obtener el idejecucion de la primer grid en donde guarde en el viewstate el id
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_MostrarCausasRelacionadas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                    cmd.Parameters.AddWithValue("@TipoAsunto", HiddenTipo.Value);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }
            GridViewDetalles.DataSource = dt;
            GridViewDetalles.DataBind();
            bool tieneDatos = dt.Rows.Count > 0;
            tituloTablaPromociones.Visible = dt.Rows.Count > 0;
            tituloDetallesCausa.Visible = dt.Rows.Count > 0;
            insertarPromoventeAnexos.Style.Add("display", dt.Rows.Count > 0 ? "block" : "none");
            lblMensajeSinDatos.Visible = !tieneDatos;
            if (!tieneDatos)
            {
                lblMensajeSinDatos.Text = "No se encontraron datos.";
            }
        }
        //CODIGO PARA BORRAR RELACION DE CAUSAS A EJEUCION EN EL SEGUNDO GRID
        protected void BorrarCausa_Click(object sender, EventArgs e)
        {
            // Obtener el conteo de registros actualmente en la GridView
            int conteoRegistros = GridViewDetalles.Rows.Count;

            // Verificar si hay más de un registro antes de permitir la eliminación
            if (conteoRegistros > 1)
            {
                Button btn = (Button)sender;
                int idAsunto = Convert.ToInt32(btn.CommandArgument);
                // Obtener IdEjecucion de ViewState
                int idEjecucion = Convert.ToInt32(ViewState["IdEjecucionPrimerGrid"]);
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Ejecucion_EliminarEjecucionAsunto", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                VerDetalles(idEjecucion);
            }
            else
            {
                string mensaje = "No puede eliminar la ultima causa relacionada a este numero de ejecucion";
                string script = $"toastInfo('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
        }
        //--------------------------------------------------ANEXOS------------------------------------------------------------
        private void CargarAnexos()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_EjecucionCatAnexos WHERE Tipo = 'B' OR Descripcion = 'OTRO' ORDER BY CASE WHEN Descripcion = 'OTRO' THEN 1 ELSE 0 END, Descripcion", con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Descripcion"].ToString();
                        listItem.Value = dr["Descripcion"].ToString();
                        CatAnexosDD.Items.Add(listItem);
                    }
                }
            }
        }
        //DESHABILITAR OPCION DE OTRO CUANDO SE CAMBIE EL DROPDOWN VALUE DE ANEXOS
        protected void CatAnexosDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            OtroAnexo.Disabled = CatAnexosDD.SelectedValue != "OTRO";
        }
        [Serializable]
        public class ListaAnexos
        {
            public string NombreAnexo { get; set; }
            public string CantidadAnexo { get; set; }
        }
        //AGREGAR A VIEWSTATE ANEXOS
        protected void AgregarATabla(object sender, EventArgs e)
        {
            string anexo = CatAnexosDD.SelectedItem.Text;
            string valorAnexo = CatAnexosDD.SelectedItem.Value; // Obtiene el valor del ítem seleccionado

            if (anexo == "OTRO")
            {
                anexo = OtroAnexo.Value;
            }
            string cantidad = CantidadInput.Value;

            List<ListaAnexos> salas = ViewState["anexos"] as List<ListaAnexos> ?? new List<ListaAnexos>();
            // Verificar si la opción "Seleccionar" fue elegida
            if (valorAnexo.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase))
            {
                MostrarMensajeToast("Debes seleccionar una opción válida");
                TablasAnexos.Visible = false;
                return; // Finaliza la ejecución de la función
            }

            // Verificar si los campos no están vacíos y la cantidad es mayor que cero
            if (!string.IsNullOrWhiteSpace(anexo) && int.TryParse(cantidad, out int cantidadNumerica) && cantidadNumerica > 0)
            {
                var salaExistente = salas.FirstOrDefault(s => s.NombreAnexo.Equals(anexo, StringComparison.OrdinalIgnoreCase));
                if (salaExistente != null)
                {
                    // Actualizar la cantidad del anexo existente
                    salaExistente.CantidadAnexo = cantidad;
                }
                else
                {
                    // Agregar un nuevo anexo
                    salas.Add(new ListaAnexos { NombreAnexo = anexo, CantidadAnexo = cantidad });
                }
                ViewState["anexos"] = salas;
                tablaDatos.DataSource = salas;
                tablaDatos.DataBind();
                TablasAnexos.Visible = true;
                BotonGuardarDiv.Style.Add("display", "block");
                CatAnexosDD.ClearSelection();
                OtroAnexo.Value = "";
                OtroAnexo.Disabled = true;
                CantidadInput.Value = "";
            }
            else
            {
                MostrarMensajeToast("No puedes dejar campos vacíos y la cantidad debe ser mayor que cero");
                TablasAnexos.Visible = false;
            }
        }
        //MENSAJE DE ERROR
        private void MostrarMensajeToast(string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
        }
        //BORRAR FILA DE LOS ANEXOS EN LA TABLA DEL VIEWSTATE
        protected void BorrarFila(object sender, GridViewDeleteEventArgs e)
        {
            List<ListaAnexos> salas = (List<ListaAnexos>)ViewState["anexos"];
            salas.RemoveAt(e.RowIndex);
            ViewState["anexos"] = salas;
            tablaDatos.DataSource = salas;
            tablaDatos.DataBind();
            // Verificar si la tabla aún tiene elementos después de borrar la fila
            if (salas.Count == 0)
            {
                // Si no tiene elementos, oculta la tabla y el botón de guardar
                TablasAnexos.Visible = false;
                BotonGuardarDiv.Style.Add("display", "none");
            }
        }
        //ROWDATABOUND CAMBIAR EL COLOR A GRIS OBSCURO DE MI GRID TABLA DATOS
        protected void tablaDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary text-uppercase";
                }
            }
        }
        //ABRIR MODAL DE GUARDAR DATOS SOLO MODAL
        protected void btnModalPromociones_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "abrirModalGuardarDatos2();", true);
        }
        //AQUI EMPIEZA EL INSERT
        protected void GridViewPromociones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //obtenemos el idejecucion del valor oculto d emi grid
                HiddenField hiddenIdEjecucion = (HiddenField)e.Row.FindControl("HiddenIdEjecucion");
                int idEjecucion = int.Parse(hiddenIdEjecucion.Value);
                // Guardar el IdEjecucion en ViewState o en otro lugar para su uso posterior
                ViewState["IdEjecucionSeleccionado"] = idEjecucion;
            }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Style.Add("display", "none");
            }
        }
        //--------------------------------------------- INSERT ANEXOS --------------------------------------------------------
        //CODIGO BOTON DE INSERCION HACIA 
        protected void btnGuardarPromocion_Click(object sender, EventArgs e)
        {
            int idEjecucion = 0; // Valor predeterminado, por ejemplo, 0
            string nombreJuzgado = ObtenerNombreJuzgadoPorID(selectBusJuzgados.Value);
            int totalAnexos = CalcularTotalAnexos();
            string numeroEjecucion = ViewState["NumeroEjecucionSeleccionado"] != null ? ViewState["NumeroEjecucionSeleccionado"].ToString() : "No disponible";

            if (ViewState["IdEjecucionSeleccionado"] != null)
            {
                idEjecucion = (int)ViewState["IdEjecucionSeleccionado"];
            }
            else
            {
                string mensajeNoDatos = "No se pudo rastrear la procedencia del oficio verifica la consulta inicial.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                return; //salir del método si no hay un IdEjecucion válido
            }
            int idUser = 0;
            if (HttpContext.Current.Session["IdUsuario"] != null && int.TryParse(HttpContext.Current.Session["IdUsuario"].ToString(), out idUser))
            {
                // idUser tiene ahora el valor de la sesión
            }
            else
            {
                string mensajeNoDatos = "No se encontro un usuario logeado.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                return; // Salir del método si no hay un IdUser válido
            }
            string promovente = $"{inPromoventeNombre.Value} {inPromoventePaterno.Value} {inPromoventeMaterno.Value}";
            DateTime fechaIngreso = DateTime.Now;
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    //INSERTAR EN EJECUCION POSTERIOR
                    int idEjecucionPosterior = InsertarEnEjecucionPosterior(conn, transaction, idEjecucion, promovente, fechaIngreso, totalAnexos, idUser);

                    // Insertar anexos utilizando el idEjecucionPosterior obtenido
                    List<ListaAnexos> anexos = ViewState["anexos"] as List<ListaAnexos>;
                    if (anexos != null)
                    {
                        foreach (ListaAnexos anexo in anexos)
                        {
                            string nombreAnexo = anexo.NombreAnexo;
                            int cantidad = Convert.ToInt32(anexo.CantidadAnexo);
                            InsertarDatosAnexos(conn, transaction, idEjecucion, idEjecucionPosterior, nombreAnexo, cantidad);

                        }
                    }
                    transaction.Commit();
                    ScriptManager.RegisterStartupScript(
                       this.UpdatePanelPromociones,
                       this.UpdatePanelPromociones.GetType(),
                       "CerrarModalGuardarDatos2",
                       "CerrarModalGuardarDatos2();",
                       true
                   );
                    string mensajeExito = "¡Se guardaron los datos de la promocion exitoamente!.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastExito", $"mostrarToast('{mensajeExito}');", true);
                    DatosSello datosSello = new DatosSello
                    {
                        Juzgado = nombreJuzgado,
                        NumeroEjecucion = numeroEjecucion,
                        Folio = idEjecucionPosterior.ToString(),
                        Fecha = fechaIngreso,
                        AnexosDetalles = ObtenerAnexosDesdeViewState(),
                        TotalAnexos = totalAnexos
                    };
                    Limpiar();
                    tituloSello.Style.Add("display", "block");
                    string ticket = CrearTicketSELLO(datosSello);
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicket();", true);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    ScriptManager.RegisterStartupScript(
                        this.UpdatePanelPromociones,
                        this.UpdatePanelPromociones.GetType(),
                        "CerrarModalGuardarDatos2",
                        "CerrarModalGuardarDatos2();",
                        true
                    );
                    string mensajeError = "Ocurrió un error al procesar su solicitud.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastError", $"toastError('{mensajeError}');", true);
                    tituloSello.Style.Add("display", "none");
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }
        //CODIGO PARA SABER EL NOMBRE DEL JUZGADO POR SU ID
        private string ObtenerNombreJuzgadoPorID(string idJuzgado)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = $"SELECT Nombre FROM P_CatJuzgados WHERE IdJuzgado = {idJuzgado}";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            return null;
        }
        // CODIGO PARA SUMAR LOS ANEXOS
        private int CalcularTotalAnexos()
        {
            // Código para sumar las cantidades de los anexos de la tablaDatos
            int total = 0;
            // Suponiendo que tienes una lista de objetos Sala que representa los anexos
            List<ListaAnexos> anexos = ViewState["anexos"] as List<ListaAnexos>;
            if (anexos != null)
            {
                total = anexos.Sum(a => Convert.ToInt32(a.CantidadAnexo));
            }
            return total;
        }
        //CODIGO UNITARIO PARA LA INSERCION HACIA P_EJECUCIONPOSTERIOR
        private int InsertarEnEjecucionPosterior(SqlConnection conn, SqlTransaction transaction, int idEjecucion, string promovente, DateTime fechaIngreso, int anexos, int idUser)
        {
            string query = @"INSERT INTO [SIPOH].[dbo].[P_EjecucionPosterior] (IdEjecucion, Promovente, FechaIngreso, Anexos, IdUser)
                     VALUES (@IdEjecucion, @Promovente, @FechaIngreso, @Anexos, @IdUser);
                     SELECT CAST(scope_identity() AS int);"; // Modificado para devolver el ID generado
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                cmd.Parameters.AddWithValue("@Promovente", promovente);
                cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                cmd.Parameters.AddWithValue("@Anexos", anexos);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
                return (int)cmd.ExecuteScalar(); // Devuelve el ID generado
            }
        }
        //CODIGO PARA INSERTAR EN P_EJECUCIONANEXOS
        private void InsertarDatosAnexos(SqlConnection conn, SqlTransaction transaction, int idEjecucion, int idEjecucionPosterior, string nombreAnexo, int cantidad)
        {
            string query = @"
                INSERT INTO [SIPOH].[dbo].[P_EjecucionAnexos]
                (IdEjecucion, IdEjecucionPosterior, Detalle, Cantidad)
                VALUES
                (@IdEjecucion, @IdEjecucionPosterior, @Detalle, @Cantidad)";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                cmd.Parameters.AddWithValue("@IdEjecucionPosterior", idEjecucionPosterior);
                cmd.Parameters.AddWithValue("@Detalle", nombreAnexo);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.ExecuteNonQuery();
            }
        }
        //CODIGO INVISIBLE EL BOTON SI NO HAY PROMOVENTE
        private void VerificarCamposYDeshabilitarBoton()
        {
            var nombre = inPromoventeNombre.Value;
            var apellidoPaterno = inPromoventePaterno.Value;
            var apellidoMaterno = inPromoventeMaterno.Value;
            // Deshabilitar el botón si alguno de los campos está vacío
            btnGuardarDatosModal.Enabled = !string.IsNullOrWhiteSpace(nombre) &&
                                           !string.IsNullOrWhiteSpace(apellidoPaterno) &&
                                           !string.IsNullOrWhiteSpace(apellidoMaterno);
        }
        //CODIGO GENERAL DE LIMPIAR TODA LA PANTALLA DESACTUALIZADO HASTA RELACIONAR CAUSA
        private void Limpiar()
        {
            selectBusJuzgados.Items.Clear();
            inpuBusEjecucion.Value = "";
            GridViewPromociones.DataSource = null;
            GridViewPromociones.DataBind();
            inPromoventeNombre.Value = "";
            inPromoventePaterno.Value = "";
            inPromoventeMaterno.Value = "";
            CatAnexosDD.ClearSelection();
            OtroAnexo.Value = "";
            CantidadInput.Value = "";
            tablaDatos.DataSource = null;
            tablaDatos.DataBind();
            GridViewDetalles.DataSource = null;
            GridViewDetalles.DataBind();
            //INVISIBLE LOS DIVS
            tituloDetallesCausa.Visible = false;
            TablasAnexos.Visible = false;
            tituloTablaPromociones.Visible = false;
            tituloSello.Style["display"] = "block";
            BotonGuardarDiv.Style["display"] = "none";
            insertarPromoventeAnexos.Style["display"] = "none";
            primerRowPromocion.Style["display"] = "none";
            rowGridDetalles.Style["display"] = "none";
        }
        //---------------------------------------------------- INICIO SELLO --------------------------------------------------------
        //creo que este ObtenerNumeroEjecucionDesdeGridView no se usa checar bien no me creo jaja
        private string ObtenerNumeroEjecucionDesdeGridView()
        {
            if (GridViewPromociones.SelectedRow != null)
            {
                return GridViewPromociones.SelectedRow.Cells[1].Text;
            }
            else
            {
                return "";
            }
        }
        //CODIGO BOTON DE LIMPIAR PANTALLA
        protected void btnLimpiarPromocion_Click(object sender, EventArgs e)
        {
            tituloTablaPromociones.Visible = false;
            tituloDetallesCausa.Visible = false;
            GridViewPromociones.DataBind();
            GridViewPromociones.DataSource = null;
            GridViewDetalles.DataSource = null;
            GridViewDetalles.DataBind();
            insertarPromoventeAnexos.Style.Add("display", "none");
            divrelacionar.Style.Add("display", "none");
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        //CODIGO PARA LAS LISTAS DE ANEXOS
        public class AnexoPromocion
        {
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
        }

        private List<AnexoPromocion> ObtenerAnexosDesdeViewState()
        {
            List<ListaAnexos> salas = ViewState["anexos"] as List<ListaAnexos>;
            if (salas != null)
            {
                return salas.Select(s => new AnexoPromocion
                {
                    Descripcion = s.NombreAnexo,
                    Cantidad = Convert.ToInt32(s.CantidadAnexo)
                }).ToList();
            }
            return new List<AnexoPromocion>();
        }

        //CODIGO NUEVO DE OBTENER DATOS DEL SELLO
        public class DatosSello
        {
            public string Juzgado { get; set; }
            public string NumeroEjecucion { get; set; }
            public string Folio { get; set; }
            public DateTime Fecha { get; set; }
            public List<AnexoPromocion> AnexosDetalles { get; set; }

            public DatosSello()
            {
                AnexosDetalles = new List<AnexoPromocion>();
            }
            public int TotalAnexos { get; set; }
        }

        //fin de nueva obtencion de datos sello
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
        public string CrearTicketSELLO(DatosSello datos)
        {
            StringBuilder ticket = new StringBuilder();
            string[] lines = {
                "TRIBUNAL SUPERIOR DE JUSTICIA",
                "DEL ESTADO DE HIDALGO",
                "ATENCION CIUDADANA",
                "SENTENCIA EJECUTORIADA",
                "POSTERIOR"
            };
            int maxLength = lines.Max(line => line.Length);
            foreach (string line in lines)
            {
                int totalPadding = maxLength - line.Length;
                int padLeft = totalPadding / 2 + line.Length;
                string centeredLine = line.PadLeft(padLeft).PadRight(maxLength);
                ticket.AppendLine(centeredLine);
            }
            ticket.AppendLine("-----------------------------------");
            List<string> juzgadoLineas = DividirTextoEnLineas(datos.Juzgado, maxLength);
            foreach (string linea in juzgadoLineas)
            {
                ticket.AppendLine(linea.PadRight(maxLength));
            }
            ticket.AppendLine("-----------------------------------");
            ticket.AppendLine($"Numero de Ejecucion: {datos.NumeroEjecucion}");
            ticket.AppendLine($"Folio: {datos.Folio}");
            ticket.AppendLine($"Fecha: {datos.Fecha.ToString("dd/MM/yyyy")}");
            foreach (AnexoPromocion anexo in datos.AnexosDetalles)
            {
                ticket.AppendLine($"{anexo.Descripcion}: {anexo.Cantidad}");
            }

            ticket.AppendLine($"Total Anexos: {datos.TotalAnexos}");
            return ticket.ToString();
        }

        // fin 
    }
}