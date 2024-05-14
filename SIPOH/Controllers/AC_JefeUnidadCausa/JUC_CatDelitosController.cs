using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_JefeUnidadCausa
{
    public class JUC_CatDelitosController : Controller
    {
        // Clase para representar un delito
        public class Delito
        {
            public int IdDelito { get; set; }
            public string Nombre { get; set; }
            public string Clave { get; set; }
            public int IdDelitoNorma { get; set; }
            public string DescripcionINEGI { get; set; }
            public string DelitoGenero { get; set; }
            public string TipoViolencia { get; set; }
        }

        // Método estático para obtener un delito utilizando el IdDeliAsunto
        public static Delito GetDelitoByDeliAsunto(int idDeliAsunto)
        {
            Delito delito = null;
            string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AC_JUC_CatDelitos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDeliAsunto", idDeliAsunto);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            delito = new Delito
                            {
                                IdDelito = reader.GetInt32(reader.GetOrdinal("IdDelito")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Clave = reader.GetString(reader.GetOrdinal("Clave")),
                                IdDelitoNorma = reader.GetInt32(reader.GetOrdinal("IdDelitoNorma")),
                                DescripcionINEGI = reader.GetString(reader.GetOrdinal("DescripcionINEGI")),
                                DelitoGenero = reader.GetString(reader.GetOrdinal("DelitoGenero")),
                                TipoViolencia = reader.GetString(reader.GetOrdinal("Tipoviolencia"))
                            };
                        }
                    }
                }
            }

            return delito;
        }
        public void LoadDelitoByDeliAsunto(DropDownList ddl, int idDeliAsunto)
        {
            var delito = GetDelitoByDeliAsunto(idDeliAsunto);
            if (delito != null)
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem(delito.Nombre, delito.IdDelito.ToString()));
            }
            else
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("-- No se encontró el delito --", "0"));
            }
        }
//
    }
}
