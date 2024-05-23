using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class JUC_CatDelitoDetalleController : Controller
    {
        // Clase para representar el detalle del delito
        public class DelitoDetalleModel
        {
            public int IdDelDetalle { get; set; }
            public string DelitoDetalle { get; set; }
            public int IdDelito { get; set; }
        }

        // Método para obtener el catálogo de modalidades
        public static List<DelitoDetalleModel> GetDelitoDetallesPorId(int idDelito, int? idDelDetalle)
        {
            List<DelitoDetalleModel> detalles = new List<DelitoDetalleModel>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatDelitoDetalle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDelito", idDelito);
                    if (idDelDetalle.HasValue)
                        cmd.Parameters.AddWithValue("@IdDelDetalle", idDelDetalle.Value);
                    else
                        cmd.Parameters.AddWithValue("@IdDelDetalle", DBNull.Value);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(new DelitoDetalleModel
                            {
                                IdDelDetalle = reader.GetInt32(reader.GetOrdinal("IdDelDetalle")),
                                DelitoDetalle = reader.GetString(reader.GetOrdinal("DelitoDetalle")),
                                IdDelito = reader.GetInt32(reader.GetOrdinal("IdDelito"))
                            });
                        }
                    }
                }
            }

            return detalles;
        }

        //
    }
}
