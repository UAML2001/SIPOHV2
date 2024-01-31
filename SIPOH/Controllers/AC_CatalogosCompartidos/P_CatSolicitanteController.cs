using DatabaseConnection;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class P_CatSolicitanteController : Controller
    {
        public class DataTipoSolicitante
        {
            public string CS_IdSolicitante { get; set; }
            public string CS_Solicitante { get; set; }
            public string CS_Tipo { get; set; }
        }
        public static List<DataTipoSolicitante> GetCatSolicitante()
        {
            List<DataTipoSolicitante> resultados = new List<DataTipoSolicitante>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetCatSolicitante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using(SqlDataReader readerCatSolicitante = command.ExecuteReader())
                    {
                        while (readerCatSolicitante.Read())
                        {
                            DataTipoSolicitante tipoSolicitante = new DataTipoSolicitante();
                            //tipoSolicitante.CS_IdSolicitante = readerCatSolicitante["IdSolicitante"].ToString();
                            tipoSolicitante.CS_Solicitante = readerCatSolicitante["Solicitante"].ToString();
                            //tipoSolicitante.CS_Tipo = readerCatSolicitante["Tipo"].ToString();
                            resultados.Add(tipoSolicitante);
                        }
                    }
                }
            }
            return resultados;
        }
        
    }
}