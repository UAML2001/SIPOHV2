<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="AgendaBandeja.aspx.cs" Inherits="SIPOH.BandejaAgenda" %>

<asp:Content ID="ContentBuzonAgenda" ContentPlaceHolderID="BuzonAgenda" runat="server">
    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }
    </style>
    <asp:ScriptManager ID="ScriptManagerAgendaBuzon" runat="server"></asp:ScriptManager>
    <!-- BASE MASTER -->
    <link href="Conteffnt/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">
    <link href="Content/css/Iniciales.css" rel="stylesheet" />
    <link href="Content/css/Permiso.css" rel="stylesheet" />

    <div>
        <h1 style="margin-left: 5%" class="h5">Bandeja de agenda<i class="fas fa-angle-right"></i><span
            id="dataSplash" class="text-primary fw-bold"></span></h1>
    </div>

        <div class="m-0">
            <div class="row">
                <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                               <div class="card">
                <div class="card-body">
                    <!-- Tab panes -->
                    <div class="tab-content ">
                        <div class="tab-pane active" id="IAcusatorio" role="tabpanel">
                            <asp:UpdatePanel ID="UpdatePanelBandejaAgenda" runat="server">
                                <ContentTemplate>
                                   <b>prueba bandeja agenda
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
        integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQ+zqX6gSbd85u4mG4QzX+"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js"
        integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+"
        crossorigin="anonymous"></script>
    <script src="Scripts/consignaciones/Consignaciones.js"></script>
    <script src="Scripts/Ejecucion/formatoInput.js" charset="utf-8"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="Scripts/consignaciones/toast.js"></script>

</asp:Content>
