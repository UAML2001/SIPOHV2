using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public class ActualizarImputado
{
    public void UpdateImputData(int idAsunto, int idPartes, string apPaterno, string apMaterno, string nombre, string genero, string tipoParte, string rfc, string curp, string edad, DateTime feNacimiento, string aliasImp, int idContNacido, int idPaisNacido, int idEstadoNacido, string idMunicipioNacido, int idNacionalidad, int idCondicion, int idEstadoCivil, int idGradoEstudios, int idAlfabet, int idiomaEspañol, int idPueblo, int hablaIndigena, int idDialecto, int idOcupacion, int idProfesion, string domOcupacion, int discapacidad, int idLengExtra, int idRelacImput, int idAsisMigra, int idContiResidencia, int idPaisResidencia, int idEstadoResidencia, string idMunicipioResidencia, string domResidencia, int idDefensor, int interprete, int ordenProteccion, DateTime feIndividualizacion, int idDocIdentificador, string numDocumento, string privacidad, string telefono, string correo, string fax, string domNotificacion, string otroTipo, int idUser, List<string> idsDiscapacidades, Page page, int idEstadoPsi, int idaccipenal, int idReinci, int idTipoDeten, int idOrdenJudi, int idCondFamiliar, int depEcon, int idSustancias)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Actualizar P_PartesAsunto utilizando IdParte
                string queryPartesAsunto = @"
                    UPDATE [SIPOH].[dbo].[P_PartesAsunto] 
                    SET [Nombre] = @Nombre, [APaterno] = @APaterno, [AMaterno] = @AMaterno, [Genero] = @Genero, [TipoParte] = @TipoParte, [Alias] = @Alias 
                    WHERE [IdPartes] = @IdPartes";

                using (SqlCommand cmd = new SqlCommand(queryPartesAsunto, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdPartes", idPartes);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@APaterno", apPaterno);
                    cmd.Parameters.AddWithValue("@AMaterno", apMaterno);
                    cmd.Parameters.AddWithValue("@Genero", genero);
                    cmd.Parameters.AddWithValue("@TipoParte", tipoParte);
                    cmd.Parameters.AddWithValue("@Alias", aliasImp);
                    cmd.ExecuteNonQuery();
                }

                // Verificar si existe un registro asociado entre P_Inculpado y P_Partes
                string queryVerificar = @"
                    SELECT COUNT(*) 
                    FROM [SIPOH].[dbo].[P_Inculpado] 
                    WHERE [IdInculpado] = @IdPartes";

                using (SqlCommand cmdVerificar = new SqlCommand(queryVerificar, conn, transaction))
                {
                    cmdVerificar.Parameters.AddWithValue("@IdPartes", idPartes);
                    int count = (int)cmdVerificar.ExecuteScalar();

                    if (count > 0)
                    {
                        // Actualizar P_Inculpado utilizando IdParte
                        string queryImputado = @"
                            UPDATE [SIPOH].[dbo].[P_Inculpado] 
                            SET [CURP] = @CURP, [RFC] = @RFC, [Edad] = @Edad, [FeNacimiento] = @FeNacimiento, [IdNacionalidad] = @IdNacionalidad,
                            [IdContinenteNacido] = @IdContinenteNacido, [IdPaisNacido] = @IdPaisNacido, [IdEstadoNacido] = @IdEstadoNacido, [IdMunicipioNacido] = @IdMunicipioNacido, [IdCondicion] = @IdCondicion,
                            [IdEstadoCivil] = @IdEstadoCivil, [IdGradoEstudios] = @IdGradoEstudios, [IdAlfabet] = @IdAlfabet, [IdiomaEspañol] = @IdiomaEspañol, [IdPueblo] = @IdPueblo,
                            [HablaIndigena] = @HablaIndigena, [IdDialecto] = @IdDialecto, [IdCondFamiliar] = @IdCondFamiliar, [DepEcon] = @DepEcon, [IdSustancias] = @IdSustancias,
                            [IdOcupacion] = @IdOcupacion, [IdProfesion] = @IdProfesion, [DomOcupacion] = @DomOcupacion, [Discapacidad] = @Discapacidad, [IdContinenteResidencia] = @IdContinenteResidencia,
                            [IdPaisResidencia] = @IdPaisResidencia, [IdEstadoResidencia] = @IdEstadoResidencia, [IdMunicipioResidencia] = @IdMunicipioResidencia, [DomResidencia] = @DomResidencia, [IdDefensor] = @IdDefensor,
                            [Interprete] = @Interprete, [FeIndividualización] = @FeIndividualización, [IdDocIdentificador] = @IdDocIdentificador, [NumDocumento] = @NumDocumento, [Privacidad] = @Privacidad,
                            [Telefono] = @Telefono, [Correo] = @Correo, [Fax] = @Fax, [DomNotificacion] = @DomNotificacion, [OtroTipo] = @OtroTipo,
                            [IdUser] = @IdUser, [TipoConsignacion] = @TipoConsignacion, [IdTipoDetencion] = @IdTipoDetencion, [IdOrdenJudicial] = @IdOrdenJudicial, [IdPsicofisico] = @IdPsicofisico,
                            [IdReincidente] = @IdReincidente, [DeclaracionP] = @DeclaracionP, [FechaDeclaracionP] = @FechaDeclaracionP, [FechaUltAudiencia] = @FechaUltAudiencia, [FehaImputacion] = @FehaImputacion,
                            [FechaCierreInv] = @FechaCierreInv, [TotAudiencias] = @TotAudiencias, [TiempoCierreInv] = @TiempoCierreInv, [CalifDetencion] = @CalifDetencion, [FechaCalifDetencion] = @FechaCalifDetencion,
                            [Tramite] = @Tramite, [AsistMigratoria] = @AsistMigratoria, [IdRelacInput] = @IdRelacInput, [IdLengExtra] = @IdLengExtra
                            WHERE [IdInculpado] = @IdInculpado";

                        using (SqlCommand cmd = new SqlCommand(queryImputado, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdInculpado", idPartes);
                            cmd.Parameters.AddWithValue("@CURP", curp);
                            cmd.Parameters.AddWithValue("@RFC", rfc);
                            cmd.Parameters.AddWithValue("@Edad", edad);
                            cmd.Parameters.AddWithValue("@FeNacimiento", feNacimiento);
                            cmd.Parameters.AddWithValue("@IdNacionalidad", idNacionalidad);
                            cmd.Parameters.AddWithValue("@IdContinenteNacido", idContNacido);
                            cmd.Parameters.AddWithValue("@IdPaisNacido", idPaisNacido);
                            cmd.Parameters.AddWithValue("@IdEstadoNacido", idEstadoNacido);
                            cmd.Parameters.AddWithValue("@IdMunicipioNacido", idMunicipioNacido);
                            cmd.Parameters.AddWithValue("@IdCondicion", idCondicion);
                            cmd.Parameters.AddWithValue("@IdEstadoCivil", idEstadoCivil);
                            cmd.Parameters.AddWithValue("@IdGradoEstudios", idGradoEstudios);
                            cmd.Parameters.AddWithValue("@IdAlfabet", idAlfabet);
                            cmd.Parameters.AddWithValue("@IdiomaEspañol", idiomaEspañol);
                            cmd.Parameters.AddWithValue("@IdPueblo", idPueblo);
                            cmd.Parameters.AddWithValue("@HablaIndigena", hablaIndigena);
                            cmd.Parameters.AddWithValue("@IdDialecto", idDialecto);
                            cmd.Parameters.AddWithValue("@IdCondFamiliar", idCondFamiliar);
                            cmd.Parameters.AddWithValue("@DepEcon", depEcon);
                            cmd.Parameters.AddWithValue("@IdSustancias", idSustancias);
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
                            cmd.Parameters.AddWithValue("@TipoConsignacion", idaccipenal);
                            cmd.Parameters.AddWithValue("@IdTipoDetencion", idTipoDeten);
                            cmd.Parameters.AddWithValue("@IdOrdenJudicial", idOrdenJudi);
                            cmd.Parameters.AddWithValue("@IdPsicofisico", idEstadoPsi);
                            cmd.Parameters.AddWithValue("@IdReincidente", idReinci);
                            cmd.Parameters.AddWithValue("@DeclaracionP", 0);
                            cmd.Parameters.AddWithValue("@FechaDeclaracionP", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaUltAudiencia", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FehaImputacion", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaCierreInv", DBNull.Value);
                            cmd.Parameters.AddWithValue("@TotAudiencias", DBNull.Value);
                            cmd.Parameters.AddWithValue("@TiempoCierreInv", DBNull.Value);
                            cmd.Parameters.AddWithValue("@CalifDetencion", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaCalifDetencion", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Tramite", DBNull.Value);
                            cmd.Parameters.AddWithValue("@IdRelacInput", idRelacImput);
                            cmd.Parameters.AddWithValue("@IdLengExtra", idLengExtra);
                            cmd.Parameters.AddWithValue("@AsistMigratoria", idAsisMigra);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insertar un nuevo registro en P_Inculpado
                        string queryImputado = @"
                            INSERT INTO [SIPOH].[dbo].[P_Inculpado] (
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
                            ) VALUES (
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
                            )";
                        using (SqlCommand cmd = new SqlCommand(queryImputado, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IdInculpado", idPartes);
                            cmd.Parameters.AddWithValue("@CURP", curp);
                            cmd.Parameters.AddWithValue("@RFC", rfc);
                            cmd.Parameters.AddWithValue("@Edad", edad);
                            cmd.Parameters.AddWithValue("@FeNacimiento", feNacimiento);
                            cmd.Parameters.AddWithValue("@IdNacionalidad", idNacionalidad);
                            cmd.Parameters.AddWithValue("@IdContinenteNacido", idContNacido);
                            cmd.Parameters.AddWithValue("@IdPaisNacido", idPaisNacido);
                            cmd.Parameters.AddWithValue("@IdEstadoNacido", idEstadoNacido);
                            cmd.Parameters.AddWithValue("@IdMunicipioNacido", idMunicipioNacido);
                            cmd.Parameters.AddWithValue("@IdCondicion", idCondicion);
                            cmd.Parameters.AddWithValue("@IdEstadoCivil", idEstadoCivil);
                            cmd.Parameters.AddWithValue("@IdGradoEstudios", idGradoEstudios);
                            cmd.Parameters.AddWithValue("@IdAlfabet", idAlfabet);
                            cmd.Parameters.AddWithValue("@IdiomaEspañol", idiomaEspañol);
                            cmd.Parameters.AddWithValue("@IdPueblo", idPueblo);
                            cmd.Parameters.AddWithValue("@HablaIndigena", hablaIndigena);
                            cmd.Parameters.AddWithValue("@IdDialecto", idDialecto);
                            cmd.Parameters.AddWithValue("@IdCondFamiliar", idCondFamiliar);
                            cmd.Parameters.AddWithValue("@DepEcon", depEcon);
                            cmd.Parameters.AddWithValue("@IdSustancias", idSustancias);
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
                            cmd.Parameters.AddWithValue("@TipoConsignacion", idaccipenal);
                            cmd.Parameters.AddWithValue("@IdTipoDetencion", idTipoDeten);
                            cmd.Parameters.AddWithValue("@IdOrdenJudicial", idOrdenJudi);
                            cmd.Parameters.AddWithValue("@IdPsicofisico", idEstadoPsi);
                            cmd.Parameters.AddWithValue("@IdReincidente", idReinci);
                            cmd.Parameters.AddWithValue("@DeclaracionP", 0);
                            cmd.Parameters.AddWithValue("@FechaDeclaracionP", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaUltAudiencia", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FehaImputacion", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaCierreInv", DBNull.Value);
                            cmd.Parameters.AddWithValue("@TotAudiencias", DBNull.Value);
                            cmd.Parameters.AddWithValue("@TiempoCierreInv", DBNull.Value);
                            cmd.Parameters.AddWithValue("@CalifDetencion", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaCalifDetencion", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Tramite", DBNull.Value);
                            cmd.Parameters.AddWithValue("@AsistMigratoria", idAsisMigra);
                            cmd.Parameters.AddWithValue("@IdRelacInput", idRelacImput);
                            cmd.Parameters.AddWithValue("@IdLengExtra", idLengExtra);
                            cmd.ExecuteNonQuery();
                        }
                    }

                        string queryInsertDiscapacidades = @"INSERT INTO [SIPOH].[dbo].[P_Discapacidades] 
                            ([IdPartes], [IdDiscapacidad]) 
                            VALUES 
                            (@IdPartes, @IdDiscapacidad)";
                    foreach (string idDiscapacidad in idsDiscapacidades)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryInsertDiscapacidades, conn, transaction))
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
                            ((Imputados)page).MostrarPDFActualizar("~/ExpedienteDigital/Imputados/CedulaImputados.pdf");

                            // Registro del script de Toastr después de la inserción
                            ScriptManager.RegisterStartupScript(page, page.GetType(), "alertMessage", "toastr.success('Imputado actualizado correctamente.', 'Éxito');", true);

                            // Confirmar la transacción
                            transaction.Commit();

                        }
                    }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar datos del imputado", ex);
            }
        }
    }
}


