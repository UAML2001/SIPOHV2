using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatTipoSolicitudController;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class CatTipoSolicitudController : Controller
    {
        // GET: CatTipoSolicitud
        public class DataTipoAudiencia
        {
            //CAA: P_CatAudiAsunto
            //CA: P_CatAudiencias
            public string CA_IdAudiencia { get; set; }
            public string CAA_IdAudi { get; set; }
            public string CAA_Descripcion { get; set; }
        }

        public static List<DataTipoAudiencia> GetCatTipoSolicitud()
        {
            List<DataTipoAudiencia> resultados = new List<DataTipoAudiencia>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetTipoSolicitud", connection))
                {
                    command.Parameters.AddWithValue("@TipoAsunto", "JO");
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader readerCatTipoAudi = command.ExecuteReader())
                    {
                        while (readerCatTipoAudi.Read())
                        {
                            DataTipoAudiencia tipoAudiencia = new DataTipoAudiencia();
                            tipoAudiencia.CA_IdAudiencia = readerCatTipoAudi["IdAudiencia"].ToString();
                            tipoAudiencia.CAA_Descripcion = readerCatTipoAudi["Descripcion"].ToString();
                            tipoAudiencia.CAA_IdAudi = readerCatTipoAudi["IdAudi"].ToString();
                            resultados.Add(tipoAudiencia);

                        }
                    }
                }
            }
            return resultados;
        }
    }
}