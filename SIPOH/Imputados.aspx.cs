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
    public partial class Imputados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TipoDetenc.Visible = false;
                OrdenJudic.Visible = false;

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
                dropdownFiller.DropdownContinentes(ContiNac);
                dropdownFiller.DropdownContinentes(ContiRes);
                dropdownFiller.DropdownNacionalidad(NacVicti);
                dropdownFiller.DropdownCondicMigratoria(CondMigVic);
                dropdownFiller.DropdownEstadoCivil(EstCivil);
                dropdownFiller.DropdownGradoEstudios(GradEst);
                dropdownFiller.DropdownCondicAlfabetismo(CondAlfVic);
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
                dropdownFiller.DropdownCondFamiliar(CondiFam);
                dropdownFiller.DropdownConsSustancias(ConsSus);
                dropdownFiller.DropdownEstadoPsico(EstPsi);
                dropdownFiller.DropdownReincidente(Reinci);
                dropdownFiller.DropdownDetencion(TipoDeten);
                dropdownFiller.DropdownOrdenJud(OrdenJudi);


            }
        }

        protected void SvImput_Click(object sender, EventArgs e)
        {
            // Mapea los nombres de los campos con sus valores correspondientes
            Dictionary<string, string> campos = new Dictionary<string, string>
        {
            { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text }, { "Nombre", NomVic.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue }, { "RFC", RFCVicti.Text },
            { "CURP", CURPVicti.Text }, { "Edad", EdadVicti.Text }, { "Fecha de Nacimiento", FeNacVic.Text }, { "Continente", ContiNac.SelectedValue }, { "Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue }, { "Nacionalidad", NacVicti.SelectedValue },
            { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estadio Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Habla Español", HablEsp.SelectedValue }, { "Pueblo Indigena", PuebloIndi.SelectedValue },
            { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Lengua Indigena", LengIndi.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue }, { "Domicilio de trabajo", DomiTrabVicti.Text }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
            { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue }, { "Domicilio Personal", DomicPersonVicti.Text }, { "Asesor Juridico", AseJur.SelectedValue },
            { "Requiere Interprete", ReqInter.SelectedValue }, { "Hora de individualizacion", HoraIndivi.Text }, { "Identificación", IDVicti.SelectedValue }, { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text },
            { "Fax", Fax.Text }, { "Estado psicofísico al momento del delito", EstPsi.SelectedValue }, { "Reincidente", Reinci.SelectedValue }, { "Ejercicio de la acción penal", AcciPenal.SelectedValue }, { "Tipo de Detención", TipoDeten.SelectedValue }, { "Orden Judicial", OrdenJudi.SelectedValue },
            { "Condicion Familiar", CondiFam.SelectedValue }, { "Consume Sustancias", ConsSus.SelectedValue }, { "Dependientes Economicos", DepEconom.Text },
                // Agrega tantos campos como necesites...
            };

            // Verifica si los campos requeridos están vacíos
            foreach (var campo in campos)
            {
                if (string.IsNullOrWhiteSpace(campo.Value))
                {
                    ShowToastr($"Por favor, llena el campo {campo.Key}.", "Error", "error");
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

            string tipoParte = "I";

            string rfc = RFCVicti.Text.ToUpper();
            string curp = CURPVicti.Text.ToUpper();
            string edad = EdadVicti.Text.ToUpper();
            DateTime feNacimiento = DateTime.Now; // Fecha válida por defecto

            if (!DateTime.TryParse(FeNacVic.Text.ToUpper(), out DateTime fecha))
            {
                throw new FormatException("La fecha ingresada no tiene el formato correcto.");
            }
            feNacimiento = fecha.Date; // Esto será solo la fecha, la hora se establecerá a medianoche (00:00:00)

            string aliasImp = AliasImp.Text.ToUpper();
            int idContNacido = int.Parse(ContiNac.SelectedValue.ToUpper());
            int idPaisNacido = int.Parse(PaisNac.SelectedValue.ToUpper());
            int idEstadoNacido = int.Parse(EstNaci.SelectedValue.ToUpper());
            string idMunicipioNacido = MuniNac.SelectedValue.ToUpper();
            int idNacionalidad = int.Parse(NacVicti.SelectedValue.ToUpper());
            int idCondicion = int.Parse(CondMigVic.SelectedValue.ToUpper());
            int idEstadoCivil = int.Parse(EstCivil.SelectedValue.ToUpper());
            int idGradoEstudios = int.Parse(GradEst.SelectedValue.ToUpper());
            int idAlfabet = int.Parse(CondAlfVic.SelectedValue.ToUpper());
            int idiomaEspañol = int.Parse(HablEsp.SelectedValue.ToUpper());
            int idPueblo = int.Parse(PuebloIndi.SelectedValue.ToUpper());
            int hablaIndigena = int.Parse(HablLengIndi.SelectedValue.ToUpper());
            int idDialecto = int.Parse(LengIndi.SelectedValue.ToUpper());
            int idOcupacion = int.Parse(OcupaVicti.SelectedValue.ToUpper());
            int idProfesion = int.Parse(DetaOcupaVic.SelectedValue.ToUpper());
            string domOcupacion = DomiTrabVicti.Text.ToUpper();
            int discapacidad = int.Parse(CuenDisca.SelectedValue.ToUpper());
            int idCondFamiliar = int.Parse(CondiFam.SelectedValue.ToUpper());
            int depEcon = int.Parse(DepEconom.Text.ToUpper());
            int idSustancias = int.Parse(ConsSus.SelectedValue.ToUpper());
            int idContiResidencia = int.Parse(ContiRes.SelectedValue.ToUpper());
            int idPaisResidencia = int.Parse(PaisRes.SelectedValue.ToUpper());
            int idEstadoResidencia = int.Parse(EstaRes.SelectedValue.ToUpper());
            string idMunicipioResidencia = MuniRes.SelectedValue.ToUpper();
            string domResidencia = DomicPersonVicti.Text.ToUpper();
            int idDefensor = int.Parse(AseJur.SelectedValue.ToUpper());
            int interprete = int.Parse(ReqInter.SelectedValue.ToUpper());
            int ordenProteccion = 0;
            DateTime feIndividualizacion = DateTime.Parse(HoraIndivi.Text.ToUpper());
            int idDocIdentificador = int.Parse(IDVicti.SelectedValue.ToUpper());
            string numDocumento = "752552";
            string privacidad = AceptaDatos.SelectedValue.ToUpper();
            string telefono = TelCont.Text.ToUpper();
            string correo = EmailCont.Text.ToUpper();
            string fax = Fax.Text.ToUpper();
            string domNotificacion = DomicPersonVicti.Text.ToUpper();
            string otroTipo = "OtroTipo";
            int idUser = IdUsuarioPorSesion.ObtenerIdUsuario();

            // Nuevos campos
            int idEstadoPsi = int.Parse(EstPsi.SelectedValue.ToUpper());
            int idReinci = int.Parse(Reinci.SelectedValue.ToUpper());
            int idaccipenal = int.Parse(AcciPenal.SelectedValue.ToUpper());

            int idTipoDeten = 5; // Valor por defecto si el dropdown TipoDeten no está visible
            int idOrdenJudi = 5; // Valor por defecto si el dropdown OrdenJudi no está visible

            if (TipoDeten.Visible)
            {
                idTipoDeten = int.Parse(TipoDeten.SelectedValue.ToUpper());
            }
            else if (OrdenJudi.Visible)
            {
                idOrdenJudi = int.Parse(OrdenJudi.SelectedValue.ToUpper());
            }


            // Datos para P_Discapacidad
            // Obtener los IDs de las discapacidades agregadas en el GridView
            List<string> idsDiscapacidades = ObtenerIdsDiscapacidades();



            InsertarImputado insertador = new InsertarImputado();
            insertador.InsertImputData(
                idAsunto, apPaterno, apMaterno, nombre, genero, tipoParte, rfc, curp, edad, feNacimiento, aliasImp, idContNacido, idPaisNacido,
                idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                idGradoEstudios, idAlfabet, idiomaEspañol, idPueblo, hablaIndigena,
                idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idContiResidencia, idPaisResidencia,
                idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this.Page, idEstadoPsi, idaccipenal, idReinci, 
                idTipoDeten, idOrdenJudi, idCondFamiliar, depEcon, idSustancias
            );
        }

        protected void AgregarInputadoForm_Click(object sender, EventArgs e)
        {
            accVictim.Style["display"] = "block";

            ReiniciarFormularioImput reiniciarFormulario = new ReiniciarFormularioImput();
            reiniciarFormulario.Reiniciar(UpVict, LimpVicti, SvVicti, APVic, AMVic, NomVic, GeneVicti, CURPVicti, RFCVicti, FeNacVic, EdadVicti, ContiNac,
                PaisNac, EstNaci, MuniNac, NacVicti, HabLenExtra, HablEsp, LengIndi, CondMigVic, CondAlfVic, HablLengIndi, PuebloIndi, DomiTrabVicti, EstCivil, GradEst, OcupaVicti,
                DetaOcupaVic, CuenDisca, TipoDisca, DiscaEspe, ContiRes, PaisRes, EstaRes, MuniRes, DomicPersonVicti, AseJur, ReqInter, TelCont, EmailCont, Fax, RelacVic, HoraIndivi, IDVicti,
                Domici, OtroMed, AceptaDatos, AliasImp, CondiFam, ConsSus, DepEconom, EstPsi, Reinci, AcciPenal, TipoDeten, OrdenJudi);
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaImputado buscador = new BusquedaImputado();
            (string mensaje, DataTable dt) = buscador.BuscarImputados(TAsunto.SelectedValue, numexpe.Text);
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

            LlenarFormularioTrasConsultaImputados llenador = new LlenarFormularioTrasConsultaImputados();
            InformacionFormularioImputados info = llenador.LlenarFormulario(idAsunto, idPartes);

            APVic.Text = info.APaterno;
            AMVic.Text = info.AMaterno;
            NomVic.Text = info.Nombre;
            GeneVicti.SelectedValue = info.GeneroNumerico;

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
            if (item4 != null)
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

            AliasImp.Text = info.Alias;
            CondiFam.SelectedValue = info.IdCondFamiliar;
            ConsSus.SelectedValue = info.IdSustancias;
            DepEconom.Text = info.DepEcon;
            EstPsi.SelectedValue = info.IdPsicofisico;
            Reinci.SelectedValue = info.IdReincidente;
            AcciPenal.SelectedValue = info.TipoConsignacion; 

            TipoDeten.SelectedValue = info.IdTipoDetencion;
            OrdenJudi.SelectedValue = info.IdOrdenJudicial;
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

        protected void AcciPenal_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (AcciPenal.SelectedValue)
            {
                case "1":
                    TipoDetenc.Visible = true;
                    OrdenJudic.Visible = false;
                    break;
                case "EA":
                    TipoDetenc.Visible = false;
                    OrdenJudic.Visible = false;
                    break;
                default:
                    TipoDetenc.Visible = false;
                    OrdenJudic.Visible = true;
                    break;
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