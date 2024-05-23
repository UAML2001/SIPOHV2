using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.ExpedienteDigital.Victimas.CSVictimas
{
    public class AgregarDiscapacidad
    {
            public void AgregarDiscapacidades(GridView gvDiscapacidades, DropDownList DiscaEspe, DropDownList TipoDisca, string idDiscapacidad, string discapacidad, object viewState)
            {
                // Si no se ha seleccionado una discapacidad o el dropdown está vacío, muestra un mensaje de error y retorna
                if (discapacidad == "SD" || DiscaEspe.Items.Count == 0)
                {
                    string script = "toastr.error('Por favor, seleccione una discapacidad.');";
                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "showalert", script, true);
                    return;
                }

                DataTable dt;
                if (viewState != null)
                {
                    dt = (DataTable)viewState;
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("Discapacidad");
                    dt.Columns.Add("IdDiscapacidad"); // Añadir columna para el IdDiscapacidad
                }

                // Verifica si la discapacidad ya existe en el GridView
                if (dt.AsEnumerable().Any(row => row.Field<string>("Discapacidad") == discapacidad))
                {
                    // Muestra un mensaje de error con toastr
                    string script = "toastr.error('La discapacidad ya ha sido agregada.');";
                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "showalert", script, true);
                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Discapacidad"] = discapacidad;
                    newRow["IdDiscapacidad"] = idDiscapacidad;
                    dt.Rows.Add(newRow);

                    gvDiscapacidades.DataSource = dt;
                    gvDiscapacidades.DataBind();

                    // Actualiza el ViewState
                    HttpContext.Current.Session["Discapacidades"] = dt; // Usar sesión en lugar de ViewState

                    // Reinicia los dropdowns TipoDisca y DiscaEspe
                    TipoDisca.SelectedIndex = 0;
                    DiscaEspe.SelectedIndex = 0;
                }
            }


        public void EliminarDiscapacidad(int index, GridView gvDiscapacidades, object viewState)
            {
                DataTable dt = (DataTable)viewState;
                if (dt != null && dt.Rows.Count > index)
                {
                    dt.Rows.RemoveAt(index);
                    gvDiscapacidades.DataSource = dt;
                    gvDiscapacidades.DataBind();

                    // Actualiza el ViewState
                    HttpContext.Current.Session["Discapacidades"] = dt; // Usar sesión en lugar de ViewState
                }
            }
        }
    }