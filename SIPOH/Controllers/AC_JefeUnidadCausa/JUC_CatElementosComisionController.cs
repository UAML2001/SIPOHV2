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
    public class JUC_CatElementosComisionController : Controller
    {
        //
        public class ElementoComision
        {
            public int Id_CatElemComision { get; set; }
            public string ElemComision { get; set; }
            public int Id_INEGI { get; set; }
        }
        public static List<ElementoComision> GetElementosComision()
        {
            List<ElementoComision> elementosComision = new List<ElementoComision>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatElementosComision", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            elementosComision.Add(new ElementoComision
                            {
                                Id_CatElemComision = reader.GetInt32(reader.GetOrdinal("Id_CatElemComision")),
                                ElemComision = reader.GetString(reader.GetOrdinal("ElemComision")),
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return elementosComision;
        }
        //
    }
}