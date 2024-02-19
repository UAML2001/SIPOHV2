<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="ConsignacionesHistorica.aspx.cs" Inherits="SIPOH.ConsignacionesHistorica" %>
<asp:Content ID="ContentContentECHistorica10" ContentPlaceHolderID="ContentECHistorica" runat="server">
    
     <div class="container">
        <link href="Content/css/Consignaciones.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
                     <link rel="stylesheet" type="text/css"
                href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
          <style type="text/css">
                .mayusculas {
                    text-transform: uppercase;
                }
            </style>
        <div>
            <h1 style="margin-left: 5%" class="h5">Consignaciones Historicas <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold">Sistema Acusatorio</span> </h1>
        </div>
        <div class="m-0">
            <div class="row">
                <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                    <!-- Nav tabs -->
                    <div class="card">
                        <div class="card-header">
                            <ul class="nav nav-tabs justify-content-center" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active  onSplash" data-toggle="tab" href="#ICH" role="tab">Sistema Acusatorio o Tradicional
                                    </a>
                                </li>
                                 <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#ICHCausa" role="tab">Consignación Histórica CAUSA
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <asp:ScriptManager ID="ScriptManagerCH" runat="server"></asp:ScriptManager>
                            <!-- Tab panes -->
                            <div class="tab-content ">
                                <div class="tab-pane active" id="ICH" role="tabpanel">
                                    <%--Importacion de Controles--%>
                                    <%@ Register Src="~/Views/InicialConsignacionesHistoricas.ascx" TagPrefix="uc1" TagName="InicialConsignacionesHistoricas" %>
                                    <uc1:InicialConsignacionesHistoricas runat="server" id="InicialConsignacionesHistoricas" />
                                </div>
                                <div class="tab-pane" id="ICHCausa" role="tabpanel">
                                    <%--Importacion de Controles--%>
                                    <%@ Register Src="~/Views/InicialCHCausa.ascx" TagPrefix="uc2" TagName="InicialCHCausa" %>
                                    <uc2:InicialCHCausa runat="server" id="InicialCHCausa" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
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
                function toastExito(mensaje) {
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
                    if ((e.keyCode == 13) && (node.type == "text")) {
                        e.preventDefault();
                        return false;
                    }
                }, true);
            </script>
         <script type="text/javascript">


         </script>

    </div>
</asp:Content>