using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatEjecucion_CatSolicitudController : Controller
    {
        public class DataSolicitud
        {
            public string Nombre { get; set; }
        }
        public static List<DataSolicitud> GetSolicitudes()
        {
            List<DataSolicitud> solicitudes = new List<DataSolicitud>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Solicitud FROM P_EjecucionCatSolicitud ORDER BY CASE WHEN Solicitud = 'OTRO' THEN 1 ELSE 0 END, Solicitud", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            solicitudes.Add(new DataSolicitud
                            {
                                Nombre = dr["Solicitud"].ToString()
                            });
                        }
                    }
                }
            }
            return solicitudes;
        }

    }
}