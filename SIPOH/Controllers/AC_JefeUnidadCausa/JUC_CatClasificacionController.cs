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
    public class JUC_CatClasificacionController : Controller
    {
        //
        public class Clasificacion
        {
            public int Id_CatClasificacion { get; set; }
            public string ClasificacionNombre { get; set; }
            public int Id_INEGI { get; set; }
        }
        public static List<Clasificacion> GetClasificaciones()
        {
            List<Clasificacion> clasificaciones = new List<Clasificacion>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatClasificacion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clasificaciones.Add(new Clasificacion
                            {
                                Id_CatClasificacion = reader.GetInt32(reader.GetOrdinal("Id_CatClasificacion")),
                                ClasificacionNombre = reader.GetString(reader.GetOrdinal("Clasificacion")),
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return clasificaciones;
        }

        //
    }
}