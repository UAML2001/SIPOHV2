<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="BuzonControl.aspx.cs" Inherits="SIPOH.BuzonControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentBuzonControl" ContentPlaceHolderID="ContentBuzonControl" runat="server">
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }
    </style>
    <asp:ScriptManager ID="ScriptManagerPromociones" runat="server"></asp:ScriptManager>
    <link href="Conteffnt/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
    <link href="Content/css/Iniciales.css" rel="stylesheet" />
    <div>
        <h1 style="margin-left: 5%" class="h5">Buzón Digital <i class="fas fa-angle-right"></i><span
            id="dataSplash" class="text-primary fw-bold"></span></h1>
    </div>
    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="tab-content ">
                            <div class="tab-pane active" id="divBuzonControl" role="tabpanel">
                                <asp:UpdatePanel ID="UpdatePanelBuzon" runat="server">
                                    <ContentTemplate>
                                        <div class="row" id="rowBuzonControl" runat="server">
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                <label for="ddlTipoBusqueda" class="form-label text-secondary">Tipo de busqueda</label>
                                                <asp:DropDownList runat="server" ID="ddlTipoBusqueda" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoBusqueda_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                                <label class="form-label text-secondary">Número de NUC, asunto o folio</label>
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" type="text" ID="txtNumeroAsunto" class="form-control mayusculas" />
                                                    <div class="input-group-append">
                                                        <asp:Button ID="btnBuscarBuzon" runat="server" Text="Buscar" CssClass="btn btn-success btn-block mb-4" OnClick="btnBuscarBuzon_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12 col-lg-12 mt-2 table-responsive">
                                                <asp:GridView ID="gridBuzonControl" runat="server" DataKeyNames="IdSolicitudBuzon, RutaDoc"
                                                    OnRowCommand="gridBuzonControl_RowCommand" AutoGenerateColumns="false"
                                                    CssClass="table table-sm table-striped table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="gridBuzonControl_PageIndexChanging">
                                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="Primera" LastPageText="Última" />
                                                    <PagerStyle CssClass="pager" HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:BoundField DataField="IdSolicitudBuzon" HeaderText="Folio">
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                            <ItemStyle CssClass="p-2" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud">
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                            <ItemStyle CssClass="p-2" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha de Solicitud">
                                                            <ItemStyle CssClass="p-2" />
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                                            <ItemStyle CssClass="p-2" />
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Estatus" HeaderText="Estatus">
                                                            <ItemStyle CssClass="p-2" />
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoAsunto" HeaderText="Tipo de Asunto">
                                                            <ItemStyle CssClass="p-2" />
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Numero" HeaderText="Número de Asunto">
                                                            <ItemStyle CssClass="p-2" />
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FeIngreso" HeaderText="Fecha de Aceptación"
                                                            DataFormatString="{0:dd/MM/yyyy}"
                                                            NullDisplayText="EN ESPERA">
                                                            <ItemStyle CssClass="p-2" />
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Ver">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnVerDocumento" runat="server" CommandName="Ver" Text="👁️"
                                                                    CssClass="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" />

                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                            <ItemStyle CssClass="p-2 text-center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registrar">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnGuardarBuzon" runat="server" Text="📄" CssClass="btn btn-success"
                                                                    CommandName="Guardar" CommandArgument="<%# Container.DataItemIndex %>" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                            <ItemStyle CssClass="p-2 text-center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rechazar">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnCancelarBuzon" runat="server" CommandName="Rechazar"
                                                                    Text="✖️" CssClass="btn btn-danger" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                    OnClientClick="return confirmarRechazo();" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="bg-success text-white" />
                                                            <ItemStyle CssClass="p-2 text-center" />
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
        window.addEventListener('keydown', function (e) {
            var node = (e.target) ? e.target : ((e.srcElement) ? e.srcElement : null);
            if ((e.keyCode == 13) && (node.type == "text" || node.type == "number")) {
                e.preventDefault();
                return false;
            }
        }, true);
    </script>
    <script>
        function aplicarEventos() {
            var ddlTipoFiltrado = document.getElementById('<%= ddlTipoBusqueda.ClientID %>');
            var inputBuscarInicial = document.getElementById('<%= txtNumeroAsunto.ClientID %>');
            if (!ddlTipoFiltrado || !inputBuscarInicial) {
                return;
            }
            seleccionarOpcion();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(aplicarEventos);
        function seleccionarOpcion() {
            var ddlTipoFiltrado = document.getElementById('<%= ddlTipoBusqueda.ClientID %>');
            var inputBuscarInicial = document.getElementById('<%= txtNumeroAsunto.ClientID %>');
            if (!ddlTipoFiltrado || !inputBuscarInicial) {
                alert('Alguno de los elementos no está disponible.');
                return;
            }
            if (ddlTipoFiltrado.value === "N") {
                inputBuscarInicial.onblur = function () { formatNuc2(this); };
            } else if (ddlTipoFiltrado.value === "C" || ddlTipoFiltrado.value === "JO") {
                inputBuscarInicial.onblur = function () { padLeadingZeros(this); };
            } else {
                inputBuscarInicial.onblur = null;
            }
        }
    </script>
    <script type="text/javascript">
        function confirmarRechazo() {
            return confirm('¿Estás seguro de que deseas rechazar este elemento?');
        }
    </script>

</asp:Content>
