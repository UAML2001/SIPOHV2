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
    public class JUC_CatFormaComisionController : Controller
    {
        //
        public class FormaComision
        {
            public int Id_CatComision { get; set; }
            public string Comision { get; set; }
            public int Id_INEGI { get; set; }
        }
        public static List<FormaComision> GetFormasComision()
        {
            List<FormaComision> formasComision = new List<FormaComision>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatFormaComision", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            formasComision.Add(new FormaComision
                            {
                                Id_CatComision = reader.GetInt32(reader.GetOrdinal("Id_CatComision")),
                                Comision = reader.GetString(reader.GetOrdinal("Comision")),
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return formasComision;
        }

        //
    }
}