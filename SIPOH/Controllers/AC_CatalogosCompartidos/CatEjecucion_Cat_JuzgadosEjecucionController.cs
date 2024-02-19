using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatEjecucion_Cat_JuzgadosEjecucionController : Controller
    {
        public class DataSalaEjecucion
        {
            public string IdJuzgado { get; set; }
            public string Nombre { get; set; }
        }

        public static List<DataSalaEjecucion> GetSalas()
        {
            List<DataSalaEjecucion> salas = new List<DataSalaEjecucion>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosEjecucion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            salas.Add(new DataSalaEjecucion
                            {
                                IdJuzgado = dr["IdJuzgado"].ToString(),
                                Nombre = dr["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return salas;
        }

    }
}