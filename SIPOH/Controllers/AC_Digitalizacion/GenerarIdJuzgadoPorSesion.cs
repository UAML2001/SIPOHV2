using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class GenerarIdJuzgadoPorSesion
    {
        public int ObtenerIdJuzgadoDesdeSesion()
        {
            // Asegúrate de que la clave de sesión sea la correcta
            if (HttpContext.Current.Session["IdJuzgado"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdJuzgado"]);
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return -1; // O un valor por defecto según tu lógica de negocio
        }
    }
}
