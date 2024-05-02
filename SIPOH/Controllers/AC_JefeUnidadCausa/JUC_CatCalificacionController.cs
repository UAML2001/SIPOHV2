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
    public class JUC_CatCalificacionController : Controller
    {
        //
        public class Calificacion
        {
            public int Id_CatCalificacion { get; set; }
            public string CalificacionNombre { get; set; }
            public int Id_INEGI { get; set; }
        }
        public static List<Calificacion> GetCalificaciones()
        {
            List<Calificacion> calificaciones = new List<Calificacion>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatCalificacion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            calificaciones.Add(new Calificacion
                            {
                                Id_CatCalificacion = reader.GetInt32(reader.GetOrdinal("Id_CatCalificacion")),
                                CalificacionNombre = reader.GetString(reader.GetOrdinal("Calificacion")),
                                Id_INEGI = reader.GetInt32(reader.GetOrdinal("Id_INEGI")) // If needed
                            });
                        }
                    }
                }
            }
            return calificaciones;
        }
        //
    }
}