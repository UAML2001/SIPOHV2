using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class JUC_DeleteClasiDelitosController : Controller
    {
        public bool EliminarClasificacionDelito(string accion, int idDeliAsunto, int idDelitoC)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_Eliminar_ClasiDelitos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdDeliAsunto", idDeliAsunto);
                    cmd.Parameters.AddWithValue("@IdDelitoC", idDelitoC);

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
    }
}
