using SIPOH.Controllers.AC_CatalogosCompartidos;
using SIPOH.Controllers.AC_Digitalizacion;
using SIPOH.ExpedienteDigital.Victimas.CSVictimas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            }
        }

        protected void SvVicti_Click(object sender, EventArgs e)
        {
            // Mapea los nombres de los campos con sus valores correspondientes
            Dictionary<string, string> campos = new Dictionary<string, string>
        {
        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text }, { "Nombre", NomVic.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue},
        { "Tipo de Victima", TipoVict.SelectedValue }, { "RFC", RFCVicti.Text }, { "CURP", CURPVicti.Text }, { "Edad", EdadVicti.Text }, { "Fecha de Nacimiento", FeNacVic.Text }, { "Continente", ContiNac.SelectedValue },
        { "Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue }, { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue },
        { "Estadio Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Habla Español", HablEsp.SelectedValue }, { "Vulnerabilidad", VicVulne.SelectedValue },
        { "Pueblo Indigena", PuebloIndi.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Lengua Indigena", LengIndi.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
        { "Domicilio de trabajo", DomiTrabVicti.Text }, { "Cuenta con discapacidad", CuenDisca.SelectedValue }, { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue },
        { "Municipio de Residencia", MuniRes.SelectedValue }, { "Domicilio Personal", DomicPersonVicti.Text }, { "Asesor Juridico", AseJur.SelectedValue }, { "Requiere Interprete", ReqInter.SelectedValue }, { "Hora de individualizacion", HoraIndivi.Text },
        { "Identificación", IDVicti.SelectedValue }, { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text },{ "Fax", Fax.Text },
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
            int tipoVictima = int.Parse(TipoVict.SelectedValue.ToUpper());
            string victima = TipoVict.SelectedValue;
            string rfc = TipoVict.SelectedValue == "1" ? RFCVicti.Text.ToUpper() : string.Empty;
            string curp = TipoVict.SelectedValue == "1" ? CURPVicti.Text.ToUpper() : string.Empty;
            string edad = TipoVict.SelectedValue == "1" ? EdadVicti.Text.ToUpper() : string.Empty;
            DateTime feNacimiento = DateTime.Now; // Fecha válida por defecto

            if (TipoVict.SelectedValue == "1")
            {
                if (!DateTime.TryParse(FeNacVic.Text.ToUpper(), out DateTime fecha))
                {
                    throw new FormatException("La fecha ingresada no tiene el formato correcto.");
                }
                feNacimiento = fecha.Date; // Esto será solo la fecha, la hora se establecerá a medianoche (00:00:00)
            }

            int idContNacido = TipoVict.SelectedValue == "1" ? int.Parse(ContiNac.SelectedValue.ToUpper()) : 7;
            int idPaisNacido = TipoVict.SelectedValue == "1" ? int.Parse(PaisNac.SelectedValue.ToUpper()) : 227;
            int idEstadoNacido = TipoVict.SelectedValue == "1" ? int.Parse(EstNaci.SelectedValue.ToUpper()) : -2;
            string idMunicipioNacido = TipoVict.SelectedValue == "1" ? MuniNac.SelectedValue.ToUpper() : "-2";
            int idNacionalidad = TipoVict.SelectedValue == "1" ? int.Parse(NacVicti.SelectedValue.ToUpper()) : 99;
            int idCondicion = TipoVict.SelectedValue == "1" ? int.Parse(CondMigVic.SelectedValue.ToUpper()) : 12;
            int idEstadoCivil = TipoVict.SelectedValue == "1" ? int.Parse(EstCivil.SelectedValue.ToUpper()) : 9;
            int idGradoEstudios = TipoVict.SelectedValue == "1" ? int.Parse(GradEst.SelectedValue.ToUpper()) : -2;
            int idAlfabet = TipoVict.SelectedValue == "1" ? int.Parse(CondAlfVic.SelectedValue.ToUpper()) : 9;
            int idiomaEspañol = TipoVict.SelectedValue == "1" ? int.Parse(HablEsp.SelectedValue.ToUpper()) : 3;
            int idVulnerabilidad = TipoVict.SelectedValue == "1" ? int.Parse(VicVulne.SelectedValue.ToUpper()) : 99;
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

            InsertarVictima insertador = new InsertarVictima();
            insertador.InsertVictimData(
                idAsunto, apPaterno, apMaterno, nombre, genero, tipoParte, tipoVictima, victima, rfc, curp, edad, feNacimiento, idContNacido, idPaisNacido,
                idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                idGradoEstudios, idAlfabet, idiomaEspañol, idVulnerabilidad, idPueblo, hablaIndigena,
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
                Domici, OtroMed, AceptaDatos);
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
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
                Domici, OtroMed, AceptaDatos);
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
            accVictim.Style["display"] = "block";

            LlenarFormularioTrasConsulta llenador = new LlenarFormularioTrasConsulta();
            InformacionFormulario info = llenador.LlenarFormulario(idAsunto, idPartes);

            APVic.Text = info.APaterno;
            AMVic.Text = info.AMaterno;
            NomVic.Text = info.Nombre;
            GeneVicti.SelectedValue = info.GeneroNumerico;
            TipoVict.SelectedValue = info.TipoVictima;
            string valorAClasifVicti = info.Victima;
            ListItem item = ClasifVicti.Items.FindByValue(valorAClasifVicti);
            if (item != null)
            {
                ClasifVicti.SelectedValue = valorAClasifVicti;
            }
            else
            {
                ClasifVicti.ClearSelection();
            }
            RFCVicti.Text = info.RFC;
            CURPVicti.Text = info.CURP;
            if (info.FeNacimiento.HasValue)
            {
                FeNacVic.Text = info.FeNacimiento.Value.ToString("yyyy-MM-dd");
            }
            EdadVicti.Text = info.Edad;
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();
            dropdownFiller.DropdownContinentes(ContiNac);
            if (!string.IsNullOrEmpty(info.IdContinenteNacido))
            {
                ContiNac.SelectedValue = info.IdContinenteNacido;
            }
            dropdownFiller.DropdownPaises(PaisNac, Convert.ToInt32(ContiNac.SelectedValue));
            if (!string.IsNullOrEmpty(info.IdPaisNacido))
            {
                PaisNac.SelectedValue = info.IdPaisNacido;
            }
            dropdownFiller.DropdownEntidades(EstNaci);
            if (!string.IsNullOrEmpty(info.IdEstadoNacido))
            {
                EstNaci.SelectedValue = info.IdEstadoNacido;
            }
            dropdownFiller.DropdownMunicipios(MuniNac, EstNaci.SelectedValue);
            if (!string.IsNullOrEmpty(info.IdMunicipioNacido))
            {
                MuniNac.SelectedValue = info.IdMunicipioNacido;
            }
            NacVicti.SelectedValue = info.IdNacionalidad;
            HablEsp.SelectedValue = info.IdiomaEspañol;
            LengIndi.SelectedValue = info.IdDialecto;
            VicVulne.SelectedValue = info.IdVulnerabilidad;
            CondMigVic.SelectedValue = info.IdCondicion;
            CondAlfVic.SelectedValue = info.IdAlfabet;
            HablLengIndi.SelectedValue = info.HablaIndigena;
            PuebloIndi.SelectedValue = info.IdPueblo;
            DomiTrabVicti.Text = info.DomiTrabVicti;
            EstCivil.SelectedValue = info.IdEstadoCivil;
            OcupaVicti.SelectedValue = info.IdOcupacion;
            GradEst.SelectedValue = info.IdGradoEstudios;
            string selectedIdOcupacion = OcupaVicti.SelectedValue;
            CatalogosVictimas dropdownFiller2 = new CatalogosVictimas();
            dropdownFiller2.DropdownProfesiones(DetaOcupaVic, selectedIdOcupacion);
            string idProfesion = info.IdProfesion;
            ListItem item3 = DetaOcupaVic.Items.FindByValue(idProfesion);
            if (item3 != null)
            {
                DetaOcupaVic.SelectedValue = item3.Value;
            }
            else
            {
                // El valor no se encuentra en la lista, puedes dejar el DropDownList vacío
                DetaOcupaVic.ClearSelection();
                // O puedes mostrar un mensaje de advertencia o tomar alguna otra acción adecuada
            }
            string discapacidad = info.Discapacidad;
            ListItem item2 = CuenDisca.Items.FindByValue(discapacidad);
            if (item2 != null)
            {
                CuenDisca.SelectedValue = item2.Value;
            }
            else
            {
                // El valor no se encuentra en la lista, puedes dejar el DropDownList vacío
                CuenDisca.ClearSelection();
                // O puedes mostrar un mensaje de advertencia o tomar alguna otra acción adecuada
            }

            CatalogosVictimas dropdownFiller3 = new CatalogosVictimas();
            dropdownFiller3.DropdownContinentes(ContiRes);
            if (!string.IsNullOrEmpty(info.IdContinenteResidencia))
            {
                ContiRes.SelectedValue = info.IdContinenteResidencia;
            }
            dropdownFiller.DropdownPaises(PaisRes, Convert.ToInt32(ContiRes.SelectedValue));
            if (!string.IsNullOrEmpty(info.IdPaisResidencia))
            {
                PaisRes.SelectedValue = info.IdPaisResidencia;
            }
            dropdownFiller.DropdownEntidades(EstaRes);
            if (!string.IsNullOrEmpty(info.IdEstadoResidencia))
            {
                EstaRes.SelectedValue = info.IdEstadoResidencia;
            }
            dropdownFiller.DropdownMunicipios(MuniRes, EstaRes.SelectedValue);
            if (!string.IsNullOrEmpty(info.IdMunicipioResidencia))
            {
                MuniRes.SelectedValue = info.IdMunicipioResidencia;
            }
            DomicPersonVicti.Text = info.DomResidencia;
            AseJur.SelectedValue = info.IdDefensor;
            ReqInter.SelectedValue = info.Interprete;
            TelCont.Text = info.Telefono;
            EmailCont.Text = info.Correo;
            Fax.Text = info.Fax;

            string idDocIdentificador = info.IdDocIdentificador;
            ListItem item4 = IDVicti.Items.FindByValue(idDocIdentificador);
            if (item != null)
            {
                IDVicti.SelectedValue = item4.Value;
            }
            else
            {
                // El valor no se encuentra en la lista, puedes dejar el DropDownList vacío
                IDVicti.ClearSelection();
                // O puedes mostrar un mensaje de advertencia o tomar alguna otra acción adecuada
            }

            HoraIndivi.Text = info.FeIndividualización;
            Domici.Text = info.DomNotificacion;
            AceptaDatos.SelectedValue = info.Privacidad;

            // Crear un DataTable y agregar las columnas necesarias
            DataTable dtDiscapacidades = info.dtDiscapacidades;

            // Asignar el DataTable al GridView
            gvDiscapacidades2.Visible = true;
            gvDiscapacidades2.DataSource = dtDiscapacidades;
            gvDiscapacidades2.DataBind();
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
                MuniRes.Items.Clear(); // Cambia MuniNac a MuniRes aquí
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
            if (selectedValue == "1")
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
            string idDiscapacidad = DiscaEspe.SelectedValue;
            string discapacidad = DiscaEspe.SelectedItem.Text;
            AgregarDiscapacidad AgregarDisca = new AgregarDiscapacidad();
            AgregarDisca.AgregarDiscapacidades(gvDiscapacidades, DiscaEspe, TipoDisca, idDiscapacidad, discapacidad, HttpContext.Current.Session["Discapacidades"]);
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
                LengIndi.SelectedValue = "15";
                LengIndi.Enabled = false;
            }
        }
        protected void gvVictimas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVictimas.PageIndex = e.NewPageIndex;

            BusquedaVictimas busquedaVictimas = new BusquedaVictimas();
            var result = busquedaVictimas.BuscarVictimas(TAsunto.SelectedValue, numexpe.Text);
            if (result.dt != null)
            {
                gvVictimas.DataSource = result.dt;
                gvVictimas.DataBind();
            }
            else
            {
                // Manejar el caso cuando no hay datos
            }
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
    }
}