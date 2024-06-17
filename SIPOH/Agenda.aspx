<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="SIPOH.Agenda" %>

<asp:Content ID="ContentAgenda4" ContentPlaceHolderID="ContentAgenda" runat="server">
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }
        #calendar {
            max-width: 1100px;
            margin: 40px auto;
        }
    </style>

    <div class="modal fade" id="modalAudiencias" aria-hidden="true" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-calendar-check superpermisoGuardarCambios text-success text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="lblTituloConfirmacionModal">Agregar Audiencia</h1>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="lblTituloAgenda">Titulo de la audiencia</label>
                        <input id="new-event" type="text" class="form-control form-control-sm">
                    </div>
                    <div class="form-group">
                        <label for="lblJuez">Asignar Juez</label>
                        <asp:DropDownList runat="server" ID="ddlJuecesAgenda" CssClass="form-select form-select-sm" AutoPostBack="true">
                            <asp:ListItem Value="">-- SELECCIONAR --</asp:ListItem>
                            <asp:ListItem Value="Carlos">Juez Carlos</asp:ListItem>
                            <asp:ListItem Value="David">Juez David</asp:ListItem>
                            <asp:ListItem Value="Brayan">Juez Brayan</asp:ListItem>
                            <asp:ListItem Value="Sofia">Jueza Sofia</asp:ListItem>
                            <asp:ListItem Value="Martina">Jueza Martina</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="lblDescripcion">Descripcion de la Audiencia</label>
                        <textarea class="form-control" id="descripcionAgenda" rows="3"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" onclick="CerrarModalAudiencias()" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-danger">Eliminar</button>
                    <button type="button" class="btn btn-success">Agregar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManagerAgenda" runat="server"></asp:ScriptManager>
    <!-- BASE MASTER -->
    <link href="Conteffnt/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
    <link href="Content/css/Iniciales.css" rel="stylesheet" />
    <link href="Content/css/Permiso.css" rel="stylesheet" />
    <link href="Content/css/Agenda.css" rel="stylesheet" />
    <link href="Content/css/agenda/main.min.css" rel="stylesheet" />

    <div>
        <h1 style="margin-left: 5%" class="h5">Agenda de audiencias <i class="fas fa-angle-right"></i><span
            id="dataSplash" class="text-primary fw-bold"></span></h1>
    </div>
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <button id="abrirModalAgenda" type="button" class="btn btn-success" onclick="abrirModalAudiencias()">Agregar Auiencia</button>
                    </ol>
                </div>
            </div>
        </div>
    </section>
    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <div class="tab-content">
                    <div class="tab-pane active" id="divtab" role="tabpanel">
                        <asp:UpdatePanel ID="UpdatePanelAgenda" runat="server">
                            <ContentTemplate>
                                <!-- CONTENT AGENDA -->
                                <div class="row justify-content-center" >
                                    <div class="content-wrapper">
                                        <div class="card">
                                            <div class="card-header">
                                                <h3 class="card-title">Lista de Jueces</h3>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <a class="text-primary d-block" href="#"><i class="fas fa-square"></i> Juez Carlos</a>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a class="text-warning d-block" href="#"><i class="fas fa-square"></i> Juez David</a>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a class="text-success d-block" href="#"><i class="fas fa-square"></i> Juez Brayan</a>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a class="text-danger d-block" href="#"><i class="fas fa-square"></i> Jueza Sofia</a>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a class="text-muted d-block" href="#"><i class="fas fa-square"></i> Jueza Martina</a>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a class="text-info d-block" href="#"><i class="fas fa-square"></i> Jueza Fernanda</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                                <!-- INICIO ROW -->
                                                    <div class="card card-primary">
                                                            <!-- THE CALENDAR -->
                                                            <div id="calendar" class="container mt-5"></div>
                                                    </div>
                                                <!-- FIN ROW -->
                                    </div>
                                    <!-- CONTENT AGENDA -->
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- BASE MASTER -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"
        integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQ+zqX6gSbd85u4mG4QzX+"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js"
        integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+"
        crossorigin="anonymous"></script>
    <script src="Scripts/consignaciones/Consignaciones.js"></script>
    <script src="Scripts/Ejecucion/formatoInput.js" charset="utf-8"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="Scripts/consignaciones/toast.js"></script>
    <!-- AGENDA -->
    <script src="Scripts/Agenda/agenda.js"></script>
    <script src="Scripts/Agenda/main.min.js"></script>

    <script>
        function abrirModalAudiencias() {
            $('#modalAudiencias').modal('show');
        }

        function CerrarModalAudiencias() {
            $('#modalAudiencias').modal('hide');
            $('body').removeClass('modal-open').css('overflow', '');
            $('.modal-backdrop').remove();
        }
</script>

</asp:Content>
