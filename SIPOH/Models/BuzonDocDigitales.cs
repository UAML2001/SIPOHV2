using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class BuzonDocDigitales
    {
        public long IdDocDigital { get; set; }
        public long IdSolicitudBuzon { get; set; }
        public string RutapdfOriginal { get; set; }
        public string Nombrearchivopdf_Original { get; set; }
        public string Rutapdf_Firmado { get; set; }
        public string Nombrearchivopdf_Firmado { get; set; }
        public DateTime FeRegistro { get; set; }





    }
}