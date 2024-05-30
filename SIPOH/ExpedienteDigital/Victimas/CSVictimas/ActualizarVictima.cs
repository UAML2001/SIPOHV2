using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public class ActualizarVictima
{
    public void UpdateVictimData(
       int idPartes, int idAsunto, string apPaterno, string apMaterno, string nombre, string genero, string tipoParte, int tipoVictima,
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
                // Actualizar los datos en P_PartesAsunto
                string queryUpdatePartesAsunto = @"
                UPDATE [SIPOH].[dbo].[P_PartesAsunto] 
                SET 
                    [IdAsunto] = @IdAsunto,
                    [Nombre] = @Nombre,
                    [APaterno] = @APaterno,
                    [AMaterno] = @AMaterno,
                    [Genero] = @Genero,
                    [TipoParte] = @TipoParte,
                    [Alias] = @Alias
                WHERE [IdPartes] = @IdPartes;";

                using (SqlCommand cmd = new SqlCommand(queryUpdatePartesAsunto, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@APaterno", apPaterno);
                    cmd.Parameters.AddWithValue("@AMaterno", apMaterno);
                    cmd.Parameters.AddWithValue("@Genero", genero);
                    cmd.Parameters.AddWithValue("@TipoParte", tipoParte);
                    cmd.Parameters.AddWithValue("@Alias", "");
                    cmd.Parameters.AddWithValue("@IdPartes", idPartes);

                    cmd.ExecuteNonQuery();
                }

                // Actualizar los datos en la tabla P_Victima
                string queryUpdateVictima = @"
                UPDATE [SIPOH].[dbo].[P_Victima] 
                SET
                    [TipoVictima] = @TipoVictima,
                    [Victima] = @Victima,
                    [RFC] = @RFC,
                    [CURP] = @CURP,
                    [Edad] = @Edad,
                    [FeNacimiento] = @FeNacimiento,
                    [IdContinenteNacido] = @IdContinenteNacido,
                    [IdPaisNacido] = @IdPaisNacido,
                    [IdEstadoNacido] = @IdEstadoNacido,
                    [IdMunicipioNacido] = @IdMunicipioNacido,
                    [IdNacionalidad] = @IdNacionalidad,
                    [IdCondicion] = @IdCondicion,
                    [IdEstadoCivil] = @IdEstadoCivil,
                    [IdGradoEstudios] = @IdGradoEstudios,
                    [IdAlfabet] = @IdAlfabet,
                    [IdiomaEspañol] = @IdiomaEspañol,
                    [IdVulnerabilidad] = @IdVulnerabilidad,
                    [IdPueblo] = @IdPueblo,
                    [HablaIndigena] = @HablaIndigena,
                    [IdDialecto] = @IdDialecto,
                    [IdOcupacion] = @IdOcupacion,
                    [IdProfesion] = @IdProfesion,
                    [DomOcupacion] = @DomOcupacion,
                    [Discapacidad] = @Discapacidad,
                    [IdContinenteResidencia] = @IdContinenteResidencia,
                    [IdPaisResidencia] = @IdPaisResidencia,
                    [IdEstadoResidencia] = @IdEstadoResidencia,
                    [IdMunicipioResidencia] = @IdMunicipioResidencia,
                    [DomResidencia] = @DomResidencia,
                    [IdDefensor] = @IdDefensor,
                    [Interprete] = @Interprete,
                    [OrdenProteccion] = @OrdenProteccion,
                    [FeIndividualización] = @FeIndividualizacion,
                    [IdDocIdentificador] = @IdDocIdentificador,
                    [NumDocumento] = @NumDocumento,
                    [Privacidad] = @Privacidad,
                    [Telefono] = @Telefono,
                    [Correo] = @Correo,
                    [Fax] = @Fax,
                    [DomNotificacion] = @DomNotificacion,
                    [OtroTipo] = @OtroTipo,
                    [IdUser] = @IdUser
                WHERE [IdVictima] = @IdVictima;";

                using (SqlCommand cmd = new SqlCommand(queryUpdateVictima, conn, transaction))
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
                    cmd.Parameters.AddWithValue("@FeIndividualizacion", feIndividualizacion);
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

                // Actualizar los datos en la tabla P_Discapacidades
                string queryDeleteDiscapacidades = @"DELETE FROM [SIPOH].[dbo].[P_Discapacidades] WHERE [IdPartes] = @IdPartes";
                using (SqlCommand cmd = new SqlCommand(queryDeleteDiscapacidades, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdPartes", idPartes);
                    cmd.ExecuteNonQuery();
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

                using (SqlCommand cmd = new SqlCommand("LlenarCedulaVictima", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUser);
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    cmd.Parameters.AddWithValue("@IdPartes", idPartes);
                    cmd.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Victimas/CedulaVictimas.rpt");
                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(rutaInforme);
                    reporte.SetDataSource(dt);

                    reporte.SetParameterValue("@IdUsuario", idUser);
                    reporte.SetParameterValue("@IdAsunto", idAsunto);
                    reporte.SetParameterValue("@IdPartes", idPartes);

                    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Victimas/CedulaVictimas.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    reporte.Export();
                    ((ExpeDigital)page).MostrarPDFActualizar("~/ExpedienteDigital/Victimas/CedulaVictimas.pdf");

                    ScriptManager.RegisterStartupScript(page, page.GetType(), "alertMessage", "toastr.success('Victima actualizada correctamente.', 'Éxito');", true);
                    transaction.Commit();
                }
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                string errorMessage = "Error al actualizar datos:<br/>";
                foreach (SqlError error in ex.Errors)
                {
                    errorMessage += $"{error.Message}<br/>";
                }
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showerror", $"toastr.error('{errorMessage}', 'Error');", true);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
