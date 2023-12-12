using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Description;
using System.Text.RegularExpressions;
using System.Web;

namespace SIPOH.Views
{
    public partial class InicialAcusatorio : System.Web.UI.UserControl
    {
        protected System.Web.UI.HtmlControls.HtmlGenericControl tablaResultadosHtmlDiv; // Cambiar el nombre del control
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarJuzgados();
                CargarSalas();
                ViewState["salas"] = new List<string[]>();
                ViewState["sentencias"] = new List<string>();
                CargarSolicitantes();
                CargarSolicitudes();
                CargarAnexos();
                tituloSalas.Visible = false;
                tituloSentencias.Visible = false;
                DivExAm.Style["display"] = "none";
                ContinuarRegistro.Style["display"] = "none";
                RegistroPartesIn.Style["display"] = "none";
                OtroAnexo.Disabled = true;
                InputOtraSolicitud.Disabled = true;

            }
        }
        private void CargarSalas()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT IdJuzgado, Nombre FROM P_CatJuzgados WHERE SubTipo = 'A' AND (IdJuzgado = 7 OR IdJuzgado = 232)";
                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListItem item = new ListItem(dr["Nombre"].ToString(), dr["IdJuzgado"].ToString());
                        selectSalas.Items.Add(item);
                    }
                }
            }
        }
        private void CargarJuzgados()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT IdJuzgado, Nombre FROM P_CatJuzgados WHERE IdCircuito = 1 AND Tipo = 'P' AND SubTipo = 'A'";
                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListItem item = new ListItem(dr["Nombre"].ToString(), dr["IdJuzgado"].ToString());
                        inputRadicacion.Items.Add(item);
                    }
                }
            }
        }
        [Serializable]
        public class Sala
        {
            public string NombreSala { get; set; }
            public string NumeroToca { get; set; }
        }
        protected void AgregarSalaATabla(object sender, EventArgs e)
        {
            string sala = selectSalas.SelectedItem.Text;
            string valorSala = selectSalas.SelectedItem.Value; // Obtiene el valor del item seleccionado
            string numeroToca = inputNumeroToca.Text;
            List<Sala> salas = ViewState["salas"] as List<Sala> ?? new List<Sala>();

            // Verificar si se seleccionó la opción "Seleccionar"
            if (valorSala.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase))
            {
                // Manejar el caso de opción "Seleccionar" seleccionada
                string mensaje = "Debes seleccionar una sala válida";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return; // Finaliza la ejecución de la función
            }

            // Verificar si los campos no están vacíos
            if (!string.IsNullOrWhiteSpace(sala) && !string.IsNullOrWhiteSpace(numeroToca))
            {
                // Verificar si la sala ya existe
                if (!salas.Any(s => s.NombreSala == sala && s.NumeroToca == numeroToca))
                {
                    salas.Add(new Sala { NombreSala = sala, NumeroToca = numeroToca });
                    ViewState["salas"] = salas;
                    tablaSalas.DataSource = salas;
                    tablaSalas.DataBind();
                    ActualizarVisibilidadTitulo();
                }
                else
                {
                    // Manejar el caso de sala duplicada
                    string mensaje = "No puedes guardar salas y tocas repetidas";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                }
            }
            else
            {
                // Manejar el caso de campos vacíos
                string mensaje = "No puedes dejar campos vacíos";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
        }


        protected void AgregarSentenciaATabla(object sender, EventArgs e)
        {
            string sentencia = inputSentencia.Text;
            List<string> sentencias = (List<string>)ViewState["sentencias"] ?? new List<string>();

            // Verificar si el campo no está vacío
            if (!string.IsNullOrWhiteSpace(sentencia))
            {
                // Verificar si la sentencia ya existe
                if (!sentencias.Contains(sentencia))
                {
                    sentencias.Add(sentencia);
                    ViewState["sentencias"] = sentencias;
                    tablaSentencias.DataSource = sentencias.Select(x => new { Sentencia = x }).ToList();
                    tablaSentencias.DataBind();
                    ActualizarVisibilidadTitulo();
                }
                else
                {
                    // Manejar el caso de sentencia duplicada
                    string mensaje = "No puedes guardar sentencias iguales";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                }
            }
            else
            {
                // Manejar el caso de campo vacío
                string mensaje = "No puedes dejar el campo de sentencia vacío";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
        }

        protected void BorrarSala(object sender, GridViewDeleteEventArgs e)
        {
            List<Sala> salas = (List<Sala>)ViewState["salas"];
            salas.RemoveAt(e.RowIndex);
            ViewState["salas"] = salas;
            tablaSalas.DataSource = salas;
            tablaSalas.DataBind();
            ActualizarVisibilidadTitulo();
        }
        protected void BorrarSentencia(object sender, GridViewDeleteEventArgs e)
        {
            List<string> sentencias = (List<string>)ViewState["sentencias"];
            sentencias.RemoveAt(e.RowIndex);
            ViewState["sentencias"] = sentencias;
            tablaSentencias.DataSource = sentencias.Select(x => new { Sentencia = x }).ToList();
            tablaSentencias.DataBind();
            ActualizarVisibilidadTitulo();
        }
        protected void tablaSalas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        protected void tablaSentencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string juzgadoSeleccionado = inputRadicacion.Value;
            string tipoBusqueda = inputIncomJuzgado.Value;
            string numeroCausaNuc = inputNuc.Value;
            Session["NumeroCausaNuc"] = numeroCausaNuc;
            // Verificar si se seleccionó un juzgado válido
            if (juzgadoSeleccionado == "Seleccionar" || string.IsNullOrEmpty(juzgadoSeleccionado))
            {
                string mensaje = "Por favor seleccione un juzgado, no puede estar vacio";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return; // Detener la ejecución adicional del método
            }
            lblResultado.Text = "";
            // Verificar si se proporcionó un número de causa
            if (string.IsNullOrEmpty(numeroCausaNuc))
            {
                string mensaje = "Por favor seleccione un numero de causa valido";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return; // Detener la ejecución adicional del método
            }

            string nombreJuzgado = ObtenerNombreJuzgadoPorID(juzgadoSeleccionado);
            if (!string.IsNullOrEmpty(nombreJuzgado))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = $"[dbo].[Ejecucion_ConsultarCausa]";

                    Debug.WriteLine($"Consulta SQL: {query}");
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Juzgado", System.Data.SqlDbType.Int).Value = juzgadoSeleccionado;
                        cmd.Parameters.Add("@Numero", System.Data.SqlDbType.VarChar).Value = numeroCausaNuc;


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                System.Text.StringBuilder htmlTable = new System.Text.StringBuilder();
                                htmlTable.Append("<table class='table table-sm table-striped table-hover mb-0'>");
                                htmlTable.Append("<thead>");
                                htmlTable.Append("<tr class='text-center bg-primary text-white'>");
                                htmlTable.Append("<th class='bg-success text-white'>Causa|Nuc</th>");
                                htmlTable.Append("<th class='bg-success text-white'>N°Juzgado</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Ofendido(s)</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Inculpado(s)</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Delito(s)</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Quitar</th>");
                                htmlTable.Append("</tr>");
                                htmlTable.Append("</thead>");
                                htmlTable.Append("<tbody>");
                                while (dr.Read())
                                {
                                    int idAsunto = Convert.ToInt32(dr["IdAsunto"]);
                                    Session["IdAsunto"] = idAsunto; 
                                    htmlTable.Append("<tr>");
                                    if (tipoBusqueda == "2")
                                    {
                                        htmlTable.Append($"<td class='text-dark'>{dr["NUC"]}</td>");
                                    }
                                    else
                                    {
                                        htmlTable.Append($"<td class='text-dark'>{dr["NumeroCausa"]}</td>");
                                    }
                                    htmlTable.Append($"<td class='text-secondary'>{nombreJuzgado}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NombreOfendido"]}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NombreInculpado"]}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NombreDelito"]}</td>");
                                    htmlTable.Append("<td class='text-secondary'><button class='btn btn-sm eliminar-button btn-danger' onclick='eliminarFila(this)'><i class=\"bi bi-trash\"></i></button></td>");
                                    htmlTable.Append("</tr>");
                                }
                                htmlTable.Append("</tbody>");
                                htmlTable.Append("</table>");
                                tablaResultadosHtmlDiv.InnerHtml = htmlTable.ToString();
                                // La consulta tiene resultados, muestra el modal de éxito
                                string mensajeExito = $"Se encontró la CAUSA | NUC {numeroCausaNuc} en el juzgado {nombreJuzgado}.";
                                string scriptExito = $"<script type='text/javascript'>mostrarAlerta('{mensajeExito}');</script>";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarAlertaScript", scriptExito, false);
                                DivExAm.Style["display"] = "block";
                            }
                            else
                            {
                                tablaResultadosHtmlDiv.InnerHtml = "";
                                string mensajeError = "No se encontró la CAUSA | NUC en el JUZGADO elegido.";
                                string scriptError = $"<script type='text/javascript'>mostrarError('{mensajeError}');</script>";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarErrorScript", scriptError, false);
                                DivExAm.Style["display"] = "none";
                                ContinuarRegistro.Style["display"] = "none";
                                RegistroPartesIn.Style["display"] = "none";
                            }
                        }
                    }
                }
            }
            else
            {
                lblResultado.Text = "El juzgado seleccionado no es válido.";
            }
        }


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
 
        protected void BuscarPartes_Click(object sender, EventArgs e)
        {
            string nombre = InputNombreBusqueda.Value;
            string apPaterno = InputApPaternoBusqueda.Value;
            string apMaterno = inputApMaterno.Value;
            int Circuito= 1;
            int Opcion = (!string.IsNullOrWhiteSpace(nombre) &&
                 !string.IsNullOrWhiteSpace(apPaterno) &&
                 !string.IsNullOrWhiteSpace(apMaterno)) ? 2 : 1;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = $"[dbo].[Ejecucion_ConsultarBeneficiario]";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = nombre;
                    cmd.Parameters.Add("@apellidoPaterno", System.Data.SqlDbType.VarChar).Value = apPaterno;
                    cmd.Parameters.Add("@apellidoMaterno", System.Data.SqlDbType.VarChar).Value = apMaterno;
                    cmd.Parameters.Add("@idCircuito", System.Data.SqlDbType.Int).Value = Circuito;
                    cmd.Parameters.Add("@opcion", System.Data.SqlDbType.Int).Value = Opcion;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        GridView1.DataSource = reader;
                        GridView1.DataBind();
                        string mensaje = "Se encontraron resultados semejantes verifica si es el que necesitas antes de guardar nueva informacion.";
                        string scriptToast = $"toastInfo('{mensaje}');";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastInfoScript", scriptToast, true);
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        string mensaje = "No se encontro registro, puedes guardar un nuevo dato pero verifica antes que sea correcto.";
                        string script = $"toastWarning('{mensaje}');";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    }
                }
            }
        }

        private void CargarSolicitantes()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Solicitante FROM P_EjecucionCatSolicitante ORDER BY CASE WHEN Solicitante = 'OTRO' THEN 1 ELSE 0 END, Solicitante", con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Solicitante"].ToString();
                        listItem.Value = dr["Solicitante"].ToString();
                        CatSolicitantesDD.Items.Add(listItem);
                    }
                }
            }
        }
        private void CargarSolicitudes()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Solicitud FROM P_EjecucionCatSolicitud ORDER BY CASE WHEN Solicitud = 'OTRO' THEN 1 ELSE 0 END, Solicitud", con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Solicitud"].ToString();
                        listItem.Value = dr["Solicitud"].ToString();
                        CatSolicitudDD.Items.Add(listItem);
                    }
                }
            }
        }
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
        protected void AgregarATabla(object sender, EventArgs e)
        {
            string anexo = CatAnexosDD.SelectedItem.Text;
            if (anexo == "OTRO")
            {
                anexo = OtroAnexo.Value;
            }
            string cantidad = CantidadInput.Value;

            List<Sala> salas = ViewState["anexos"] as List<Sala> ?? new List<Sala>();
            salas.Add(new Sala { NombreSala = anexo, NumeroToca = cantidad });
            ViewState["anexos"] = salas;
            tablaDatos.DataSource = salas;
            tablaDatos.DataBind();
        }
        protected void BorrarFila(object sender, GridViewDeleteEventArgs e)
        {
            List<Sala> salas = (List<Sala>)ViewState["anexos"];
            salas.RemoveAt(e.RowIndex);
            ViewState["anexos"] = salas;
            tablaDatos.DataSource = salas;
            tablaDatos.DataBind();
        }
        protected void tablaDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        //AQUI EMPIEZA EL INSERT
        public static class GlobalesId
        {
            public static int IdEjecucion { get; set; }
        }
        public class Anexo
        {
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
        }
        protected void btnGuardarAcusatorio_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                 
                    InsertarDatosAcusatorio(conn, transaction);
                    int idAsunto = Convert.ToInt32(Session["IdAsunto"]);
                    InsertarEnEjecucionAsunto(conn, transaction, GlobalesId.IdEjecucion, idAsunto);

                    List<Sala> salas = ViewState["salas"] as List<Sala>;
                    if (salas != null)
                    {
                        foreach (var sala in salas)
                        {
                            string nombreSala = sala.NombreSala;
                            string numeroToca = sala.NumeroToca;
                            if (!string.IsNullOrEmpty(nombreSala) && !string.IsNullOrEmpty(numeroToca))
                            {
                                int idJuzgado = ObtenerIdJuzgadoPorNombre(nombreSala);
                                InsertarDatosEjecucionOriToca(conn, transaction, numeroToca, idJuzgado);
                            }
                        }
                    }
                    List<string> sentencias = ViewState["sentencias"] as List<string>;
                    if (sentencias != null)
                    {
                        foreach (var sentencia in sentencias)
                        {
                            if (!string.IsNullOrEmpty(sentencia))
                            {
                                InsertarDatosEjecucionOriAmpa(conn, transaction, sentencia);
                            }
                        }
                    }
                    List<Sala> anexos = ViewState["anexos"] as List<Sala>;
                    if (anexos != null && anexos.Count > 0)
                    {
                        foreach (var anexo in anexos)
                        {
                            string descripcionAnexo = anexo.NombreSala;
                            int cantidad = Convert.ToInt32(anexo.NumeroToca);
                            int idCatAnexoEjecucion = ObtenerIdCatAnexoPorDescripcion(descripcionAnexo);

                            if (idCatAnexoEjecucion != 0) // Asegúrate de que el ID es válido
                            {
                                InsertarDatosAnexos(conn, transaction, descripcionAnexo, cantidad);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Error en el else de insertaranexos  "); // Manejar el caso en que el ID no se encuentre
                            }
                        }
                    }
                    transaction.Commit();
                    ScriptManager.RegisterStartupScript(
                        this.UpdateAcusatorio,
                        this.UpdateAcusatorio.GetType(),
                        "cerrarModal",
                        "CerrarModalGuardarDatos();",
                        true
                    );
                    string scriptToast = "mostrarToast();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", scriptToast, true);

                    // Recargar la página después de un tiempo determinado (por ejemplo, 5 segundos)
                    string scriptRecarga = "setTimeout(function(){ window.location.href = window.location.href; }, 5000);";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "recargaPaginaScript", scriptRecarga, true);
                }
                catch (Exception ex)
                {
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }
                    ScriptManager.RegisterStartupScript(
                        this.UpdateAcusatorio,
                        this.UpdateAcusatorio.GetType(),
                        "cerrarModal",
                        "CerrarModalGuardarDatos();",
                        true
                    );

                    Debug.WriteLine("NO JALO LA WEA DE JOSUE POR: " + HttpContext.Current.Session["IdUsuario"]);
                    string mensaje = "Falto algun dato que es necesario para guardar revisa por favor";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }
        private string ObtenerClavePorNombre(string nombre, string tablaCatalogo, string columnaNombre, string columnaClave)
            {
                string clave = "";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                string query = $"SELECT {columnaClave} FROM [SIPOH].[dbo].[{tablaCatalogo}] WHERE {columnaNombre} = @Nombre";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            clave = result.ToString();
                        }
                        conn.Close();
                    }
                }
                return clave;
            }
        private int ObtenerIdJuzgadoPorNombre(string nombreJuzgado)
        {
            int idJuzgado = 0;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = "SELECT IdJuzgado FROM [SIPOH].[dbo].[P_CatJuzgados] WHERE Nombre = @NombreJuzgado";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NombreJuzgado", nombreJuzgado);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            idJuzgado = Convert.ToInt32(result);
                        }
                        else
                        {
                            // No se encontró el juzgado
                            System.Diagnostics.Debug.WriteLine("No se encontró un IdJuzgado para el nombre proporcionado: " + nombreJuzgado);
                            idJuzgado = -1; // O cualquier otro valor que indique una situación anómala
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y registrar el error
                System.Diagnostics.Debug.WriteLine("Error al obtener IdJuzgado: " + ex.Message);
                // Decide si quieres retornar un valor específico en caso de excepción
                idJuzgado = -1; // O cualquier otro valor que indique error
            }

            return idJuzgado;
        }

        private void InsertarEnEjecucionAsunto(SqlConnection conn, SqlTransaction transaction, int idEjecucion, int idAsunto)
        {
            string query = @"INSERT INTO [SIPOH].[dbo].[P_EjecucionAsunto] (IdEjecucion, IdAsunto)
                     VALUES (@IdEjecucion, @IdAsunto)";
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // La inserción falló, mostrar un mensaje de error
                    string mensaje = "Error al insertar en la tabla P_EjecucionAsunto.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastErrorScript", script, true);

                    // Puedes decidir si lanzar una excepción aquí para manejar el rollback en el bloque try-catch
                    throw new Exception("Error al insertar en P_EjecucionAsunto");
                }
            }
        }

        private void InsertarDatosAcusatorio(SqlConnection conn, SqlTransaction transaction)
        {
            //GridViewRow primeraFila = GridView1.Rows[0];
            string noEjecucion = "0000/0000";
            string fechaEjecucion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string nombreSolicitanteSeleccionado = CatSolicitantesDD.SelectedItem.Text;
            string nombreSolicitudSeleccionado = CatSolicitudDD.SelectedItem.Text;
            string cveSolicitante = ObtenerClavePorNombre(nombreSolicitanteSeleccionado, "P_EjecucionCatSolicitante", "Solicitante", "CveSolicitante");
            string cveSolicitud = ObtenerClavePorNombre(nombreSolicitudSeleccionado, "P_EjecucionCatSolicitud", "Solicitud", "CveSolicitud");
            if (string.IsNullOrWhiteSpace(detalleSolicitantes.Value))
            {
                string mensaje = "Falto ingresar el nombre del solicitante";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return; // Detiene la ejecución si el campo está vacío
            }

            //string idusuario= (string)Session["IdUsuario"];
            string detalleSolicitante = detalleSolicitantes.Value;
            string otraSolicitud = InputOtraSolicitud.Value;
            string interno = siInterno.Checked ? "S" : "N"; // Asumiendo que solo hay dos opciones "S" o "N"
            string nombreBeneficiario = InputNombreBusqueda.Value;
            string apellidoPaternoBeneficiario = InputApPaternoBusqueda.Value;
            string apellidoMaternoBeneficiario = inputApMaterno.Value;
            if (string.IsNullOrWhiteSpace(nombreBeneficiario) ||
            string.IsNullOrWhiteSpace(apellidoPaternoBeneficiario))
            {
                string mensaje = "El nombre y apellido paterno son obligatorios, llenalos de nuevo y vuelve a generar la consulta de busqueda";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return; // Salir del método si alguno de los campos obligatorios está vacío
            }
            apellidoMaternoBeneficiario = string.IsNullOrWhiteSpace(apellidoMaternoBeneficiario) ? "-" : apellidoMaternoBeneficiario;
            //string nombreJuzgadoHtml = primeraFila.Cells[1].Text;
            string nombreJuzgado = "JUZGADO PRIMERO DE EJECUCIÓN DEL SISTEMA PROCESAL PENAL ACUSATORIO ORAL";
            int idJuzgado = ObtenerIdJuzgadoPorNombre(nombreJuzgado);
            string query = @"INSERT INTO [SIPOH].[dbo].[P_Ejecucion]
                     ([NoEjecucion], [FechaEjecucion], [CveSolicitante], [DetalleSolicitante], [CveSolicitud], [OtroSolicita], [BeneficiarioNombre], [BeneficiarioApellidoPaterno], [BeneficiarioApellidoMaterno], [IdJuzgado], [Interno], [IdUser], [Tipo])
                     VALUES
                     (@NoEjecucion, @FechaEjecucion, @CveSolicitante, @DetalleSolicitante, @CveSolicitud, @OtroSolicita, @BeneficiarioNombre, @BeneficiarioApellidoPaterno,
                      @BeneficiarioApellidoMaterno, @IdJuzgado, @Interno, @IdUser, 'Ejecución');
                     SELECT CAST(scope_identity() AS int);";
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@NoEjecucion", noEjecucion);
                cmd.Parameters.AddWithValue("@FechaEjecucion", Convert.ToDateTime(fechaEjecucion));
                cmd.Parameters.AddWithValue("@CveSolicitante", cveSolicitante);
                cmd.Parameters.AddWithValue("@DetalleSolicitante", detalleSolicitante);
                cmd.Parameters.AddWithValue("@CveSolicitud", cveSolicitud);
                cmd.Parameters.AddWithValue("@OtroSolicita", string.IsNullOrEmpty(otraSolicitud) ? (object)DBNull.Value : otraSolicitud);
                cmd.Parameters.AddWithValue("@IdUser", HttpContext.Current.Session["IdUsuario"]);
                cmd.Parameters.AddWithValue("@BeneficiarioNombre", nombreBeneficiario);
                cmd.Parameters.AddWithValue("@BeneficiarioApellidoPaterno", apellidoPaternoBeneficiario);
                cmd.Parameters.AddWithValue("@BeneficiarioApellidoMaterno", apellidoMaternoBeneficiario);
                cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                cmd.Parameters.AddWithValue("@Interno", interno);
                int idEjecucion = (int)cmd.ExecuteScalar();
                GlobalesId.IdEjecucion = idEjecucion;
            }
        }
        private int ObtenerIdCatAnexoPorDescripcion(string descripcionAnexo)
        {
            if (descripcionAnexo.Equals("OTRO", StringComparison.OrdinalIgnoreCase))
            {
                return 8; // ID para "OTRO"
            }

            int idCatAnexo = 0;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = "SELECT IdCatAnexoEjecucion FROM [SIPOH].[dbo].[P_EjecucionCatAnexos] WHERE Descripcion = @DescripcionAnexo";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DescripcionAnexo", descripcionAnexo);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idCatAnexo = Convert.ToInt32(result);
                    }
                    else
                    {
                        return 8; // Retorna el ID para "OTRO" si no se encuentra la descripción
                    }
                }
            }
            return idCatAnexo;
        }
        private void InsertarDatosAnexos(SqlConnection conn, SqlTransaction transaction, string nombreAnexo, int cantidad)
        {
            string query = @"
                INSERT INTO [SIPOH].[dbo].[P_EjecucionAnexos]
                (IdEjecucion, Detalle, Cantidad)
                VALUES
                (@IdEjecucion, @Detalle, @Cantidad)";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                int idEjecucion = GlobalesId.IdEjecucion;
                cmd.Parameters.AddWithValue("@IdEjecucion", idEjecucion);
                cmd.Parameters.AddWithValue("@Detalle", nombreAnexo);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertarDatosEjecucionOriToca(SqlConnection conn, SqlTransaction transaction, string numeroDeToca, int idJuzgado)
        {
            string query = @"INSERT INTO [SIPOH].[dbo].[P_EjecucionOriToca] ([IdEjecucion], [NumeroDeToca], [IdJuzgado])
                     VALUES (@IdEjecucion, @NumeroDeToca, @IdJuzgado)";
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdEjecucion", GlobalesId.IdEjecucion);
                cmd.Parameters.AddWithValue("@NumeroDeToca", numeroDeToca);
                cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                cmd.ExecuteNonQuery();
            }
        }
        private void InsertarDatosEjecucionOriAmpa(SqlConnection conn, SqlTransaction transaction, string amparo)
        { 
            string query = @"INSERT INTO [SIPOH].[dbo].[P_EjecucionOriAmpa] ([IdEjecucion], [Amparo])
                     VALUES (@IdEjecucion, @Amparo)";
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdEjecucion", GlobalesId.IdEjecucion);
                cmd.Parameters.AddWithValue("@Amparo", amparo);
                cmd.ExecuteNonQuery();
            }
        }
        // HASTA AQUI EL INSERT
        //Nuevas funcionalidades
        private void ActualizarVisibilidadTitulo()
        {
            bool haySalas = tablaSalas.Rows.Count > 0;
            bool haySentencias = tablaSentencias.Rows.Count > 0;
            tituloSalas.Visible = haySalas;
            tituloSentencias.Visible = haySentencias;
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSi.Checked)
            {
                RegistroPartesIn.Style["display"] = "block";
            }
            else if (CheckNo.Checked)
            {
                RegistroPartesIn.Style["display"] = "none";
            }
        }
        protected void btSiRegistro2_Click(object sender, EventArgs e)
        {
            ContinuarRegistro.Style["display"] = "block";
        }
        protected void btNoRegistro2_Click(object sender, EventArgs e)
        {
            ContinuarRegistro.Style["display"] = "none";
        }
        protected void CatAnexosDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            OtroAnexo.Disabled = CatAnexosDD.SelectedValue != "OTRO";
        }
        protected void CatSolicitudDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputOtraSolicitud.Disabled = CatSolicitudDD.SelectedValue != "OTRO";
        }
        //fin nuevas funcionalidades
        //FUNCION QUE GUARDA DATOS QUE INSERTARA PARA LUEGO MOSTRAR EN EL INSERT
        public void RecolectarDatosParaModal()
        {
            //if (GridView1.Rows.Count == 0)
            //{
            //    // Si no hay filas, muestra la alerta de error y retorna de la función
            //    string mensaje = "No Se podra guardar nada porque no has generado la busqueda del sentenciado / beneficiario";
            //    string script = $"toastError('{mensaje}');";
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            //    return; // Importante para detener la ejecución de la función aquí
            //}
            //GridViewRow primeraFila = GridView1.Rows[0];

            string fechaEjecucion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string nombreBeneficiario = InputNombreBusqueda.Value;
            string apellidoPaternoBeneficiario = InputApPaternoBusqueda.Value;
            string apellidoMaternoBeneficiario = inputApMaterno.Value;
            string nombreSolicitanteSeleccionado = CatSolicitantesDD.SelectedItem.Text;
            string nombreSolicitudSeleccionado = CatSolicitudDD.SelectedItem.Text;
            string detalleSolicitante = detalleSolicitantes.Value;
            string otraSolicitud = InputOtraSolicitud.Value;
            string interno = siInterno.Checked ? "S" : "N";
            string numeroCausaNuc = Session["NumeroCausaNuc"] as string;

            List<Sala> salas = ViewState["salas"] as List<Sala>;
            if (salas != null && salas.Count > 0)
            {
                var salasYTocas = salas.Select(s => $"{s.NombreSala} - {s.NumeroToca}");
                string todasLasSalasYTocas = string.Join(", ", salasYTocas);
                lblSalasYTocas.Text = todasLasSalasYTocas; // Asegúrate de tener una etiqueta lblSalasYTocas en tu interfaz de usuario
            }
            List<string> sentencias = ViewState["sentencias"] as List<string>;
            if (sentencias != null && sentencias.Count > 0)
            {
                string todasLasSentencias = string.Join(", ", sentencias);
                lblSentencias.Text = todasLasSentencias;
            }
            List<Sala> anexos = ViewState["anexos"] as List<Sala>;
            if (anexos != null && anexos.Count > 0)
            {
                var anexosConCantidad = anexos.Select(a => $"{a.NombreSala} - {a.NumeroToca}");
                string todosLosAnexos = string.Join(", ", anexosConCantidad);
                lblAnexos.Text = todosLosAnexos;
            }
            lblFechaEjecucion.Text = fechaEjecucion;
            lblnombreBeneficiario.Text = nombreBeneficiario;
            lblapellidoPaternoBeneficiario.Text = apellidoPaternoBeneficiario;
            lblapellidoMaternoBeneficiario.Text = apellidoMaternoBeneficiario;
            lblnombreSolicitanteSeleccionado.Text = nombreSolicitanteSeleccionado;
            lblnombreSolicitudSeleccionado.Text = nombreSolicitudSeleccionado;
            lbldetalleSolicitante.Text = detalleSolicitante;
            lblotraSolicitud.Text = otraSolicitud;
            lblinterno.Text = interno;
            ltTituloModal.Text = $"¿Quieres guardar los siguientes datos en la causa | nuc - {numeroCausaNuc} ?";
        }
        protected void btnGuardarDatosModal_Click(object sender, EventArgs e)
        {
            RecolectarDatosParaModal();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "abrirModalGuardarDatos();", true);
        }
        //FIN de funcion que guarda los datos para luego mostrar en el modal
    }
}