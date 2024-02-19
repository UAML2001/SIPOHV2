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
    public class CatEjecucion_Cat_DistritosController : Controller
    {
        public class Distrito
        {
            public string IdDistrito { get; set; }
            public string Nombre { get; set; }
        }

        public static List<Distrito> GetDistritos(int idCircuito)
        {
            List<Distrito> distritos = new List<Distrito>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Distritos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCircuito", idCircuito);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            distritos.Add(new Distrito
                            {
                                IdDistrito = reader["IdDistrito"].ToString(),
                                Nombre = reader["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return distritos;
        }
    }
}