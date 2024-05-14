<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="RecepcionExpedientes.aspx.cs" Inherits="SIPOH.RecepcionExpedientes" %>


<asp:Content ID="ContentRecepcionExpedientes" ContentPlaceHolderID="ContentRecepcionExpedientes" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <asp:ScriptManager ID="ScriptManagerRecepcionExpedientes" runat="server"></asp:ScriptManager>
    <%--Importacion de Controles--%>
    
    <div class="d-flex justify-content-between px-3 mb-4">
        <span class="text-sm">Control <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold ">Recepción de Expedientes</span> </span>
        <b>Recepción</b>
    </div>
    <div class="col-md-12  col-xl-12 m-0">
        <!-- Nav tabs -->
        <div class="card py-4 px-3  ">                       
                <%@ Register Src="~/Views/ContenidoExpediente/InformacionExpediente.ascx" TagPrefix="Informacion" TagName="InformacionExpediente" %>
                <Informacion:InformacionExpediente runat="server" id="InformacionExpediente" />               
        </div>
    </div>
    
    <script src="Scripts/Ejecucion/formatoInput.js"></script>   
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

                function mostrarToast() {
                    toastr.toastsuccess(mensaje, "Exito");
                }
                function toastError(mensaje) {
                    toastr.error(mensaje, "Error");
                }
                function toastInfo(mensaje) {
                    toastr.info(mensaje, "Informacion");
                }
            </script>
</asp:Content>

