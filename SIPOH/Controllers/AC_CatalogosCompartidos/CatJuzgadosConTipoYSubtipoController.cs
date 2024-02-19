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
    public class CatJuzgadosConTipoYSubtipoController : Controller
    {

        public class DataJuzgado
        {
            public string IdJuzgado { get; set; }
            public string NombreJuzgado { get; set; }
        }

        // Método para obtener la lista de juzgados desde el almacenamiento
        public static List<DataJuzgado> GetJuzgadosConTipoYSubtipo(int idCircuito, char tipo)
        {
            List<DataJuzgado> juzgados = new List<DataJuzgado>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_JuzgadosConTipoYSubtipo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCircuito", idCircuito);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DataJuzgado juzgado = new DataJuzgado
                            {
                                IdJuzgado = dr["IdJuzgado"].ToString(),
                                NombreJuzgado = dr["Nombre"].ToString()
                            };
                            juzgados.Add(juzgado);
                        }
                    }
                }
            }

            return juzgados;
        }

    }
}