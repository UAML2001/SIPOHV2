using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class ExpeOficios : System.Web.UI.Page
    {
        public string NumeroExpediente { get; set; }
        public string Delitos { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Procedimiento { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string EstatusRevisionJuez { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Crear una lista de expedientes con datos ficticios
                List<ExpeOficios> expedientes = new List<ExpeOficios>
        {
            new ExpeOficios { NumeroExpediente = "CAUSA / 0001/2024", Delitos = "ROBO", TipoDocumento = "OFICIO", FechaIngreso = new DateTime(2024, 6, 17), Procedimiento = "INVESTIGACIÓN", FechaRecepcion = new DateTime(2024, 6, 18), EstatusRevisionJuez = "PENDIENTE" },
            new ExpeOficios { NumeroExpediente = "CAUSA / 0002/2024", Delitos = "FRAUDE", TipoDocumento = "OFICIO", FechaIngreso = new DateTime(2024, 6, 16), Procedimiento = "JUICIO", FechaRecepcion = new DateTime(2024, 6, 17), EstatusRevisionJuez = "REVISADO" }
            // Puedes agregar más expedientes aquí
        };

                // Asignar la lista como DataSource y enlazar los datos
                ubiExpe.DataSource = expedientes;
                ubiExpe.DataBind();
            }
        }
    }
}