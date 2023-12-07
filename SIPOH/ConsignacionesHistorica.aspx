<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="ConsignacionesHistorica.aspx.cs" Inherits="SIPOH.ConsignacionesHistorica" %>
<asp:Content ID="ContentContentECHistorica10" ContentPlaceHolderID="ContentECHistorica" runat="server">
    
     <div class="container">
        <link href="Content/css/Consignaciones.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
        <div>
            <h1 style="margin-left: 5%" class="h5">Consignaciones Historicas <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold">Sistema Acusatorio</span> </h1>
        </div>
        <div class="m-0">
            <div class="row">
                <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                    <!-- Nav tabs -->
                    <div class="card">
                        <div class="card-header">
                            <ul class="nav nav-tabs justify-content-center" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active  onSplash" data-toggle="tab" href="#ICHAcusatorio" role="tab">Sistema Acusatorio
                                    </a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#ICHTradicional" role="tab">Sistema Tradicional
                                    </a>
                                </li>
                                 <li class="nav-item">
                                    <a class="nav-link  onSplash" data-toggle="tab" href="#ICHCausa" role="tab">Consignación Histotica Causa
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <!-- Tab panes -->
                            <div class="tab-content ">
                                <div class="tab-pane active" id="ICHAcusatorio" role="tabpanel">
                                    <%--Importacion de Controles--%>
                                    <%@ Register Src="~/Views/InicialCHSAcusatorio.ascx" TagPrefix="uc1" TagName="InicialCHSAcusatorio" %>
                                    <uc1:InicialCHSAcusatorio runat="server" id="InicialCHSAcusatorio" />
                                </div>
                                <div class="tab-pane" id="ICHTradicional" role="tabpanel">
                                    <%--Importacion de Controles--%>
                                    <%@ Register Src="~/Views/InicialCHSTradicional.ascx" TagPrefix="uc1" TagName="InicialCHSTradicional" %>
                                    <uc1:InicialCHSTradicional runat="server" id="InicialCHSTradicional" />
                                </div>
                                <div class="tab-pane" id="ICHCausa" role="tabpanel">
                                    <%--Importacion de Controles--%>
                                    <%@ Register Src="~/Views/InicialCHCausa.ascx" TagPrefix="uc1" TagName="InicialCHCausa" %>
                                    <uc1:InicialCHCausa runat="server" id="InicialCHCausa" />
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