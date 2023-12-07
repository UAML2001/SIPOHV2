<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="SIPOH.Reportes" %>
<asp:Content ID="ContentEPReportes9" ContentPlaceHolderID="ContentEPReportes" runat="server">
    <div class="container">
        <link href="Content/css/Consignaciones.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
        <div>
            <h1 style="margin-left: 5%" class="h5">Reportes <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold">Reportes por Día</span> </h1>
        </div>
        <div class="m-0">
            <div class="row">
                <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                    <!-- Nav tabs -->
                    <div class="card">
                        <div class="card-header">
                            <ul class="nav nav-tabs justify-content-center" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active  onSplash" data-toggle="tab" href="#IRDia" role="tab">Reportes por Día
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#IRFecha" role="tab">Reportes por Fecha
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <!-- Tab panes -->
                            <div class="tab-content ">
                                <div class="tab-pane active" id="IRDia" role="tabpanel">
                                    <%@ Register Src="~/Views/InicialReportePorDia.ascx" TagPrefix="uc1" TagName="InicialReportePorDia" %>
                                    <uc1:InicialReportePorDia runat="server" ID="InicialReportePorDia" />
                                </div>
                                <div class="tab-pane" id="IRFecha" role="tabpanel">
                                    <%--Importacion de Controles--%>
                                    <%@ Register Src="~/Views/InicialReportePorFecha.ascx" TagPrefix="uc1" TagName="InicialReportePorFecha" %>
                                    <uc1:InicialReportePorFecha runat="server" ID="InicialReportePorFecha" />
                                </div>
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

    </div>
</asp:Content>
