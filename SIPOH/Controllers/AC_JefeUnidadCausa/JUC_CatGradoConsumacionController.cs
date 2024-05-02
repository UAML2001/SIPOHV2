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
    public class JUC_CatGradoConsumacionController : Controller
    {
        public class GradoConsumacion
        {
            public int Id_CatConsumacion { get; set; }
            public string Consumacion { get; set; }
        }
        public static List<GradoConsumacion> GetGradosConsumacion()
        {
            List<GradoConsumacion> grados = new List<GradoConsumacion>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatGradoConsumacion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            grados.Add(new GradoConsumacion
                            {
                                Id_CatConsumacion = reader.GetInt32(reader.GetOrdinal("Id_CatConsumacion")),
                                Consumacion = reader.GetString(reader.GetOrdinal("Consumacion"))
                            });
                        }
                    }
                }
            }
            return grados;
        }

        //
    }
}