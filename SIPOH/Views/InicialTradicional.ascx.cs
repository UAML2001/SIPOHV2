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

                if (!IsPostBack)
                {
                    CheckSiTradicional.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
                    CheckNoTradicional.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
                    RegistroPartesInTradicional.Style["display"] = "none";
                }
            }
        }
        //FUNCION PARA CARGAR LOS DROPDOWN DE BUSQUEDA INICIAL DINAMICOS EL UNO DEL OTRO
        protected void LoadDistritos()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT IdDistrito, nombre FROM P_CatDistritos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        InDistritoTra.DataSource = reader;
                        InDistritoTra.DataTextField = "nombre";
                        InDistritoTra.DataValueField = "IdDistrito";
                        InDistritoTra.DataBind();
                    }
                }
                InDistritoTra.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
        }

        protected void InDistritoTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDistritoId = int.Parse(InDistritoTra.SelectedValue);
            LoadJuzgados(selectedDistritoId);
        }

        protected void LoadJuzgados(int distritoId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT IdJuzgado, Nombre FROM P_CatJuzgados WHERE IdDistrito = @DistritoId AND SubTipo = 'T' AND Tipo NOT IN('T')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DistritoId", distritoId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        InJuzgadoProcedenciaTra.DataSource = reader;
                        InJuzgadoProcedenciaTra.DataTextField = "Nombre";
                        InJuzgadoProcedenciaTra.DataValueField = "IdJuzgado";
                        InJuzgadoProcedenciaTra.DataBind();
                    }
                }
                InJuzgadoProcedenciaTra.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
        }
        //FUNCION PARA BUSCAR POR IDJUZGADO Y NUC Y MOSTRAR EN TABLA
        protected void btnBuscarTradicional_Click(object sender, EventArgs e)
        {
            int idJuzgado = Convert.ToInt32(InJuzgadoProcedenciaTra.SelectedValue);
            string numeroCausa = InCausaTra.Value.Trim();

            if (idJuzgado > 0 && !string.IsNullOrEmpty(numeroCausa))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT A.Numero AS NumeroCausa, J.Nombre AS NombreJuzgado, [OF].Nombre AS NombreOfendido, IC.Nombre AS NombreInculpado, CD.Nombre AS NombreDelito 
                             FROM P_Asunto AS A 
                             LEFT JOIN P_Causa AS C ON A.IdAsunto = C.IdAsunto 
                             LEFT JOIN P_CatJuzgados AS J ON A.IdJuzgado = J.IdJuzgado 
                             LEFT JOIN P_PartesAsunto AS [OF] ON A.IdAsunto = [OF].IdAsunto AND [OF].TipoParte = 'V' 
                             LEFT JOIN P_PartesAsunto AS IC ON A.IdAsunto = IC.IdAsunto AND IC.TipoParte = 'I' 
                             LEFT JOIN P_AsuntoDelito AS AD ON A.IdAsunto = AD.IdAsunto 
                             LEFT JOIN P_CatDelitos AS CD ON AD.IdDelito = CD.IdDelito 
                             WHERE A.IdJuzgado = @IdJuzgado AND A.Numero = @NumeroCausa";

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                        cmd.Parameters.AddWithValue("@NumeroCausa", numeroCausa);
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
                                htmlTable.Append("</tr>");
                                htmlTable.Append("</thead>");
                                htmlTable.Append("<tbody>");

                                while (dr.Read())
                                {
                                    htmlTable.Append("<tr>");
                                    htmlTable.Append($"<td>{dr["NumeroCausa"]}</td>");
                                    htmlTable.Append($"<td>{dr["NombreJuzgado"]}</td>");
                                    htmlTable.Append($"<td>{dr["NombreOfendido"]}</td>");
                                    htmlTable.Append($"<td>{dr["NombreInculpado"]}</td>");
                                    htmlTable.Append($"<td>{dr["NombreDelito"]}</td>");
                                    htmlTable.Append("</tr>");
                                }

                                htmlTable.Append("</tbody>");
                                htmlTable.Append("</table>");
                                tablaResultadosHtmlDivTradicional.InnerHtml = htmlTable.ToString();
                                string mensajeExito = $"Se encontró la causa {numeroCausa} en el juzgado {idJuzgado}.";
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
            string numeroTocaTradicional = inputNumeroTocaTradicional.Text;
            List<SalaTradicional> salasTradicional = ViewState["salasTradicional"] as List<SalaTradicional> ?? new List<SalaTradicional>();
            salasTradicional.Add(new SalaTradicional { NombreSala = salaTradicional, NumeroToca = numeroTocaTradicional });
            ViewState["salasTradicional"] = salasTradicional;
            tablaSalasTradicional.DataSource = salasTradicional;
            tablaSalasTradicional.DataBind();
            ActualizarVisibilidadTitulo();
        }

        protected void AgregarSentenciaATablaTradicional(object sender, EventArgs e)
        {
            string sentenciaTradicional = inputSentenciaTradicional.Text;
            List<string> sentenciasTradicional = (List<string>)ViewState["sentenciasTradicional"];
            sentenciasTradicional.Add(sentenciaTradicional);
            ViewState["sentenciasTradicional"] = sentenciasTradicional;
            tablaSentenciasTradicional.DataSource = sentenciasTradicional.Select(x => new { Sentencia = x }).ToList();
            tablaSentenciasTradicional.DataBind();
            ActualizarVisibilidadTitulo();
        }
        protected void BorrarSalaTradicional(object sender, GridViewDeleteEventArgs e)
        {
            List<SalaTradicional> salasTradicional = (List<SalaTradicional>)ViewState["salasTradicional"];
            salasTradicional.RemoveAt(e.RowIndex);
            ViewState["salasTradicional"] = salasTradicional;
            tablaSalasTradicional.DataSource = salasTradicional;
            tablaSalasTradicional.DataBind();
            //invisible el titulo de la sala
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
        //FIN DE FUNCIONES COPIADAS
  
        // FUNCIONES COPIADAS 2


        protected void BuscarPartesTradicional_Click(object sender, EventArgs e)
        {
            string nombre = InputNombreBusquedaTradicional.Value;
            string apPaterno = InputApPaternoBusquedaTradicional.Value;
            string apMaterno = inputApMaternoTradicional.Value;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            string query = @"SELECT P_Ejecucion.NoEjecucion, P_CatJuzgados.Nombre as Juzgado, 
                 P_Ejecucion.BeneficiarioNombre as Nombre, P_Ejecucion.BeneficiarioApellidoPaterno as ApPaterno, 
                 P_Ejecucion.BeneficiarioApellidoMaterno as ApMaterno, P_Ejecucion.FechaEjecucion as FechaEjecucion 
                 FROM P_Ejecucion 
                 INNER JOIN P_CatJuzgados ON P_Ejecucion.IdJuzgado = P_CatJuzgados.IdJuzgado 
                 WHERE SOUNDEX(BeneficiarioNombre) = SOUNDEX(@nombre) AND SOUNDEX(BeneficiarioApellidoPaterno) = SOUNDEX(@apPaterno) AND SOUNDEX(BeneficiarioApellidoMaterno) = SOUNDEX(@apMaterno)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apPaterno", apPaterno);
                    cmd.Parameters.AddWithValue("@apMaterno", apMaterno);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    GridView1Tradicional.DataSource = reader;
                    GridView1Tradicional.DataBind();
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
            if (anexo == "OTRO")
            {
                anexo = OtroAnexoTradicional.Value;
            }
            string cantidad = CantidadInputTradicional.Value;

            List<Sala> salasTradicional = ViewState["anexosTradicional"] as List<Sala> ?? new List<Sala>();
            salasTradicional.Add(new Sala { NombreSala = anexo, NumeroToca = cantidad });
            ViewState["anexosTradicional"] = salasTradicional;
            tablaDatosTradicional.DataSource = salasTradicional;
            tablaDatosTradicional.DataBind();
        }

        protected void BorrarFilaTradicional(object sender, GridViewDeleteEventArgs e)
        {
            List<Sala> salasTradicional = (List<Sala>)ViewState["anexosTradicional"];
            salasTradicional.RemoveAt(e.RowIndex);
            ViewState["anexosTradicional"] = salasTradicional;
            tablaDatosTradicional.DataSource = salasTradicional;
            tablaDatosTradicional.DataBind();
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
        //(Esta funcion de momento no se usa pienso que es para el insert)

    }
}
