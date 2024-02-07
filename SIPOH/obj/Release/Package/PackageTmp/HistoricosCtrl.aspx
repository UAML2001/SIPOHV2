<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="HistoricosCtrl.aspx.cs" Inherits="SIPOH.HistoricosCtrl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentCausa" runat="server">

    <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

    <div>
    <h1 style="margin-left: 5%" class="h5" >Historicos <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold">Amparo</span> </h1>
	</div>



    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <!-- Nav tabs -->
                <div class="card">
                    <div class="card-header">
                        <ul class="nav nav-tabs justify-content-center" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active  onSplash" data-toggle="tab" href="#amparo" role="tab">
                                    Amparo
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link  onSplash" data-toggle="tab" href="#causa" role="tab">
                                    Causa
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#cupre" role="tab">
                                    Cupre
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#juciooral" role="tab">
                                    Juicio Oral
                                </a>
                            </li>
                        </ul>
                    </div>


                    <div class="card-body">
                        <!-- Tab panes -->
                        <div class="tab-content ">
                            <div class="tab-pane active" id="amparo" role="tabpanel">
                                <h1>Historicos Amparo</h1>
                            </div>

                            <div class="tab-pane" id="causa" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/HistoricosCausa.ascx" TagPrefix="form" TagName="HistoricosCausa" %>
                                <form:HistoricosCausa runat="server" ID="HistoricosCausa" />
                            </div>

                            <div class="tab-pane" id="cupre" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/HistoricosCupre.ascx" TagPrefix="form" TagName="HistoricosCupre" %>
                                <form:HistoricosCupre runat="server" ID="HistoricosCupre" />
                            </div>
                            <div class="tab-pane" id="juciooral" role="tabpanel">
                                <h1>Historicos Juicio Oral</h1>
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

</asp:Content>

