using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class AC_BandejaBuzonControlController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
        // Asegúrate de tener esta clase modelo definida en alguna parte adecuada de tu proyecto
        public class BandejaBuzonControlModel
        {
            public long IdSolicitudBuzon { get; set; }
            public string Solicitud { get; set; }
            public string Tipo { get; set; }
            public string TipoAsunto { get; set; }
            public string Numero { get; set; }
            public int IdJuzgado { get; set; }
            public string Estatus { get; set; }
            public DateTime Fecha { get; set; }
            public DateTime? FeIngreso { get; set; }
            public string RutaDoc { get; set; }
        }

        public List<BandejaBuzonControlModel> ObtenerBandejaSeguimientoBuzon(int idJuzgado)
        {
            List<BandejaBuzonControlModel> bandeja = new List<BandejaBuzonControlModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spr_AC_ObtenerBandejaSeguimientoBuzon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BandejaBuzonControlModel item = new BandejaBuzonControlModel
                            {
                                IdSolicitudBuzon = Convert.ToInt64(dr["IdSolicitudBuzon"]),
                                Solicitud = dr["Solicitud"].ToString(),
                                Tipo = dr["Tipo"].ToString(),
                                TipoAsunto = dr["TipoAsunto"].ToString(),
                                Numero = dr["Numero"].ToString(),
                                IdJuzgado = Convert.ToInt32(dr["IdJuzgado"]),
                                Estatus = dr["Estatus"].ToString(),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                FeIngreso = dr["FeIngreso"] != DBNull.Value ? Convert.ToDateTime(dr["FeIngreso"]) : (DateTime?)null,
                                RutaDoc = dr["RutaDoc"].ToString()
                            };

                            bandeja.Add(item);
                        }
                    }
                }
            }

            return bandeja;
        }

    }
}