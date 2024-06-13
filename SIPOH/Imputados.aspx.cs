using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class Imputados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();

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
                dropdownFiller.DropdownPreguntas(CuenFeNac);
                dropdownFiller.DropdownPreguntas(AsisMigra);
            }
        }

        protected void UpImput_Click(object sender, EventArgs e)
        {
            {
                // Recuperar idPartes de la variable de sesión
                if (Session["idPartes"] != null)
                {
                    int idPartes = (int)Session["idPartes"];

                    // Mapea los nombres de los campos con sus valores correspondientes
                    Dictionary<string, string> campos = new Dictionary<string, string>
                    {
                        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text }, { "Nombre", NomVic.Text },
                        { "Alias", AliasImp.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue }, { "CURP", CURPVicti.Text }, { "RFC", RFCVicti.Text },
                        { "Edad", EdadVicti.Text },
                        { "Continente", ContiNac.SelectedValue },  {"Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue },
                        { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },

                        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
                        { "Habla Español", HablEsp.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
                        { "Lengua Indigena", LengIndi.SelectedValue }, { "Pueblo Indigena", PuebloIndi.SelectedValue }, 

                        { "Condicion Familiar", CondiFam.SelectedValue },{ "Domicilio de trabajo", DomiTrabVicti.Text },
                        { "Consume Sustancias", ConsSus.SelectedValue }, { "Dependientes Economicos", DepEconom.Text },

                        { "Asistencia Migratoria", AsisMigra.SelectedValue },

                        { "Estado psicofísico al momento del delito", EstPsi.SelectedValue }, { "Reincidente", Reinci.SelectedValue },
                        { "Ejercicio de la acción penal", AcciPenal.SelectedValue },

                        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue },
                        { "Domicilio Personal", DomicPersonVicti.Text },
                        { "Asesor Juridico", AseJur.SelectedValue },
                        { "Hora Individualizacion", HoraIndivi.Text },
                        { "Requiere Interprete", ReqInter.SelectedValue },
                        { "Relacion con el Imputado", RelacVic.SelectedValue },
                        { "Identificación", IDVicti.SelectedValue },
                        { "Telefono de contacto", TelCont.Text },
                        { "Correo Electronico", EmailCont.Text },
                        { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, 

                        
                        // Agrega tantos campos como necesites...
                    };

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
                        else if (campo.Key == "Edad" && int.TryParse(campo.Value, out int age) && (age > 99 || age <= 0))
                        {
                            ShowToastr("La edad no puede ser mayor a 99 ni menor a 0.", "Error", "error");
                            return;
                        }
                        // Verifica que los dependientes económicos no sean menores a 0
                        else if (campo.Key == "Dependientes Economicos" && int.TryParse(campo.Value, out int depEconom) && depEconom < 0)
                        {
                            ShowToastr("El número de dependientes económicos no puede ser menor a 0.", "Error", "error");
                            return;
                        }
                    }
                    // Crear una instancia de ConsultarIdAsunto
                    ConsultarIdAsunto consulta = new ConsultarIdAsunto();
                    int idAsunto = consulta.GetIdAsunto(TAsunto.SelectedValue, numexpe.Text, new GenerarIdJuzgadoPorSesion().ObtenerIdJuzgadoDesdeSesion());

                    // Datos para P_PartesAsunto
                    string apPaterno = APVic.Text.ToUpper();
                    string apMaterno = AMVic.Text.ToUpper();
                    string nombre = NomVic.Text.ToUpper();
                    string selectedValue = GeneVicti.SelectedValue.ToUpper();
                    string genero;

                    // Asignar valor a 'genero' según 'selectedValue'
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

                    // Fecha de nacimiento predeterminada
                    DateTime feNacimiento = DateTime.ParseExact("09/09/1899 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                    // Comprobar si el input de fecha está habilitado
                    if (FeNacVic.Enabled)
                    {
                        if (!DateTime.TryParse(FeNacVic.Text, out feNacimiento))
                        {
                            throw new FormatException("La fecha ingresada no tiene el formato correcto.");
                        }
                    }

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

                    int idLengExtra = int.Parse(HabLenExtra.SelectedValue.ToUpper());
                    int idRelacImput = int.Parse(RelacVic.SelectedValue.ToUpper());
                    int idAsisMigra = int.Parse(AsisMigra.SelectedValue.ToUpper());
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
                    int ordenProteccion = 0; // Valor predeterminado
                    DateTime feIndividualizacion = DateTime.Parse(HoraIndivi.Text.ToUpper());
                    int idDocIdentificador = int.Parse(IDVicti.SelectedValue.ToUpper());
                    string numDocumento = "752552"; // Valor predeterminado
                    string privacidad = AceptaDatos.SelectedValue.ToUpper();
                    string telefono = TelCont.Text.ToUpper();
                    string correo = EmailCont.Text.ToUpper();
                    string fax = Fax.Text.ToUpper();
                    string domNotificacion = DomicPersonVicti.Text.ToUpper();
                    string otroTipo = "OtroTipo"; // Valor predeterminado
                    int idUser = IdUsuarioPorSesion.ObtenerIdUsuario();

                    // Nuevos campos
                    int idEstadoPsi = int.Parse(EstPsi.SelectedValue.ToUpper());
                    int idReinci = int.Parse(Reinci.SelectedValue.ToUpper());
                    int idaccipenal = int.Parse(AcciPenal.SelectedValue.ToUpper());

                    int idTipoDeten = 5; // Valor por defecto si el dropdown TipoDeten no está visible
                    int idOrdenJudi = 5; // Valor por defecto si el dropdown OrdenJudi no está visible

                    // Asignar valores según la visibilidad de los dropdowns
                    if (TipoDeten.Visible)
                    {
                        idTipoDeten = int.Parse(TipoDeten.SelectedValue.ToUpper());
                    }
                    else if (OrdenJudi.Visible)
                    {
                        idOrdenJudi = int.Parse(OrdenJudi.SelectedValue.ToUpper());
                    }

                    // Obtener los IDs de las discapacidades agregadas en el GridView
                    List<string> idsDiscapacidades = ObtenerIdsDiscapacidades();

                    // Llamar al método que ejecuta el procedimiento almacenado y llena los elementos del formulario
                    ActualizarImputado insertador = new ActualizarImputado();
                    insertador.UpdateImputData(
                        idAsunto, idPartes, apPaterno, apMaterno, nombre, genero, tipoParte, rfc, curp, edad, feNacimiento, aliasImp, idContNacido, idPaisNacido,
                        idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil, idGradoEstudios, idAlfabet, idiomaEspañol, idPueblo, hablaIndigena,
                        idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idLengExtra, idRelacImput, idAsisMigra, idContiResidencia, idPaisResidencia,
                        idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete, ordenProteccion, feIndividualizacion, idDocIdentificador,
                        numDocumento, privacidad, telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this, idEstadoPsi, idaccipenal, idReinci,
                        idTipoDeten, idOrdenJudi, idCondFamiliar, depEcon, idSustancias
                    );
                }
            }
        }

        protected void SvImput_Click(object sender, EventArgs e)
        {
            // Mapea los nombres de los campos con sus valores correspondientes
            Dictionary<string, string> campos = new Dictionary<string, string>
                    {
                        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text }, { "Nombre", NomVic.Text },
                        { "Alias", AliasImp.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue }, { "CURP", CURPVicti.Text }, { "RFC", RFCVicti.Text },
                        { "Edad", EdadVicti.Text },
                        { "Continente", ContiNac.SelectedValue },  {"Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue },
                        { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },

                        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
                        { "Habla Español", HablEsp.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
                        { "Lengua Indigena", LengIndi.SelectedValue }, { "Pueblo Indigena", PuebloIndi.SelectedValue },

                        { "Condicion Familiar", CondiFam.SelectedValue },{ "Domicilio de trabajo", DomiTrabVicti.Text },
                        { "Consume Sustancias", ConsSus.SelectedValue }, { "Dependientes Economicos", DepEconom.Text },

                        { "Asistencia Migratoria", AsisMigra.SelectedValue },

                        { "Estado psicofísico al momento del delito", EstPsi.SelectedValue }, { "Reincidente", Reinci.SelectedValue },
                        { "Ejercicio de la acción penal", AcciPenal.SelectedValue },

                        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue },
                        { "Domicilio Personal", DomicPersonVicti.Text },
                        { "Asesor Juridico", AseJur.SelectedValue },
                        { "Hora Individualizacion", HoraIndivi.Text },
                        { "Requiere Interprete", ReqInter.SelectedValue },
                        { "Relacion con el Imputado", RelacVic.SelectedValue },
                        { "Identificación", IDVicti.SelectedValue },
                        { "Telefono de contacto", TelCont.Text },
                        { "Correo Electronico", EmailCont.Text },
                        { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, 
                        
                        // Agrega tantos campos como necesites...
                    };

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
                else if (campo.Key == "Edad" && int.TryParse(campo.Value, out int age) && (age > 99 || age <= 0))
                {
                    ShowToastr("La edad no puede ser mayor a 99 ni menor a 0.", "Error", "error");
                    return;
                }
                // Verifica que los dependientes económicos no sean menores a 0
                else if (campo.Key == "Dependientes Economicos" && int.TryParse(campo.Value, out int depEconom) && depEconom < 0)
                {
                    ShowToastr("El número de dependientes económicos no puede ser menor a 0.", "Error", "error");
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
            int edad = int.Parse(EdadVicti.Text.ToString());
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

            int idLengExtra = int.Parse(HabLenExtra.SelectedValue.ToUpper());
            int idRelacImput = int.Parse(RelacVic.SelectedValue.ToUpper());
            int idAsisMigra = int.Parse(AsisMigra.SelectedValue.ToUpper());

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
                idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idLengExtra, idRelacImput, idAsisMigra, idContiResidencia, idPaisResidencia,
                idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this, idEstadoPsi, idaccipenal, idReinci,
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
                Domici, OtroMed, AceptaDatos, AliasImp, CondiFam, ConsSus, DepEconom, EstPsi, Reinci, AcciPenal, TipoDeten, OrdenJudi, AsisMigra);
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

            LlenarFormularioTrasConsultaImputados llenador = new LlenarFormularioTrasConsultaImputados();
            InformacionFormularioImputados info = llenador.LlenarFormulario(idAsunto, idPartes);
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

            if (GeneVicti.Items.FindByValue(info.GeneroNumerico) != null)
            {
                GeneVicti.SelectedValue = info.GeneroNumerico;
            }
            else
            {
                GeneVicti.Items.Add(new ListItem(info.GeneroNumerico, info.GeneroNumerico));
                GeneVicti.SelectedValue = info.GeneroNumerico;
            }

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

            // Llenar Continentes
            dropdownFiller.DropdownContinentes(ContiNac);
            dropdownFiller.DropdownContinentes(ContiRes);

            // Llenar y seleccionar valores de Nacimiento
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
                    EstNaci.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                    EstNaci.Items.Add(new ListItem("No aplica", "-2"));
                    MuniNac.Items.Clear();
                    MuniNac.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                    MuniNac.Items.Add(new ListItem("No aplica", "-2"));
                }
            }

            if (info.IdEstadoNacido != null)
            {
                EstNaci.SelectedValue = info.IdEstadoNacido;
                dropdownFiller.DropdownMunicipios(MuniNac, info.IdEstadoNacido);
            }

            // Verificar si hay municipio de nacimiento y seleccionarlo
            if (info.IdMunicipioNacido != null)
            {
                MuniNac.SelectedValue = info.IdMunicipioNacido;
            }

            // Llenar y seleccionar valores de Residencia
            if (info.IdContinenteResidencia != null)
            {
                ContiRes.SelectedValue = info.IdContinenteResidencia;
                dropdownFiller.DropdownPaises(PaisRes, int.Parse(info.IdContinenteResidencia));
            }

            if (info.IdPaisResidencia != null)
            {
                PaisRes.SelectedValue = info.IdPaisResidencia;
                if (int.Parse(info.IdPaisResidencia) == 141)
                {
                    dropdownFiller.DropdownEntidades(EstaRes);
                }
                else
                {
                    EstaRes.Items.Clear();
                    EstaRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                    EstaRes.Items.Add(new ListItem("No aplica", "-2"));
                    MuniRes.Items.Clear();
                    MuniRes.Items.Add(new ListItem("----- SELECCIONE -----", "S"));
                    MuniRes.Items.Add(new ListItem("No aplica", "-2"));
                }
            }

            if (info.IdEstadoResidencia != null)
            {
                EstaRes.SelectedValue = info.IdEstadoResidencia;
                dropdownFiller.DropdownMunicipios(MuniRes, info.IdEstadoResidencia);
            }

            // Verificar si hay municipio de residencia y seleccionarlo
            if (info.IdMunicipioResidencia != null)
            {
                MuniRes.SelectedValue = info.IdMunicipioResidencia;
            }


            if (info.IdOcupacion != null)
            {
                // Verificar si el valor existe en la lista antes de asignarlo
                if (OcupaVicti.Items.FindByValue(info.IdOcupacion) != null)
                {
                    OcupaVicti.SelectedValue = info.IdOcupacion;
                }
                else
                {
                    OcupaVicti.ClearSelection();
                }
                dropdownFiller.DropdownProfesiones(DetaOcupaVic, info.IdOcupacion);
            }

            EstablecerValorDropDownList(NacVicti, info.IdNacionalidad);
            EstablecerValorDropDownList(HablEsp, info.IdiomaEspañol);
            // Luego, verifica si el valor que deseas seleccionar existe en la lista de elementos
            if (HabLenExtra.Items.FindByValue(info.IdLengExtra) != null)
            {
                // Si el valor existe, entonces establece el valor seleccionado
                HabLenExtra.SelectedValue = info.IdLengExtra;
            }
            else
            {
                HabLenExtra.SelectedValue = "S";
            }
            // Primero, verifica si el valor que deseas seleccionar existe en la lista de elementos
            if (RelacVic.Items.FindByValue(info.IdRelacVicti.ToString()) != null)
            {
                // Si el valor existe, entonces establece el valor seleccionado
                RelacVic.SelectedValue = info.IdRelacVicti.ToString();
            }
            else
            {
                RelacVic.SelectedValue = "S";
            }

            EstablecerValorDropDownList(LengIndi, info.IdDialecto);

            if (info.IdCondicion != null && CondMigVic.Items.FindByValue(info.IdCondicion) != null)
            {
                CondMigVic.SelectedValue = info.IdCondicion;
            }
            else
            {
                //CondMigVic.Items.Insert(0, new ListItem("--- SELECCIONE ---", "S"));
                CondMigVic.SelectedValue = "S";
            }

            EstablecerValorDropDownList(CondAlfVic, info.IdAlfabet);
            EstablecerValorDropDownList(HablLengIndi, info.HablaIndigena);
            EstablecerValorDropDownList(PuebloIndi, info.IdPueblo);
            EstablecerValorTextBox(DomiTrabVicti, info.DomiTrabVicti);
            EstablecerValorDropDownList(EstCivil, info.IdEstadoCivil);

            if (info.IdOcupacion != null && OcupaVicti.Items.FindByValue(info.IdOcupacion) != null)
            {
                OcupaVicti.SelectedValue = info.IdOcupacion;
            }
            else
            {
                //OcupaVicti.Items.Insert(0, new ListItem("--- SELECCIONE ---", "S"));
                OcupaVicti.SelectedValue = "S";

                DetaOcupaVic.Enabled = false;
                DetaOcupaVic.Items.Insert(0, new ListItem("--- SELECCIONE ---", "S"));
                DetaOcupaVic.SelectedValue = "S";
            }


            if (GradEst.Items.FindByValue(info.IdGradoEstudios) != null)
            {
                GradEst.SelectedValue = info.IdGradoEstudios;
            }
            else
            {
                GradEst.Items.Add(new ListItem(info.IdGradoEstudios, info.IdGradoEstudios));
                GradEst.SelectedValue = "S";
            }

            // Asegúrate de que 'info' y 'info.Discapacidad' no sean null
            if (info != null && info.Discapacidad != null)
            {
                // Asegúrate de que 'CuenDisca' no sea null y que los datos ya estén enlazados
                if (CuenDisca != null && CuenDisca.Items.Count > 0)
                {
                    string discapacidadValue = info.Discapacidad.ToString();
                    if (CuenDisca.Items.FindByValue(discapacidadValue) != null)
                    {
                        // Si el valor existe, entonces establece el valor seleccionado
                        CuenDisca.SelectedValue = discapacidadValue;
                    }
                    else
                    {
                        // Si el valor no existe, establece un valor predeterminado
                        CuenDisca.SelectedValue = "S";
                    }
                }
                else
                {
                    // Maneja el caso donde 'CuenDisca' es null o no tiene elementos
                    // Por ejemplo, podrías cargar los elementos aquí o mostrar un mensaje de error
                }
            }
            else
            {
                // Maneja el caso donde 'info' o 'info.Discapacidad' son null
                // Por ejemplo, podrías asignar valores predeterminados o mostrar un mensaje de error
            }


            if (AsisMigra.Items.FindByValue(info.IdAsisMigra) != null)
            {
                AsisMigra.SelectedValue = info.IdAsisMigra;
            }
            else
            {
                //AsisMigra.Items.Insert(0, new ListItem("--- SELECCIONE ---", "S"));
                AsisMigra.SelectedValue = "S";
            }



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

            EstablecerValorTextBox(HoraIndivi, info.FeIndividualización);
            Domici.Text = info.DomNotificacion;
            AceptaDatos.SelectedValue = info.Privacidad;

            DataTable dtDiscapacidades = info.dtDiscapacidades;
            gvDiscapacidades2.Visible = true;
            gvDiscapacidades2.DataSource = dtDiscapacidades;
            gvDiscapacidades2.DataBind();

            EstablecerValorTextBox(AliasImp, info.Alias);
            EstablecerValorDropDownList(CondiFam, info.IdCondFamiliar);
            EstablecerValorDropDownList(ConsSus, info.IdSustancias);
            EstablecerValorTextBox(DepEconom, info.DepEcon);
            EstablecerValorDropDownList(EstPsi, info.IdPsicofisico);
            EstablecerValorDropDownList(Reinci, info.IdReincidente);
            EstablecerValorDropDownList(AcciPenal, info.TipoConsignacion);
            EstablecerValorDropDownList(TipoDeten, info.IdTipoDetencion);
            EstablecerValorDropDownList(OrdenJudi, info.IdOrdenJudicial);
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

            BusquedaImputado busqueda = new BusquedaImputado();
            var resultado = busqueda.BuscarImputados(tipoAsunto, numeroExpediente);

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

            LimpiarFormularioImputado reiniciarFormulario = new LimpiarFormularioImputado();
            reiniciarFormulario.Reiniciar(UpVict, LimpVicti, SvVicti, APVic, AMVic, NomVic, GeneVicti, CURPVicti, RFCVicti, FeNacVic, EdadVicti, ContiNac,
                PaisNac, EstNaci, MuniNac, NacVicti, HabLenExtra, HablEsp, LengIndi, CondMigVic, CondAlfVic, HablLengIndi, PuebloIndi, DomiTrabVicti, EstCivil, GradEst, OcupaVicti,
                DetaOcupaVic, CuenDisca, TipoDisca, DiscaEspe, ContiRes, PaisRes, EstaRes, MuniRes, DomicPersonVicti, AseJur, ReqInter, TelCont, EmailCont, Fax, RelacVic, HoraIndivi, IDVicti,
                Domici, OtroMed, AceptaDatos, AliasImp, CondiFam, ConsSus, DepEconom, EstPsi, Reinci, AcciPenal, TipoDeten, OrdenJudi, AsisMigra);
        }

        protected void ImprCedula_Click(object sender, EventArgs e)
        {
            // Recuperar idPartes de la variable de sesión
            if (Session["idPartes"] != null)
            {
                int idPartes = (int)Session["idPartes"];
                {
                    // Mapea los nombres de los campos con sus valores correspondientes
                    Dictionary<string, string> campos = new Dictionary<string, string>
                    {
                        { "TAsunto", TAsunto.SelectedValue }, { "numexpe", numexpe.Text }, { "Apellido Paterno", APVic.Text }, { "Apellido Materno", AMVic.Text }, { "Nombre", NomVic.Text },
                        { "Alias", AliasImp.Text }, { "Genero", GeneVicti.SelectedValue == "SG" ? "SG" : GeneVicti.SelectedValue }, { "CURP", CURPVicti.Text }, { "RFC", RFCVicti.Text },
                        { "Edad", EdadVicti.Text },
                        { "Continente", ContiNac.SelectedValue },  {"Pais", PaisNac.SelectedValue }, { "Estado", EstNaci.SelectedValue }, { "Municipio", MuniNac.SelectedValue },
                        { "Nacionalidad", NacVicti.SelectedValue }, { "Condicion Migratoria", CondMigVic.SelectedValue }, { "Estado Civil", EstCivil.SelectedValue }, { "Grado de Estudios", GradEst.SelectedValue },

                        { "Habla Lengua Extranjera", HabLenExtra.SelectedValue }, { "Condicion de Alfabetismo", CondAlfVic.SelectedValue }, { "Ocupacion", OcupaVicti.SelectedValue }, { "Profesion", DetaOcupaVic.SelectedValue },
                        { "Habla Español", HablEsp.SelectedValue }, { "Habla Lengua Indigena", HablLengIndi.SelectedValue }, { "Cuenta con discapacidad", CuenDisca.SelectedValue },
                        { "Lengua Indigena", LengIndi.SelectedValue }, { "Pueblo Indigena", PuebloIndi.SelectedValue },

                        { "Condicion Familiar", CondiFam.SelectedValue },{ "Domicilio de trabajo", DomiTrabVicti.Text },
                        { "Consume Sustancias", ConsSus.SelectedValue }, { "Dependientes Economicos", DepEconom.Text },

                        { "Asistencia Migratoria", AsisMigra.SelectedValue },

                        { "Estado psicofísico al momento del delito", EstPsi.SelectedValue }, { "Reincidente", Reinci.SelectedValue },
                        { "Ejercicio de la acción penal", AcciPenal.SelectedValue },

                        { "Continente de Residencia", ContiRes.SelectedValue }, { "Pais de Residencia", PaisRes.SelectedValue }, { "Estado de Residencia", EstaRes.SelectedValue }, { "Municipio de Residencia", MuniRes.SelectedValue },
                        { "Domicilio Personal", DomicPersonVicti.Text },
                        { "Asesor Juridico", AseJur.SelectedValue },
                        { "Hora Individualizacion", HoraIndivi.Text },
                        { "Requiere Interprete", ReqInter.SelectedValue },
                        { "Relacion con el Imputado", RelacVic.SelectedValue },
                        { "Identificación", IDVicti.SelectedValue },
                        { "Telefono de contacto", TelCont.Text },
                        { "Correo Electronico", EmailCont.Text },
                        { "Acepta Publicacion de Datos", AceptaDatos.SelectedValue }, 
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
                    foreach (var dropdown in campos)
                    {
                        // Verifica si el valor seleccionado es "S"
                        if (dropdown.Value == "S")
                        {
                            ShowToastr($"Por favor, selecciona un valor válido para {dropdown.Key}.", "Error", "error");
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
                    // No es necesario un else aquí, ya que la fecha predeterminada ya está establecida Esto será solo la fecha, la hora se establecerá a medianoche (00:00:00)

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

                    int idLengExtra = int.Parse(HabLenExtra.SelectedValue.ToUpper());
                    int idRelacImput = int.Parse(RelacVic.SelectedValue.ToUpper());
                    int idAsisMigra = int.Parse(AsisMigra.SelectedValue.ToUpper());

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

                    MostrarCedulaImputado insertador = new MostrarCedulaImputado();
                    insertador.MostrarCedula(
                        idAsunto, idPartes, apPaterno, apMaterno, nombre, genero, tipoParte, rfc, curp, edad, feNacimiento, aliasImp, idContNacido, idPaisNacido,
                        idEstadoNacido, idMunicipioNacido, idNacionalidad, idCondicion, idEstadoCivil,
                        idGradoEstudios, idAlfabet, idiomaEspañol, idPueblo, hablaIndigena,
                        idDialecto, idOcupacion, idProfesion, domOcupacion, discapacidad, idLengExtra, idRelacImput, idAsisMigra, idContiResidencia, idPaisResidencia,
                        idEstadoResidencia, idMunicipioResidencia, domResidencia, idDefensor, interprete,
                        ordenProteccion, feIndividualizacion, idDocIdentificador, numDocumento, privacidad,
                        telefono, correo, fax, domNotificacion, otroTipo, idUser, idsDiscapacidades, this, idEstadoPsi, idaccipenal, idReinci,
                        idTipoDeten, idOrdenJudi, idCondFamiliar, depEcon, idSustancias
                        );
                }
            }
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
    }
}