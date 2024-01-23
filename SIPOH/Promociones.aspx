<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true"
    CodeBehind="Promociones.aspx.cs" Inherits="SIPOH.Promociones" %>



    <asp:Content ID="ContentPromociones7" ContentPlaceHolderID="ContentEPromociones" runat="server">
        <div class="container">
            <asp:ScriptManager ID="ScriptManagerPromociones" runat="server"></asp:ScriptManager>
            <link href="Content/css/Consignaciones.css" rel="stylesheet" />
            <link rel="stylesheet"
                href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
            <link rel="stylesheet" type="text/css"
                href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
            <link href="Content/css/Iniciales.css" rel="stylesheet" />
            <div>
                <h1 style="margin-left: 5%" class="h5">Promociones <i class="fas fa-angle-right"></i><span
                        id="dataSplash" class="text-primary fw-bold"></span></h1>
            </div>

            <div class="card">

                <div class="card-body">
                    <!-- Tab panes -->
                    <div class="tab-content ">
                        <div class="tab-pane active" id="IAcusatorio" role="tabpanel">
                            <asp:UpdatePanel ID="UpdatePanelPromociones" runat="server">

                                <ContentTemplate>
                                    <div class="modal fade" id="guardarDatos2" role="dialog">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header modalGuardar">
                                                    <h5 class="modal-title">
                                                        <b>Verificación de datos a guardar</b>
                                                    </h5>
                                                    <button type="button" class="close"
                                                        data-bs-dismiss="modal">×</button>
                                                </div>
                                                <div class="modal-body">
                                                    <p class="DatosModal"><b>Está a punto de registrar una promoción,
                                                            ¿Desea continuar?</b></p>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-outline-danger"
                                                        data-bs-dismiss="modal">Cancelar</button>
                                                    <asp:Button ID="btnGuardarAcusatorio" runat="server"
                                                        CssClass="btn btn-outline-warning"
                                                        OnClick="btnGuardarPromocion_Click" Text="Guardar" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="primerRowPromocion" runat="server">
                                        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                            <label for="inputRadicacion" class="form-label text-secondary">Nombre de
                                                Juzgado</label>
                                            <select class="form-select form-select-sm" id="selectBusJuzgados"
                                                runat="server">
                                                <option selected>Seleccionar</option>
                                            </select>
                                        </div>
                                        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                            <label for="LabelEjecucion" class="form-label text-secondary">Numero de
                                                Ejecución</label>
                                            <div class="input-group">
                                                <input type="text" class="form-control form-control-sm"
                                                    id="inpuBusEjecucion" runat="server" maxlength="9" onblur="padLeadingZeros(this)" placeholder="">
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnBuscarPromocion" runat="server" Text="Buscar"
                                                        CssClass="btn btn-outline-secondary btn-sm ml-2"
                                                        OnClick="btnBuscarPromocion_Click" />
                                                    <button id="btnLimpiarPromocion" runat="server" type="button"
                                                        class="btn btn-outline-danger btn-sm ml-2"
                                                        onserverclick="btnLimpiarPromocion_Click">Limpiar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <p></p>
                                    <div class="row">
                                        <asp:Label ID="tituloTablaPromociones" runat="server"
                                            CssClass="textoTablasArriba">
                                            <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta de
                                                    promociones</i></h2>
                                        </asp:Label>
                                        <asp:GridView ID="GridViewPromociones" CssClass="table custom-gridview"
                                            runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="GridViewPromociones_RowCommand"
                                            OnRowDataBound="GridViewPromociones_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HiddenIdEjecucion" runat="server"
                                                            Value='<%# Bind("IdEjecucion") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NoEjecucion" HeaderText="N° Ejecución">
                                                    <HeaderStyle CssClass="bg-success text-white" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Juzgado" HeaderText="Juzgado">
                                                    <HeaderStyle CssClass="bg-success text-white" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante">
                                                    <HeaderStyle CssClass="bg-success text-white" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario">
                                                    <HeaderStyle CssClass="bg-success text-white" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Solicitud" HeaderText="Solicitud">
                                                    <HeaderStyle CssClass="bg-success text-white" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <HeaderStyle CssClass="bg-success text-white" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnVerDetalleCausa" runat="server"
                                                            CommandName="VerDetalles"
                                                            CommandArgument='<%# Eval("IdAsunto") %>' Text="Causa"
                                                            CssClass="btn btn-secondary" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="tituloDetallesCausa" runat="server" CssClass="textoTablasArriba">
                                            <h2 class="textoTablasArriba"><i class="bi bi-table">Causas Relacionadas</i>
                                            </h2>
                                        </asp:Label>
                                        <div id="detallesCausa" class="table-responsive" runat="server"></div>
                                    </div>
                                    <div class="container" id="insertarPromoventeAnexos" runat="server">
                                        <p></p>
                                        <div class="row">
                                            <form class="container-lg col-xxl-12">
                                                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                                    <label for="labelPromoNombre"
                                                        class="form-label text-secondary">Promovente Nombre(s)</label>
                                                    <input type="text" class="form-control form-control-sm"
                                                        id="inPromoventeNombre" runat="server"
                                                        onkeyup="verificarCampos()">
                                                </div>
                                                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                                    <label for="LabelPromoApellidoP"
                                                        class="form-label text-secondary">Apellido Paterno</label>
                                                    <input type="text" class="form-control form-control-sm"
                                                        id="inPromoventePaterno" runat="server"
                                                        onkeyup="verificarCampos()">
                                                </div>
                                                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                                    <label for="LabelPromoApellidoM"
                                                        class="form-label text-secondary">Apellido Materno</label>
                                                    <div class="input-group">
                                                        <input type="text" class="form-control form-control-sm"
                                                            id="inPromoventeMaterno" clientidmode="Static"
                                                            runat="server" onkeyup="verificarCampos()">
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                        <p></p>
                                        <div class="row">
                                            <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                                                <label class="form-label-sm text-secondary">Anexos</label>
                                                <asp:DropDownList ID="CatAnexosDD" runat="server"
                                                    CssClass="form-select form-select-sm" AutoPostBack="true"
                                                    OnSelectedIndexChanged="CatAnexosDD_SelectedIndexChanged">
                                                    <asp:ListItem Text="Seleccionar" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                                                <label class="form-label text-secondary">Otro Anexo</label>
                                                <input type="text" id="OtroAnexo" class="form-control form-control-sm"
                                                    runat="server" clientidmode="Static" />
                                            </div>
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                                <label class="form-label text-secondary">Cantidad</label>
                                                <div class="input-group">
                                                    <input type="number" id="CantidadInput"
                                                        class="form-control form-control-sm" runat="server"
                                                        clientidmode="Static">
                                                    <div class="input-group-append">
                                                        <asp:Button ID="AgregarBtn" runat="server" Text="Agregar"
                                                            CssClass="btn btn-outline-secondary btn-sm"
                                                            OnClick="AgregarATabla" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="TablasAnexos" runat="server" CssClass="textoTablasArriba">
                                                <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla de Anexos</i>
                                                </h2>
                                            </asp:Label>
                                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12">
                                                <asp:GridView ID="tablaDatos" CssClass="table" runat="server"
                                                    AutoGenerateColumns="False" OnRowDeleting="BorrarFila"
                                                    OnRowDataBound="tablaDatos_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="NombreSala" HeaderText="Anexo">
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumeroToca" HeaderText="Cantidad">
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar"
                                                            DeleteText="Borrar">
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="container" id="BotonGuardarDiv" runat="server">
                                            <div class="row justify-content-md-center">
                                                <div class="col-xl-2 col-sm-12 col-md-2">
                                                    <asp:Button ID="btnGuardarDatosModal" runat="server"
                                                        Text="Guardar Datos"
                                                        CssClass="btn btn-outline-secondary btn-sm col-12"
                                                        OnClick="btnModalPromociones_Click" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="container" id="tituloSello" runat="server">
                                        <div class="row justify-content-center">
                                            <div class="col-auto">
                                                <h3 class="text-center">¡Se ha guardado tu informacion y ya puedes
                                                    imprimir tu sello aqui!</h3>
                                            </div>
                                        </div>
                                        <p></p>
                                        <div class="row justify-content-center">
                                            <div
                                                class="col-sm-12 col-md-6 col-lg-6 col-xl-6 col-xxl-6 d-flex justify-content-center">
                                                <button class="btn btn-success" onclick="imprimirTicket()">Imprimir
                                                    SELLO</button>
                                            </div>
                                            <div
                                                class="col-sm-12 col-md-6 col-lg-6 col-xl-6 col-xxl-6 d-flex justify-content-center">
                                                <button class="btn btn-primary" onclick="recargarPagina()">Registrar
                                                    otra inicial</button>
                                            </div>
                                        </div>
                                    </div>
                                    <p></p>


                                    <pre id="TicketDiv" runat="server"></pre>
                                </ContentTemplate>

                            </asp:UpdatePanel>

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
            <script src="Scripts/Ejecucion/formatoInput.js"></script>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
            <script>
                toastr.options = {
                    closeButton: true,
                    debug: false,
                    newestOnTop: false,
                    progressBar: true,
                    positionClass: "toast-bottom-right",
                    preventDuplicates: false,
                    onclick: null,
                    showDuration: "300",
                    hideDuration: "1000",
                    timeOut: "5000",
                    extendedTimeOut: "1000",
                    showEasing: "swing",
                    hideEasing: "linear",
                    showMethod: "fadeIn",
                    hideMethod: "fadeOut"
                };

                function mostrarToast(mensaje) {
                    toastr.success(mensaje, "Exito");
                }
                function toastError(mensaje) {
                    toastr.error(mensaje, "Error");
                }
                function toastInfo(mensaje) {
                    toastr.info(mensaje, "Informacion");
                }
                function toastWarning(mensaje) {
                    toastr.warning(mensaje, "Atención");
                }

            </script>
            <script>
                function abrirModalGuardarDatos2() {
                    $('#guardarDatos2').modal('show');
                }
                function CerrarModalGuardarDatos2() {
                    $('#guardarDatos2').modal('hide');
                    $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
                    $('.modal-backdrop').remove();
                }
            </script>
            <script type="text/javascript">
                function verificarCampos() {
                    var nombre = document.getElementById('<%= inPromoventeNombre.ClientID %>').value;
                    var apellidoPaterno = document.getElementById('<%= inPromoventePaterno.ClientID %>').value;
                    var apellidoMaterno = document.getElementById('<%= inPromoventeMaterno.ClientID %>').value;

                    var botonGuardar = document.getElementById('<%= btnGuardarDatosModal.ClientID %>');

                    if (nombre.trim() !== '' && apellidoPaterno.trim() !== '' && apellidoMaterno.trim() !== '') {
                        botonGuardar.disabled = false;
                    } else {
                        botonGuardar.disabled = true;
                    }
                }
            </script>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);

                function endRequestHandler(sender, args) {
                    verificarCampos(); // Llamar a verificarCampos después de cada postback parcial
                }
            </script>
            <script>
                window.addEventListener('keydown', function (e) {
                    var node = (e.target) ? e.target : ((e.srcElement) ? e.srcElement : null);
                    if ((e.keyCode == 13) && (node.type == "text")) {
                        e.preventDefault();
                        return false;
                    }
                }, true);
            </script>
            <script>
                function imprimirTicket() {
                    var contenido = document.getElementById('<%= TicketDiv.ClientID %>').innerHTML;
                    var ventanaImpresion = window.open('', '_blank');

                    var estilos = `<style>
                pre {
                    font-family: monospace;
                    white-space: pre;
                    margin: 1em 0;
                }
            </style>`;

                    ventanaImpresion.document.write(estilos + '<pre>' + contenido + '</pre>');
                    ventanaImpresion.document.close();
                    ventanaImpresion.print();
                    ventanaImpresion.onfocus = function () { setTimeout(function () { ventanaImpresion.close(); }, 500); }
                }

                function recargarPagina() {
                    window.location.href = window.location.href;
                }
            </script>
        </div>
    </asp:Content>