using Newtonsoft.Json;
using SIPOH.Controllers.AC_CatalogosCompartidos;
using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.ExpedienteDigital.Victimas.CSVictimas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIPOH
{
    public partial class ExpeDigital : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DetaOcupaVic.Enabled = false;
                TipoDisca.Enabled = false;
                DiscaEspe.Enabled = false;

                ViewState["Discapacidades"] = new DataTable();
                ((DataTable)ViewState["Discapacidades"]).Columns.Add("Discapacidad");

                if (Session["ToastrMessage"] != null && Session["ToastrType"] != null)
                {
                    ShowToastr((string)Session["ToastrMessage"], (string)Session["ToastrType"]);
                    Session["ToastrMessage"] = null;
                    Session["ToastrType"] = null;
                }

                CatalogosVictimas dropdownFiller = new CatalogosVictimas();

                dropdownFiller.DropdownGenero(GeneVicti);
                dropdownFiller.DropdownTipoVictima(TipoVict);
                dropdownFiller.DropdownTipoMoral(TipoSocie);
                dropdownFiller.DropdownContinentes(ContiNac);
                dropdownFiller.DropdownContinentes(ContiRes);
                dropdownFiller.DropdownNacionalidad(NacVicti);
                dropdownFiller.DropdownCondicMigratoria(CondMigVic);
                dropdownFiller.DropdownEstadoCivil(EstCivil);
                dropdownFiller.DropdownGradoEstudios(GradEst);
                dropdownFiller.DropdownCondicAlfabetismo(CondAlfVic);
                dropdownFiller.DropdownVulnerabilidad(VicVulne);
                dropdownFiller.DropdownPuebloIndigena(PuebloIndi);
                dropdownFiller.DropdownLenguaIndigena(LengIndi);
                dropdownFiller.DropdownOcupacion(OcupaVicti);
                dropdownFiller.DropdownDiscapacidad(TipoDisca);
                dropdownFiller.DropdownAsesorJuridico(AseJur);
                dropdownFiller.DropdownRelacion(RelacVic);
                dropdownFiller.DropdownIdentifiacion(IDVicti);
                dropdownFiller.DropdownPreguntas(HabLenExtra);
                dropdownFiller.DropdownPreguntas(HablEsp);
                dropdownFiller.DropdownPreguntas(HablLengIndi);
                dropdownFiller.DropdownPreguntas(ReqInter);


            }

        }

        protected void ContiNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContinente = Convert.ToInt32(ContiNac.SelectedValue);
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            dropdownFiller.DropdownPaises(PaisNac, idContinente);
        }

        protected void PaisNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPais = Convert.ToInt32(PaisNac.SelectedValue);
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            if (idPais == 141)
            {
                EstNaci.Items.Clear();
                MuniNac.Items.Clear();
                dropdownFiller.DropdownEntidades(EstNaci);

            }
            else
            {
                EstNaci.Items.Clear();
                MuniNac.Items.Clear();
                ListItem listItem = new ListItem();
                listItem.Text = "No aplica";
                listItem.Value = "-2";
                EstNaci.Items.Add(listItem);
                MuniNac.Items.Add(listItem);
            }
        }

        protected void EstNaci_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el IdEstado seleccionado
            string selectedIdEstado = EstNaci.SelectedValue;

            // Limpia los elementos existentes en el control DropDownList de los municipios
            MuniNac.Items.Clear();

            // Llena el control DropDownList de los municipios basado en el IdEstado seleccionado
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            dropdownFiller.DropdownMunicipios(MuniNac, selectedIdEstado);


            int idPais = Convert.ToInt32(PaisNac.SelectedValue);
            CatalogosVictimas dropdownFiller2 = new CatalogosVictimas();
            if (idPais == 141)
            {
                MuniNac.Items.Clear();
                dropdownFiller.DropdownMunicipios(MuniNac, selectedIdEstado);
            }
            else
            {
                MuniNac.Items.Clear();
                ListItem listItem = new ListItem();
                listItem.Text = "No aplica";
                listItem.Value = "-2";
                MuniNac.Items.Add(listItem);
            }
        }


        protected void ContiRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idContinente = Convert.ToInt32(ContiRes.SelectedValue);
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            dropdownFiller.DropdownPaises(PaisRes, idContinente);
        }

        protected void PaisRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPais = Convert.ToInt32(PaisRes.SelectedValue);
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            if (idPais == 141)
            {
                EstaRes.Items.Clear();
                MuniRes.Items.Clear();
                dropdownFiller.DropdownEntidades(EstaRes);
            }
            else
            {
                EstaRes.Items.Clear();
                MuniRes.Items.Clear();
                ListItem listItem = new ListItem();
                listItem.Text = "No aplica";
                listItem.Value = "-2";
                EstaRes.Items.Add(listItem);
                MuniRes.Items.Add(listItem); 
            
            }
        }
   

        protected void EstRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el IdEstado seleccionado
            string selectedIdEstado = EstaRes.SelectedValue;

            // Limpia los elementos existentes en el control DropDownList de los municipios
            MuniRes.Items.Clear();

            // Llena el control DropDownList de los municipios basado en el IdEstado seleccionado
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            dropdownFiller.DropdownMunicipios(MuniRes, selectedIdEstado);

            int idPais = Convert.ToInt32(PaisRes.SelectedValue);
            CatalogosVictimas dropdownFiller2 = new CatalogosVictimas();
            if (idPais == 141)
            {
                MuniNac.Items.Clear();
                dropdownFiller2.DropdownMunicipios(MuniRes, selectedIdEstado);
            }
            else
            {
                MuniRes.Items.Clear();
                ListItem listItem = new ListItem();
                listItem.Text = "No aplica";
                listItem.Value = "-2";
                MuniRes.Items.Add(listItem);

            }


        }

        public void DropdownOcupacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el IdOcupacion seleccionado del dropdown Ocupación
            string selectedIdOcupacion = OcupaVicti.SelectedValue;

            // Limpia los elementos existentes en el dropdown Detalle Ocupación
            DetaOcupaVic.Items.Clear();

            // Si el valor seleccionado es "SO", deshabilita el dropdown "DetaOcupaVic" y selecciona la opción "SDO"
            if (selectedIdOcupacion == "SO")
            {
                DetaOcupaVic.Enabled = false;
                DetaOcupaVic.Items.Add(new ListItem("Seleccione el detalle de la ocupación...", "SDO"));
                DetaOcupaVic.SelectedValue = "SDO";
            }
            else
            {
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                // Llama a la función para llenar el dropdown Detalle Ocupación con el IdOcupacion seleccionado
                dropdownFiller.DropdownProfesiones(DetaOcupaVic, selectedIdOcupacion);

                DetaOcupaVic.Enabled = true;
            }
        }


        protected void CuenDisca_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el valor seleccionado del dropdown CuenDisca
            string selectedValue = CuenDisca.SelectedValue;

            // Si el valor seleccionado es "S", habilita los dropdowns TipoDisca y DiscaEspe
            if (selectedValue == "S")
            {
                TipoDisca.Enabled = true;
                DiscaEspe.Enabled = true;
            }
            else
            {
                TipoDisca.Enabled = false;
                DiscaEspe.Enabled = false;
            }
        }

        public void DropdownDiscapacidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el IdOcupacion seleccionado del dropdown Ocupación
            string selectedIdDiscapacidad = TipoDisca.SelectedValue;

            // Limpia los elementos existentes en el dropdown Detalle Ocupación
            DiscaEspe.Items.Clear();

            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            // Llama a la función para llenar el dropdown Detalle Ocupación con el IdOcupacion seleccionado
            dropdownFiller.DropdownEspeDiscapacidad(DiscaEspe, selectedIdDiscapacidad);
        }

        protected void btnAgregarDiscapacidad_Click(object sender, EventArgs e)
        {
            string discapacidad = DiscaEspe.SelectedValue;
            AgregarDiscapacidad AgregarDisca = new AgregarDiscapacidad();
            AgregarDisca.AgregarDiscapacidades(gvDiscapacidades, DiscaEspe, TipoDisca, discapacidad, ViewState["Discapacidades"]);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                AgregarDiscapacidad EliminarDisca = new AgregarDiscapacidad();
                EliminarDisca.EliminarDiscapacidad(index, gvDiscapacidades, ViewState["Discapacidades"]);
            }
        }

        protected void PDigitalizar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaginacionInicial paginacion = new PaginacionInicial();
            paginacion.CambioIndicePaginaVicti(sender, e, gvVictimas);
        }

        private void ShowToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}');", true);
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            // Hacer visible el accordion
            accVictim.Style.Add("display", "block");
            btnFinales.Style.Add("display", "block");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaVictimas buscador = new BusquedaVictimas();
            (string mensaje, DataTable dt) = buscador.BuscarVictimas(TAsunto.SelectedValue, numexpe.Text);
            MostrarMensajeToastr(mensaje, mensaje.StartsWith("Se encontraron registros") ? "success" : "error");
            if (dt != null)
            {
                TablVicti.Style.Add("display", "block");
                gvVictimas.DataSource = dt;
                gvVictimas.DataBind();
            }
        }

        private void MostrarMensajeToastr(string mensaje, string tipo)
        {
            string script = $"toastr.{tipo}('{mensaje}');";
            ClientScript.RegisterStartupScript(GetType(), "toastrMessage", script, true);
        }
    }
}