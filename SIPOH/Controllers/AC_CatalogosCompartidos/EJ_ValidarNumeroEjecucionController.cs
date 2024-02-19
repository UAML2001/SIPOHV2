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
    public class EJ_ValidarNumeroEjecucionController : Controller
    {
        public class ValidacionNoEjecucion
        {
            public bool ExisteNumeroEjecucion { get; set; }
        }

        public static ValidacionNoEjecucion ValidarNumeroEjecucion(string noEjecucion, int idJuzgado)
        {
            ValidacionNoEjecucion resultadoValidacion = new ValidacionNoEjecucion();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Ejecucion_ValidarNumeroEjecucion]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NoEjecucion", noEjecucion);
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultadoValidacion.ExisteNumeroEjecucion = reader.GetInt32(0) == 1;
                        }
                    }
                }
            }
            return resultadoValidacion;
        }
    }
}