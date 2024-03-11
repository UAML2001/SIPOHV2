using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class DatosFirmaUsuario
    {
        public string subjectName { get; set; } //: Nombre
        public string subjectEmail { get; set; } //: Correo electrónico
        public string subjectOrganization { get; set; } //: Organización a la que pertenece
        public string subjectDepartament { get; set; } //: Departamente a la que pertenece
        public string subjectState { get; set; } //: Estado donde habita
        public string subjectCountry { get; set; } //: País donde habita
        public string subjectRFC { get; set; } //: RFC
        public string subjectCURP { get; set; } //: CURP

    }
}