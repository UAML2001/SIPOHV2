<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="PromocionesCtrl.aspx.cs" Inherits="SIPOH.PromocionesCtrl" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentCausa" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <asp:ScriptManager ID="ScriptManagerPromociones" runat="server"></asp:ScriptManager>
    <%--Importacion de Controles--%>
    <div>
        <h1 style="margin-left: 5%" class="h5" >Control <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold">Promociones</span> </h1>
	</div>
    <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <!-- Nav tabs -->
                <div class="card py-5">
                    <div class="card-header">
        <%@ Register Src="~/Views/CustomPromocion.ascx" TagPrefix="form" TagName="CustomPromocion" %>
        <form:CustomPromocion runat="server" ID="CustomPromocion" />
    </div>
</div>
        </div>
    
            
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