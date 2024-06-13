using SIPOH.Controllers.AC_CatalogosCompartidos;
using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.ExpedienteDigital.Victimas.CSVictimas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
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
                BindGridView();

                DetaOcupaVic.Enabled = false;
                TipoDisca.Enabled = false;
                DiscaEspe.Enabled = false;
                LengIndi.Enabled = false;

                UpVict.Visible = false;
                LimpVicti.Visible = false;
                SvVicti.Visible = false;

                accVictim.Style["display"] = "none";
                //btnFinales.Style["display"] = "none";

                ViewState["Discapacidades"] = new DataTable();
                ((DataTable)ViewState["Discapacidades"]).Columns.Add("Discapacidad");

                if (Session["ToastrMessage"] != null && Session["ToastrType"] != null)
                {
                    ShowToastr2((string)Session["ToastrMessage"], (string)Session["ToastrType"]);
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
                dropdownFiller.DropdownPreguntas(CuenFeNac);
                dropdownFiller.DropdownPreguntas(AsisMigra);
            }
        }


        protected void UpVicti_Click(object sender, EventArgs e)
        {
       
        }


        protected void SvVicti_Click(object sender, EventArgs e)
        {
            // Mapea los nombres de los campos con sus valores correspondientes
            Dictionary<string, string> campos = new Dictionary<string, string>
    {
        { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text },
        { "Nombre", NomVic.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue },
        { "Tipo Victima", TipoVict.SelectedValue },{ "Clasificacion de Victima", ClasifVicti.SelectedValue },{ "Tipo de Sector", SectorVicti.SelectedValue },
        { "Tipo Sociedad", TipoSocie.SelectedValue }, { "CURP", CURPVicti.Text }, { "RFC", RFCVicti.Text }, { "Edad", EdadVicti.Text },
        { "Conoce Fecha de Nacimiento", CuenFeNac.SelectedValue }, { "Fecha de Nacimiento", FeNacVic.Text },
        { "Continente", ContiNac.SelectedValue }, { "Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue },
        { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },
        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
        { "Habla Español", HablEsp.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
        { "Lengua Indigena", LengIndi.SelectedValue },{ "Pueblo Indigena", PuebloIndi.SelectedValue },
        { "Domicilio de trabajo", DomiTrabVicti.Text }, { "Vulnerabilidad", VicVulne.Text }, { "Asistencia Migratoria", AsisMigra.SelectedValue },
        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue },
        { "Relacion con el Imputado", RelacVic.SelectedValue }, { "Domicilio Personal", DomicPersonVicti.Text }, { "Asesor Juridico", AseJur.SelectedValue }, { "Requiere Interprete", ReqInter.SelectedValue }, { "Hora Individualizacion", HoraIndivi.Text }, { "Identificación", IDVicti.SelectedValue },
        { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text }, { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }
    };

            // Verifica si los campos requeridos están vacíos
            foreach (var campo in campos)
            {
                if (string.IsNullOrWhiteSpace(campo.Value))
                {
                    ShowToastr($"Por favor, llena el campo {campo.Key}.", "Error", "error");
                    return;
                }
                else if (campo.Value == "S")
                {
                    ShowToastr($"Por favor, selecciona un valor válido para {campo.Key}.", "Error", "error");
                    return;
                }
                // Verifica que la edad no sea mayor a 99
                else if (campo.Key == "Edad" && int.TryParse(campo.Value, out int age) && age > 99)
                {
                    ShowToastr("La edad no puede ser mayor a 99.", "Error", "error");
                    return;
                }
            }

            ConsultarIdAsunto consulta = new ConsultarIdAsunto();
            int idAsunto = consulta.GetIdAsunto(TAsunto.SelectedValue, numexpe.Text, new GenerarIdJuzgadoPorSesion().ObtenerIdJuzgadoDesdeSesion());

            // Datos para P_PartesAsunto
            string apPaterno = APVic.Text.ToUpper();
            string apMaterno = AMVic.Text.ToUpper();
            string nombre = NomVic.Text.ToUpper();
            string selectedValue = GeneVicti.SelectedValue.ToUpper();
            string genero;

            switch (selectedValue)
            {
                case "1":
                    genero = "M";
                    break;
                case "2":
                    genero = "F";
                    break;
                case "3":
                    genero = "P";
                    break;
                case "4":
                    genero = "N";
                    break;
                default:
                    genero = "Valor no reconocido";
                    break;
            }

            string tipoParte = "V";

            int tipoVictima = int.Parse(TipoVict.SelectedValue.ToUpper());
            string victima = TipoVict.SelectedValue;
            string rfc = TipoVict.SelectedValue == "1" ? RFCVicti.Text.ToUpper() : string.Empty;
            string curp = TipoVict.SelectedValue == "1" ? CURPVicti.Text.ToUpper() : string.Empty;
            string edad = TipoVict.SelectedValue == "1" ? EdadVicti.Text.ToUpper() : string.Empty;

            // En tu método existente, actualiza la lógica para manejar la fecha predeterminada
            DateTime feNacimiento = DateTime.ParseExact("09/09/1899 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            // Comprueba si el input está habilitado antes de intentar parsear la fecha
            if (FeNacVic.Enabled)
            {
                if (!DateTime.TryParse(FeNacVic.Text, out feNacimiento))
                {
                    // Maneja el error de formato aquí
                    throw new FormatException("La fecha ingresada no tiene el formato correcto.");
                }
            }
            // No es necesario un else aquí, ya que la fecha predeterminada ya está establecida

            int idContNacido = TipoVict.SelectedValue == "1" ? int.Parse(ContiNac.SelectedValue.ToUpper()) : 7;
            int idPaisNacido = TipoVict.SelectedValue == "1" ? int.Parse(PaisNac.SelectedValue.ToUpper()) : 227;
            int idEstadoNacido = TipoVict.SelectedValue == "1" ? int.Parse(EstNaci.SelectedValue.ToUpper()) : -2;
            string idMunicipioNacido = TipoVict.SelectedValue == "1" ? MuniNac.SelectedValue.ToUpper() : "-2";
            int idNacionalidad = TipoVict.SelectedValue == "1" ? int.Parse(NacVicti.SelectedValue.ToUpper()) : 99;
            int idCondicion = TipoVict.SelectedValue == "1" ? int.Parse(CondMigVic.SelectedValue.ToUpper()) : 12;
            int idEstadoCivil = TipoVict.SelectedValue == "1" ? int.Parse(EstCivil.SelectedValue.ToUpper()) : 9;
            int idGradoEstudios = TipoVict.SelectedValue == "1" ? int.Parse(GradEst.SelectedValue.ToUpper()) : -2;
            int idLengExtra = TipoVict.SelectedValue == "1" ? int.Parse(HabLenExtra.SelectedValue.ToUpper()) : 3;
            int idRelacImput = TipoVict.SelectedValue == "1" ? int.Parse(RelacVic.SelectedValue.ToUpper()) : 3;
            int idAlfabet = TipoVict.SelectedValue == "1" ? int.Parse(CondAlfVic.SelectedValue.ToUpper()) : 11;
            int idiomaEspañol = TipoVict.SelectedValue == "1" ? int.Parse(HablEsp.SelectedValue.ToUpper()) : 3;
            int idVulnerabilidad = TipoVict.SelectedValue == "1" ? int.Parse(VicVulne.SelectedValue.ToUpper()) : 99;
            int idAsisMigra = TipoVict.SelectedValue == "1" ? int.Parse(AsisMigra.SelectedValue.ToUpper()) : 3;
            int idPueblo = TipoVict.SelectedValue == "1" ? int.Parse(PuebloIndi.SelectedValue.ToUpper()) : 99;
            int hablaIndigena = TipoVict.SelectedValue == "1" ? int.Parse(HablLengIndi.SelectedValue.ToUpper()) : 3;
            int idDialecto = TipoVict.SelectedValue == "1" ? int.Parse(LengIndi.SelectedValue.ToUpper()) : 15;
            int idOcupacion = TipoVict.SelectedValue == "1" ? int.Parse(OcupaVicti.SelectedValue.ToUpper()) : -2;
            int idProfesion = TipoVict.SelectedValue == "1" ? int.Parse(DetaOcupaVic.SelectedValue.ToUpper()) : 0;
            string domOcupacion = TipoVict.SelectedValue == "1" ? DomiTrabVicti.Text.ToUpper() : string.Empty;
            int discapacidad = TipoVict.SelectedValue == "1" ? int.Parse(CuenDisca.SelectedValue.ToUpper()) : 0;
            int idContiResidencia = TipoVict.SelectedValue == "1" ? int.Parse(ContiRes.SelectedValue.ToUpper()) : 7;
            int idPaisResidencia = TipoVict.SelectedValue == "1" ? int.Parse(PaisRes.SelectedValue.ToUpper()) : 227;
            int idEstadoResidencia = TipoVict.SelectedValue == "1" ? int.Parse(EstaRes.SelectedValue.ToUpper()) : -2;
            string idMunicipioResidencia = TipoVict.SelectedValue == "1" ? MuniRes.SelectedValue.ToUpper() : "-2";
            string domResidencia = TipoVict.SelectedValue == "1" ? DomicPersonVicti.Text.ToUpper() : string.Empty;
            int idDefensor = TipoVict.SelectedValue == "1" ? int.Parse(AseJur.SelectedValue.ToUpper()) : 9;
            int interprete = TipoVict.SelectedValue == "1" ? int.Parse(ReqInter.SelectedValue.ToUpper()) : 3;
            int ordenProteccion = 0;
            DateTime feIndividualizacion = TipoVict.SelectedValue == "1" ? DateTime.Parse(HoraIndivi.Text.ToUpper()) : DateTime.Now;
            int idDocIdentificador = TipoVict.SelectedValue == "1" ? int.Parse(IDVicti.SelectedValue.ToUpper()) : 0;
            string numDocumento = "752552";
            string privacidad = TipoVict.SelectedValue == "1" ? AceptaDatos.SelectedValue.ToUpper() : string.Empty;
            string telefono = TipoVict.SelectedValue == "1" ? TelCont.Text.ToUpper() : string.Empty;
            string correo = TipoVict.SelectedValue == "1" ? EmailCont.Text.ToUpper() : string.Empty;
            string fax = TipoVict.SelectedValue == "1" ? Fax.Text.ToUpper() : string.Empty;
            string domNotificacion = TipoVict.SelectedValue == "1" ? DomicPersonVicti.Text.ToUpper() : string.Empty;
            string otroTipo = "OtroTipo";
            int idUser = IdUsuarioPorSesion.ObtenerIdUsuario();

            List<string> idsDiscapacidades = ObtenerIdsDiscapacidades();

            InsertarVictima insertador = new InsertarVictima();
            insertador.InsertVictimData(
                idAsunto, apPaterno, apMaterno, nombre, genero, tipoParte, tipoVictima, victima, rfc, curp, edad, feNacimiento, idContNacido, idPaisNacido,
                idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                idGradoEstudios, idLengExtra, idRelacImput, idAlfabet, idiomaEspañol, idVulnerabilidad, idAsisMigra, idPueblo, hablaIndigena,
                idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idContiResidencia, idPaisResidencia,
                idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this.Page
            );
        }


        protected void AgregarVicitmaForm_Click(object sender, EventArgs e)
        {
            accVictim.Style["display"] = "block";

            ReiniciarFormulario reiniciarFormulario = new ReiniciarFormulario();
            reiniciarFormulario.Reiniciar(UpVict, LimpVicti, SvVicti, SectorVicti, TipoSocie, APVic, AMVic, NomVic, GeneVicti, ClasifVicti, CURPVicti, RFCVicti, FeNacVic, EdadVicti, ContiNac,
                PaisNac, EstNaci, MuniNac, NacVicti, HabLenExtra, HablEsp, LengIndi, VicVulne, CondMigVic, CondAlfVic, HablLengIndi, PuebloIndi, DomiTrabVicti, EstCivil, GradEst, OcupaVicti,
                DetaOcupaVic, CuenDisca, TipoDisca, DiscaEspe, ContiRes, PaisRes, EstaRes, MuniRes, DomicPersonVicti, AseJur, ReqInter, TelCont, EmailCont, Fax, RelacVic, HoraIndivi, IDVicti,
                Domici, OtroMed, AceptaDatos, AsisMigra);
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {
            BusquedaVictimas buscador = new BusquedaVictimas();
            (string mensaje, DataTable dt) = buscador.BuscarVictimas(TAsunto.SelectedValue, numexpe.Text);
            ShowToastr2(mensaje, mensaje.StartsWith("Se encontraron registros") ? "success" : "error");
            if (dt != null)
            {
                buscarVicti.Style.Add("display", "block");
                TablVicti.Style.Add("display", "block");
                accVictim.Style.Add("display", "none");
                gvVictimas.DataSource = dt;
                gvVictimas.DataBind();
            }
        }

        protected void TipoVict_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambioTipoVictima handler = new CambioTipoVictima();
            handler.TipoVictChanged(sender, e, TipoVict, SectorVicti, TipoSocie, AMVic, NomVic, GeneVicti, ClasifVicti, CURPVicti, RFCVicti, FeNacVic, EdadVicti, ContiNac,
                PaisNac, EstNaci, MuniNac, NacVicti, HabLenExtra, HablEsp, LengIndi, VicVulne, CondMigVic, CondAlfVic, HablLengIndi, PuebloIndi, DomiTrabVicti, EstCivil, GradEst, OcupaVicti,
                DetaOcupaVic, CuenDisca, TipoDisca, DiscaEspe, ContiRes, PaisRes, EstaRes, MuniRes, DomicPersonVicti, AseJur, ReqInter, TelCont, EmailCont, Fax, RelacVic, HoraIndivi, IDVicti,
                Domici, OtroMed, AceptaDatos, AsisMigra);
        }

        protected void gvVictimas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra los controles HiddenField en la fila
                HiddenField hidIdAsunto = (HiddenField)e.Row.FindControl("hidIdAsunto");
                HiddenField hidIdPartes = (HiddenField)e.Row.FindControl("hidIdPartes");

                // Obtén los valores necesarios para esta fila y asígnalos a los controles HiddenField
                int idAsunto = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdAsunto"));
                int idPartes = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdPartes"));

                hidIdAsunto.Value = idAsunto.ToString();
                hidIdPartes.Value = idPartes.ToString();
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            // Obtén el CheckBox que disparó el evento
            CheckBox chkSelect = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkSelect.NamingContainer;

            // Verifica si el CheckBox está seleccionado
            bool isChecked = chkSelect.Checked;

            // Si está seleccionado, obtener y mostrar la información, de lo contrario ocultar el formulario
            if (isChecked)
            {
                int idAsunto = Convert.ToInt32(((HiddenField)row.FindControl("hidIdAsunto")).Value);
                int idPartes = Convert.ToInt32(((HiddenField)row.FindControl("hidIdPartes")).Value);

                // Guardar idPartes en una variable de sesión
                Session["idPartes"] = idPartes;

                // Llamar al método que ejecuta el procedimiento almacenado y llena los elementos del formulario
                LlenarFormulario(idAsunto, idPartes);

                UpVict.Visible = true;
                LimpVicti.Visible = true;
                SvVicti.Visible = false;
            }
            else
            {
            }
        }

        private void LlenarFormulario(int idAsunto, int idPartes)
        {

        }


        protected void ContiNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContiNac.SelectedValue != "S")
            {
                int idContinente = Convert.ToInt32(ContiNac.SelectedValue);
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                PaisNac.SelectedValue = "S";
                EstNaci.Items.Clear();
                EstNaci.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstNaci.SelectedValue = "S";
                MuniNac.Items.Clear();
                MuniNac.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniNac.SelectedValue = "S";
                dropdownFiller.DropdownPaises(PaisNac, idContinente);
            }
            else
            {
                PaisNac.SelectedValue = "S";
                PaisNac.Items.Clear();
                PaisNac.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstNaci.Items.Clear();
                EstNaci.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstNaci.SelectedValue = "S";
                MuniNac.Items.Clear();
                MuniNac.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniNac.SelectedValue = "S";
            }
        }

        protected void PaisNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PaisNac.SelectedValue != "S")
            {
                int idPais = Convert.ToInt32(PaisNac.SelectedValue);
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                EstNaci.Items.Clear();
                EstNaci.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstNaci.SelectedValue = "S";
                MuniNac.Items.Clear();
                MuniNac.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniNac.SelectedValue = "S";

                if (idPais == 141)
                {
                    dropdownFiller.DropdownEntidades(EstNaci);
                }
                else
                {
                    ListItem listItem = new ListItem("No aplica", "-2");
                    EstNaci.Items.Add(listItem);
                    MuniNac.Items.Add(listItem);
                }
            }
        }

        protected void EstNaci_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstNaci.SelectedValue != "S")
            {
                string selectedIdEstado = EstNaci.SelectedValue;
                MuniNac.Items.Clear();
                MuniNac.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniNac.SelectedValue = "S";

                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                int idPais = Convert.ToInt32(PaisNac.SelectedValue);

                if (idPais == 141)
                {
                    dropdownFiller.DropdownMunicipios(MuniNac, selectedIdEstado);
                }
                else
                {
                    ListItem listItem = new ListItem("No aplica", "-2");
                    MuniNac.Items.Add(listItem);
                }
            }
        }

        // Repite el mismo patrón para los otros eventos ContiRes_SelectedIndexChanged, PaisRes_SelectedIndexChanged, y EstRes_SelectedIndexChanged

        protected void ContiRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContiRes.SelectedValue != "S")
            {
                int idContinente = Convert.ToInt32(ContiRes.SelectedValue);
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                PaisRes.SelectedValue = "S";
                EstaRes.Items.Clear();
                EstaRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstaRes.SelectedValue = "S";
                MuniRes.Items.Clear();
                MuniRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniRes.SelectedValue = "S";
                dropdownFiller.DropdownPaises(PaisRes, idContinente);
            }
            else
            {
                PaisRes.SelectedValue = "S";
                PaisRes.Items.Clear();
                PaisRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstaRes.Items.Clear();
                EstaRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstaRes.SelectedValue = "S";
                MuniRes.Items.Clear();
                MuniRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniRes.SelectedValue = "S";
            }
        }

        protected void PaisRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PaisRes.SelectedValue != "S")
            {
                int idPais = Convert.ToInt32(PaisRes.SelectedValue);
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                EstaRes.Items.Clear();
                EstaRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                EstaRes.SelectedValue = "S";
                MuniRes.Items.Clear();
                MuniRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniRes.SelectedValue = "S";

                if (idPais == 141)
                {
                    dropdownFiller.DropdownEntidades(EstaRes);
                }
                else
                {
                    ListItem listItem = new ListItem("No aplica", "-2");
                    EstaRes.Items.Add(listItem);
                    MuniRes.Items.Add(listItem);
                }
            }
        }

        protected void EstRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstaRes.SelectedValue != "S")
            {
                string selectedIdEstado = EstaRes.SelectedValue;
                MuniRes.Items.Clear();
                MuniRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                MuniRes.SelectedValue = "S";

                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                int idPais = Convert.ToInt32(PaisRes.SelectedValue);

                if (idPais == 141)
                {
                    dropdownFiller.DropdownMunicipios(MuniRes, selectedIdEstado);
                }
                else
                {
                    ListItem listItem = new ListItem("No aplica", "-2");
                    MuniRes.Items.Add(listItem);
                }
            }
        }

        public void DropdownOcupacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el IdOcupacion seleccionado del dropdown Ocupación
            string selectedIdOcupacion = OcupaVicti.SelectedValue;

            // Limpia los elementos existentes en el dropdown Detalle Ocupación
            DetaOcupaVic.Items.Clear();

            // Si el valor seleccionado es "S", deshabilita el dropdown Detalle Ocupación y asigna el valor "S" seleccionado
            if (selectedIdOcupacion == "S")
            {
                DetaOcupaVic.Enabled = false;
                DetaOcupaVic.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                DetaOcupaVic.SelectedValue = "S";
            }
            else
            {
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();

                // Llama a la función para llenar el dropdown Detalle Ocupación con el IdOcupacion seleccionado
                dropdownFiller.DropdownProfesiones(DetaOcupaVic, selectedIdOcupacion);

                // Habilita el dropdown Detalle Ocupación
                DetaOcupaVic.Enabled = true;

                // Selecciona el valor "S" por defecto
                DetaOcupaVic.Items.Insert(0, new ListItem("----- SELECCIONE -----", "S"));
                DetaOcupaVic.SelectedValue = "S";
            }
        }



        protected void CuenDisca_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el valor seleccionado del dropdown CuenDisca
            string selectedValue = CuenDisca.SelectedValue;

            // Si el valor seleccionado es "1"
            if (selectedValue == "1")
            {
                TipoDisca.Enabled = true;
                DiscaEspe.Enabled = true;

                // Establece los valores de los dropdowns TipoDisca y DiscaEspe en "S"
                TipoDisca.SelectedValue = "S";
                DiscaEspe.SelectedValue = "S";

                // Llama a la función para llenar el dropdown DiscaEspe con opciones correspondientes al valor "S" seleccionado en TipoDisca
                DropdownDiscapacidad_SelectedIndexChanged(sender, e);
            }
            else
            {
                // Si el valor seleccionado es diferente de "1", deshabilita los dropdowns TipoDisca y DiscaEspe
                TipoDisca.Enabled = false;
                DiscaEspe.Enabled = false;

                // Limpia los valores seleccionados en los dropdowns TipoDisca y DiscaEspe
                TipoDisca.ClearSelection();
                DiscaEspe.ClearSelection();
            }
        }

        public void DropdownDiscapacidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el IdOcupacion seleccionado del dropdown Ocupación
            string selectedIdDiscapacidad = TipoDisca.SelectedValue;

            // Limpia los elementos existentes en el dropdown Detalle Ocupación
            DiscaEspe.Items.Clear();

            // Agrega el elemento de selección "----- SELECCIONE -----" con valor "S"
            DiscaEspe.Items.Add(new ListItem("----- SELECCIONE -----", "S"));

            // Selecciona siempre "S" en el dropdown DiscaEspe
            DiscaEspe.SelectedValue = "S";

            if (!string.IsNullOrEmpty(selectedIdDiscapacidad) && selectedIdDiscapacidad != "S")
            {
                CatalogosVictimas dropdownFiller = new CatalogosVictimas();
                // Llama a la función para llenar el dropdown Detalle Ocupación con el IdOcupacion seleccionado
                dropdownFiller.DropdownEspeDiscapacidad(DiscaEspe, selectedIdDiscapacidad);
            }
        }

        protected void btnAgregarDiscapacidad_Click(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del dropdown DiscaEspe
            string idDiscapacidad = DiscaEspe.SelectedValue;

            // Verificar si el valor seleccionado es "S"
            if (idDiscapacidad == "S")
            {
                // Generar el script de toastr para mostrar el mensaje de error
                string script = "toastr.error('Por favor, seleccione una discapacidad válida.');";
                // Registrar el script en el cliente
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrError", script, true);
                return; // Salir del método para evitar la inserción
            }

            // Obtener el texto del dropdown DiscaEspe
            string discapacidad = DiscaEspe.SelectedItem.Text;

            // Llamar al método para agregar discapacidad
            AgregarDiscapacidad AgregarDisca = new AgregarDiscapacidad();
            AgregarDisca.AgregarDiscapacidades(gvDiscapacidades, DiscaEspe, TipoDisca, idDiscapacidad, discapacidad, HttpContext.Current.Session["Discapacidades"]);

            // Limpiar la selección actual y seleccionar "S" con el texto "----- SELECCIONE -----" después de agregar la discapacidad
            DiscaEspe.ClearSelection();
            DiscaEspe.Items.Insert(0, new ListItem("----- SELECCIONE -----", "S"));
            DiscaEspe.SelectedValue = "S";
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                AgregarDiscapacidad EliminarDisca = new AgregarDiscapacidad();
                EliminarDisca.EliminarDiscapacidad(index, gvDiscapacidades, HttpContext.Current.Session["Discapacidades"]);
            }
        }


        protected void HabLenIndi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HablLengIndi.SelectedValue == "1")
            {
                LengIndi.Enabled = true;
            }
            else
            {
                // Si no se selecciona "1", establece el valor predeterminado "15" y deshabilita el control
                LengIndi.SelectedValue = "15";
                LengIndi.Enabled = false;
            }

            if (HablLengIndi.SelectedValue == "S")
            {
                // Si se selecciona "S", establece el valor "S" y deshabilita el control
                LengIndi.SelectedValue = "S";
                LengIndi.Enabled = false;
            }
        }
        private void BindGridView()
        {
            // Obtén los parámetros necesarios para la búsqueda.
            string tipoAsunto = TAsunto.SelectedValue; // Reemplaza esto con el valor real
            string numeroExpediente = numexpe.Text; // Reemplaza esto con el valor real

            BusquedaVictimas busqueda = new BusquedaVictimas();
            var resultado = busqueda.BuscarVictimas(tipoAsunto, numeroExpediente);

            if (resultado.dt != null)
            {
                gvVictimas.DataSource = resultado.dt;
                gvVictimas.DataBind();
            }
            else
            {
                // Manejar el caso cuando no se encuentran registros
                gvVictimas.DataSource = new DataTable();
                gvVictimas.DataBind();
            }
        }

        protected void gvVictimas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVictimas.PageIndex = e.NewPageIndex;
            BindGridView();
        }


        private List<string> ObtenerIdsDiscapacidades()
        {
            List<string> idsDiscapacidades = new List<string>();
            DataTable dt = (DataTable)HttpContext.Current.Session["Discapacidades"];
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    idsDiscapacidades.Add(row["IdDiscapacidad"].ToString());
                }
            }
            return idsDiscapacidades;
        }

        private void ShowToastr(string message, string title, string type)
        {
            // Asegúrate de tener Toastr en tu proyecto y de haberlo importado correctamente
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}', '{title}');", true);
        }

        private void ShowToastr2(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Toastr", $"toastr.{type}('{message}');", true);
        }

        public void MostrarPDFInsertar(string pdfPath)
        {
            AgregadoExito.Visible = true;
            CedulaExito.Visible = true;
            cedulaimput.Attributes["src"] = pdfPath;
            //PanelPdfInsertar.Visible = true;
            panelPdfInsertar.Style["display"] = "block";
            GenerarOtro.Visible = true;

            btnBuscar.Enabled = false;
            numexpe.Enabled = false;
            TAsunto.Enabled = false;

            TablVicti.Style["display"] = "none";
            accVictim.Style["display"] = "none";

            UpVict.Visible = false;
            SvVicti.Visible = false;
            LimpVicti.Visible = false;
        }

        public void MostrarPDFActualizar(string pdfPath)
        {
            ActExito.Visible = true;
            CedulaActExito.Visible = true;
            cedulaimputAct.Attributes["src"] = pdfPath;
            //PanelPdfActualizar.Visible = true;
            panelPdfActualizar.Style["display"] = "block";
            GenerarOtroImp.Visible = true;

            btnBuscar.Enabled = false;
            numexpe.Enabled = false;
            TAsunto.Enabled = false;

            TablVicti.Style["display"] = "none";
            accVictim.Style["display"] = "none";

            UpVict.Visible = false;
            SvVicti.Visible = false;
            LimpVicti.Visible = false;
        }

        public void MostrarCedula(string pdfPath)
        {
            GenCedula.Visible = true;
            OcultCedula.Visible = true;
            CedulaSinInsert.Attributes["src"] = pdfPath;
            //PanelPdfActualizar.Visible = true;
            panelPdfMostrar.Style["display"] = "block";

        }

        protected void GenerarOtro_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void EstablecerValorDropDownList(DropDownList dropDownList, string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                dropDownList.ClearSelection();
            }
            else
            {
                dropDownList.SelectedValue = valor;
            }
        }

        // Método auxiliar para establecer el valor o dejar el TextBox vacío según la disponibilidad del valor
        private void EstablecerValorTextBox(TextBox textBox, string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                textBox.Text = string.Empty;
            }
            else
            {
                textBox.Text = valor;
            }
        }

        protected void LimpiarForm_Click(object sender, EventArgs e)
        {
            accVictim.Style["display"] = "block";

            LimpiarFormularioVictima reiniciarFormulario = new LimpiarFormularioVictima();
            reiniciarFormulario.Reiniciar(UpVict, LimpVicti, SvVicti, APVic, AMVic, NomVic, GeneVicti, CURPVicti, RFCVicti, FeNacVic, EdadVicti, ContiNac,
                PaisNac, EstNaci, MuniNac, NacVicti, HabLenExtra, HablEsp, LengIndi, CondMigVic, CondAlfVic, HablLengIndi, PuebloIndi, DomiTrabVicti, EstCivil, GradEst, OcupaVicti,
                DetaOcupaVic, CuenDisca, TipoDisca, DiscaEspe, ContiRes, PaisRes, EstaRes, MuniRes, DomicPersonVicti, AseJur, ReqInter, TelCont, EmailCont, Fax, RelacVic, HoraIndivi, IDVicti,
                Domici, OtroMed, AceptaDatos, AsisMigra);
        }

        protected void OcultCedula_Click(object sender, EventArgs e)
        {
            GenCedula.Visible = false;
            OcultCedula.Visible = false;
            CedulaSinInsert.Style["display"] = "none";
            panelPdfMostrar.Style["display"] = "none";
        }

        protected void CuenFeNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Comprueba si el valor seleccionado es '3' o '2'
            if (CuenFeNac.SelectedValue == "3" || CuenFeNac.SelectedValue == "2")
            {
                // Deshabilita el input y establece el valor predeterminado
                FeNacVic.Enabled = false;
                // Usa "HH" para el formato de 24 horas
                FeNacVic.Text = DateTime.ParseExact("09/09/1899", "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                // Habilita el input si se selecciona otro valor
                FeNacVic.Enabled = true;
            }

        }

        protected void ImprCedula_Click1(object sender, EventArgs e)
        {
            // Recuperar idPartes de la variable de sesión
            if (Session["idPartes"] != null)
            {
                int idPartes = (int)Session["idPartes"];

                // Mapea los nombres de los campos con sus valores correspondientes
                Dictionary<string, string> campos = new Dictionary<string, string>
                    {
                        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text },
                        { "Nombre", NomVic.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue }, { "RFC", RFCVicti.Text },
                        { "CURP", CURPVicti.Text }, { "Edad", EdadVicti.Text }, { "Fecha de Nacimiento", FeNacVic.Text }, { "Continente", ContiNac.SelectedValue },
                        { "Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue }, { "Nacionalidad", NacVicti.SelectedValue },
                        { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },
                        { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Habla Español", HablEsp.SelectedValue }, { "Pueblo Indigena", PuebloIndi.SelectedValue },
                        { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Lengua Indigena", LengIndi.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue },
                        { "Profesion", DetaOcupaVic.SelectedValue }, { "Domicilio de trabajo", DomiTrabVicti.Text }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
                        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue },
                        { "Municipio de Residencia", MuniRes.SelectedValue }, { "Domicilio Personal", DomicPersonVicti.Text }, { "Asesor Juridico", AseJur.SelectedValue },
                        { "Requiere Interprete", ReqInter.SelectedValue }, { "Identificación", IDVicti.SelectedValue },{ "Asistencia Migratoria", AsisMigra.SelectedValue },
                        { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text },
                        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Relacion con el Imputado", RelacVic.SelectedValue }, 
                        // Agrega tantos campos como necesites...
                    };


                // Obtén el valor seleccionado en el dropdown "TipoVict"
                string tipoVict = TipoVict.SelectedValue;

                // Si el valor seleccionado en "TipoVict" NO es "2", "3", "4" o "5", verifica los campos
                if (tipoVict != "2" && tipoVict != "3" && tipoVict != "4" && tipoVict != "5")
                {
                    // Verifica si los campos requeridos están vacíos
                    foreach (var campo in campos)
                    {
                        if (string.IsNullOrWhiteSpace(campo.Value))
                        {
                            ShowToastr($"Por favor, llena el campo {campo.Key}.", "Error", "error");
                            return;
                        }
                    }
                    foreach (var dropdown in campos)
                    {
                        // Verifica si el valor seleccionado es "S"
                        if (dropdown.Value == "S")
                        {
                            ShowToastr($"Por favor, selecciona un valor válido para {dropdown.Key}.", "Error", "error");
                            return;
                        }
                    }
                }

                ConsultarIdAsunto consulta = new ConsultarIdAsunto();
                int idAsunto = consulta.GetIdAsunto(TAsunto.SelectedValue, numexpe.Text, new GenerarIdJuzgadoPorSesion().ObtenerIdJuzgadoDesdeSesion());

                // Datos para P_PartesAsunto
                string apPaterno = APVic.Text.ToUpper();
                string apMaterno = AMVic.Text.ToUpper();
                string nombre = NomVic.Text.ToUpper();
                string selectedValue = GeneVicti.SelectedValue.ToUpper();
                string genero;

                switch (selectedValue)
                {
                    case "1":
                        genero = "M";
                        break;
                    case "2":
                        genero = "F";
                        break;
                    case "3":
                        genero = "P";
                        break;
                    case "4":
                        genero = "N";
                        break;
                    default:
                        genero = "Valor no reconocido";
                        break;
                }

                string tipoParte = "V";

                // Datos para P_Victima
                int tipoVictima = TipoVict.SelectedValue == "1" ? int.Parse(TipoVict.SelectedValue.ToUpper()) : 0;
                string victima = TipoVict.SelectedValue;
                string rfc = TipoVict.SelectedValue == "1" ? RFCVicti.Text.ToUpper() : string.Empty;
                string curp = TipoVict.SelectedValue == "1" ? CURPVicti.Text.ToUpper() : string.Empty;
                string edad = TipoVict.SelectedValue == "1" ? EdadVicti.Text.ToUpper() : string.Empty;
                // En tu método existente, actualiza la lógica para manejar la fecha predeterminada
                DateTime feNacimiento = DateTime.ParseExact("09/09/1899 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                // Comprueba si el input está habilitado antes de intentar parsear la fecha
                if (FeNacVic.Enabled)
                {
                    if (!DateTime.TryParse(FeNacVic.Text, out feNacimiento))
                    {
                        // Maneja el error de formato aquí
                        throw new FormatException("La fecha ingresada no tiene el formato correcto.");
                    }
                }
                // No es necesario un else aquí, ya que la fecha predeterminada ya está establecida

                int idContNacido = TipoVict.SelectedValue == "1" ? int.Parse(ContiNac.SelectedValue.ToUpper()) : 7;
                int idPaisNacido = TipoVict.SelectedValue == "1" ? int.Parse(PaisNac.SelectedValue.ToUpper()) : 227;
                int idEstadoNacido = TipoVict.SelectedValue == "1" ? int.Parse(EstNaci.SelectedValue.ToUpper()) : -2;
                string idMunicipioNacido = TipoVict.SelectedValue == "1" ? MuniNac.SelectedValue.ToUpper() : "-2";
                int idNacionalidad = TipoVict.SelectedValue == "1" ? int.Parse(NacVicti.SelectedValue.ToUpper()) : 99;
                int idCondicion = TipoVict.SelectedValue == "1" ? int.Parse(CondMigVic.SelectedValue.ToUpper()) : 12;
                int idEstadoCivil = TipoVict.SelectedValue == "1" ? int.Parse(EstCivil.SelectedValue.ToUpper()) : 9;
                int idGradoEstudios = TipoVict.SelectedValue == "1" ? int.Parse(GradEst.SelectedValue.ToUpper()) : -2;
                int idLengExtra = TipoVict.SelectedValue == "1" ? int.Parse(HabLenExtra.SelectedValue.ToUpper()) : 3;
                int idRelacImput = TipoVict.SelectedValue == "1" ? int.Parse(RelacVic.SelectedValue.ToUpper()) : 11;
                int idAlfabet = TipoVict.SelectedValue == "1" ? int.Parse(CondAlfVic.SelectedValue.ToUpper()) : 9;
                int idiomaEspañol = TipoVict.SelectedValue == "1" ? int.Parse(HablEsp.SelectedValue.ToUpper()) : 3;
                int idVulnerabilidad = TipoVict.SelectedValue == "1" ? int.Parse(VicVulne.SelectedValue.ToUpper()) : 99;
                int idAsisMigra = TipoVict.SelectedValue == "1" ? int.Parse(AsisMigra.SelectedValue.ToUpper()) : 3;
                int idPueblo = TipoVict.SelectedValue == "1" ? int.Parse(PuebloIndi.SelectedValue.ToUpper()) : 99;
                int hablaIndigena = TipoVict.SelectedValue == "1" ? int.Parse(HablLengIndi.SelectedValue.ToUpper()) : 3;
                int idDialecto = TipoVict.SelectedValue == "1" ? int.Parse(LengIndi.SelectedValue.ToUpper()) : 15;
                int idOcupacion = TipoVict.SelectedValue == "1" ? int.Parse(OcupaVicti.SelectedValue.ToUpper()) : -2;
                int idProfesion = TipoVict.SelectedValue == "1" ? int.Parse(DetaOcupaVic.SelectedValue.ToUpper()) : 0;
                string domOcupacion = TipoVict.SelectedValue == "1" ? DomiTrabVicti.Text.ToUpper() : string.Empty;
                int discapacidad = TipoVict.SelectedValue == "1" ? int.Parse(CuenDisca.SelectedValue.ToUpper()) : 0;
                int idContiResidencia = TipoVict.SelectedValue == "1" ? int.Parse(ContiRes.SelectedValue.ToUpper()) : 7;
                int idPaisResidencia = TipoVict.SelectedValue == "1" ? int.Parse(PaisRes.SelectedValue.ToUpper()) : 227;
                int idEstadoResidencia = TipoVict.SelectedValue == "1" ? int.Parse(EstaRes.SelectedValue.ToUpper()) : -2;
                string idMunicipioResidencia = TipoVict.SelectedValue == "1" ? MuniRes.SelectedValue.ToUpper() : "-2";
                string domResidencia = TipoVict.SelectedValue == "1" ? DomicPersonVicti.Text.ToUpper() : string.Empty;
                int idDefensor = TipoVict.SelectedValue == "1" ? int.Parse(AseJur.SelectedValue.ToUpper()) : 9;
                int interprete = TipoVict.SelectedValue == "1" ? int.Parse(ReqInter.SelectedValue.ToUpper()) : 3;
                int ordenProteccion = 0;
                DateTime feIndividualizacion = TipoVict.SelectedValue == "1" ? DateTime.Parse(HoraIndivi.Text.ToUpper()) : DateTime.Now;
                int idDocIdentificador = TipoVict.SelectedValue == "1" ? int.Parse(IDVicti.SelectedValue.ToUpper()) : 0;
                string numDocumento = "752552";
                string privacidad = TipoVict.SelectedValue == "1" ? AceptaDatos.SelectedValue.ToUpper() : string.Empty;
                string telefono = TipoVict.SelectedValue == "1" ? TelCont.Text.ToUpper() : string.Empty;
                string correo = TipoVict.SelectedValue == "1" ? EmailCont.Text.ToUpper() : string.Empty;
                string fax = TipoVict.SelectedValue == "1" ? Fax.Text.ToUpper() : string.Empty;
                string domNotificacion = TipoVict.SelectedValue == "1" ? DomicPersonVicti.Text.ToUpper() : string.Empty;
                string otroTipo = "OtroTipo";
                int idUser = IdUsuarioPorSesion.ObtenerIdUsuario();

                // Datos para P_Discapacidad
                // Obtener los IDs de las discapacidades agregadas en el GridView
                List<string> idsDiscapacidades = ObtenerIdsDiscapacidades();

                MostrarCedulaVictima insertador = new MostrarCedulaVictima();
                insertador.MostrarCedula(
                    idAsunto, idPartes, apPaterno, apMaterno, nombre, genero, tipoParte, rfc, curp, edad, feNacimiento, idContNacido, idPaisNacido,
                    idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                    idGradoEstudios, idAlfabet, idiomaEspañol, idPueblo, hablaIndigena,
                    idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idLengExtra, idRelacImput, idAsisMigra, idContiResidencia, idPaisResidencia,
                    idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                    ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                    telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this.Page
                );

            }
        }
    }
}
