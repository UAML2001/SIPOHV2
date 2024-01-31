using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_RegistroInicialJuicioOral
{
    public class InicialJuicioOralController : Controller
    {
        // GET: InicialJuicioOral
        public class DataJuicioOral
        {
            public string dNumero { get; set; }
            public int dIdJuzgado { get; set; }
            //public string dFeIngreso { get; set; } //GETDATE()
            public string dTipoAsunto { get; set; }
            public string dDigitalizado { get; set; }
            //public string dFeCaptura { get; set; } //GETDATE()
            public int dIdUsuario { get; set; }
            public int dIdAudiencia { get; set; }
            public string Observaciones { get; set; }
            public string QuienIngresa { get; set; }
            public string dMP { get; set; }
            public string Prioridad { get; set; }
            public string Fojas { get; set; }

        }

        //public static 
        //CONTROLADORES JUICIO ORAL
        public static bool ObtenerDatos(List<DataJuicioOral> objetoDataJuicioOral )
        {
            return true;
        } 
        //public static bool Get(string DataNumero, string DataTipoAsunto)
    }
}