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
    public class CatEjecucion_Cat_JuzgadosController : Controller
    {
        public class Juzgado
        {
            public string IdJuzgado { get; set; }
            public string Nombre { get; set; }
        }
        //LO CONSUMI CON OPCION 2 con filtro de Tipo = 'P' AND SubTipo = 'T'
        public static List<Juzgado> GetJuzgados(int idDistrito)
        {
            List<Juzgado> juzgados = new List<Juzgado>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Ejecucion_Cat_Juzgados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDistrito", idDistrito);
                    cmd.Parameters.AddWithValue("@Opcion", 2);
                    
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            juzgados.Add(new Juzgado
                            {
                                IdJuzgado = reader["IdJuzgado"].ToString(),
                                Nombre = reader["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return juzgados;
        }
    }
}