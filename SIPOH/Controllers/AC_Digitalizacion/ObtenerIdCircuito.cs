using CrystalDecisions.ReportAppServer.DataDefModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class ObtenerIdCircuito
    {
        public int ObtenerIdDistritoDesdeSesion()
        {
            // Asegúrate de que la clave de sesión sea la correcta
            if (HttpContext.Current.Session["IdDistrito"] != null)
            {
                // Obtiene el IdDistrito de la sesión
                int idDistrito = Convert.ToInt32(HttpContext.Current.Session["IdDistrito"]);

                return idDistrito;
            }

            // En caso de que la clave de sesión no esté presente o sea nula
            return -1; // O un valor por defecto según tu lógica de negocio
        }




    }
}