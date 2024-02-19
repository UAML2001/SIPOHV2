using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.EJ_Storages
{
    public class Ejecucion_ConsultarCausaController : Controller
    {
        public class DataCausa
        {
            public int IdAsunto { get; set; }
            public string NumeroCausa { get; set; }
            public string NUC { get; set; }
            public string NumeroJuzgado { get; set; }
            public string NombreJuzgado { get; set; } // Nombre del juzgado por clase obtener nombre
            public string NombreOfendido { get; set; }
            public string NombreInculpado { get; set; }
            public string NombreDelito { get; set; }
        }


        private string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        public List<DataCausa> ConsultarCausa(string juzgadoSeleccionado, string numeroCausaNuc)
        {
            List<DataCausa> causas = new List<DataCausa>();
            ObtenerNombreJuzgadoPorIDController obtenerNombreJuzgado = new ObtenerNombreJuzgadoPorIDController(); // Instancia de tu clase para obtener nombres de juzgados

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Ejecucion_ConsultarCausa]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Juzgado", juzgadoSeleccionado);
                    cmd.Parameters.AddWithValue("@Numero", numeroCausaNuc);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var idJuzgado = dr["NumeroJuzgado"].ToString();
                            var juzgado = obtenerNombreJuzgado.ObtenerJuzgadoPorID(idJuzgado); // Obtiene el nombre del juzgado/
                            var nombreJuzgado = juzgado != null ? juzgado.Nombre : "Nombre no encontrado";

                            causas.Add(new DataCausa
                            {
                                IdAsunto = Convert.ToInt32(dr["IdAsunto"]),
                                NumeroCausa = dr["NumeroCausa"].ToString(),
                                NUC = dr["NUC"].ToString(),
                                NumeroJuzgado = idJuzgado,
                                NombreJuzgado = nombreJuzgado, // Establece el nombre del juzgado
                                NombreOfendido = dr["NombreOfendido"].ToString(),
                                NombreInculpado = dr["NombreInculpado"].ToString(),
                                NombreDelito = dr["NombreDelito"].ToString()
                            });
                        }
                    }
                }
            }
            return causas;
        }

    }
}