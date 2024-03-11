using SIPOH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH.Externo
{
    public partial class MasterExterno : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["IdUsuarioExterno"] == null)
            {
                Session.Clear();
                Response.Redirect("LoginExterno.aspx");

            }
            else
            {
                UsuarioExterno user = Page.Session["Usuario"] as UsuarioExterno;
                if (user != null)
                {
                    txtnombre.Text = user.NombreCompleto;

                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("LoginExterno.aspx");
        }
    }
    }
