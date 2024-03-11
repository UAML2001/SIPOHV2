using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class LineamientosHash
    {
        public int IdNLineamientos { get; set; }
        public string Hash { get; set; }
        public bool Vigente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreArchivo { get; set; }
    }
}