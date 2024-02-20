<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Consignaciones.aspx.cs" Inherits="SIPOH.Consignaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentCausa" runat="server">
    <%--query general para consiganciones, carpeta de origen: scripts--%>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
     <%--query general para consiganciones, carpeta de origen: scripts--%>
    <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <div class="d-flex justify-content-between px-3 mb-4">
        <span  class="text-sm" >Consignaciones <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold ">Amparo</span> </span>
        <b>Registro</b>
	</div>
    
            <div class="col-md-10  col-xl-12 m-0">
                <!-- Nav tabs -->
                <div class="card">
                    <div class="card-header">
                        <ul class="nav nav-tabs justify-content-center" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active  onSplash" data-toggle="tab" href="#amparo" role="tab" >
                                    Amparo
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link  onSplash" data-toggle="tab" href="#registroIniciales" role="tab">
                                    Registro de iniciales
                                </a>
                            </li>
                            
                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#exhorto" role="tab" >
                                    Exhorto
                                </a>
                            </li>

                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#juciooral" role="tab">
                                    Juicio Oral
                                </a>
                            </li>
                        </ul>
                    </div>

                    <%--importante--%>
                    <div class="card-body mx-3  p-0">
                        <!-- Tab panes -->
                        <asp:ScriptManager ID="ScriptManagerConsignaciones" runat="server"></asp:ScriptManager>
                        <div class="tab-content ">
                            <div class="tab-pane active" id="amparo" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomAmparo.ascx" TagPrefix="form" TagName="CustomAmparo" %>
                                <form:CustomAmparo runat="server" ID="CustomAmparo" />
                            </div>

                            <div class="tab-pane" id="registroIniciales" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomRegistroIniciales.ascx" TagPrefix="form" TagName="CustomRegistroIniciales" %>
                                <form:CustomRegistroIniciales runat="server" ID="CustomRegistroIniciales1" />
                            </div>

                            

                            <div class="tab-pane" id="exhorto" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomExhorto.ascx" TagPrefix="form" TagName="CustomExhorto" %>
                                <form:CustomExhorto runat="server" ID="CustomExhorto" />
                            </div>




                            <div class="tab-pane" id="juciooral" role="tabpanel">
                                 <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomJuicio.ascx" TagPrefix="form" TagName="CustomJuicio" %>

                                <form:CustomJuicio runat="server" ID="CustomJuicio" />
                            </div>


                        </div>
                    </div>



            
            <%--modales--%>
            <!-- Modal Registrar -->
            <div class="modal fade" id="modal5" tabindex="-1" aria-labelledby="modal5Label" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <%--contenido--%>
                        <div class="modal-header">
                            <i class="bi bi-eye text-secondary fs-6 pr-2"></i>
                            <h5 class="modal-title text-secondary fs-6">Registrar</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            ...
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                            <button type="button" class="btn btn-primary btn-sm"><i class="bi bi-check-lg"></i>Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal Registrar -->
            <div class="modal fade" id="modal4" tabindex="-1" aria-labelledby="modal4Label" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <%--contenido--%>
                        <div class="modal-header">
                            <i class="bi bi-eye text-secondary fs-6 pr-2"></i>
                            <h5 class="modal-title text-secondary fs-6">Asignar</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            ...
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                            <button type="button" class="btn btn-primary btn-sm"><i class="bi bi-check-lg"></i>Guardar</button>
                        </div>
                    </div>
                </div>
            </div>


                    <script src="Scripts/Ejecucion/formatoInput.js"></script>

            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
            <script src="Scripts/consignaciones/Consignaciones.js"></script> 
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

