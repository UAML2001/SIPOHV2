using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class Cat_Ejecucion_Cat_JuzgadosPorCircuitoEController : Controller
    {
        public class DataJuzgadoEjecucion
        {
            public string IdJuzgado { get; set; }
            public string Nombre { get; set; }
        }

        public static List<DataJuzgadoEjecucion> GetJuzgadosPorCircuito(int idCircuito)
        {
            List<DataJuzgadoEjecucion> juzgados = new List<DataJuzgadoEjecucion>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosPorCircuitoE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCircuito", idCircuito);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            juzgados.Add(new DataJuzgadoEjecucion
                            {
                                IdJuzgado = dr["IdJuzgado"].ToString(),
                                Nombre = dr["Nombre"].ToString()
                            });
                        }
                    }
                }
            }

            return juzgados;
        }
    }
}