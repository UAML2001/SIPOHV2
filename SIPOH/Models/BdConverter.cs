using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class BdConverter
    {
        /// <summary>
        /// Convierte un campo (Object) a un cadena
        /// Si el valor es nulo devuelve una cadena Vacia
        /// devuelve 0;
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static string FieldToString(object Campo)
        {
            if (Campo == DBNull.Value)
                return string.Empty;
            else
                return Campo.ToString();
        }

        /// <summary>
        /// Convierte un campo (Object) a un Entero
        /// Si el valor es nulo devuelve una cadena Vacia
        /// devuelve 0;
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static int FieldToInt(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return int.Parse(Campo.ToString());
        }

        /// <summary>
        /// Convierte un campo (Object) a un Entero
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static Int64 FieldToInt64(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (Int64)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Flotante
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static float FieldToFloat(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (float)(Double)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Double
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static Double FieldToDouble(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (Double)Campo;
        }

        public static DateTime FieldToDate(Object Campo)
        {
            if (Campo == DBNull.Value)
                return new DateTime();
            else
                return Convert.ToDateTime(Campo);
        }

        public static Boolean FieldToBoolean(Object Campo)
        {
            if (Campo == DBNull.Value)
                return false;
            else
                return Convert.ToBoolean(Campo);
        }
    }
}
