<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="HistoricosCtrl.aspx.cs" Inherits="SIPOH.HistoricosCtrl" %>

<%@ Register Src="~/Views/CustomHistoricosCupreCausa.ascx" TagPrefix="Form" TagName="CustomHistoricosCupreCausa" %>
<%@ Register Src="~/Views/CustomHistoricosJuicioOral.ascx" TagPrefix="Form" TagName="CustomHistoricosJuicioOral" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCausa" runat="server">

    <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>

    <div class="d-flex justify-content-between px-3 mb-4">
        <span  class="text-sm" >Consignaciones historicas <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold "> Histórico de Causa</span> </span>
        <b>Registro</b>
	</div>


    
            <div class="col-md-10 col-xl-12 m-0">
                <!-- Nav tabs -->
                <div class="card">
                    <div class="card-header">
                        <ul class="nav nav-tabs justify-content-center" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active  onSplash" data-toggle="tab" href="#CausaCupre" role="tab">
                                    Histórico de Causa
                                </a>
                            </li>
                            
                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#JuicioOral" role="tab">
                                   Histórico de Juicio Oral
                                </a>
                            </li>
                        </ul>
                    </div>
                    <asp:ScriptManager ID="ScriptManagerHistoricos" runat="server"></asp:ScriptManager>

                    <div class="card-body">
                        <!-- Tab panes -->
                        <div class="tab-content ">
                            <div class="tab-pane active" id="CausaCupre" role="tabpanel">
                                <%--importacion de controles--%>
                                <Form:CustomHistoricosCupreCausa runat="server" id="CustomHistoricosCupreCausa" /> 
                            </div>

                            <div class="tab-pane" id="JuicioOral" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <Form:CustomHistoricosJuicioOral runat="server" id="CustomHistoricosJuicioOral" />
                               
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
    <script src="Scripts/Ejecucion/HistoricosCausa.js"></script>  
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

