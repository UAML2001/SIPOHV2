using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public class InsertarImputado
{
    public void InsertImputData(int idAsunto, string apPaterno, string apMaterno, string nombre, string genero, string tipoParte, string rfc, string curp, int edad, DateTime feNacimiento, string aliasImp, int idContNacido, int idPaisNacido,
        int idEstadoNacido, string idMunicipioNacido, int idNacionalidad, int idCondicion, int idEstadoCivil,
        int idGradoEstudios, int idAlfabet, int idiomaEspañol, int idPueblo, int hablaIndigena,
        int idDialecto, int idOcupacion, int idProfesion, string domOcupacion, int discapacidad, int idLengExtra, int idRelacImput, int idAsisMigra, int idContiResidencia, int idPaisResidencia,
        int idEstadoResidencia, string idMunicipioResidencia, string domResidencia, int idDefensor, int interprete,
        int ordenProteccion, DateTime feIndividualizacion, int idDocIdentificador, string numDocumento, string privacidad,
        string telefono, string correo, string fax, string domNotificacion, string otroTipo, int idUser, List<string> idsDiscapacidades,
        Page page, int idEstadoPsi, int idaccipenal, int idReinci, int idTipoDeten, int idOrdenJudi, int idCondFamiliar, int depEcon, int idSustancias)
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
                    cmd.Parameters.AddWithValue("@Alias", aliasImp);

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
                string queryImputado = @"
                INSERT INTO [SIPOH].[dbo].[P_Inculpado] 
                (
                    [IdInculpado], [CURP], [RFC], [Edad], [FeNacimiento], [IdNacionalidad], 
                    [IdContinenteNacido], [IdPaisNacido], [IdEstadoNacido], [IdMunicipioNacido], [IdCondicion], 
                    [IdEstadoCivil], [IdGradoEstudios], [IdAlfabet], [IdiomaEspañol], [IdPueblo], 
                    [HablaIndigena], [IdDialecto], [IdCondFamiliar], [DepEcon], [IdSustancias], 
                    [IdOcupacion], [IdProfesion], [DomOcupacion], [Discapacidad], [IdContinenteResidencia], 
                    [IdPaisResidencia], [IdEstadoResidencia], [IdMunicipioResidencia], [DomResidencia], [IdDefensor], 
                    [Interprete], [FeIndividualización], [IdDocIdentificador], [NumDocumento], [Privacidad], 
                    [Telefono], [Correo], [Fax], [DomNotificacion], [OtroTipo], 
                    [IdUser], [TipoConsignacion], [IdTipoDetencion], [IdOrdenJudicial], [IdPsicofisico], 
                    [IdReincidente], [DeclaracionP], [FechaDeclaracionP], [FechaUltAudiencia], [FehaImputacion], 
                    [FechaCierreInv], [TotAudiencias], [TiempoCierreInv], [CalifDetencion], [FechaCalifDetencion], 
                    [Tramite], [AsistMigratoria], [IdRelacInput], [IdLengExtra]
                )
                VALUES
                (
                    @IdInculpado, @CURP, @RFC, @Edad, @FeNacimiento, @IdNacionalidad, 
                    @IdContinenteNacido, @IdPaisNacido, @IdEstadoNacido, @IdMunicipioNacido, @IdCondicion, 
                    @IdEstadoCivil, @IdGradoEstudios, @IdAlfabet, @IdiomaEspañol, @IdPueblo, 
                    @HablaIndigena, @IdDialecto, @IdCondFamiliar, @DepEcon, @IdSustancias, 
                    @IdOcupacion, @IdProfesion, @DomOcupacion, @Discapacidad, @IdContinenteResidencia, 
                    @IdPaisResidencia, @IdEstadoResidencia, @IdMunicipioResidencia, @DomResidencia, @IdDefensor, 
                    @Interprete, @FeIndividualización, @IdDocIdentificador, @NumDocumento, @Privacidad, 
                    @Telefono, @Correo, @Fax, @DomNotificacion, @OtroTipo, 
                    @IdUser, @TipoConsignacion, @IdTipoDetencion, @IdOrdenJudicial, @IdPsicofisico, 
                    @IdReincidente, @DeclaracionP, @FechaDeclaracionP, @FechaUltAudiencia, @FehaImputacion, 
                    @FechaCierreInv, @TotAudiencias, @TiempoCierreInv, @CalifDetencion, @FechaCalifDetencion, 
                    @Tramite, @AsistMigratoria, @IdRelacInput, @IdLengExtra
                );";




                var command = new SqlCommand(queryImputado, conn, transaction);


                command.Parameters.AddWithValue("@IdInculpado", idPartes);
                command.Parameters.AddWithValue("@CURP", curp);
                command.Parameters.AddWithValue("@RFC", rfc);
                command.Parameters.AddWithValue("@Edad", edad);
                command.Parameters.AddWithValue("@FeNacimiento", feNacimiento);
                command.Parameters.AddWithValue("@IdNacionalidad", idNacionalidad);
                command.Parameters.AddWithValue("@IdContinenteNacido", idContNacido);
                command.Parameters.AddWithValue("@IdPaisNacido", idPaisNacido);
                command.Parameters.AddWithValue("@IdEstadoNacido", idEstadoNacido);
                command.Parameters.AddWithValue("@IdMunicipioNacido", idMunicipioNacido);
                command.Parameters.AddWithValue("@IdCondicion", idCondicion);
                command.Parameters.AddWithValue("@IdEstadoCivil", idEstadoCivil);
                command.Parameters.AddWithValue("@IdGradoEstudios", idGradoEstudios);
                command.Parameters.AddWithValue("@IdAlfabet", idAlfabet);
                command.Parameters.AddWithValue("@IdiomaEspañol", idiomaEspañol);
                command.Parameters.AddWithValue("@IdPueblo", idPueblo);
                command.Parameters.AddWithValue("@HablaIndigena", hablaIndigena);
                command.Parameters.AddWithValue("@IdDialecto", idDialecto);
                command.Parameters.AddWithValue("@IdCondFamiliar", idCondFamiliar); // Nueva columna
                command.Parameters.AddWithValue("@DepEcon", depEcon); // Nueva columna
                command.Parameters.AddWithValue("@IdSustancias", idSustancias); // Nueva columna
                command.Parameters.AddWithValue("@IdOcupacion", idOcupacion);
                command.Parameters.AddWithValue("@IdProfesion", idProfesion);
                command.Parameters.AddWithValue("@DomOcupacion", domOcupacion);
                command.Parameters.AddWithValue("@Discapacidad", discapacidad);
                command.Parameters.AddWithValue("@IdContinenteResidencia", idContiResidencia);
                command.Parameters.AddWithValue("@IdPaisResidencia", idPaisResidencia);
                command.Parameters.AddWithValue("@IdEstadoResidencia", idEstadoResidencia);
                command.Parameters.AddWithValue("@IdMunicipioResidencia", idMunicipioResidencia);
                command.Parameters.AddWithValue("@DomResidencia", domResidencia);
                command.Parameters.AddWithValue("@IdDefensor", idDefensor);
                command.Parameters.AddWithValue("@Interprete", interprete);
                command.Parameters.AddWithValue("@FeIndividualización", feIndividualizacion);
                command.Parameters.AddWithValue("@IdDocIdentificador", idDocIdentificador);
                command.Parameters.AddWithValue("@NumDocumento", numDocumento);
                command.Parameters.AddWithValue("@Privacidad", privacidad);
                command.Parameters.AddWithValue("@Telefono", telefono);
                command.Parameters.AddWithValue("@Correo", correo);
                command.Parameters.AddWithValue("@Fax", fax);
                command.Parameters.AddWithValue("@DomNotificacion", domNotificacion);
                command.Parameters.AddWithValue("@OtroTipo", otroTipo);
                command.Parameters.AddWithValue("@IdUser", idUser);
                command.Parameters.AddWithValue("@TipoConsignacion", idaccipenal); // Nueva columna
                command.Parameters.AddWithValue("@IdTipoDetencion", idTipoDeten); // Nueva columna
                command.Parameters.AddWithValue("@IdOrdenJudicial", idOrdenJudi); // Nueva columna
                command.Parameters.AddWithValue("@IdPsicofisico", idEstadoPsi); // Nueva columna
                command.Parameters.AddWithValue("@IdReincidente", idReinci); // Nueva columna
                command.Parameters.AddWithValue("@DeclaracionP", 0); // Nueva columna
                command.Parameters.AddWithValue("@FechaDeclaracionP", DateTime.Today); // Nueva columna
                command.Parameters.AddWithValue("@FechaUltAudiencia", DateTime.Today); // Nueva columna
                command.Parameters.AddWithValue("@FehaImputacion", DateTime.Today); // Nueva columna
                command.Parameters.AddWithValue("@FechaCierreInv", DateTime.Today); // Nueva columna
                command.Parameters.AddWithValue("@TotAudiencias", 0); // Nueva columna
                command.Parameters.AddWithValue("@TiempoCierreInv", 0); // Nueva columna
                command.Parameters.AddWithValue("@CalifDetencion", 0); // Nueva columna
                command.Parameters.AddWithValue("@FechaCalifDetencion", DateTime.Today); // Nueva columna
                command.Parameters.AddWithValue("@Tramite", 0); // Nueva columna
                command.Parameters.AddWithValue("@AsistMigratoria", idAsisMigra);
                command.Parameters.AddWithValue("@IdRelacInput", idRelacImput);
                command.Parameters.AddWithValue("@IdLengExtra", idLengExtra);

                command.ExecuteNonQuery();


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

                // Ejecutar el procedimiento almacenado LlenarCedulaImputado
                using (SqlCommand cmd = new SqlCommand("LlenarCedulaImputado", conn, transaction))
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
                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Imputados/CedulaImputados.rpt");

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
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Imputados/CedulaImputados.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    // Exporta el informe a PDF
                    reporte.Export();
                }

                    // Llama al método en el archivo .aspx para mostrar el PDF
                    ((Imputados)page).MostrarPDFInsertar("~/ExpedienteDigital/Imputados/CedulaImputados.pdf");

                // Registro del script de Toastr después de la inserción
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alertMessage", "toastr.success('Imputado agregado correctamente.', 'Éxito');", true);

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


            }
            finally
            {
                conn.Close();
            }
        }
    }
}