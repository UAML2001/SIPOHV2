using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatEjecucion_CatAnexosController : Controller
    {

        public class DataAnexo
        {
            public string Descripcion { get; set; }
            public string Valor { get; set; }
        }
        public List<DataAnexo> ObtenerAnexos()
        {
            List<DataAnexo> anexos = new List<DataAnexo>();
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM P_EjecucionCatAnexos WHERE Tipo = 'B' OR Descripcion = 'OTRO' ORDER BY CASE WHEN Descripcion = 'OTRO' THEN 1 ELSE 0 END, Descripcion", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            anexos.Add(new DataAnexo
                            {
                                Descripcion = dr["Descripcion"].ToString(),
                                Valor = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return anexos;
        }

    }
}