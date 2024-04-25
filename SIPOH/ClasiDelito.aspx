<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="ClasiDelito.aspx.cs" Inherits="SIPOH.clasidelito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentClasiDelito" ContentPlaceHolderID="ContentClasiDelito" runat="server">
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }
    </style>
    <asp:ScriptManager ID="ScriptManagerClasiDeli" runat="server"></asp:ScriptManager>
    <link href="Conteffnt/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
    <link href="Content/css/Iniciales.css" rel="stylesheet" />
    <div>
        <h1 style="margin-left: 5%" class="h5">Clasificación de delitos <i class="fas fa-angle-right"></i><span
            id="dataSplash" class="text-primary fw-bold"></span></h1>
    </div>
    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="tab-content ">
                            <div class="tab-pane active" id="divBusqueda" role="tabpanel">
                                <asp:UpdatePanel ID="UpdatePanelClasiDeli" runat="server">
                                    <ContentTemplate>
                                        <div class="row" id="rowBusqueda" runat="server">
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Tipo Asunto</label>
                                                <asp:DropDownList runat="server" ID="ddlTipoAsunto" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>SELECCIONAR</asp:ListItem>
                                                    <asp:ListItem>CAUSA</asp:ListItem>
                                                    <asp:ListItem>CUPRE</asp:ListItem>
                                                    <asp:ListItem>JUICIO ORAL</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                <label class="form-label text-secondary">Número</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="txtNumeroAsunto" class="form-control form-control-sm mayusculas" />
                                                    <div class="input-group-append">
                                                        <asp:Button ID="btnBuscarClasiDelito" runat="server" Text="Buscar"
                                                            CssClass="btn btn-outline-primary btn-sm ml-2" />
                                                        <asp:Button ID="btnAgregarClasiDelito" runat="server" Text="Agregar"
                                                            CssClass="btn btn-outline-success btn-sm ml-2" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12 col-lg-12 mt-2 table-responsive">
                                                <table class="table table-sm table-bordered custom-gridview">
                                                    <thead>
                                                        <tr>
                                                            <th class="bg-success text-white">Delito</th>
                                                            <th class="bg-success text-white">Grado de consumación</th>
                                                            <th class="bg-success text-white">Calificación</th>
                                                            <th class="bg-success text-white">Tipo de concurso</th>
                                                            <th class="bg-success text-white">Clasificación de resultado</th>
                                                            <th class="bg-success text-white">Comisión</th>
                                                            <th class="bg-success text-white">Acción</th>
                                                            <th class="bg-success text-white">Modalidad</th>
                                                            <th class="bg-success text-white">Elementos de comisión</th>
                                                            <th class="bg-success text-white">Lugar de ocurrencia del delito</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>ROBO</td>
                                                            <td>CONSUMADO</td>
                                                            <td>SE DESCONOCE</td>
                                                            <td>NO HAY CONCURSO DE DELITOS</td>
                                                            <td>INSTANTANEO</td>
                                                            <td>DOLOSO</td>
                                                            <td>SIN VIOLENCIA</td>
                                                            <td>CALIFICADO</td>
                                                            <td>CON ALGUNA PARTE DEL CUERPO</td>
                                                            <td>TLAHUELILPAN</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row justify-content-center">
                                            <div style="display: flex; align-items: center; justify-content: center;">
                                                <p style="margin-right: 10px; margin-top: 10px;">Reclasificacion del delito </p>
                                                <div class="form-check" style="margin-right: 10px;">
                                                    <asp:RadioButton ID="CheckSi" runat="server" CssClass="radio-custom" />
                                                    <label class="form-check-label" for="CheckSi">
                                                        Si
                                                    </label>
                                                </div>
                                                <div class="form-check">
                                                    <asp:RadioButton ID="CheckNo" runat="server" />
                                                    <label class="form-check-label" for="CheckNo">
                                                        No
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <p></p>
                                        <div class="row">
                                            <div class="col-7 col-sm-12 col-md-7 col-lg-7 col-xl-7">
                                                <div class="form-group">
                                                    <label class="form-label text-secondary">Tipo de persecución del delito</label>
                                                    <div class="row">
                                                        <!-- Añade una fila para contener las columnas -->
                                                        <!-- Primera columna -->
                                                        <div class="col-6">
                                                            <div class="form-check">
                                                                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                                <label class="form-check-label" for="RadioButton3">
                                                                    Querella
                                                                </label>
                                                            </div>
                                                            <div class="form-check">
                                                                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                                <label class="form-check-label" for="RadioButton1">
                                                                    Denuncia
                                                                </label>
                                                            </div>
                                                            <div class="form-check">
                                                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                                <label class="form-check-label" for="RadioButton2">
                                                                    No Identificado
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-5 col-sm-12 col-md-5 col-lg-5 col-xl-5">
                                                <label class="form-label text-secondary">Fecha en que ocurrio el delito</label>
                                                <div class="form-check">
                                                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                    <label class="form-check-label" for="RadioButton4">
                                                        No Identificado
                                                    </label>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="form-label text-secondary" for="FechaReclasificacion">Fecha de reclasificación</label>
                                                <asp:TextBox ID="FechaReclasificacion" runat="server" CssClass="form-control form-control-sm" TextMode="Date" />
                                            </div>
                                            <div class="col-md-6">
                                                <label class="form-label text-secondary" for="FechaDelito">Fecha Ocurrió el delito</label>
                                                <asp:TextBox ID="FechaDelito" runat="server" CssClass="form-control form-control-sm" TextMode="Date" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12">
                                                <label class="form-label text-secondary">Delito:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox8" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <label class="form-label text-secondary">Modalidad:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox9" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12 col-lg-12 mt-2 table-responsive">
                                                <table class="table table-sm table-bordered custom-gridview">
                                                    <thead>
                                                        <tr>
                                                            <th class="bg-success text-white">Modalidad</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>ROBO</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Lugar de ocurrencia:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList9" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatMunicipios</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Localidad:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox1" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Calle:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox2" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Número:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox3" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Manzana:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox4" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Barrio:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox5" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Colonia:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox6" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label class="form-label text-secondary">*Lote:</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="TextBox7" class="form-control form-control-sm mayusculas" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <b>clasificacion del delito</b>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Grado de consumación:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatGradoConsumacion</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Tipo de concurso:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatConcurso</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Forma de acción:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList3" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatFormaAccion</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Calificación:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList4" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatCalificacion</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Orden de resultado:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList5" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatClasificacion</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Elementos para su comisión:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList6" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatElementosComision</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Forma de comisión:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList7" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatFormaComision</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Modalidad:</label>
                                                <asp:DropDownList runat="server" ID="DropDownList8" CssClass="form-select form-select-sm" AutoPostBack="true">
                                                    <asp:ListItem>P_CatModalidad</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <p></p>
                                        <div class="row justify-content-md-center">
                                            <div class="col col-lg-2">
                                                <asp:Button ID="btnGuardarClasiDeli" runat="server" Text="Guardar" CssClass="btn btn-sm btn-success" />
                                            </div>
                                            <div class="col col-lg-2">
                                                <asp:Button ID="btnUpdateClasiDeli" runat="server" Text="Agregar" CssClass="btn btn-sm btn-primary" />
                                            </div>
                                            <div class="col col-lg-2">
                                                <asp:Button ID="btnDeleteClasiDeli" runat="server" Text="Actualizar" CssClass="btn btn-sm btn-warning" />
                                            </div>
                                            <div class="col col-lg-2">
                                                <asp:Button ID="Button1" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" />
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
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"
        integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js"
        integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+"
        crossorigin="anonymous"></script>
    <script src="Scripts/consignaciones/Consignaciones.js"></script>
    <script src="Scripts/Ejecucion/formatoInput.js" charset="utf-8"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
</asp:Content>
