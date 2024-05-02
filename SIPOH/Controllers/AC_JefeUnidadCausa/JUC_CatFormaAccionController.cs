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
    public class JUC_CatFormaAccionController : Controller
    {
        //
        public class FormaAccion
        {
            public int Id_CatAccion { get; set; }
            public string Accion { get; set; }
            public int Id_INEGI { get; set; }
        }
        public static List<FormaAccion> GetFormasAccion()
        {
            List<FormaAccion> formasAccion = new List<FormaAccion>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatFormaAccion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            formasAccion.Add(new FormaAccion
                            {
                                Id_CatAccion = reader.GetInt32(reader.GetOrdinal("Id_CatAccion")),
                                Accion = reader.GetString(reader.GetOrdinal("Accion")),
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return formasAccion;
        }
        //
    }
}