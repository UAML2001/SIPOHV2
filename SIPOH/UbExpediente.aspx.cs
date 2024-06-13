using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class UbExpediente : System.Web.UI.Page
    {

        // Declara una variable global para almacenar el IdAsunto
        private int idAsunto;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string idJuzgado = Session["Idjuzgado"]?.ToString();
            if (string.IsNullOrEmpty(idJuzgado))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                    "toastr.error('El ID del juzgado no está disponible en la sesión.')", true);
                return;
            }

            string tipoAsunto = TAsunto.SelectedValue;
            if (string.IsNullOrEmpty(tipoAsunto) || tipoAsunto == "SO")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                    "toastr.error('El tipo de asunto es requerido.')", true);
                return;
            }

            string numeroExpediente = numexpe.Text;
            if (string.IsNullOrEmpty(numeroExpediente))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                    "toastr.error('El número de expediente es requerido.')", true);
                return;
            }

            var regex = new System.Text.RegularExpressions.Regex(@"^\d{4}/\d{4}$");
            if (!regex.IsMatch(numeroExpediente))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                    "toastr.error('El formato del número de expediente es inválido. Debe ser XXXX/XXXX.')", true);
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("P_GetPromocion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDJuzgado", SqlDbType.NVarChar).Value = idJuzgado;
                    cmd.Parameters.Add("@DataNumero", SqlDbType.NVarChar).Value = numeroExpediente;
                    cmd.Parameters.Add("@DataTipoAsunto", SqlDbType.NVarChar).Value = tipoAsunto;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblNumeroAsunto.Text = reader["TipoAsunto"].ToString() + " / " + reader["Numero"].ToString();
                                lblDelitos.Text = reader["Delitos"].ToString();
                                lblVictimas.Text = reader["Victimas"].ToString();
                                lblImputados.Text = reader["Inculpados"].ToString();
                                // Asignar el valor del IdAsunto a la variable global
                                idAsunto = Convert.ToInt32(reader["IdAsunto"]);


                                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                    "toastr.success('Se encontró el expediente.')", true);

                                // Mostrar el panel de expediente
                                PanelExpediente.Visible = true;
                                AddComentario.Visible = true;
                                BindGrid();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                    "toastr.error('No se encontraron resultados para los criterios de búsqueda proporcionados.')", true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                            $"toastr.error('Ocurrió un error al ejecutar la consulta: {ex.Message}')", true);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }

            // Ocultar el panel de búsqueda después de la búsqueda
            PanelBusqueda.Visible = false;
        }

        protected void BindGrid()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("TraerUbicacionExpediente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdAsunto", idAsunto);

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            ubiExpe.DataSource = dataTable;
                            ubiExpe.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                    }
                }
            }
        }


        protected void BuscarOtro_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void AddComentario_Click(object sender, EventArgs e)
        {
            PanelIngComent.Visible = true;
            ubiExpe.Visible = true;
        }

        protected void CancelarComment_Click(object sender, EventArgs e)
        {
            PanelIngComent.Visible = false;
        }
    }
}