using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using static SIPOH.Views.InicialAcusatorio;
using System.Collections;
using System.Configuration;
using System.Web;

namespace SIPOH.Views
{
    public partial class InicialTradicional : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDistritos();
                //COPIADOS
                CargarJuzgadosTradicional();
                CargarSalasTradicional();
                CargarSolicitantesTradicional();
                CargarSolicitudesTradicional();
                CargarAnexosTradicional();
                ViewState["salasTradicional"] = new List<string[]>();
                ViewState["sentenciasTradicional"] = new List<string>();
                ViewState["anexosTradicional"] = new List<Sala>();
                //INVISIBLE TITULO DE TABLAS
                tituloSalas.Visible = false;
                tituloSentencias.Visible = false;
                //DESHABILITAR INPUTS INICIALMENTE
                OtroAnexoTradicional.Disabled = true;
                InputOtraSolicitud.Disabled = true;
                //INICIALIZAR DIV EN INVIISBLE
                OcultarTradicional.Style["display"] = "none";
                RegistroPartesInTradicional.Style["display"] = "none";
                ContinuarRegistroTradicional.Style["display"] = "none";
                CheckSiTradicional.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
                CheckNoTradicional.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
                RegistroPartesInTradicional.Style["display"] = "none";
                TablasAnexos.Visible = false;
                tituloSello.Style["display"] = "none";

            }
        }
        //FUNCION PARA CARGAR LOS DROPDOWN DE BUSQUEDA INICIAL DINAMICOS EL UNO DEL OTRO
        protected void LoadDistritos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Distritos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // IdCircuito, como una variable de sesión
                    int idCircuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                    cmd.Parameters.AddWithValue("@IdCircuito", idCircuito);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        InDistritoTra.DataSource = reader;
                        InDistritoTra.DataTextField = "nombre";
                        InDistritoTra.DataValueField = "IdDistrito";
                        InDistritoTra.DataBind();
                    }
                }
            }
            InDistritoTra.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void InDistritoTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDistritoId = int.Parse(InDistritoTra.SelectedValue);
            LoadJuzgados(selectedDistritoId);
        }

        protected void LoadJuzgados(int distritoId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Juzgados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDistrito", distritoId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        InJuzgadoProcedenciaTra.DataSource = reader;
                        InJuzgadoProcedenciaTra.DataTextField = "Nombre";
                        InJuzgadoProcedenciaTra.DataValueField = "IdJuzgado";
                        InJuzgadoProcedenciaTra.DataBind();
                    }
                }
            }
            InJuzgadoProcedenciaTra.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }
        //FUNCION PARA BUSCAR POR IDJUZGADO Y NUC Y MOSTRAR EN TABLA
        public static class VariablesGlobales
        {
            public static int IdAsuntoGlobal { get; set; }
            //int idAsunto = VariablesGlobales.IdAsuntoGlobal; usarlo en otra funcion asi
        }
        protected void btnBuscarTradicional_Click(object sender, EventArgs e)
        {
            int idJuzgado = Convert.ToInt32(InJuzgadoProcedenciaTra.SelectedValue);
            string numeroCausa = InCausaTra.Value.Trim();
            Session["NumeroCausa"] = numeroCausa;

            if (idJuzgado > 0 && !string.IsNullOrEmpty(numeroCausa))
            {
                string nombreJuzgado = ObtenerNombreJuzgadoPorID(idJuzgado.ToString());
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string storedProcedure = "[dbo].[Ejecucion_ConsultarCausa]";


                    using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Asegúrate de establecer el tipo de comando
                        cmd.Parameters.AddWithValue("@Juzgado", idJuzgado); // Verifica que el nombre del parámetro coincide exactamente
                        cmd.Parameters.AddWithValue("@Numero", numeroCausa); // Verifica que el nombre del parámetro coincide exactamente
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                System.Text.StringBuilder htmlTable = new System.Text.StringBuilder();
                                htmlTable.Append("<table class='table table-sm table-striped table-hover mb-0'>");
                                htmlTable.Append("<thead>");
                                htmlTable.Append("<tr class='text-center bg-primary text-white'>");
                                htmlTable.Append("<th class='bg-success text-white'>Causa</th>");
                                htmlTable.Append("<th class='bg-success text-white'>N°Juzgado</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Ofendido(s)</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Inculpado(s)</th>");
                                htmlTable.Append("<th class='bg-success text-white'>Delito(s)</th>");
                                htmlTable.Append("</tr>");
                                htmlTable.Append("</thead>");
                                htmlTable.Append("<tbody>");
                                while (dr.Read())
                                {
                                    // Almacenar el IdAsunto en la variable global
                                    VariablesGlobales.IdAsuntoGlobal = Convert.ToInt32(dr["IdAsunto"]);
                                    htmlTable.Append("<tr>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NumeroCausa"]}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{nombreJuzgado}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NombreOfendido"]}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NombreInculpado"]}</td>");
                                    htmlTable.Append($"<td class='text-secondary'>{dr["NombreDelito"]}</td>");
                                    htmlTable.Append("</tr>");
                                }
                                htmlTable.Append("</tbody>");
                                htmlTable.Append("</table>");
                                tablaResultadosHtmlDivTradicional.InnerHtml = htmlTable.ToString();
                                string mensajeExito = $"Se encontró la causa {numeroCausa} en el juzgado {nombreJuzgado}.";
                                string scriptExito = $"<script type='text/javascript'>mostrarModalExito('{mensajeExito}');</script>";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MostrarModalExitoScript", scriptExito, false);
                                OcultarTradicional.Style["display"] = "block";

                            }
                            else
                            {
                                tablaResultadosHtmlDivTradicional.InnerHtml = "";
                                string mensajeError = "No se encontraron resultados para la búsqueda realizada.";
                                string scriptError = $"<script type='text/javascript'>mostrarModalError('{mensajeError}');</script>";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "MostrarModalErrorScript", scriptError, false);
                                OcultarTradicional.Style["display"] = "none";
                                RegistroPartesInTradicional.Style["display"] = "none";
                                ContinuarRegistroTradicional.Style["display"] = "none";
                            }
                        }
                    }
                }
            }
            else
            {
                string mensajeError = "Por favor, seleccione un juzgado y proporcione un número de causa válido.";
                string scriptInputError = $"<script type='text/javascript'>mostrarModalError('{mensajeError}');</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorInputScript", scriptInputError, false);
            }
        }
        //INICIO FUNCION COPIADAS
        private void CargarSalasTradicional()
        {
            string connectionStringTradicional = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conTradicional = new SqlConnection(connectionStringTradicional))
            {
                conTradicional.Open();
                string queryTradicional = "SELECT IdJuzgado, Nombre FROM P_CatJuzgados WHERE SubTipo = 'A' AND (IdJuzgado = 7 OR IdJuzgado = 232)";
                SqlCommand cmdTradicional = new SqlCommand(queryTradicional, conTradicional);
                using (SqlDataReader drTradicional = cmdTradicional.ExecuteReader())
                {
                    while (drTradicional.Read())
                    {
                        ListItem itemTradicional = new ListItem(drTradicional["Nombre"].ToString(), drTradicional["IdJuzgado"].ToString());
                        selectSalasTradicional.Items.Add(itemTradicional);
                    }
                }
            }
        }

        private void CargarJuzgadosTradicional()
        {
            string connectionStringTradicional = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conTradicional = new SqlConnection(connectionStringTradicional))
            {
                conTradicional.Open();
                string queryTradicional = "SELECT IdJuzgado, Nombre FROM P_CatJuzgados WHERE IdCircuito = 1 AND Tipo = 'P' AND SubTipo = 'A'";
                SqlCommand cmdTradicional = new SqlCommand(queryTradicional, conTradicional);
                using (SqlDataReader drTradicional = cmdTradicional.ExecuteReader())
                {
                    while (drTradicional.Read())
                    {
                        ListItem itemTradicional = new ListItem(drTradicional["Nombre"].ToString(), drTradicional["IdJuzgado"].ToString());
                        InJuzgadoProcedenciaTra.Items.Add(itemTradicional);
                    }
                }
            }
        }
        [Serializable]
        public class SalaTradicional
        {
            public string NombreSala { get; set; }
            public string NumeroToca { get; set; }
        }

        protected void AgregarSalaATablaTradicional(object sender, EventArgs e)
        {
            string salaTradicional = selectSalasTradicional.SelectedItem.Text;
            string valorSalaTradicional = selectSalasTradicional.SelectedItem.Value;
            string numeroTocaTradicional = inputNumeroTocaTradicional.Text;
            List<SalaTradicional> salasTradicional = ViewState["salasTradicional"] as List<SalaTradicional> ?? new List<SalaTradicional>();

            if (valorSalaTradicional.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase))
            {
                MostrarMensajeError("Debes seleccionar una sala válida");
                return;
            }

            if (!string.IsNullOrWhiteSpace(salaTradicional) && !string.IsNullOrWhiteSpace(numeroTocaTradicional))
            {
                if (!salasTradicional.Any(s => s.NombreSala == salaTradicional && s.NumeroToca == numeroTocaTradicional))
                {
                    salasTradicional.Add(new SalaTradicional { NombreSala = salaTradicional, NumeroToca = numeroTocaTradicional });
                    ViewState["salasTradicional"] = salasTradicional;
                    tablaSalasTradicional.DataSource = salasTradicional;
                    tablaSalasTradicional.DataBind();
                    ActualizarVisibilidadTitulo();
                }
                else
                {
                    MostrarMensajeError("No puedes guardar salas y tocas repetidas");
                }
            }
            else
            {
                MostrarMensajeError("No puedes dejar campos vacíos");
            }
        }

       


        protected void AgregarSentenciaATablaTradicional(object sender, EventArgs e)
        {
            string sentenciaTradicional = inputSentenciaTradicional.Text;
            List<string> sentenciasTradicional = ViewState["sentenciasTradicional"] as List<string> ?? new List<string>();
            if (!string.IsNullOrWhiteSpace(sentenciaTradicional))
            {
                if (!sentenciasTradicional.Contains(sentenciaTradicional))
                {
                    sentenciasTradicional.Add(sentenciaTradicional);
                    ViewState["sentenciasTradicional"] = sentenciasTradicional;
                    tablaSentenciasTradicional.DataSource = sentenciasTradicional.Select(x => new { Sentencia = x }).ToList();
                    tablaSentenciasTradicional.DataBind();
                    ActualizarVisibilidadTitulo();
                }
                else
                {
                    MostrarMensajeError("No puedes guardar sentencias iguales");
                }
            }
            else
            {
                MostrarMensajeError("No puedes dejar el campo de sentencia vacío");
            }
        }

        private void MostrarMensajeError(string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
        }

        protected void BorrarSalaTradicional(object sender, GridViewDeleteEventArgs e)
        {
            List<SalaTradicional> salasTradicional = (List<SalaTradicional>)ViewState["salasTradicional"];
            salasTradicional.RemoveAt(e.RowIndex);
            ViewState["salasTradicional"] = salasTradicional;
            tablaSalasTradicional.DataSource = salasTradicional;
            tablaSalasTradicional.DataBind();
            ActualizarVisibilidadTitulo();
        }

        protected void BorrarSentenciaTradicional(object sender, GridViewDeleteEventArgs e)
        {
            List<string> sentenciasTradicional = (List<string>)ViewState["sentenciasTradicional"];
            sentenciasTradicional.RemoveAt(e.RowIndex);
            ViewState["sentenciasTradicional"] = sentenciasTradicional;
            tablaSentenciasTradicional.DataSource = sentenciasTradicional.Select(x => new { Sentencia = x }).ToList();
            tablaSentenciasTradicional.DataBind();
            //Invisible el titulo de la tabla
            ActualizarVisibilidadTitulo();
        }
        protected void tablaSalasTradicional_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        protected void tablaSentenciasTradicional_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        protected void GridView1Tradicional_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "text-secondary";
                }
            }
        }
        // FUNCIONES COPIADAS 2
        protected void BuscarPartesTradicional_Click(object sender, EventArgs e)
        {
            string nombre = InputNombreBusquedaTradicional.Value;
            string apPaterno = InputApPaternoBusquedaTradicional.Value;
            string apMaterno = inputApMaternoTradicional.Value;

            int Circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
            int Opcion = (!string.IsNullOrWhiteSpace(nombre) &&
                         !string.IsNullOrWhiteSpace(apPaterno) &&
                         !string.IsNullOrWhiteSpace(apMaterno)) ? 2 : 1;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = "[dbo].[Ejecucion_ConsultarBeneficiario]";

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
                        GridView1Tradicional.DataSource = reader;
                        GridView1Tradicional.DataBind();
                        string mensaje = "Se encontraron resultados semejantes verifica si es el que necesitas antes de guardar nueva informacion.";
                        string script = $"toastWarning('{mensaje}');";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    }
                    else
                    {
                        GridView1Tradicional.DataSource = null;
                        GridView1Tradicional.DataBind();
                        string mensaje = "No se encontro registro, puedes guardar un nuevo dato pero verifica antes que sea correcto.";
                        string scriptToast = $"toastInfo('{mensaje}');";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastInfoScript", scriptToast, true);
                    }
                }
            }
        }
        private void CargarSolicitantesTradicional()
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
                        CatSolicitantesDDTradicional.Items.Add(listItem); // Asegúrate de que este ID está actualizado en tu front-end
                    }
                }
            }
        }

        private void CargarSolicitudesTradicional()
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
                        CatSolicitudDDTradicional.Items.Add(listItem); // Asegúrate de que este ID está actualizado en tu front-end
                    }
                }
            }
        }

        private void CargarAnexosTradicional()
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
                        CatAnexosDDTradicional.Items.Add(listItem); // Asegúrate de que este ID está actualizado en tu front-end
                    }
                }
            }
        }

        protected void AgregarATablaTradicional(object sender, EventArgs e)
        {
            string anexo = CatAnexosDDTradicional.SelectedItem.Text;
            string valorAnexo = CatAnexosDDTradicional.SelectedItem.Value;
            if (anexo == "OTRO")
            {
                anexo = OtroAnexoTradicional.Value;
            }
            string cantidad = CantidadInputTradicional.Value;
            List<Sala> salasTradicional = ViewState["anexosTradicional"] as List<Sala> ?? new List<Sala>();
            if (valorAnexo.Equals("Seleccionar", StringComparison.OrdinalIgnoreCase))
            {
                MostrarMensajeToast("Debes seleccionar una opción válida");
                return;
            }
            if (!string.IsNullOrWhiteSpace(anexo) && int.TryParse(cantidad, out int cantidadNumerica) && cantidadNumerica > 0)
            {
                var salaExistente = salasTradicional.FirstOrDefault(s => s.NombreSala.Equals(anexo, StringComparison.OrdinalIgnoreCase));
                if (salaExistente != null)
                {
                    salaExistente.NumeroToca = cantidad;
                }
                else
                {
                    salasTradicional.Add(new Sala { NombreSala = anexo, NumeroToca = cantidad });
                }

                ViewState["anexosTradicional"] = salasTradicional;
                tablaDatosTradicional.DataSource = salasTradicional;
                tablaDatosTradicional.DataBind();
                CatAnexosDDTradicional.ClearSelection();
                OtroAnexoTradicional.Value = "";
                OtroAnexoTradicional.Disabled = true;
                CantidadInputTradicional.Value = "";
                TablasAnexos.Visible = true;
            }
            else
            {
                MostrarMensajeToast("No puedes dejar campos vacíos y la cantidad debe ser mayor que cero");
            }
        }

        private void MostrarMensajeToast(string mensaje)
        {
            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
        }


        protected void BorrarFilaTradicional(object sender, GridViewDeleteEventArgs e)
        {

            List<Sala> salasTradicional = (List<Sala>)ViewState["anexosTradicional"];

            if (salasTradicional != null && salasTradicional.Count > e.RowIndex)
            {
                salasTradicional.RemoveAt(e.RowIndex);
                ViewState["anexosTradicional"] = salasTradicional;
                tablaDatosTradicional.DataSource = salasTradicional;
                tablaDatosTradicional.DataBind();
                if (salasTradicional.Count == 0)
                {
                    TablasAnexos.Visible = false;
                }
            }
            else
            {
                MostrarMensajeToast("No se puede eliminar la fila seleccionada");
            }
        }
        //FIN FUNCIONES COPIADAS 2

        //NUEVAS FUNCIONES VISIBLE E INVISIBLE | el titulo que esta arriba de las tablas| div de consulta de partes | el div de formularios registro partes
        private void ActualizarVisibilidadTitulo()
        {
            bool haySalas = tablaSalasTradicional.Rows.Count > 0;
            bool haySentencias = tablaSentenciasTradicional.Rows.Count > 0;
            tituloSalas.Visible = haySalas;
            tituloSentencias.Visible = haySentencias;
        }
        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSiTradicional.Checked)
            {
                RegistroPartesInTradicional.Style["display"] = "block";
            }
            else if (CheckNoTradicional.Checked)
            {
                RegistroPartesInTradicional.Style["display"] = "none";
            }
        }
        protected void btSiRegistro_Click(object sender, EventArgs e)
        {
            ContinuarRegistroTradicional.Style["display"] = "block";
        }
        protected void btNoRegistro_Click(object sender, EventArgs e)
        {
            ContinuarRegistroTradicional.Style["display"] = "none";
        }
        protected void CatAnexosDDTradicional_SelectedIndexChanged(object sender, EventArgs e)
        {
            OtroAnexoTradicional.Disabled = CatAnexosDDTradicional.SelectedValue != "OTRO";
        }

        protected void CatSolicitudDDTradicional_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputOtraSolicitud.Disabled = CatSolicitudDDTradicional.SelectedValue != "OTRO";
        }
        //FIN NUEVAS FUNCIONES
        //NO USADA
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
        //INICIO INSERT
        public static class GlobalesId
        {
            public static int IdEjecucion { get; set; }
        }
        public class Anexo
        {
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
        }

        public static class StorageFolio
        {
            public static (int idJuzgadoFolio, int folio) EjecutarEjecucionAsignarFolioXJuzgado(SqlTransaction transaction, int circuito)
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_AsignarFolioXJuzgado", transaction.Connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Circuito", circuito);
                    cmd.Parameters.Add("@juzgado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@folio", SqlDbType.Int).Direction = ParameterDirection.Output; // Nuevo parámetro de salida para folio

                    cmd.ExecuteNonQuery();

                    int idJuzgadoFolio = (int)cmd.Parameters["@juzgado"].Value;
                    int folio = (int)cmd.Parameters["@folio"].Value;  // Captura el valor del folio
                    Debug.WriteLine($"Folio obtenido: {folio}");
                    return (idJuzgadoFolio, folio);  // Devuelve ambos valores
                }
            }
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
                    GlobalAnexosDetalles.Clear();
                    int circuito = Convert.ToInt32(HttpContext.Current.Session["IdCircuito"]);
                    var (idJuzgadoFolio, folio) = StorageFolio.EjecutarEjecucionAsignarFolioXJuzgado(transaction, circuito);
                    string nombreJuzgado = ObtenerNombreJuzgadoPorID(idJuzgadoFolio.ToString());
                    InsertarDatosAcusatorio(conn, transaction, circuito);
                    int idAsunto = VariablesGlobales.IdAsuntoGlobal;
                    InsertarEnEjecucionAsunto(conn, transaction, GlobalesId.IdEjecucion, idAsunto);

                    List<SalaTradicional> salasTradicional = ViewState["salasTradicional"] as List<SalaTradicional>;

                    if (salasTradicional != null && salasTradicional.Count > 0)
                    {
                        foreach (var sala in salasTradicional)
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

                    List<string> sentencias = ViewState["sentenciasTradicional"] as List<string>;
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
                    List<Sala> anexos = ViewState["anexosTradicional"] as List<Sala>;
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
                                System.Diagnostics.Debug.WriteLine("Error en el else de anexo  "); // Manejar el caso en que el ID no se encuentre
                            }
                        }
                    }
                    int totalAnexos = GlobalAnexosDetalles
                    .Select(anexo => {
                        var partes = anexo.Split(new[] { " - " }, StringSplitOptions.None);
                        return partes.Length == 2 && int.TryParse(partes[1], out int cantidad) ? cantidad : 0;
                    })
                    .Sum();

                    ActualizarFolios(conn, transaction, idJuzgadoFolio);

                    transaction.Commit();

                    ScriptManager.RegisterStartupScript(
                        this.UpdateTradicional1,
                        this.UpdateTradicional1.GetType(),
                        "cerrarModal",
                        "CerrarModalGuardarDatos();",
                        true
                    );
                    //ticket aqui
                    string scriptToast = "mostrarToast();";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", scriptToast, true);
                    Limpiar();
                    LimpiarViewState();
                    string datosAnexos = String.Join(", ", GlobalAnexosDetalles.Select(a => $"\"{a}\""));
                    string ticket = CrearTicketSELLO();
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicket();", true);
                    tituloSello.Style["display"] = "block";

                }
                catch (Exception ex)
                {
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }
                    ScriptManager.RegisterStartupScript(
                        this.UpdateTradicional1,
                        this.UpdateTradicional1.GetType(),
                        "cerrarModal",
                        "CerrarModalGuardarDatos();",
                        true
                    );
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
                            System.Diagnostics.Debug.WriteLine("No se encontró un IdJuzgado para el nombre proporcionado: " + nombreJuzgado);
                            idJuzgado = -1; // O cualquier otro valor que indique una situación anómala
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void InsertarDatosAcusatorio(SqlConnection conn, SqlTransaction transaction, int circuito)
        {
            //GridViewRow primeraFila = GridView1.Rows[0];

            var (idJuzgado, folio) = StorageFolio.EjecutarEjecucionAsignarFolioXJuzgado(transaction, circuito);
            folio += 1;
            int añoActual = DateTime.Now.Year;
            string noEjecucion = $"{folio:0000}/{añoActual}";
            string fechaEjecucion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string nombreSolicitanteSeleccionado = CatSolicitantesDDTradicional.SelectedItem.Text;
            string nombreSolicitudSeleccionado = CatSolicitudDDTradicional.SelectedItem.Text;
            string cveSolicitante = ObtenerClavePorNombre(nombreSolicitanteSeleccionado, "P_EjecucionCatSolicitante", "Solicitante", "CveSolicitante");
            string cveSolicitud = ObtenerClavePorNombre(nombreSolicitudSeleccionado, "P_EjecucionCatSolicitud", "Solicitud", "CveSolicitud");
            if (string.IsNullOrWhiteSpace(detalleSolicitantes.Value))
            {
                string mensaje = "Favor de dar click al boton buscar";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return; // Detiene la ejecución si el campo está vacío
            }

            //string idusuario= (string)Session["IdUsuario"];
            string detalleSolicitante = detalleSolicitantes.Value;
            string otraSolicitud = InputOtraSolicitud.Value;
            string interno = siInterno.Checked ? "S" : "N"; // Asumiendo que solo hay dos opciones "S" o "N"
            string nombreBeneficiario = InputNombreBusquedaTradicional.Value;
            string apellidoPaternoBeneficiario = InputApPaternoBusquedaTradicional.Value;
            string apellidoMaternoBeneficiario = inputApMaternoTradicional.Value;
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
            //string nombreJuzgado = "JUZGADO PRIMERO DE EJECUCIÓN DEL SISTEMA PROCESAL PENAL ACUSATORIO ORAL";
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

                ProcesarDatosDeInsercion(idJuzgado, noEjecucion, idEjecucion, fechaEjecucion);
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

                ProcesarDetalleAnexo(nombreAnexo, cantidad);

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
        private void ActualizarFolios(SqlConnection conn, SqlTransaction transaction, int idJuzgado)
        {
            string query = @"
        UPDATE [SIPOH].[dbo].[P_Folios]
        SET Folio = Folio + 1, frecuencia = frecuencia + 1
        WHERE IdJuzgado = @IdJuzgado AND Tipo = 'EJ'";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                cmd.ExecuteNonQuery();
            }
        }

        //FIN INSERT
        //datos de modal y mas funciones recolctar datos
        protected void btnGuardarDatosModal_Click(object sender, EventArgs e)
        {
            RecolectarDatosParaModal();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "abrirModal", "abrirModalGuardarDatos();", true);
        }
        public void RecolectarDatosParaModal()
        {
            string fechaEjecucion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string nombreBeneficiario = InputNombreBusquedaTradicional.Value;
            string apellidoPaternoBeneficiario = InputApPaternoBusquedaTradicional.Value;
            string apellidoMaternoBeneficiario = inputApMaternoTradicional.Value;
            string nombreSolicitanteSeleccionado = CatSolicitantesDDTradicional.SelectedItem.Text;
            string nombreSolicitudSeleccionado = CatSolicitudDDTradicional.SelectedItem.Text;
            string detalleSolicitante = detalleSolicitantes.Value;
            string otraSolicitud = InputOtraSolicitud.Value;
            string interno = siInterno.Checked ? "S" : "N";
            string numeroCausa = Session["NumeroCausa"] as string;
            //aqui ando
            List<SalaTradicional> salasTradicional = ViewState["salasTradicional"] as List<SalaTradicional>;
            if (salasTradicional != null && salasTradicional.Count > 0)
            {
                var salasYTocas = salasTradicional.Select(s => $"{s.NombreSala} - {s.NumeroToca}");
                string todasLasSalasYTocas = string.Join(", ", salasYTocas);
                lblSalasYTocas.Text = todasLasSalasYTocas;
            }
            List<string> sentenciasTradicional = ViewState["sentenciasTradicional"] as List<string>;
            if (sentenciasTradicional != null && sentenciasTradicional.Count > 0)
            {
                string todasLasSentencias = string.Join(", ", sentenciasTradicional);
                lblSentencias.Text = todasLasSentencias;
            }
            List<Sala> anexosTradicional = ViewState["anexosTradicional"] as List<Sala>;
            if (anexosTradicional != null && anexosTradicional.Count > 0)
            {
                var anexosConCantidad = anexosTradicional.Select(a => $"{a.NombreSala} - {a.NumeroToca}");
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
            ltTituloModal.Text = $"¿Quieres guardar los siguientes datos en la causa - {numeroCausa} ?";
        }
        // recolectar datos fin
        //INICIO SELLO
        private void LimpiarViewState()
        {
            ViewState.Remove("anexos");
            ViewState.Remove("salas");
            ViewState.Remove("sentencias");
        }
        private void Limpiar()
        {
            InCausaTra.Value = "";
            InJuzgadoProcedenciaTra.ClearSelection();
            InDistritoTra.ClearSelection();
            tablaResultadosHtmlDivTradicional.InnerHtml = "";
            tablaSalasTradicional.DataSource = null;
            tablaSalasTradicional.DataBind();
            tablaSentenciasTradicional.DataSource = null;
            tablaSentenciasTradicional.DataBind();
            CheckSiTradicional.Checked = false;
            CheckNoTradicional.Checked = false;
            InputNombreBusquedaTradicional.Value = "";
            InputApPaternoBusquedaTradicional.Value = "";
            inputApMaternoTradicional.Value = "";
            GridView1Tradicional.DataSource = null;
            GridView1Tradicional.DataBind();
            siInterno.Checked = false;
            noInterno.Checked = false;
            CatSolicitantesDDTradicional.ClearSelection();
            detalleSolicitantes.Value = "";
            CatSolicitudDDTradicional.ClearSelection();
            InputOtraSolicitud.Value = "";
            CatAnexosDDTradicional.ClearSelection();
            OtroAnexoTradicional.Value = "";
            CantidadInputTradicional.Value = "";
            tablaDatosTradicional.DataSource = null;
            tablaDatosTradicional.DataBind();
            selectSalasTradicional.ClearSelection();
            inputNumeroTocaTradicional.Text = "";
            inputSentenciaTradicional.Text = "";
            //INVISIBLE LOS DIVS
            tituloSalas.Visible = false;
            tituloSentencias.Visible = false;
            tituloSello.Style["display"] = "block";
            OcultarTradicional.Style["display"] = "none";
            RegistroPartesInTradicional.Style["display"] = "none";
            ContinuarRegistroTradicional.Style["display"] = "none";
            primerRowTradicional.Style["display"] = "none";
        }
        public static string GlobalNoEjecucion;
        public static string GlobalFechaEjecucion;
        public static string GlobalIdJuzgado;
        public static List<string> GlobalAnexosDetalles = new List<string>();

        private void ProcesarDatosDeInsercion(int idJuzgado, string noEjecucion, int idEjecucion, string fechaEjecucion)
        {
            GlobalIdJuzgado = idJuzgado.ToString();
            GlobalNoEjecucion = noEjecucion;
            GlobalFechaEjecucion = fechaEjecucion;
            // Nota: idEjecucion se almacena en GlobalesId.IdEjecucion AQUI ANDO
        }


        private void ProcesarDetalleAnexo(string nombreAnexo, int cantidad)
        {
            string detalle = $"{nombreAnexo} - {cantidad}";
            GlobalAnexosDetalles.Add(detalle);
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

        public string CrearTicketSELLO()
        {
            StringBuilder ticket = new StringBuilder();
            string nombreJuzgado = ObtenerNombreJuzgadoPorID(GlobalIdJuzgado);
            List<string> lineasNombreJuzgado = DividirTextoEnLineas(nombreJuzgado, 30);
            List<Anexo> anexos = GlobalAnexosDetalles.Select(a => new Anexo
            {
                Descripcion = a.Split('-')[0].Trim(),
                Cantidad = int.Parse(a.Split('-')[1].Trim())
            }).ToList();
            int totalAnexos = anexos.Sum(a => a.Cantidad);

            string[] lines = {
                "TRIBUNAL SUPERIOR DE JUSTICIA",
                "DEL ESTADO DE HIDALGO",
                "ATENCION CIUDADANA",
                "SENTENCIA EJECUTORIADA",
                "INICIAL"
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
            foreach (string linea in lineasNombreJuzgado)
            {
                ticket.AppendLine(linea);
            }
            ticket.AppendLine("-----------------------------------");
            ticket.AppendLine($"Numero de Ejecucion: {GlobalNoEjecucion}");
            ticket.AppendLine($"Folio: {GlobalesId.IdEjecucion}");
            ticket.AppendLine($"Fecha: {GlobalFechaEjecucion}");
            foreach (var anexo in anexos)
            {
                ticket.AppendLine($"{anexo.Descripcion}: {anexo.Cantidad}");
            }
            ticket.AppendLine($"Total Anexos: {totalAnexos}");
            return ticket.ToString();
        }

        //FIN SELLO

    }
}
