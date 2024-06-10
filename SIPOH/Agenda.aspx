<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="SIPOH.Agenda" %>

<asp:Content ID="ContentAgenda4" ContentPlaceHolderID="ContentAgenda" runat="server">
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
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
                        <input type="text" class="form-control form-control-sm" id="TituloAudiencia">
                      </div>
                    
                      <div class="form-group">
                        <label for="lblJuez">Asignar Juez</label>
                              <asp:DropDownList runat="server" ID="ddlJuecesAgenda" CssClass="form-select form-select-sm" AutoPostBack="true">
                                <asp:ListItem Value="">-- SELECCIONAR --</asp:ListItem>
                            </asp:DropDownList>
                      </div>
                     <div class="form-group">
                        <label for="lblDescripcion">Descripcion de la Audiencia</label>
                        <textarea class="form-control" id="descripcionAgenda" rows="3"></textarea>
                      </div>

                </div>
                <div class="modal-footer">
                     <button type="button" class="btn btn-danger" onclick="CerrarModalAudiencias()" data-bs-dismiss="modal">Cancelar</button>
                     <asp:Button ID="btnBorrarClasiDelito" runat="server" Text="Agregar"
                        CssClass="btn btn-success ml-2" />

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
    <!-- AGENDA -->
    <link href="Content/css/Agenda.css" rel="stylesheet" />
    <div>
        <h1 style="margin-left: 5%" class="h5">Agenda de audiencias <i class="fas fa-angle-right"></i><span
            id="dataSplash" class="text-primary fw-bold"></span></h1>
    </div>
    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="tab-content ">
                            <div class="tab-pane active" id="divtab" role="tabpanel">
                                <asp:UpdatePanel ID="UpdatePanelAgenda" runat="server">
                                    <ContentTemplate>
                                        <!-- CONTENT AGENDA -->
                                        <div class="row justify-content-center" style="padding-left: 2%; padding-right: 2%">
                                            <div class="content-wrapper">
                                            <br />
                                                <section class="content">
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="sticky-top mb-3">
                                                                    <!-- Lista Audiencias -->
                                                              <div class="card">
                                                                    <div class="card-header">
                                                                        <h4 class="card-title text-center">Audiencias</h4>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div id="external-events">
                                                                            <div class="external-event bg-success">Audiencia 1103/2023</div>
                                                                            <div class="external-event bg-warning">Audiencia 2023/2023</div>
                                                                            <div class="external-event bg-info">Audiencia 1024/2023</div>
                                                                            <div class="external-event bg-primary">Audiencia 0024/2023</div>
                                                                            <div class="external-event bg-danger">Audiencia 1423/2023</div>
                                                                            <div class="checkbox">
                                                                                <label for="drop-remove">
                                                                                    <input type="checkbox" id="drop-remove" checked>
                                                                                    Remover después de asignar
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                    <!-- Fin Lista Audiencia -->
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                            <h3 class="card-title">Juez a turnar</h3>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="btn-group" style="width: 100%; margin-bottom: 10px;">
                                                                                <ul class="fc-color-picker" id="color-chooser">
                                                                                    <li><a class="text-primary" href="#"><i class="fas fa-square"></i> Juez Carlos</a></li>
                                                                                    <li><a class="text-warning" href="#"><i class="fas fa-square"></i> Juez David</a></li>
                                                                                    <li><a class="text-success" href="#"><i class="fas fa-square"></i> Juez Brayan</a></li>
                                                                                    <li><a class="text-danger" href="#"><i class="fas fa-square"></i> Jueza Sofia</a></li>
                                                                                    <li><a class="text-muted" href="#"><i class="fas fa-square"></i> Jueza Martina</a></li>
                                                                                </ul>
                                                                            </div>
                                                                            <div class="input-group">
                                                                                <input id="new-event" type="text" class="form-control" placeholder="Audiencia">

                                                                                <div class="input-group-append">
                                                                                    <button id="add-new-event" type="button" class="btn btn-success">➕</button>
                                                                                </div>
                                                                                <p></p>
                                                                                <div class="input-group-append">
                                                                                    <button id="abrirModalAgenda" type="button" class="btn btn-success" onclick="abrirModalAudiencias()">Agregar Auiencia</button>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-9">
                                                                <div class="card card-primary">
                                                                    <div class="card-body p-0">
                                                                        <!-- THE CALENDAR -->
                                                                        <div id="calendar"></div>
                                                                    </div>
                                                                </div>
                                                                <!-- /.card -->
                                                            </div>
                                                            <!-- /.col -->
                                                        </div>
                                                        <!-- /.row -->
                                                    </div>
                                                </section>
                                            </div>
                                            <br />
                                                <!-- CONTENT AGENDA -->
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
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
        integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r"
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
    <script src="Scripts/Agenda/index.global.js"></script>
    <script src="Scripts/Agenda/index.global.min.js"></script>

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
