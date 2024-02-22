using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatEjecucion_CatSolicitanteController : Controller
    {

        public class DataSolicitante
        {
            public string Clave { get; set; }
            public string Nombre { get; set; }
        }
        public static List<DataSolicitante> GetSolicitantes()
        {
            List<DataSolicitante> solicitantes = new List<DataSolicitante>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_EjecucionCatSolicitante ORDER BY CASE WHEN Solicitante = 'OTRO' THEN 1 ELSE 0 END, Solicitante", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            solicitantes.Add(new DataSolicitante
                            {
                                Clave = dr["CveSolicitante"].ToString(),
                                Nombre = dr["Solicitante"].ToString()
                            });
                        }
                    }
                }
            }
            return solicitantes;
        }


    }
}