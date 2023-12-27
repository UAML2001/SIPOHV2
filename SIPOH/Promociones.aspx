
<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Promociones.aspx.cs" Inherits="SIPOH.Promociones" %>



<asp:Content ID="ContentPromociones7" ContentPlaceHolderID="ContentEPromociones" runat="server">
    <div class="container">
        <asp:ScriptManager ID="ScriptManagerPromociones" runat="server"></asp:ScriptManager>
        <link href="Content/css/Consignaciones.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
        <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
        <div>
            <h1 style="margin-left: 5%" class="h5">Promociones <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"></span></h1>
        </div>
        <div class="card">

            <div class="card-body">
                <!-- Tab panes -->
                <div class="tab-content ">
                    <div class="tab-pane active" id="IAcusatorio" role="tabpanel">
                        <asp:UpdatePanel ID="UpdatePanelPromociones" runat="server">
                        <ContentTemplate>
                        <div class="row">
                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                <label for="inputRadicacion" class="form-label text-secondary">Nombre de Juzgado</label>
                                <select class="form-select form-select-sm" id="selectBusJuzgados" runat="server">
                                    <option selected>Seleccionar</option>
                                </select>
                            </div>
                            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                <label for="LabelEjecucion" class="form-label text-secondary">Numero de Ejecución</label>
                                <div class="input-group">
                                    <input type="text" class="form-control form-control-sm" id="inpuBusEjecucion" runat="server">
                                    <div class="input-group-append">
                                         <asp:Button ID="btnBuscarPromocion" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarPromocion_Click" />
                                         <button id="btnLimpiarPromocion" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiarPromocion_Click">Limpiar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <p></p>
                        <div class="row">
                            <asp:Label ID="tituloTablaPromociones" runat="server" CssClass="textoTablasArriba">
                                <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta de promociones</i></h2>
                            </asp:Label>
                            <asp:GridView ID="GridViewPromociones" CssClass="table custom-gridview" runat="server"  AutoGenerateColumns="False">
                         <Columns>
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
                            </Columns>
                        </asp:GridView>
                        </div>

                           </ContentTemplate>
                     
                    </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>




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
    </div>
</asp:Content>