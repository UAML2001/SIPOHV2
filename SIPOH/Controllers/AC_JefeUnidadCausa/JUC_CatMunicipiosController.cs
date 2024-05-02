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
    public class JUC_CatMunicipiosController : Controller
    {
        //
        public class Municipio
        {
            public int IdMunicipio { get; set; }
            public string MunicipioNombre { get; set; }
            public int IdEntidad { get; set; }  // Assuming you need this field as well
        }

        public static List<Municipio> GetMunicipios()
        {
            List<Municipio> municipios = new List<Municipio>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatMunicipios", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            municipios.Add(new Municipio
                            {
                                IdMunicipio = reader.GetInt32(reader.GetOrdinal("IdMunicipio")),
                                MunicipioNombre = reader.GetString(reader.GetOrdinal("Municipio")),
                                IdEntidad = reader.GetInt32(reader.GetOrdinal("IdEntidad"))  // If needed
                            });
                        }
                    }
                }
            }
            return municipios;
        }

        //
    }
}