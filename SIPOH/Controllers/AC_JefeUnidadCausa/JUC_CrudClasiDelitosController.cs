using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class JUC_CrudClasiDelitosController : Controller
    {
        public bool InsertarActualizarDelito(
            string accion, int idDeliAsunto, int consumacion, int calificacion, int concurso,
            int clasificacion, int fComision, int fAccion, int modalidad, int elemComision,
            string persecucion, int idMunicipio, DateTime feDelito, string domicilio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_Insertar_ClasiDelitos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdDeliAsunto", idDeliAsunto);
                    cmd.Parameters.AddWithValue("@Consumacion", consumacion);
                    cmd.Parameters.AddWithValue("@Calificacion", calificacion);
                    cmd.Parameters.AddWithValue("@Concurso", concurso);
                    cmd.Parameters.AddWithValue("@Clasificacion", clasificacion);
                    cmd.Parameters.AddWithValue("@FComision", fComision);
                    cmd.Parameters.AddWithValue("@FAccion", fAccion);
                    cmd.Parameters.AddWithValue("@Modalidad", modalidad);
                    cmd.Parameters.AddWithValue("@ElemComision", elemComision);
                    cmd.Parameters.AddWithValue("@Persecucion", persecucion);
                    cmd.Parameters.AddWithValue("@IdMunicipio", idMunicipio);
                    cmd.Parameters.AddWithValue("@FeDelito", feDelito);
                    cmd.Parameters.AddWithValue("@Domicilio", domicilio);

                    // Depuración de los valores enviados
                    System.Diagnostics.Debug.WriteLine("Parametros enviados al procedimiento almacenado:");
                    System.Diagnostics.Debug.WriteLine($"@Accion: {accion}, @IdDeliAsunto: {idDeliAsunto}, @Consumacion: {consumacion}, @Calificacion: {calificacion}");
                    System.Diagnostics.Debug.WriteLine($"@Concurso: {concurso}, @Clasificacion: {clasificacion}, @FComision: {fComision}, @FAccion: {fAccion}");
                    System.Diagnostics.Debug.WriteLine($"@Modalidad: {modalidad}, @ElemComision: {elemComision}, @Persecucion: {persecucion}, @IdMunicipio: {idMunicipio}");
                    System.Diagnostics.Debug.WriteLine($"@FeDelito: {feDelito}, @Domicilio: {domicilio}");

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Manejar el error (log, rethrow, etc.)
                        System.Diagnostics.Debug.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
