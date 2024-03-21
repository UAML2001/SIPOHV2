using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_Digitalizacion
{
    public class TipoAsunto
    {
        public void LlenarDropDownList(System.Web.UI.WebControls.DropDownList TAsunto)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TraerTipoAsunto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader ddlValues;
                    ddlValues = cmd.ExecuteReader();

                    TAsunto.DataSource = ddlValues;
                    TAsunto.DataTextField = "Asunto";
                    TAsunto.DataBind();

                    // Agrega el primer valor y lo selecciona
                    TAsunto.Items.Insert(0, new ListItem("Seleccione el tipo de asunto...", "0"));
                    TAsunto.Items[0].Selected = true;

                    con.Close();
                    con.Dispose();
                }
            }
        }

    }
}
