using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.EJ_Storages
{
    public class ObtenerNombreJuzgadoPorIDController : Controller
    {
        public class DataJuzgadoNombre
        {
            public string IdJuzgado { get; set; }
            public string Nombre { get; set; }
        }

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        public DataJuzgadoNombre ObtenerJuzgadoPorID(string idJuzgado)
        {
            DataJuzgadoNombre juzgado = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Nombre FROM P_CatJuzgados WHERE IdJuzgado = @IdJuzgado";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        juzgado = new DataJuzgadoNombre
                        {
                            IdJuzgado = idJuzgado,
                            Nombre = result.ToString()
                        };
                    }
                }
            }
            return juzgado;
        }
    }
}