using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SIPOH.Controllers.AC_CatalogosCompartidos.AC_BandejaBuzonControlController;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class AC_Folio_BandejaBuzonControlController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        public List<BandejaBuzonControlModel> ObtenerBandejaSeguimientoPorFolio(long idSolicitudBuzon)
        {
            List<BandejaBuzonControlModel> bandeja = new List<BandejaBuzonControlModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spr_AC_ObtenerBandejaSeguimientoBuzonxFolio", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdSolicitudBuzon", idSolicitudBuzon);

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
                                //Observaciones = dr["Observaciones"].ToString(),
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
        //
    }
}