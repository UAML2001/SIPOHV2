using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using static SIPOH.Views.InicialAcusatorio;

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
            bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/promociones"));

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
                TablasAnexos.Visible = false;
                insertarPromoventeAnexos.Style.Add("display", "none");
                BotonGuardarDiv.Style.Add("display", "none");
                CargarJuzgados();
                CargarAnexos();
                OtroAnexo.Disabled = true;
                TablasAnexos.Visible = false;
                VerificarCamposYDeshabilitarBoton();
                ScriptManager.RegisterStartupScript(this, GetType(), "verificarCampos", "verificarCampos();", true);
            }
        }
        //
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
        protected void btnBuscarPromocion_Click(object sender, EventArgs e)
        {
            string ejecucion = inpuBusEjecucion.Value; //inputNucBusqueda
            string idJuzgado = selectBusJuzgados.Value; //InputDistritoProcedencia

            if (idJuzgado == "Seleccionar") // Reemplaza "ValorPorDefecto" con el valor real que representa la selección por defecto
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

            // Mostrar los títulos si hay datos
            if (dt.Rows.Count > 0)
            {
                tituloTablaPromociones.Visible = true;
                GridViewPromociones.DataSource = dt;
                GridViewPromociones.DataBind();
                tituloDetallesCausa.Visible = false;
                detallesCausa.InnerHtml = "";
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

        protected void btnLimpiarPromocion_Click(object sender, EventArgs e)
        {
            tituloTablaPromociones.Visible = false;
            tituloDetallesCausa.Visible = false;
            GridViewPromociones.DataSource = null;
            GridViewPromociones.DataBind();
            detallesCausa.InnerHtml = "";
            insertarPromoventeAnexos.Style.Add("display", "none");
            string mensaje = "Se ha limpiado la busqueda y sus campos, puedes buscar de nuevo si lo deseas";
            string script = $"toastWarning('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrartoastWarning", script, true);
        }
        protected void VerDetalles(int idAsunto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            StringBuilder htmlTable = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_ModuloConsultasDetalle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        htmlTable.Append("<table class='table table-sm table-striped table-hover mb-0'>");
                        htmlTable.Append("<thead>");
                        htmlTable.Append("<tr class='text-center bg-primary text-white'>");
                        htmlTable.Append("<th class='bg-success text-white'>Causa</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Juzgado</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Ofendidos</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Inculpados</th>");
                        htmlTable.Append("<th class='bg-success text-white'>Delitos</th>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</thead>");
                        htmlTable.Append("<tbody>");

                        if (dr.HasRows)
                        {
                            tituloTablaPromociones.Visible = true;
                            tituloDetallesCausa.Visible = true;
                            while (dr.Read())
                            {
                                htmlTable.Append("<tr>");
                                htmlTable.Append($"<td class='text-dark'>{dr["Numero"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Juzgado"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Ofendidos"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Inculpados"]}</td>");
                                htmlTable.Append($"<td class='text-secondary'>{dr["Delitos"]}</td>");
                                htmlTable.Append("</tr>");
                            }
                        }
                        else
                        {
                            htmlTable.Append("<tr><td colspan='2'>No se encontraron detalles.</td></tr>");
                        }

                        htmlTable.Append("</tbody>");
                        htmlTable.Append("</table>");
                    }
                }
            }

            detallesCausa.InnerHtml = htmlTable.ToString();
            insertarPromoventeAnexos.Style.Add("display", "block");
        }
        protected void GridViewPromociones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int idAsunto = Convert.ToInt32(e.CommandArgument);
                VerDetalles(idAsunto);
            }
        }
        //anexos
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
        protected void CatAnexosDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            OtroAnexo.Disabled = CatAnexosDD.SelectedValue != "OTRO";
        }
        protected void AgregarATabla(object sender, EventArgs e)
        {
            string anexo = CatAnexosDD.SelectedItem.Text;
            string valorAnexo = CatAnexosDD.SelectedItem.Value; // Obtiene el valor del item seleccionado

            if (anexo == "OTRO")
            {
                anexo = OtroAnexo.Value;
            }
            string cantidad = CantidadInput.Value;

            List<Sala> salas = ViewState["anexos"] as List<Sala> ?? new List<Sala>();

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
                var salaExistente = salas.FirstOrDefault(s => s.NombreSala.Equals(anexo, StringComparison.OrdinalIgnoreCase));
                if (salaExistente != null)
                {
                    // Actualizar la cantidad del anexo existente
                    salaExistente.NumeroToca = cantidad;
                }
                else
                {
                    // Agregar un nuevo anexo
                    salas.Add(new Sala { NombreSala = anexo, NumeroToca = cantidad });
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
                // Manejar el caso de campos vacíos o cantidad inválida (cero o no numérica)
                MostrarMensajeToast("No puedes dejar campos vacíos y la cantidad debe ser mayor que cero");
                TablasAnexos.Visible = false;
            }
        }


        private void MostrarMensajeToast(string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
        }

        protected void BorrarFila(object sender, GridViewDeleteEventArgs e)
        {
            List<Sala> salas = (List<Sala>)ViewState["anexos"];
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

        //guardar datos
        protected void btnModalPromociones_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "abrirModalGuardarDatos2();", true);
        }

        //AQUI EMPIEZA EL INSERT
        protected void GridViewPromociones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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

        protected void btnGuardarPromocion_Click(object sender, EventArgs e)
        {
            int idEjecucion = 0; // Valor predeterminado, por ejemplo, 0

            if (ViewState["IdEjecucionSeleccionado"] != null)
            {
                idEjecucion = (int)ViewState["IdEjecucionSeleccionado"];
                // Continuar con el resto del código
            }
            else
            {
                string mensajeNoDatos = "No se pudo rastrear la procedencia del oficio verifica la consulta inicial.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastNoDatos", $"toastError('{mensajeNoDatos}');", true);
                return; // Asegúrate de salir del método si no hay un IdEjecucion válido
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
            int totalAnexos = CalcularTotalAnexos();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int idEjecucionPosterior = InsertarEnEjecucionPosterior(conn, transaction, idEjecucion, promovente, fechaIngreso, totalAnexos, idUser);

                    // Insertar anexos utilizando el idEjecucionPosterior obtenido
                    List<Sala> anexos = ViewState["anexos"] as List<Sala>;
                    if (anexos != null)
                    {
                        foreach (Sala anexo in anexos)
                        {
                            string nombreAnexo = anexo.NombreSala;
                            int cantidad = Convert.ToInt32(anexo.NumeroToca);
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
                    string scriptRecarga = "setTimeout(function(){ window.location.href = window.location.href; }, 5000);";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "recargaPaginaScript", scriptRecarga, true);
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
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private int CalcularTotalAnexos()
        {
            // Código para sumar las cantidades de los anexos de la tablaDatos
            int total = 0;
            // Suponiendo que tienes una lista de objetos Sala que representa los anexos
            List<Sala> anexos = ViewState["anexos"] as List<Sala>;
            if (anexos != null)
            {
                total = anexos.Sum(a => Convert.ToInt32(a.NumeroToca));
            }
            return total;
        }

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


        //
    }
}