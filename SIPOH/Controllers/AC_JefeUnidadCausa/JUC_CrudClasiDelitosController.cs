using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class JUC_CrudClasiDelitosController : Controller
    {

        public bool ActualizarClasificacionDelito(string accion, int? idDelitoC, int? idDeliAsunto, int consumacion, int calificacion, int concurso, int clasificacion, int fComision, int fAccion, int modalidad, int elemComision, string persecucion, int idMunicipio, DateTime feDelito, string domicilio, int idDelDetalle, DateTime? feReclasificacion = null, DateTime? feCaptura = null, char? reclasificar = null, int? idAsunto = null, int? idDelito = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_Insertar_ClasiDelitos", con))
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
                    cmd.Parameters.AddWithValue("@IdDelDetalle", idDelDetalle);

                    if (accion == "U")
                    {
                        cmd.Parameters.AddWithValue("@IdDelitoC", idDelitoC);
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);

                        if (feReclasificacion.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FeReclasificacion", feReclasificacion.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FeReclasificacion", DBNull.Value);
                        }

                        if (feCaptura.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FeCaptura", feCaptura.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FeCaptura", DBNull.Value);
                        }

                        if (reclasificar.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@Reclasificar", reclasificar.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Reclasificar", DBNull.Value);
                        }

                        if (idDelito.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@IdDelito", idDelito.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IdDelito", DBNull.Value);
                        }
                    }
                    else if (accion == "AG")
                    {
                        cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                        cmd.Parameters.AddWithValue("@IdDelito", idDelito);
                    }

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("SQL Error: " + ex.Message);
                        return false;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }



        //
    }
}
