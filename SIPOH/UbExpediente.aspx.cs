using SIPOH.ExpedienteDigital.Victimas.CSVictimas;
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
        int idAsunto;

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
                                BindGrid();

                                // Ocultar el panel de búsqueda después de encontrar el expediente
                                PanelBusqueda.Visible = false;

                                Session["IdAsunto"] = idAsunto;

                                // Verificar si hay comentarios registrados
                                VerificarComentariosRegistrados(idAsunto);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                    "toastr.error('No se encontraron resultados para los criterios de búsqueda proporcionados.')", true);

                                // Mostrar el panel de búsqueda si no se encuentra el expediente
                                PanelBusqueda.Visible = true;
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
        }

        protected void VerificarComentariosRegistrados(int idAsunto)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            int currentUserId = Convert.ToInt32(Session["IdUsuario"]?.ToString()); // Obtener el IdUsuario del usuario logueado

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT Comentario, IdUserPresta, FePrestamo
                         FROM P_Ubicacion
                         WHERE IdAsunto = @IdAsunto";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = idAsunto;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string comentario = reader["Comentario"].ToString();
                                int idUserPresta = Convert.ToInt32(reader["IdUserPresta"]);
                                string fePrestamo = reader["FePrestamo"].ToString();

                                if (!string.IsNullOrEmpty(comentario) || idUserPresta != 0 || !string.IsNullOrEmpty(fePrestamo))
                                {
                                    lblComent.Text = comentario;
                                    string nomUser = IdUsuarioPorSesion.ObtenerNombreCompletoUsuario(idUserPresta);
                                    lblUsuario.Text = nomUser;
                                    lblFecha.Text = fePrestamo;

                                    // Mostrar el panel de comentario ingresado
                                    //PanelComentIng.Visible = true;
                                    PanelComentIng.Style["display"] = "block";

                                    // Verificar si el usuario logueado es el mismo que ingresó el comentario
                                    if (idUserPresta == currentUserId)
                                    {
                                        ModComent.Visible = true;
                                        DelComent.Visible = true;
                                    }
                                    else
                                    {
                                        ModComent.Visible = false;
                                        DelComent.Visible = false;
                                    }
                                }
                                else
                                {
                                    // Ocultar el panel de comentario ingresado si no hay datos
                                    //PanelComentIng.Visible = false;
                                    PanelComentIng.Style["display"] = "none";
                                }
                            }
                            else
                            {
                                // Ocultar el panel de comentario ingresado si no hay registros
                                //PanelComentIng.Visible = false;
                                PanelComentIng.Style["display"] = "none";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                            $"toastr.error('Ocurrió un error al verificar los comentarios registrados: {ex.Message}')", true);
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
            // Verificar si ya existen registros con valores en Comentario, IdUserPresta, y FePrestamo
            if (HayRegistrosExistentes())
            {
                // Mostrar mensaje de error si ya existen registros
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                    "toastr.error('Ya existe un comentario registrado. No se puede agregar otro comentario.');", true);
                // Agregar este código para mantener visible el GridView
                ubiExpe.DataBind();
            }
            else
            {
                // Mostrar el panel para ingresar un nuevo comentario si no existen registros
                //PanelIngComent.Visible = true;
                PanelIngComent.Style["display"] = "block";
                // Agregar este código para mantener visible el GridView
                ubiExpe.DataBind();
            }
        }

        protected bool HayRegistrosExistentes()
        {
            bool existenRegistros = false;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = @"SELECT COUNT(*) 
                     FROM P_Ubicacion 
                     WHERE Comentario IS NOT NULL 
                     AND IdUserPresta IS NOT NULL 
                     AND FePrestamo IS NOT NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            existenRegistros = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                            $"toastr.error('Ocurrió un error al verificar los registros existentes: {ex.Message}')", true);
                    }
                }
            }

            return existenRegistros;
        }

        protected void GuardarComentario_Click(object sender, EventArgs e)
        {
            // Implementar la lógica para guardar el nuevo comentario
            // ...
        }

        protected void CancelarComentario_Click(object sender, EventArgs e)
        {
            // Ocultar el panel de agregar comentario sin realizar cambios
            //PanelIngComent.Visible = false;
            PanelIngComent.Style["display"] = "none";
        }


        protected void CancComent_Click(object sender, EventArgs e)
        {
            //PanelIngComent.Visible = false;
            PanelIngComent.Style["display"] = "none";
        }


        // Agregacion del comentario

        protected void SvComent_Click(object sender, EventArgs e)
        {
            // Obtener el comentario ingresado
            string comentario = IngresaComentario.InnerHtml;
            // Obtener el IdUserPresta desde la sesión
            int idUser = IdUsuarioPorSesion.ObtenerIdUsuario();
            // Obtiene el Nombre del Usuario
            string nomUser = IdUsuarioPorSesion.ObtenerNombreCompletoUsuario(idUser);
            // Obtener la fecha y hora actual
            DateTime fePrestamo = DateTime.Now;
            // Obtener el IdAsunto desde la variable de sesión
            int idAsunto = Convert.ToInt32(Session["IdAsunto"]);

            // Actualizar los controles en el panel con los datos ingresados
            lblComent.Text = "'"+comentario+"'"; // Comentario
            lblFecha.Text = fePrestamo.ToString(); // Fecha del comentario
            lblUsuario.Text = nomUser.ToString(); // Usuario que comentó

            // Mostrar el panel de comentarios ingresados
            //PanelComentIng.Visible = true;
            PanelComentIng.Style["display"] = "block";

            // Realizar la actualización en la base de datos
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = @"UPDATE P_Ubicacion
                    SET Comentario = @Comentario,
                        IdUserPresta = @IdUserPresta,
                        FePrestamo = @FePrestamo
                    WHERE IdAsunto = @IdAsunto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Comentario", comentario);
                    command.Parameters.AddWithValue("@IdUserPresta", idUser);
                    command.Parameters.AddWithValue("@FePrestamo", fePrestamo);
                    command.Parameters.AddWithValue("@IdAsunto", idAsunto);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            //PanelIngComent.Visible = false;
                            PanelIngComent.Style["display"] = "none";
                            // La actualización fue exitosa
                            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                "toastr.success('Comentario agregado correctamente.')", true);
                        }
                        else
                        {
                            // No se encontraron registros para actualizar
                            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                "toastr.error('No se pudo agregar el comentario')", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                            $"toastr.error('Ocurrió un error al agregar el comentario: {ex.Message}')", true);
                    }
                }
            }
        }

        // Eliminacion del comentario

        protected void DelComent_Click(object sender, EventArgs e)
        {
            // Obtener el IdAsunto desde la variable de sesión
            int idAsunto = Convert.ToInt32(Session["IdAsunto"]);

            // Realizar la actualización en la base de datos para establecer los valores como nulos
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = @"UPDATE P_Ubicacion
                    SET Comentario = NULL,
                        IdUserPresta = NULL,
                        FePrestamo = NULL
                    WHERE IdAsunto = @IdAsunto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsunto", idAsunto);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            //PanelComentIng.Visible = false;
                            PanelComentIng.Style["display"] = "none";
                            //PanelIngComent.Visible = false;
                            PanelIngComent.Style["display"] = "none";
                            // La eliminación fue exitosa
                            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                "toastr.success('Se eliminó el comentario correctamente.')", true);
                        }
                        else
                        {
                            // No se encontraron registros para actualizar
                            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                "toastr.error('No se encontraron registros para eliminar en la tabla P_Ubicacion.')", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                            $"toastr.error('Ocurrió un error al eliminar el comentario en la tabla P_Ubicacion: {ex.Message}')", true);
                    }
                }
            }
        }

        // Modificación del comentario

        protected void ModComent_Click(object sender, EventArgs e)
        {
            // Mostrar el panel para modificar el comentario
            //ModificarComentario.Visible = true;
            ModificarComentario.Style["display"] = "block";


            // Llenar el textarea con el comentario actual para facilitar la edición
            ComentModificado.Text = lblComent.Text;
        }

        protected void CancelarModificacion_Click1(object sender, EventArgs e)
        {
            // Ocultar el panel de modificación sin realizar cambios
            //ModificarComentario.Visible = false;
            ModificarComentario.Style["display"] = "none";
        }

        protected void GuardarModificacion_Click(object sender, EventArgs e)
        {
            // Obtener el IdAsunto desde la variable de sesión
            int idAsunto = Convert.ToInt32(Session["IdAsunto"]);

            // Obtener el nuevo comentario ingresado
            string nuevoComentario = ComentModificado.Text;

            // Obtener el IdUserPresta desde la sesión
            int idUser = IdUsuarioPorSesion.ObtenerIdUsuario();

            // Obtiene el Nombre del Usuario
            string nomUser = IdUsuarioPorSesion.ObtenerNombreCompletoUsuario(idUser);

            // Realizar la actualización en la base de datos
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            string query = @"UPDATE P_Ubicacion
                    SET Comentario = @NuevoComentario,
                        IdUserPresta = @IdUserPresta,
                        FePrestamo = @FePrestamo
                    WHERE IdAsunto = @IdAsunto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NuevoComentario", nuevoComentario);
                    command.Parameters.AddWithValue("@IdUserPresta", idUser);
                    command.Parameters.AddWithValue("@FePrestamo", DateTime.Now);
                    command.Parameters.AddWithValue("@IdAsunto", idAsunto);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // La modificación fue exitosa, actualizar los labels y ocultar el textarea
                            lblComent.Text = "'"+nuevoComentario+"'";
                            lblFecha.Text = DateTime.Now.ToString(); // Formato general para fecha y hora
                            lblUsuario.Text = nomUser.ToString(); // Asume que el nombre del usuario está en la sesión
                            //ModificarComentario.Visible = false;
                            ModificarComentario.Style["display"] = "none";

                            // Mostrar mensaje de éxito
                            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                "toastr.success('Se modificó el comentario correctamente.')", true);
                        }
                        else
                        {
                            // No se encontraron registros para actualizar
                            ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                                "toastr.error('No se encontraron registros para modificar en la tabla P_Ubicacion.')", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrMessage",
                            $"toastr.error('Ocurrió un error al modificar el comentario en la tabla P_Ubicacion: {ex.Message}')", true);
                    }
                }
            }
        }

    }
}