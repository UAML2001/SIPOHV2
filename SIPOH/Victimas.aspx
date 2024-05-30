<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="Victimas.aspx.cs" Inherits="SIPOH.ExpeDigital" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="ContentExpeDigital" ContentPlaceHolderID="ContentDigitInici" runat="server">

    <div>
        <h1 class="h5"><i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold"> Victimas</span> </h1>
    </div>

    <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }

        .h5 {
            margin-left: 5%;
        }

        .myRadioButtonList input[type="radio"] {
            margin-left: 10px;
            margin-right: 5px;
        }

        .accordion-button:not(.collapsed) {
            background-color: #1bbe83; /* Color success de Bootstrap */
            color: white;
            border-color: #1bbe83; /* Un verde más oscuro para el borde */
        }
    </style>

    <!-- Include Toastr CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />

    <!-- Include jQuery (Toastr depends on it) -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- Include Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

    <!-- Toastr initialization -->
    <script>
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <!-- Nav tabs -->
                <div class="card">
                    <div class="card-body">
                        <div class="container col-12">
                            <div style="padding: 2% 2% 0;">
                                <h5 class="text-secondary mb-3"><b>Busqueda de Victimas</b></h5>
                            </div>
                            <div class="row pt-5">
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <label class="form-label text-secondary"><b>Tipo de asunto: </b></label>
                                    <asp:DropDownList ID="TAsunto" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                        <asp:ListItem runat="server" Value="SO" Selected="True" Text="Seleccione el tipo de asunto..."></asp:ListItem>
                                        <asp:ListItem runat="server" Value="C" Text="CAUSA"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="CP" Text="CUPRE"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="E" Text="EXHORTO"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="JO" Text="JUICIO ORAL"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="T" Text="TRADICIONAL"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <label for="numdesp" class="form-label text-secondary"><b>Ingrese número de expediente: </b></label>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de Expediente" ID="numexpe" onchange="formatoNumeroToca(this)"></asp:TextBox>
                                </div>
                            </div>

                            <br />

                            <center>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                        <asp:Button ID="btnBuscar" runat="server" Text="🔎 Buscar Asunto" CssClass="btn btn-success btn-sm mayusculas" OnClick="btnBuscar_Click" />
                                    </div>
                                </div>
                            </center>

                            <center>
                            <div class="center-panel" runat="server" id="panelPdfInsertar" style="display: none;">
                                    <br />
                                    <br />
                                    <asp:Label ID="AgregadoExito" runat="server" CssClass="h4" Text="¡Victima agregada exitosamente! 🎉" Visible="false" />
                                    <br />
                                    <br />
                                    <asp:Label ID="CedulaExito" runat="server" CssClass="h6" Text="¡Cedula de victima generada con exito! 🎉" Visible="false" />
                                    <br />
                                    <br />
                                    <asp:Button ID="GenerarOtro" CssClass="btn btn-success align-content-center" runat="server" Text="🔙 Administrar otras victimas" Visible="false" OnClick="GenerarOtro_Click" />
                                    <br />
                                    <br />
                                    <iframe id="cedulaimput" runat="server" style="width: 100%; height: 500px;" src=""></iframe>
                            </div>

                            <div class="center-panel" runat="server" id="panelPdfActualizar" style="display: none;">
                                    <br />
                                    <br />
                                    <asp:Label ID="ActExito" runat="server" CssClass="h4" Text="¡Victima actualizada exitosamente! 🎉" Visible="false" />
                                    <br />
                                    <br />
                                    <asp:Label ID="CedulaActExito" runat="server" CssClass="h6" Text="¡Cedula de victima actualizada con éxito! 🎉" Visible="false" />
                                    <br />
                                    <br />
                                    <asp:Button ID="GenerarOtroImp" CssClass="btn btn-success align-content-center" runat="server" Text="🔙 Administrar otras victimas" Visible="false" OnClick="GenerarOtro_Click" />
                                    <br />
                                    <br />
                                    <iframe id="cedulaimputAct" runat="server" style="width: 100%; height: 500px;" src=""></iframe>
                            </div>
                            </center>

                            <br />

                            <div class="scrollable" runat="server" id="TablVicti" style="display:none;">
                                <div class="mb-2 d-flex justify-content-between align-items-center">
                                    <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i> Victimas Asociadas: </span>
                                    <asp:Button ID="buscarVicti" runat="server" CssClass="btn btn-success btn-sm mayusculas" OnClick="AgregarVicitmaForm_Click" Text="➕ Agregar Victima" />
                                </div>
                                        <asp:GridView ID="gvVictimas" CssClass="table table-striped text-center table-hover table-sm" runat="server" OnRowDataBound="gvVictimas_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="3" OnPageIndexChanging="gvVictimas_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="APaterno" HeaderText="Apellido Paterno o Razón Social" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="AMaterno" HeaderText="Apellido Materno" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre(s)" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="Delitos" HeaderText="Delito(s) Cometido(s)" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="Edad" HeaderText="Edad" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="Genero" HeaderText="Genero" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:TemplateField HeaderStyle-CssClass="bg-success text-white">
                                                    <ItemTemplate>
                                                        <asp:HiddenField Visible="False" ID="hidIdAsunto" runat="server" Value='<%# Eval("IdAsunto") %>' />
                                                        <asp:HiddenField Visible="False" ID="hidIdPartes" runat="server" Value='<%# Eval("IdPartes") %>' />
                                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <asp:Button ID="btnFirst" CssClass="btn btn-primary btn-sm mayusculas" runat="server" CommandName="Page" CommandArgument="First" Text=" Primero" />
                                                <asp:Button ID="btnPrev" CssClass="btn btn-success btn-sm mayusculas" runat="server" CommandName="Page" CommandArgument="Prev" Text="⏮️ Anterior" />
                                                <asp:Button ID="btnNext" CssClass="btn btn-success btn-sm mayusculas" runat="server" CommandName="Page" CommandArgument="Next" Text="⏭️ Siguiente" />
                                                <asp:Button ID="btnLast" CssClass="btn btn-primary btn-sm mayusculas" runat="server" CommandName="Page" CommandArgument="Last" Text=" Ultimo" />
                                            </PagerTemplate>
                                        </asp:GridView>
                            </div>

                            <br />
                          
                            <div id="accVictim" runat="server" style="display: none">
                                <div class="mb-2">
                                    <br />
                                    <span class="text-success fw-bold m-2"><i class="bi bi-pencil-square"></i> Información de la Victima: </span>
                                </div>
                                <div class="accordion" id="AccordionVictimas">
                                    <div class="accordion-item">
                                        <h2 class="accordion-header">
                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Element1" aria-expanded="true" aria-controls="collapseOne">
                                                <b>1.- Datos Personales</b>
                                            </button>
                                        </h2>
                                        <div id="Element1" class="accordion-collapse collapse" data-bs-parent="#AccordionVictimas">
                                            <div class="accordion-body">
                                                <h5 class="text-secondary mb-4"><b>Datos Personales de la Victima</b></h5>
                                                <div class="row">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="" class="form-label text-secondary">Apellido Paterno:</label>
                                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese apellido paterno o razón social" ID="APVic"></asp:TextBox>
                                                    </div>
                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="Procedencia" class="form-label text-secondary">Apellido Materno:</label>
                                                        <asp:TextBox runat="server" ID="AMVic" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese apellido materno"></asp:TextBox>
                                                    </div>
                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="fecha1" class="form-label text-secondary">Nombre(s): </label>
                                                        <asp:TextBox runat="server" ID="NomVic" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese el nombre"></asp:TextBox>
                                                    </div>
                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="fojas1" class="form-label text-secondary">Genero: </label>
                                                        <asp:DropDownList ID="GeneVicti" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                            <asp:ListItem runat="server" Value="SG" Selected="True" Text="Seleccione el género..."></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>


                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="fojas1" class="form-label text-secondary">Tipo Victima: </label>
                                                        <asp:DropDownList ID="TipoVict" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TipoVict_SelectedIndexChanged">
                                                            <asp:ListItem runat="server" Value="SV" Selected="True" Text="Seleccione el tipo de víctima..."></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="fojas1" class="form-label text-secondary">Clasificacion de Victima: </label>
                                                        <asp:DropDownList ID="ClasifVicti" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                            <asp:ListItem runat="server" Value="CV" Selected="True" Text="Seleccione la clasificacion de la victima..."></asp:ListItem>
                                                            <asp:ListItem runat="server" Value="1" Text="DIRECTA"></asp:ListItem>
                                                            <asp:ListItem runat="server" Value="0" Text="INDIRECTA"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="fojas1" class="form-label text-secondary">Tipo de Sector: </label>
                                                        <asp:DropDownList ID="SectorVicti" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                            <asp:ListItem runat="server" Value="SS" Selected="True" Text="Seleccione el sector..."></asp:ListItem>
                                                            <asp:ListItem runat="server" Value="PUB" Text="PUBLICO"></asp:ListItem>
                                                            <asp:ListItem runat="server" Value="PRI" Text="PRIVADO"></asp:ListItem>
                                                            <asp:ListItem runat="server" Value="NID" Text="NO IDENTIFICADO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label class="form-label text-secondary">Tipo de Sociedad: </label>
                                                        <asp:DropDownList ID="TipoSocie" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                            <asp:ListItem runat="server" Value="SP" Selected="True" Text="Seleccione el tipo de sociedad..."></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="" class="form-label text-secondary">CURP:</label>
                                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese el CURP de la victima" ID="CURPVicti" MaxLength="18"></asp:TextBox>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="" class="form-label text-secondary">RFC con Homoclave:</label>
                                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese el RFC de la victima" ID="RFCVicti" onchange="formatoRFC(this)" MaxLength="13"></asp:TextBox>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="fecha1" class="form-label text-secondary">Fecha de nacimiento: </label>
                                                        <asp:TextBox runat="server" ID="FeNacVic" CssClass="form-control form-control-sm mayusculas" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                                    </div>

                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                        <label for="" class="form-label text-secondary">Edad:</label>
                                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" Text="0" Type="Number" placeholder="Ingrese la edad de la victima" ID="EdadVicti"></asp:TextBox>
                                                    </div>

                                                        </div>
                                                    </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>

                                                            <label for="" class="form-label text-secondary">Lugar de nacimiento:</label>
                                                            <div class="row">
                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">Continente de nacimiento:</label>--%>
                                                                    <asp:DropDownList ID="ContiNac" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ContiNac_SelectedIndexChanged">
                                                                        <asp:ListItem runat="server" Value="SC" Selected="True" Text="Seleccione el continente..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">País de nacimiento</label>--%>
                                                                    <asp:DropDownList ID="PaisNac" CssClass="form-select form-select-sm text-secondary mayusculas" AutoPostBack="true"  OnSelectedIndexChanged="PaisNac_SelectedIndexChanged" runat="server">
                                                                        <asp:ListItem runat="server" Value="SP" Selected="True" Text="Seleccione el país..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">Estado de nacimiento</label>--%>
                                                                    <asp:DropDownList ID="EstNaci" AutoPostBack="true" OnSelectedIndexChanged="EstNaci_SelectedIndexChanged" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                        <asp:ListItem runat="server" Value="SE" Selected="True" Text="Seleccione el estado..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">Municipio de nacimiento</label>--%>
                                                                    <asp:DropDownList ID="MuniNac" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                        <asp:ListItem runat="server" Value="SM" Selected="True" Text="Seleccione el municipio..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                     </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                                    <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Element2" aria-expanded="false" aria-controls="collapseTwo">
                                                    <b>2.- Datos Generales</b>
                                                </button>
                                            </h2>
                                            <div id="Element2" class="accordion-collapse collapse" data-bs-parent="#AccordionVictimas">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                <div class="accordion-body">
                                                    <h5 class="text-secondary mb-4"><b>Datos Generales de la Victima</b></h5>
                                                            <div class="row">
                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                            <label for="" class="form-label text-secondary">Nacionalidad:</label>
                                                            <asp:DropDownList ID="NacVicti" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SN" Selected="True" Text="Selecciona la nacionalidad..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Habla lengua extranjera:</label>
                                                            <asp:DropDownList ID="HabLenExtra" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SLE" Selected="True" Text="Seleccione una opcion..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Dominio del idioma español:</label>
                                                            <asp:DropDownList ID="HablEsp" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SES" Selected="True" Text="Seleccione una opción..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Lengua indigena:</label>
                                                            <asp:DropDownList ID="LengIndi" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SLI" Selected="True" Text="Seleccione la lengua indigena..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Persona vulnerable:</label>
                                                            <asp:DropDownList ID="VicVulne" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SPV" Selected="True" Text="Seleccione una opcion..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                        </div>

                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                            <label for="" class="form-label text-secondary">Condicion migratoria:</label>
                                                            <asp:DropDownList ID="CondMigVic" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SCM" Selected="True" Text="Seleccione la condición migratoria..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Condicion de alfabetismo:</label>
                                                            <asp:DropDownList ID="CondAlfVic" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SCA" Selected="True" Text="Seleccione una opción..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">¿Habla alguna lengua?:</label>
                                                            <asp:DropDownList ID="HablLengIndi" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="HabLenIndi_SelectedIndexChanged">
                                                                <asp:ListItem runat="server" Value="SHL" Selected="True" Text="Seleccione una opción..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Pueblo indigena:</label>
                                                            <asp:DropDownList ID="PuebloIndi" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SPI" Selected="True" Text="Seleccione una opcion..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <label for="" class="form-label text-secondary">Domicilio del trabajo:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese el lugar de trabajo" ID="DomiTrabVicti"></asp:TextBox>
                                                            <br />
                                                        </div>

                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                            <div class="row">
                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                                    <label for="" class="form-label text-secondary">Estado Civil:</label>
                                                                    <asp:DropDownList ID="EstCivil" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                        <asp:ListItem runat="server" Value="SEC" Selected="True" Text="Seleccione el estado civil..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                                    <label for="" class="form-label text-secondary">Grado de Estudios:</label>
                                                                    <asp:DropDownList ID="GradEst" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                        <asp:ListItem runat="server" Value="SGE" Selected="True" Text="Seleccione el grado de estudios..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                                                <label for="" class="form-label text-secondary">Ocupación:</label>
                                                                                <asp:DropDownList ID="OcupaVicti" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" OnSelectedIndexChanged="DropdownOcupacion_SelectedIndexChanged" AutoPostBack="true">
                                                                                    <asp:ListItem runat="server" Value="SO" Selected="True" Text="Seleccione la ocupación..."></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                                                <label for="" class="form-label text-secondary">Detalle ocupación:</label>
                                                                                <asp:DropDownList ID="DetaOcupaVic" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                                    <asp:ListItem runat="server" Value="SDO" Selected="True" Text="Seleccione el detalle de la ocupación..."></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                                                <label for="" class="form-label text-secondary">Cuenta con discapacidad:</label>
                                                                                <asp:DropDownList ID="CuenDisca" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" OnSelectedIndexChanged="CuenDisca_SelectedIndexChanged" AutoPostBack="true">
                                                                                    <asp:ListItem runat="server" Value="SCD" Selected="True" Text="Seleccione una opción..."></asp:ListItem>
                                                                                    <asp:ListItem runat="server" Value="1" Text="SI"></asp:ListItem>
                                                                                    <asp:ListItem runat="server" Value="2" Text="NO"></asp:ListItem>
                                                                                    <asp:ListItem runat="server" Value="3" Text="NO IDENTIFICADO"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                                                <label for="" class="form-label text-secondary">Tipo discapacidad:</label>
                                                                                <asp:DropDownList ID="TipoDisca" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropdownDiscapacidad_SelectedIndexChanged">
                                                                                    <asp:ListItem runat="server" Value="SD" Selected="True" Text="Seleccione la discapacidad..."></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-12 col-xl-12 col-xxl-12">
                                                                                <label for="" class="form-label text-secondary">Especifique discapacidad:</label>
                                                                                <asp:DropDownList ID="DiscaEspe" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                                    <asp:ListItem runat="server" Value="SD" Selected="True" Text="Seleccione la discapacidad..."></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <!-- Aquí es donde agregamos la nueva tabla -->
                                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-12 col-xl-12 col-xxl-12">
                                                                            <div class="mb-0 row">
                                                                                <div class="col-md-12">
                                                                                    <span class="text-success fw-bold m-2"><i class="bi bi-person-wheelchair"></i> Discapacidades: </span>
                                                                                    <asp:Button ID="btnAgregarDiscapacidad" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="➕" OnClick="btnAgregarDiscapacidad_Click" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-responsive mt-2">
                                                                                <asp:GridView ID="gvDiscapacidades" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="GridView1_RowCommand" ShowHeaderWhenEmpty="true">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="Discapacidad" HeaderText="Discapacidad" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="mayusculas" />
                                                                                        <asp:BoundField DataField="IdDiscapacidad" Visible="false" />
                                                                                        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                                                                            <ItemTemplate>
                                                                                                <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="Eliminar" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>


                                                                                <asp:GridView ID="gvDiscapacidades2" Visible="false" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="GridView1_RowCommand" ShowHeaderWhenEmpty="true">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="DiscapacidadAgregada" HeaderText="Discapacidad" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="mayusculas" />
                                                                                        <asp:BoundField DataField="IdDiscapacidad" Visible="false" />
                                                                                        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                                                                            <ItemTemplate>
                                                                                                <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="Eliminar" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                         </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Element3" aria-expanded="false" aria-controls="collapseThree">
                                                    <b>3.- Datos para Audiencia y Notificación</b>
                                                </button>
                                            </h2>
                                            <div id="Element3" class="accordion-collapse collapse" data-bs-parent="#AccordionVictimas">
                                                <div class="accordion-body">
                                                    <div class="row">
                                                        <h5 class="text-secondary mb-4"><b>Datos para Audiencia y Notificación de la Victima</b></h5>

                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>

                                                                <label for="" class="form-label text-secondary">Lugar de Residencia:</label>
                                                                <div class="row">
                                                                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">Continente de nacimiento:</label>--%>
                                                                    <asp:DropDownList ID="ContiRes" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ContiRes_SelectedIndexChanged">
                                                                        <asp:ListItem runat="server" Value="SC" Selected="True" Text="Seleccione el continente..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">País de nacimiento</label>--%>
                                                                    <asp:DropDownList ID="PaisRes" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PaisRes_SelectedIndexChanged">
                                                                        <asp:ListItem runat="server" Value="SP" Selected="True" Text="Seleccione el país..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">Estado de nacimiento</label>--%>
                                                                    <asp:DropDownList ID="EstaRes" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="EstRes_SelectedIndexChanged">
                                                                        <asp:ListItem runat="server" Value="SE" Selected="True" Text="Seleccione el estado..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                                                    <%--<label for="" class="form-label text-secondary">Municipio de nacimiento</label>--%>
                                                                    <asp:DropDownList ID="MuniRes" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                        <asp:ListItem runat="server" Value="SM" Selected="True" Text="Seleccione el municipio..."></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-12 col-xxl-12">
                                                            <label for="" class="form-label text-secondary">Domicilio personal cierto y referencia de ubicacion: </label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese el lugar de trabajo" ID="DomicPersonVicti"></asp:TextBox>
                                                            <br />
                                                        </div>

                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                            <label for="" class="form-label text-secondary">¿Cuenta con asesor jurídico?:</label>
                                                            <asp:DropDownList ID="AseJur" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SA" Selected="True" Text="Seleccione una opción..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">¿Requiere interprete o traductor?:</label>
                                                            <asp:DropDownList ID="ReqInter" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SO" Selected="True" Text="Seleccione una opción..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">Teléfono:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese un telefono de contacto" ID="TelCont"></asp:TextBox>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">Correo Electronico:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese un correo electronico de contacto" ID="EmailCont"></asp:TextBox>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">Fax:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese un numero de fax" ID="Fax"></asp:TextBox>

                                                        </div>

                                                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                            <label for="" class="form-label text-secondary">Relación con el imputado:</label>
                                                            <asp:DropDownList ID="RelacVic" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SR" Selected="True" Text="Seleccione la relación..."></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">Hora de individualizacion:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" ID="HoraIndivi" TextMode="Time"></asp:TextBox>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">Doumento de identificación:</label>
                                                            <asp:DropDownList ID="IDVicti" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                                                <asp:ListItem runat="server" Value="SM" Selected="True" Text="Seleccione la identificación..."></asp:ListItem>
                                                            </asp:DropDownList>

                                                            <br />

                                                            <label for="" class="form-label text-secondary">Domicilio:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese el domicilio de contacto de la victima" ID="Domici"></asp:TextBox>
                                                            <br />

                                                            <label for="" class="form-label text-secondary">Otro Medio:</label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese un medio alternativo" ID="OtroMed"></asp:TextBox>
                                                        </div>

                                                        <center>
                                                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-12 col-xl-12 col-xxl-12">
                                                                <label for="" class="form-label text-secondary">Datos personales en el desarrollo de la audiencia:</label>
                                                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="AceptaDatos" runat="server" CssClass="myRadioButtonList">
                                                                    <asp:ListItem Value="PUBLICO">Acepta la Publicidad</asp:ListItem>
                                                                    <asp:ListItem Value="PRIVADO">Solicita Reserva</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </center>

                                                    </div>
                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                        </div>
                                    </div>
                                </div>
                            <br />
                                                <center>
                                                                <div class="col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                                                    <asp:Button ID="UpVict" runat="server" Text="🔃 Actualizar" CssClass="btn btn-primary btn-sm mayusculas" OnClick="UpVicti_Click" />
                                                                    <asp:Button ID="SvVicti" runat="server" Text="💾 Guardar" CssClass="btn btn-success btn-sm mayusculas" OnClick="SvVicti_Click" />
                                                                    <asp:Button ID="LimpVicti" runat="server" Text="🧹 Limpiar" CssClass="btn btn-danger btn-sm mayusculas" />
                                                                </div>
                                                </center>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function formatoRFC(input) {
            // Eliminar caracteres que no son dígitos ni letras
            var rfc = input.value.replace(/[^\dA-Za-z]/g, '');

            // Verificar que la longitud del RFC es al menos de 13 caracteres
            if (rfc.length >= 13) {
                // Separar los últimos 3 caracteres con un guión
                var formattedRFC = rfc.substring(0, rfc.length - 3) + '-' + rfc.substring(rfc.length - 3);
                input.value = formattedRFC;
            }
        }

        function formatoNumeroToca(input) {
            // Divide el valor ingresado en el número y el año
            var [number, year] = input.value.split("/");

            // Rellena el número con ceros a la izquierda hasta que tenga 4 dígitos
            number = number.padStart(4, '0');

            // Combina el número y el año para obtener el número de toca
            var numeroToca = number + "/" + year;

            // Establece el valor del input al número de toca
            input.value = numeroToca;
        }
    </script>

                    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("[id*=gvVictimas] input:checkbox").click(function () {
                                var selected = $(this).is(":checked");
                                $("[id*=gvVictimas] input:checkbox").prop("checked", false);
                                if (selected) {
                                    $(this).prop("checked", true);
                                }
                            });
                        });
                    </script>



    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js" integrity="sha384-0pUGZvbkm6XF6gxjEnlmuGrJXVbNuzT9qBBavbLwCsOGabYfZo0T0to5eqruptLy" crossorigin="anonymous"></script>

</asp:Content>
