﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true"
    CodeBehind="Iniciales.aspx.cs" Inherits="SIPOH.Iniciales" %>



    <asp:Content ID="Content5" ContentPlaceHolderID="ContentEIniciales" runat="server">
        

            <link href="Content/css/Consignaciones.css" rel="stylesheet" />
            <link rel="stylesheet"
                href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
            <link rel="stylesheet" type="text/css"
                href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
            <style type="text/css">
                .mayusculas {
                    text-transform: uppercase;
                }
            </style>
            <div>
                <h1 style="margin-left: 5%" class="h5">Iniciales <i class="fas fa-angle-right"></i><span id="dataSplash"
                        class="text-success fw-bold">Iniciales</span> </h1>
            </div>
            <div class="m-0">
                <div class="row">
                    <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                        <!-- Nav tabs -->
                        <div class="card">
                            <div class="card-header">
                                <ul class="nav nav-tabs justify-content-center" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active  onSplash" data-toggle="tab" href="#IAcusatorio"
                                            role="tab">Sistema Acusatorio
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link  onSplash" data-toggle="tab" href="#ITradicional"
                                            role="tab">Sistema Tradicional
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="card-body">
                                <!-- Tab panes -->
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <div class="tab-content ">
                                    <div class="tab-pane active" id="IAcusatorio" role="tabpanel">
                                        <%@ Register Src="~/Views/InicialAcusatorio.ascx" TagPrefix="uc1"
                                            TagName="InicialAcusatorio" %>
                                            <uc1:InicialAcusatorio runat="server" ID="InicialAcusatorio" />
                                    </div>
                                    <div class="tab-pane" id="ITradicional" role="tabpanel">
                                        <%@ Register Src="~/Views/InicialTradicional.ascx" TagPrefix="uc1"
                                            TagName="InicialTradicional" %>
                                            <uc1:InicialTradicional runat="server" ID="InicialTradicional" />
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

                        function mostrarToast() {
                            toastr.success("Los datos se insertaron correctamente", "Exito");
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