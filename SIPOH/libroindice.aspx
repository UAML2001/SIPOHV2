<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="libroindice.aspx.cs" Inherits="SIPOH.libroindice" %>

<asp:Content ID="ContentLibroIndice12" ContentPlaceHolderID="Contentlibroindice" runat="server">
    <%--  --%>
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }
    </style>
    <div class="container">
        <asp:ScriptManager ID="ScriptManagerLibroIndice" runat="server"></asp:ScriptManager>
        <link href="Content/css/Consignaciones.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
        <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
        <link href="Content/css/Iniciales.css" rel="stylesheet" />
        <div>
            <h1 style="margin-left: 5%" class="h5">Libro Índice <i class="fas fa-angle-right"></i><span
                id="dataSplash" class="text-primary fw-bold"></span></h1>
        </div>
        <div class="card">
            <div class="card-body">
                <asp:UpdatePanel ID="UpdatePanelPromociones" runat="server">
                    <ContentTemplate>
                        <h2>Libro Índice</h2>
                        <hr />

                       <div class="row">
                        <div class="col-sm-6 col-md-4">
                            <label for="inputNombre" class="form-label text-secondary">Nombre(s)</label>
                            <input type="text" class="form-control form-control-sm mayusculas" id="inputNombre" maxlength="250" runat="server" />
                        </div>
                        <div class="col-sm-6 col-md-3">
                            <label for="inputApellidoPaterno" class="form-label text-secondary">Apellido Paterno (Razón social)</label>
                            <input type="text" class="form-control form-control-sm mayusculas" maxlength="100" id="inputApellidoPaterno" runat="server" />
                        </div>
                        <div class="col-sm-6 col-md-3">
                            <label for="inputApellidoMaterno" class="form-label text-secondary">Apellido Materno</label>
                            <input type="text" class="form-control form-control-sm mayusculas" maxlength="100" id="inputApellidoMaterno" runat="server" />
                        </div>
                        <div class="col-sm-6 col-md-2 d-flex align-items-end justify-content-center">
                            <asp:Button ID="btnBuscarPCausa" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" />
                            <button id="btnLimpiar" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2">Limpiar</button>

                        </div>
                    </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
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

    </div>
    <%--  --%>
</asp:Content>

