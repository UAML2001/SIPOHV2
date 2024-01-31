using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatAnexosController : Controller
    {
        // GET: CatAnexos
        public class DataCatAnexos
        {
            public string descripcionAnexo { get; set; }
            public string tipoAnexo { get; set; }
            public string idAnexo { get; set; }
        }
        public static List<DataCatAnexos> GetCatAnexos()
        {
            List<DataCatAnexos> resultados = new List<DataCatAnexos>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("GetCatAnexos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader readerCatAnexos = command.ExecuteReader())
                    {
                        while (readerCatAnexos.Read()) 
                        {
                            DataCatAnexos anexo = new DataCatAnexos();
                            anexo.idAnexo = readerCatAnexos["IdAnexo"].ToString();
                            anexo.descripcionAnexo = readerCatAnexos["descripcion"].ToString();
                            anexo.tipoAnexo = readerCatAnexos["Tipo"].ToString();
                            resultados.Add(anexo);
                        }
                    }
                }
            }
            return resultados;
        }

    }
}