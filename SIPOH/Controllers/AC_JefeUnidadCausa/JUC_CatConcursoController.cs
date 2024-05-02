using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class JUC_CatConcursoController : Controller
    {
        public class Concurso
        {
            public int Id_CatConcurso { get; set; }
            public string NombreConcurso { get; set; }
            public int Id_INEGI { get; set; }
        }

        public static List<Concurso> GetConcursos()
        {
            List<Concurso> concursos = new List<Concurso>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatConcurso", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            concursos.Add(new Concurso
                            {
                                Id_CatConcurso = reader.GetInt32(reader.GetOrdinal("Id_CatConcurso")),
                                NombreConcurso = reader.GetString(reader.GetOrdinal("Concurso")), // Actualizado aquí
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return concursos;
        }

        //
    }
}