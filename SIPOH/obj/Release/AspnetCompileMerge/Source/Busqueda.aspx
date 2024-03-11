<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Busqueda.aspx.cs" Inherits="SIPOH.Busqueda" %>



<asp:Content ID="ContentBusquedas11" ContentPlaceHolderID="ContentBusquedas11" runat="server">
     
        <link href="Content/css/Consignaciones.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
         <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
         <link href="Content/css/Iniciales.css" rel="stylesheet" />
          <style type="text/css">
                .mayusculas {
                    text-transform: uppercase;
                }
            </style>
        <div>
            <h1 style="margin-left: 5%" class="h5">Busqueda <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold">Partes de la Causa</span> </h1>
        </div>
        
            <div class="row">
                <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                    <!-- Nav tabs -->
                    <div class="card">
                        <div class="card-header">
                            <ul class="nav nav-tabs justify-content-center" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active  onSplash" data-toggle="tab" href="#IBPCausa" role="tab">Partes de la Causa
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#IBSBeneficiario" role="tab">Sentenciado | Beneficiario
                                    </a>
                                </li>
                                 <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#IBNCausa" role="tab">N° de Causa
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#IBNNuc" role="tab">N° de Nuc
                                    </a>
                                </li>
                                 <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#IBSolicitante" role="tab">Solicitante
                                    </a>
                                </li>
                                 <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#IBDetSolicitante" role="tab">Detalle del solicitante
                                    </a>
                                </li>
                                 
                            </ul>
                        </div>
                        <div class="card-body">
                            <!-- Tab panes -->
                            <asp:ScriptManager ID="ScriptManagerBusqueda" runat="server"></asp:ScriptManager>
                            <div class="tab-content ">
                                <div class="tab-pane active" id="IBPCausa" role="tabpanel">
                                    <%@ Register Src="~/Views/InicialBusPCausa.ascx" TagPrefix="uc1" TagName="InicialBusPCausa" %>
                                    <uc1:InicialBusPCausa runat="server" ID="InicialBusPCausa" />
                                </div>
                                <div class="tab-pane" id="IBSBeneficiario" role="tabpanel">
                                    <%@ Register Src="~/Views/InicialBusSenBen.ascx" TagPrefix="uc1" TagName="InicialBusSenBen" %>
                                    <uc1:InicialBusSenBen runat="server" ID="InicialBusSenBen" />
                                </div>
                                <div class="tab-pane" id="IBNCausa" role="tabpanel">
                                     <%@ Register Src="~/Views/InicialBusNoCausa.ascx" TagPrefix="uc1" TagName="InicialBusNoCausa" %>
                                    <uc1:InicialBusNoCausa runat="server" ID="InicialBusNoCausa" />
                                </div>
                                <div class="tab-pane" id="IBNNuc" role="tabpanel">
                                    <%@ Register Src="~/Views/InicialBusNoNuc.ascx" TagPrefix="uc1" TagName="InicialBusNoNuc" %>
                                    <uc1:InicialBusNoNuc runat="server" id="InicialBusNoNuc" />
                                </div>
                                <div class="tab-pane" id="IBSolicitante" role="tabpanel">
                                     <%@ Register Src="~/Views/InicialBusSolicitante.ascx" TagPrefix="uc1" TagName="InicialBusSolicitante" %>
                                    <uc1:InicialBusSolicitante runat="server" ID="InicialBusSolicitante" />
                                </div>
                                <div class="tab-pane" id="IBDetSolicitante" role="tabpanel">
                                    <%@ Register Src="~/Views/InicialBusDetSolicitante.ascx" TagPrefix="uc1" TagName="InicialBusDetSolicitante" %>
                                    <uc1:InicialBusDetSolicitante runat="server" ID="InicialBusDetSolicitante" />
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
    
</asp:Content>
