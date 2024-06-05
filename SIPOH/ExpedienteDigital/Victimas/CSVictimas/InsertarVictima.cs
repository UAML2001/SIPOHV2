using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public class InsertarVictima
{
    public void InsertVictimData(int idAsunto, string apPaterno, string apMaterno, string nombre, string genero, string tipoParte, int tipoVictima,
        string victima, string rfc, string curp, string edad, DateTime feNacimiento, int idContNacido, int idPaisNacido, int idEstadoNacido,
        string idMunicipioNacido, int idNacionalidad, int idCondicion, int idEstadoCivil, int idGradoEstudios, int idAlfabet, int idiomaEspañol,
        int idVulnerabilidad, int idPueblo, int hablaIndigena, int idDialecto, int idOcupacion, int idProfesion, string domOcupacion,
        int discapacidad, int idContiResidencia, int idPaisResidencia, int idEstadoResidencia, string idMunicipioResidencia, string domResidencia,
        int idDefensor, int interprete, int ordenProteccion, DateTime feIndividualizacion, int idDocIdentificador, string numDocumento, string privacidad,
        string telefono, string correo, string fax, string domNotificacion, string otroTipo, int idUser, List<string> idsDiscapacidades, Page page)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                int idPartes;

                // Verificar si la tabla P_PartesAsunto está vacía
                string checkPartesAsuntoQuery = @"SELECT COUNT(*) FROM [SIPOH].[dbo].[P_PartesAsunto]";
                using (SqlCommand cmdCheck = new SqlCommand(checkPartesAsuntoQuery, conn, transaction))
                {
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        // Si la tabla está vacía, restablecer el ID
                        string resetIdQuery = @"DBCC CHECKIDENT ('[SIPOH].[dbo].[P_PartesAsunto]', RESEED, 0)";
                        using (SqlCommand cmdReset = new SqlCommand(resetIdQuery, conn, transaction))
                        {
                            cmdReset.ExecuteNonQuery();
                        }
                    }
                }

                // Insertar los datos en P_PartesAsunto
                string queryPartesAsunto = @"
            INSERT INTO [SIPOH].[dbo].[P_PartesAsunto] 
            ([IdAsunto], [Nombre], [APaterno], [AMaterno], [Genero], [TipoParte], [Alias]) 
            VALUES 
            (@IdAsunto, @Nombre, @APaterno, @AMaterno, @Genero, @TipoParte, @Alias); 
            SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(queryPartesAsunto, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@APaterno", apPaterno);
                    cmd.Parameters.AddWithValue("@AMaterno", apMaterno);
                    cmd.Parameters.AddWithValue("@Genero", genero);
                    cmd.Parameters.AddWithValue("@TipoParte", tipoParte);
                    cmd.Parameters.AddWithValue("@Alias", "");

                    idPartes = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Verificar si la tabla P_Victima está vacía
                string checkVictimaQuery = @"SELECT COUNT(*) FROM [SIPOH].[dbo].[P_Victima]";
                using (SqlCommand cmdCheck = new SqlCommand(checkVictimaQuery, conn, transaction))
                {
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        // Si la tabla está vacía, restablecer el ID
                        string resetIdQuery = @"DBCC CHECKIDENT ('[SIPOH].[dbo].[P_Victima]', RESEED, 0)";
                        using (SqlCommand cmdReset = new SqlCommand(resetIdQuery, conn, transaction))
                        {
                            cmdReset.ExecuteNonQuery();
                        }
                    }
                }

                // Insertar los datos en la tabla P_Victima
                string queryVictima = @"
            INSERT INTO [SIPOH].[dbo].[P_Victima] 
            ([IdVictima], [TipoVictima], [Victima], [RFC], [CURP], [Edad], [FeNacimiento], [IdContinenteNacido], [IdPaisNacido], 
            [IdEstadoNacido], [IdMunicipioNacido], [IdNacionalidad], [IdCondicion], [IdEstadoCivil], 
            [IdGradoEstudios], [IdAlfabet], [IdiomaEspañol], [IdVulnerabilidad], [IdPueblo], [HablaIndigena], 
            [IdDialecto], [IdOcupacion], [IdProfesion], [DomOcupacion], [Discapacidad], [IdContinenteResidencia], [IdPaisResidencia], 
            [IdEstadoResidencia], [IdMunicipioResidencia], [DomResidencia], [IdDefensor], [Interprete], 
            [OrdenProteccion], [FeIndividualización], [IdDocIdentificador], [NumDocumento], [Privacidad], 
            [Telefono], [Correo], [Fax], [DomNotificacion], [OtroTipo], [IdUser])
            VALUES
            (@IdVictima, @TipoVictima, @Victima, @RFC, @CURP, @Edad, @FeNacimiento, @IdContinenteNacido, @IdPaisNacido, 
            @IdEstadoNacido, @IdMunicipioNacido, @IdNacionalidad, @IdCondicion, @IdEstadoCivil, 
            @IdGradoEstudios, @IdAlfabet, @IdiomaEspañol, @IdVulnerabilidad, @IdPueblo, @HablaIndigena, 
            @IdDialecto, @IdOcupacion, @IdProfesion, @DomOcupacion, @Discapacidad, @IdContinenteResidencia, @IdPaisResidencia, 
            @IdEstadoResidencia, @IdMunicipioResidencia, @DomResidencia, @IdDefensor, @Interprete, 
            @OrdenProteccion, @FeIndividualización, @IdDocIdentificador, @NumDocumento, @Privacidad, 
            @Telefono, @Correo, @Fax, @DomNotificacion, @OtroTipo, @IdUser);";

                using (SqlCommand cmd = new SqlCommand(queryVictima, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdVictima", idPartes);
                    cmd.Parameters.AddWithValue("@TipoVictima", tipoVictima);
                    cmd.Parameters.AddWithValue("@Victima", victima);
                    cmd.Parameters.AddWithValue("@RFC", rfc);
                    cmd.Parameters.AddWithValue("@CURP", curp);
                    cmd.Parameters.AddWithValue("@Edad", edad);
                    cmd.Parameters.AddWithValue("@FeNacimiento", feNacimiento);
                    cmd.Parameters.AddWithValue("@IdContinenteNacido", idContNacido);
                    cmd.Parameters.AddWithValue("@IdPaisNacido", idPaisNacido);
                    cmd.Parameters.AddWithValue("@IdEstadoNacido", idEstadoNacido);
                    cmd.Parameters.AddWithValue("@IdMunicipioNacido", idMunicipioNacido);
                    cmd.Parameters.AddWithValue("@IdNacionalidad", idNacionalidad);
                    cmd.Parameters.AddWithValue("@IdCondicion", idCondicion);
                    cmd.Parameters.AddWithValue("@IdEstadoCivil", idEstadoCivil);
                    cmd.Parameters.AddWithValue("@IdGradoEstudios", idGradoEstudios);
                    cmd.Parameters.AddWithValue("@IdAlfabet", idAlfabet);
                    cmd.Parameters.AddWithValue("@IdiomaEspañol", idiomaEspañol);
                    cmd.Parameters.AddWithValue("@IdVulnerabilidad", idVulnerabilidad);
                    cmd.Parameters.AddWithValue("@IdPueblo", idPueblo);
                    cmd.Parameters.AddWithValue("@HablaIndigena", hablaIndigena);
                    cmd.Parameters.AddWithValue("@IdDialecto", idDialecto);
                    cmd.Parameters.AddWithValue("@IdOcupacion", idOcupacion);
                    cmd.Parameters.AddWithValue("@IdProfesion", idProfesion);
                    cmd.Parameters.AddWithValue("@DomOcupacion", domOcupacion);
                    cmd.Parameters.AddWithValue("@Discapacidad", discapacidad);
                    cmd.Parameters.AddWithValue("@IdContinenteResidencia", idContiResidencia);
                    cmd.Parameters.AddWithValue("@IdPaisResidencia", idPaisResidencia);
                    cmd.Parameters.AddWithValue("@IdEstadoResidencia", idEstadoResidencia);
                    cmd.Parameters.AddWithValue("@IdMunicipioResidencia", idMunicipioResidencia);
                    cmd.Parameters.AddWithValue("@DomResidencia", domResidencia);
                    cmd.Parameters.AddWithValue("@IdDefensor", idDefensor);
                    cmd.Parameters.AddWithValue("@Interprete", interprete);
                    cmd.Parameters.AddWithValue("@OrdenProteccion", ordenProteccion);
                    cmd.Parameters.AddWithValue("@FeIndividualización", feIndividualizacion);
                    cmd.Parameters.AddWithValue("@IdDocIdentificador", idDocIdentificador);
                    cmd.Parameters.AddWithValue("@NumDocumento", numDocumento);
                    cmd.Parameters.AddWithValue("@Privacidad", privacidad);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Fax", fax);
                    cmd.Parameters.AddWithValue("@DomNotificacion", domNotificacion);
                    cmd.Parameters.AddWithValue("@OtroTipo", otroTipo);
                    cmd.Parameters.AddWithValue("@IdUser", idUser);

                    cmd.ExecuteNonQuery();
                }

                // Verificar si la tabla P_Discapacidades está vacía
                string checkDiscapacidadesQuery = @"SELECT COUNT(*) FROM [SIPOH].[dbo].[P_Discapacidades]";
                using (SqlCommand cmdCheck = new SqlCommand(checkDiscapacidadesQuery, conn, transaction))
                {
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        // Si la tabla está vacía, restablecer el ID
                        string resetIdQuery = @"DBCC CHECKIDENT ('[SIPOH].[dbo].[P_Discapacidades]', RESEED, 0)";
                        using (SqlCommand cmdReset = new SqlCommand(resetIdQuery, conn, transaction))
                        {
                            cmdReset.ExecuteNonQuery();
                        }
                    }
                }

                // Insertar los datos en la tabla P_Discapacidad
                string queryIdDiscapacidadPartes = @"INSERT INTO [SIPOH].[dbo].[P_Discapacidades] 
                ([IdPartes], [IdDiscapacidad]) 
                VALUES 
                (@IdPartes, @IdDiscapacidad)";
                foreach (string idDiscapacidad in idsDiscapacidades)
                {
                    using (SqlCommand cmd = new SqlCommand(queryIdDiscapacidadPartes, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdPartes", idPartes);
                        cmd.Parameters.AddWithValue("@IdDiscapacidad", idDiscapacidad);

                        cmd.ExecuteNonQuery();
                    }
                }

                using (SqlCommand cmd = new SqlCommand("LlenarCedulaVictima", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUser);
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    cmd.Parameters.AddWithValue("@IdPartes", idPartes); // Asegúrate de definir idPartes
                    cmd.ExecuteNonQuery();

                    // Crear un DataTable para almacenar los resultados
                    DataTable dt = new DataTable();

                    // Recoger los resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Llenar el DataTable con los resultados
                        dt.Load(reader);
                    }

                    // Configura la ruta del informe Crystal Reports (.rpt)
                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Victimas/CedulaVictimas.rpt");

                    // Crea el informe
                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(rutaInforme);

                    // Asignar el DataTable como fuente de datos del informe
                    reporte.SetDataSource(dt);

                    // Configura los parámetros del informe (asegúrate de que los nombres de los parámetros coincidan con los del informe)
                    reporte.SetParameterValue("@IdUsuario", idUser);
                    reporte.SetParameterValue("@IdAsunto", idAsunto);
                    reporte.SetParameterValue("@IdPartes", idPartes);

                    // Si el informe tiene más parámetros, configúralos también aquí
                    // reporte.SetParameterValue("OtroParametro", otroValor);

                    // Configura el formato de salida como PDF
                    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Victimas/CedulaVictimas.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    // Exporta el informe a PDF
                    reporte.Export();
                }

                    // Llama al método en el archivo .aspx para mostrar el PDF
                    ((ExpeDigital)page).MostrarPDFInsertar("~/ExpedienteDigital/Victimas/CedulaVictimas.pdf");

                // Registro del script de Toastr después de la inserción
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alertMessage", "toastr.success('Victima agregada correctamente', 'Éxito');", true);

                // Confirmar la transacción
                transaction.Commit();
            }
                
            catch (SqlException ex)
            {
                // Revertir la transacción
                transaction.Rollback();

                // Construir el mensaje de error
                string errorMessage = "Error al insertar datos:<br/>";
                foreach (SqlError error in ex.Errors)
                {
                    errorMessage += $"{error.Message}<br/>";
                }

                // Mostrar un Toastr con el mensaje de error
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showerror", $"toastr.error('{errorMessage}', 'Error');", true);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}