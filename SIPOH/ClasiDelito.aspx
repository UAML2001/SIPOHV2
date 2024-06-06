<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="ClasiDelito.aspx.cs" Inherits="SIPOH.clasidelito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentClasiDelito" ContentPlaceHolderID="ContentClasiDelito" runat="server">
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }

        .selected-row {
            background-color: #a1d99b !important;
        }
    </style>

    <div class="modal fade" id="confirmacionBorrar" aria-hidden="true" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="lblTituloConfirmacionModal">¿Seguro que quiere Eliminar esta clasificacion del delito?</h1>
                <div class="modal-body">
                    <span>Recuerda que estaras eliminando la relacion entre el asunto y el delito.</span>
                </div>
                <div class="modal-footer">
                     <button type="button" class="btn btn-warning" onclick="CerrarModalConfirmar()" data-bs-dismiss="modal">No</button>
                     <asp:Button ID="btnBorrarClasiDelito" runat="server" Text="Si"
                        CssClass="btn btn-success ml-2" OnClick="btnBorrarClasiDelito_Click" />

                </div>
            </div>
        </div>
      </div>

    <asp:ScriptManager ID="ScriptManagerClasiDeli" runat="server"></asp:ScriptManager>
    <link href="Conteffnt/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
    <link href="Content/css/Iniciales.css" rel="stylesheet" />
    <link href="Content/css/Permiso.css" rel="stylesheet" />
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
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridViewClasificacionDelitos" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="row" id="rowBusqueda" runat="server">
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                <label for="lbltipoasunto" class="form-label text-secondary">Tipo Asunto</label>
                                                <asp:DropDownList runat="server" ID="ddlTipoAsunto" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoAsunto_SelectedIndexChanged">
                                                    <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                    <asp:ListItem Value="C">CAUSA</asp:ListItem>
                                                    <asp:ListItem Value="CP">CUPRE</asp:ListItem>
                                                    <asp:ListItem Value="JO">JUICIO ORAL</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                <label class="form-label text-secondary">Número</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="txtNumeroAsunto" class="form-control form-control-sm mayusculas" onblur="padLeadingZeros(this)" />
                                                    <div class="input-group-append">
                                                        <asp:Button ID="btnBuscarClasiDelito" runat="server" Text="Buscar"
                                                            CssClass="btn btn-outline-primary btn-sm ml-2" OnClick="btnBuscarClasiDelito_Click" />
                                                        <asp:Button ID="btnAgregarClasiDelito" runat="server" Text="Agregar"
                                                            CssClass="btn btn-outline-success btn-sm ml-2" OnClick="btnAgregarClasiDelito_Click" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12 col-lg-12 mt-2 table-responsive">
                                                <asp:GridView ID="GridViewClasificacionDelitos" ShowHeaderWhenEmpty="true" EmptyDataText="No hay datos disponibles"
                                                    runat="server" CssClass="table table-sm table-bordered custom-gridview" AutoGenerateColumns="False"
                                                    OnSelectedIndexChanged="GridViewClasificacionDelitos_SelectedIndexChanged"
                                                    DataKeyNames="IdDelitoC,IdAsunto,IdDeliAsunto,IdMunicipio,Id_CatConsumacion,Id_CatConcurso,Id_CatAccion,Id_CatCalificacion,Id_CatClasificacion,Id_CatComision,Id_CatElemComision,Id_CatModalidad,IdDelDetalle,Persecucion,FeDelito,Domicilio">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButtonSelect" runat="server" CommandName="Select" Text='<%# Bind("Nombre") %>'
                                                                    Style="text-decoration: none; color: inherit;"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>Delito</HeaderTemplate>
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Consumacion" HeaderText="Grado de consumación" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Calificacion" HeaderText="Calificación" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Concurso" HeaderText="Tipo de concurso" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Clasificacion" HeaderText="Clasificación de resultado" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Comision" HeaderText="Comisión" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Accion" HeaderText="Acción" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Modalidad" HeaderText="Modalidad" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="ElemComision" HeaderText="Elementos de comisión" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Municipio" HeaderText="Lugar de ocurrencia del delito" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:TemplateField HeaderText="Persecución del delito" HeaderStyle-CssClass="bg-success text-white">
                                                            <ItemTemplate>
                                                                <%# ConvertPersecucion(Eval("Persecucion")) %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="FeDelito" HeaderText="Fecha delito" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" HeaderStyle-CssClass="bg-success text-white" />
                                                        <asp:BoundField DataField="IdDelDetalle" HeaderText="IdDelDetalle" Visible="false" />
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <div class="row justify-content-center" id="divCheckReclasificar" runat="server">
                                            <div style="display: flex; align-items: center; justify-content: center;">
                                                <p style="margin-right: 10px; margin-top: 10px;">Reclasificación del delito</p>
                                                <div class="form-check" style="margin-right: 10px;">
                                                    <asp:CheckBox ID="reclasificacionSi" runat="server" CssClass="checkbox-custom" AutoPostBack="true" OnCheckedChanged="reclasificacionSi_CheckedChanged" />
                                                    <label class="form-check-label" for="reclasificacionSi">
                                                        Sí
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="divFechaReclasificacion" runat="server" clientidmode="Static">
                                            <div class="col-md-12">
                                                <label class="form-label text-secondary" for="FechaReclasificacion">Fecha de reclasificación</label>
                                                <asp:TextBox ID="FechaReclasificacion" runat="server" CssClass="form-control form-control-sm" TextMode="Date" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12">
                                                <label class="form-label text-secondary">Delito:</label>
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddDelitos" runat="server" AutoPostBack="true"
                                                        CssClass="form-control form-control-sm mayusculas" Enabled="false"
                                                        OnSelectedIndexChanged="ddDelitos_SelectedIndexChanged" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                           <div class="col-12" id="divModalidad" style="display: none;" runat="server">
                                                <label class="form-label text-secondary">Modalidad:</label>
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlModDelito" runat="server" AutoPostBack="true"
                                                                      CssClass="form-control form-control-sm mayusculas">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>

                                        <p></p>
                                        <div style="display: none;" id="divAgregarClasificacion" runat="server" clientidmode="Static">


                                            <div class="row">
                                                <label class="form-label text-secondary">Tipo de persecución del delito</label>
                                                <div class="col-4">
                                                    <div class="form-check">
                                                        <asp:RadioButton ID="rbQuerella" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                        <label class="form-check-label" for="RadioButton3">
                                                            Querella
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="form-check">
                                                        <asp:RadioButton ID="rbDenuncia" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                        <label class="form-check-label" for="RadioButton1">
                                                            Denuncia
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="form-check">
                                                        <asp:RadioButton ID="rbNoIdentificado" runat="server" GroupName="PersecucionDelito" Text="" CssClass="" />
                                                        <label class="form-check-label" for="RadioButton2">
                                                            No Identificado
                                                        </label>
                                                    </div>
                                                </div>

                                            </div>


                                            <hr />
                                            <p></p>
                                           <div class="row">
                                                <div class="col-12 col-md-12">
                                                    <label class="form-label text-secondary">Fecha en que ocurrió el delito</label>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox ID="checkNoIdentificado" runat="server" AutoPostBack="true" OnCheckedChanged="checkNoIdentificado_CheckedChanged" />
                                                    <label class="form-check-label" for="checkNoIdentificado">No Identificado</label>
                                                </div>
                                                <div class="col-12 col-md-12">
                                                    <asp:TextBox ID="FechaDelito" runat="server" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-md-12 col-lg-12 col-xl-12 col-xxl-12 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Lugar de ocurrencia:</label>
                                                    <asp:DropDownList runat="server" ID="ddlCatMunicipios" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-12 col-lg-12 col-xl-12 col-xxl-12 col-sm-12">
                                                    <label class="form-label text-secondary">*Lugar de ocurrencia:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox runat="server" type="text" ID="txtLocalidad" class="form-control form-control-sm mayusculas" />
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <b>clasificacion del delito</b>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Grado de consumación:</label>
                                                    <asp:DropDownList ID="ddlGradoConsumacion" runat="server" CssClass="form-select form-select-sm mayusculas" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoconcurso" class="form-label text-secondary">Tipo de concurso:</label>
                                                    <asp:DropDownList runat="server" ID="ddlConcurso" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lblformaaccion" class="form-label text-secondary">Forma de acción:</label>
                                                    <asp:DropDownList runat="server" ID="ddlFormaAccion" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Calificación:</label>
                                                    <asp:DropDownList runat="server" ID="ddlCalificacion" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Orden de resultado:</label>
                                                    <asp:DropDownList runat="server" ID="ddlOrdenResultado" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Elementos para su comisión:</label>
                                                    <asp:DropDownList runat="server" ID="ddlComision" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Forma de comisión:</label>
                                                    <asp:DropDownList runat="server" ID="ddlFormaComision" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-lg-3 col-xl-3 col-xxl-3 col-sm-12">
                                                    <label for="lbltipoasunto" class="form-label text-secondary">Modalidad:</label>
                                                    <asp:DropDownList runat="server" ID="ddlModalidad" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <p></p>
                                            <div class="row justify-content-md-center">
                                                <div class="col col-lg-2">
                                                    <asp:Button ID="btnGuardarClasiDeli" runat="server" Text="Guardar o Modificar" CssClass="btn btn-sm btn-outline-primary" OnClick="btnGuardarClasiDeli_Click"/>
                                                </div>
                                                <div class="col col-lg-2">
                                                    <asp:Button OnClick="btnModalBorrar_Click" ID="btnModalBorrar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger" />
                                                </div>
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
    <script src="Scripts/consignaciones/toast.js"></script>
    <script type="text/javascript">


        function setDefaultDate(isChecked) {
            var fechaDelito = document.getElementById('FechaDelito');
            if (isChecked) {
                fechaDelito.value = '1899-09-09';
            } else {
                fechaDelito.value = '';
            }
        }
        function setFechaDelito(fecha) {
            var fechaDelito = document.getElementById('FechaDelito');
            fechaDelito.value = fecha;
        }
        // para pintar la grid
        $(document).ready(function () {
            $('#<%= GridViewClasificacionDelitos.ClientID %> tr').click(function () {
                // Remover la clase 'selected-row' de todas las filas
                $('#<%= GridViewClasificacionDelitos.ClientID %> tr').removeClass('selected-row');

                // Añadir la clase 'selected-row' a la fila actualmente clickeada
                $(this).addClass('selected-row');
            });
        });

        function abrirModalConfirmar() {
            $('#confirmacionBorrar').modal('show');
        }

        function CerrarModalConfirmar() {
            $('#confirmacionBorrar').modal('hide');
            $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
            $('.modal-backdrop').remove();
        }
    </script>

</asp:Content>
