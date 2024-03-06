using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatDelitosController : Controller
    {
        // GET: CatDelitos
        public class DataCatDelitos
        {
            public int IdDelito { get; set; }
            public string Delito { get; set; }
        }
        public static List<DataCatDelitos> GetCatDelitos()
        {
            List<DataCatDelitos> resultados = new List<DataCatDelitos>();
            using(SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("GetCatDelitos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using(SqlDataReader readerCatDelitos = command.ExecuteReader())
                    {
                        while (readerCatDelitos.Read())
                        {
                            DataCatDelitos delito = new DataCatDelitos();
                            delito.IdDelito = int.Parse(readerCatDelitos["IdDelito"].ToString());
                            delito.Delito = readerCatDelitos["Nombre"].ToString();
                            resultados.Add(delito);
                        }
                    }
                }
            }
            return resultados;
        }
    }
}