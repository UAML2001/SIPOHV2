using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.ExpedienteDigital.Victimas.CSVictimas
{
    public class IdUsuarioPorSesion
    {
        public static int ObtenerIdUsuario()
        {
            // Asegúrate de que la sesión contenga el IdUsuario antes de intentar acceder a él
            if (HttpContext.Current.Session["IdUsuario"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            }
            else
            {
                // Manejar el caso en que la sesión no contenga un IdUsuario
                throw new InvalidOperationException("El IdUsuario no está presente en la sesión actual.");
            }
        }
    }
}