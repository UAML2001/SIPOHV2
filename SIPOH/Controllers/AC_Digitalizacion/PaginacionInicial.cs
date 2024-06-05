using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class PaginacionInicial
    {
        public void CambioIndicePagina(object sender, GridViewPageEventArgs e, GridView PDigitalizar)
        {
            int newPageIndex = e.NewPageIndex;
            GenerarIdJuzgadoPorSesion idJuzgado = new GenerarIdJuzgadoPorSesion();
            int id = idJuzgado.ObtenerIdJuzgadoDesdeSesion();

            ConsultaCargaInicial consultaCarga = new ConsultaCargaInicial();
            DataTable datos = consultaCarga.ConsultaCargaDigitalizacion(id.ToString());

            // Verifica si el nuevo índice de página está dentro del rango de páginas disponibles
            if (newPageIndex >= 0 && newPageIndex < PDigitalizar.PageCount)
            {
                PDigitalizar.PageIndex = newPageIndex;
            }

            PDigitalizar.DataSource = datos;
            PDigitalizar.DataBind();
        }
    }
}
