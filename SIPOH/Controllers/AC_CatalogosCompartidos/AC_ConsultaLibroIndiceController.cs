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
    public class AC_ConsultaLibroIndiceController : Controller
    {
        //
        public class LibroIndice
        {
            public string Asunto { get; set; }
            public string Numero { get; set; }
            public string Victimas { get; set; }
            public string Inculpados { get; set; }
            public string Delitos { get; set; }
            public string RelacionadoJO { get; set; }
        }

        public List<LibroIndice> ConsultaLibroIndice(int idJuzgado, string nombre, string aPaterno, string aMaterno = null)
        {
            var libroIndices = new List<LibroIndice>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_ConsultaLibroIndice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@APaterno", aPaterno);
                    cmd.Parameters.AddWithValue("@AMaterno", aMaterno ?? (object)DBNull.Value);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            libroIndices.Add(new LibroIndice
                            {
                                Asunto = reader["Asunto"].ToString(),
                                Numero = reader["Numero"].ToString(),
                                Victimas = reader["Victimas"].ToString(),
                                Inculpados = reader["Inculpados"].ToString(),
                                Delitos = reader["Delitos"].ToString(),
                                RelacionadoJO = reader["RelaionadoJO"].ToString()
                            });
                        }
                    }
                }
            }

            return libroIndices;
        }

        //
    }
}