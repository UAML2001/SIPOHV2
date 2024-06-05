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
            }
        }


        protected void UpVicti_Click(object sender, EventArgs e)
        {
            {
                // Recuperar idPartes de la variable de sesión
                if (Session["idPartes"] != null)
                {
                    int idPartes = (int)Session["idPartes"];

                    Dictionary<string, string> campos = new Dictionary<string, string>
                    {
                        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text },
                        { "Nombre", NomVic.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue },
                        { "Tipo Victima", TipoVict.SelectedValue },{ "Clasificacion de Victima", ClasifVicti.SelectedValue },{ "Tipo de Sector", SectorVicti.SelectedValue },
                        { "Tipo Sociedad", TipoSocie.SelectedValue }, { "CURP", CURPVicti.Text }, { "RFC", RFCVicti.Text }, { "Fecha de Nacimiento", FeNacVic.Text }, { "Edad", EdadVicti.Text },
                        { "Continente", ContiNac.SelectedValue }, { "Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue },


                        { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },
                        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
                        { "Habla Español", HablEsp.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
                        { "Lengua Indigena", LengIndi.SelectedValue },{ "Pueblo Indigena", PuebloIndi.SelectedValue },
                        { "Domicilio de trabajo", DomiTrabVicti.Text }, { "Vulnerabilidad", VicVulne.SelectedValue },


                        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue },
                        { "Domicilio Personal", DomicPersonVicti.Text }, { "Asesor Juridico", AseJur.SelectedValue }, { "Requiere Interprete", ReqInter.SelectedValue }, { "Hora Individualizacion", IDVicti.SelectedValue }, { "Identificación", IDVicti.SelectedValue },
                        { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text }, { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }
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
                            else if (campo.Value == "S")
                            {
                                ShowToastr($"Por favor, selecciona un valor válido para {campo.Key}.", "Error", "error");
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

                    ActualizarVictima insertador = new ActualizarVictima();
                    insertador.UpdateVictimData(
                        idPartes, idAsunto, apPaterno, apMaterno, nombre, genero, tipoParte, tipoVictima, victima, rfc, curp, edad, feNacimiento, idContNacido, idPaisNacido,
                        idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                        idGradoEstudios, idAlfabet, idiomaEspañol, idVulnerabilidad, idPueblo, hablaIndigena,
                        idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idContiResidencia, idPaisResidencia,
                        idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                        ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                        telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this.Page
                    );
                }
            }
        }


        protected void SvVicti_Click(object sender, EventArgs e)
        {
            // Mapea los nombres de los campos con sus valores correspondientes
            Dictionary<string, string> campos = new Dictionary<string, string>
                    {
                        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text },
                        { "Nombre", NomVic.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue },
                        { "Tipo Victima", TipoVict.SelectedValue },{ "Clasificacion de Victima", ClasifVicti.SelectedValue },{ "Tipo de Sector", SectorVicti.SelectedValue },
                        { "Tipo Sociedad", TipoSocie.SelectedValue }, { "CURP", CURPVicti.Text }, { "RFC", RFCVicti.Text }, { "Fecha de Nacimiento", FeNacVic.Text }, { "Edad", EdadVicti.Text },
                        { "Continente", ContiNac.SelectedValue }, { "Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue },


                        { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },
                        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
                        { "Habla Español", HablEsp.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
                        { "Lengua Indigena", LengIndi.SelectedValue },{ "Pueblo Indigena", PuebloIndi.SelectedValue },
                        { "Domicilio de trabajo", DomiTrabVicti.Text }, { "Vulnerabilidad", VicVulne.Text },


                        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue },
                        { "Domicilio Personal", DomicPersonVicti.Text }, { "Asesor Juridico", AseJur.SelectedValue }, { "Requiere Interprete", ReqInter.SelectedValue }, { "Hora Individualizacion", HoraIndivi.Text }, { "Identificación", IDVicti.SelectedValue },
                        { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text }, { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }
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
                    else if (campo.Value == "S")
                    {
                        ShowToastr($"Por favor, selecciona un valor válido para {campo.Key}.", "Error", "error");
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
            accVictim.Style["display"] = "block";

            LlenarFormularioTrasConsulta llenador = new LlenarFormularioTrasConsulta();
            InformacionFormulario info = llenador.LlenarFormulario(idAsunto, idPartes);
            ContiNac.Items.Clear();
            PaisNac.Items.Clear();
            EstNaci.Items.Clear();
            MuniNac.Items.Clear();
            ContiRes.Items.Clear();
            PaisRes.Items.Clear();
            EstaRes.Items.Clear();
            MuniRes.Items.Clear();
            DetaOcupaVic.Items.Clear();

            EstablecerValorTextBox(APVic, info.APaterno);
            EstablecerValorTextBox(AMVic, info.AMaterno);
            EstablecerValorTextBox(NomVic, info.Nombre);
            EstablecerValorDropDownList(GeneVicti, info.GeneroNumerico);
            EstablecerValorTextBox(RFCVicti, info.RFC);
            EstablecerValorTextBox(CURPVicti, info.CURP);
            if (info.FeNacimiento.HasValue)
            {
                FeNacVic.Text = info.FeNacimiento.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                FeNacVic.Text = string.Empty;
            }
            EstablecerValorTextBox(EdadVicti, info.Edad);

            // Llenar los DropDownList y establecer valores
            CatalogosVictimas dropdownFiller = new CatalogosVictimas();

            dropdownFiller.DropdownContinentes(ContiNac);
            dropdownFiller.DropdownContinentes(ContiRes);

            if (info.IdContinenteNacido != null)
            {
                ContiNac.SelectedValue = info.IdContinenteNacido;
                dropdownFiller.DropdownPaises(PaisNac, int.Parse(info.IdContinenteNacido));
            }

            if (info.IdPaisNacido != null)
            {
                PaisNac.SelectedValue = info.IdPaisNacido;
                if (int.Parse(info.IdPaisNacido) == 141)
                {
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

            if (info.IdEstadoNacido != null)
            {
                EstNaci.SelectedValue = info.IdEstadoNacido;
                dropdownFiller.DropdownMunicipios(MuniNac, info.IdEstadoNacido);
            }

            // Repite el proceso para los DropDownList de residencia

            dropdownFiller.DropdownContinentes(ContiNac);
            dropdownFiller.DropdownContinentes(ContiRes);

            if (info.IdContinenteNacido != null)
            {
                ContiRes.SelectedValue = info.IdContinenteNacido;
                dropdownFiller.DropdownPaises(PaisRes, int.Parse(info.IdContinenteNacido));
            }

            if (info.IdPaisNacido != null)
            {
                PaisRes.SelectedValue = info.IdPaisNacido;
                if (int.Parse(info.IdPaisNacido) == 141)
                {
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

            if (info.IdEstadoNacido != null)
            {
                EstaRes.SelectedValue = info.IdEstadoNacido;
                dropdownFiller.DropdownMunicipios(MuniRes, info.IdEstadoNacido);
            }

            if (info.IdOcupacion != null)
            {
                OcupaVicti.SelectedValue = info.IdOcupacion;
                dropdownFiller.DropdownProfesiones(DetaOcupaVic, info.IdOcupacion);
            }

            EstablecerValorDropDownList(NacVicti, info.IdNacionalidad);
            EstablecerValorDropDownList(HablEsp, info.IdiomaEspañol);
            EstablecerValorDropDownList(LengIndi, info.IdDialecto);
            EstablecerValorDropDownList(CondMigVic, info.IdCondicion);
            EstablecerValorDropDownList(CondAlfVic, info.IdAlfabet);
            EstablecerValorDropDownList(HablLengIndi, info.HablaIndigena);
            EstablecerValorDropDownList(PuebloIndi, info.IdPueblo);
            EstablecerValorTextBox(DomiTrabVicti, info.DomiTrabVicti);
            EstablecerValorDropDownList(EstCivil, info.IdEstadoCivil);
            EstablecerValorDropDownList(OcupaVicti, info.IdOcupacion);
            EstablecerValorDropDownList(GradEst, info.IdGradoEstudios);

            EstablecerValorDropDownList(CuenDisca, info.Discapacidad);
            EstablecerValorTextBox(DomicPersonVicti, info.DomResidencia);
            EstablecerValorDropDownList(AseJur, info.IdDefensor);
            EstablecerValorDropDownList(ReqInter, info.Interprete);
            EstablecerValorTextBox(TelCont, info.Telefono);
            EstablecerValorTextBox(EmailCont, info.Correo);
            EstablecerValorTextBox(Fax, info.Fax);

            string idDocIdentificador = info.IdDocIdentificador;
            ListItem item4 = IDVicti.Items.FindByValue(idDocIdentificador);
            if (item4 != null)
            {
                IDVicti.SelectedValue = item4.Value;
            }
            else
            {
                IDVicti.ClearSelection();
            }

            HoraIndivi.Text = info.FeIndividualización;
            Domici.Text = info.DomNotificacion;
            AceptaDatos.SelectedValue = info.Privacidad;

            DataTable dtDiscapacidades = info.dtDiscapacidades;
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
                MuniNac.Items.Clear(); // Cambia MuniNac a MuniRes aquí
                dropdownFiller2.DropdownMunicipios(MuniNac, selectedIdEstado);
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
            if (selectedIdOcupacion == "S")
            {
                DetaOcupaVic.Enabled = false;
                DetaOcupaVic.Items.Add(new ListItem("Seleccione el detalle de la ocupación...", "SDO"));
                DetaOcupaVic.SelectedValue = "S";
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
                Domici, OtroMed, AceptaDatos);
        }

        protected void ImprCedula_Click(object sender, EventArgs e)
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
                        { "Requiere Interprete", ReqInter.SelectedValue }, { "Identificación", IDVicti.SelectedValue },
                        { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, { "Telefono de contacto", TelCont.Text }, { "Correo Electronico", EmailCont.Text },
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

                MostrarCedulaVictima insertador = new MostrarCedulaVictima();
                insertador.MostrarCedula(
                    idAsunto, idPartes, apPaterno, apMaterno, nombre, genero, tipoParte, tipoVictima, victima, rfc, curp, edad, feNacimiento, idContNacido, idPaisNacido,
                    idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                    idGradoEstudios, idAlfabet, idiomaEspañol, idVulnerabilidad, idPueblo, hablaIndigena,
                    idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idContiResidencia, idPaisResidencia,
                    idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                    ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                    telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this.Page
                  );
            }
        }

        protected void OcultCedula_Click(object sender, EventArgs e)
        {
            GenCedula.Visible = false;
            OcultCedula.Visible = false;
            CedulaSinInsert.Style["display"] = "none";
            panelPdfMostrar.Style["display"] = "none";
        }


    }
}
