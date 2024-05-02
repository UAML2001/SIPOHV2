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
    public class JUC_CatModalidadController : Controller
    {
        //
        public class Modalidad
        {
            public int Id_CatModalidad { get; set; }
            public string ModalidadNombre { get; set; }
            public int Id_INEGI { get; set; }
        }
        public static List<Modalidad> GetModalidades()
        {
            List<Modalidad> modalidades = new List<Modalidad>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatModalidad", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            modalidades.Add(new Modalidad
                            {
                                Id_CatModalidad = reader.GetInt32(reader.GetOrdinal("Id_CatModalidad")),
                                ModalidadNombre = reader.GetString(reader.GetOrdinal("Modalidad")),
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return modalidades;
        }
        //
    }
}