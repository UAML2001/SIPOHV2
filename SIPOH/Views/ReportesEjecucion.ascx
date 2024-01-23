<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportesEjecucion.ascx.cs" Inherits="SIPOH.Views.ReportesEjecucion" %>

        <div>
            <h1 style="margin-left: 5%" class="h5">Ejecución <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"> Reportes</span> </h1>
        </div>

     <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

<asp:ScriptManager runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="updPanel" runat="server" EnableViewState="true" >
    <ContentTemplate>
<div class="m-0">
    <div class="row">
        <div class="col-md-10 ml-auto col-xl-11 mr-auto">
            <!-- Nav tabs -->
            <div class="card">

                <div class="card-body">
                    <div class="container col-12">
                        <div style="padding: 2%">
                            <h5 class="text-secondary mb-5">Reportes</h5>
                        </div>

                        <div class="row ">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">Tipo de Reporte: </h6>
                                <asp:DropDownList runat="server" CssClass="form-select" ID="ddlTipoReporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                                    <asp:ListItem Value=S Text="Seleccione el tipo de reporte" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="D">Dia (Hoy)</asp:ListItem>
                                    <asp:ListItem Value="F">Fecha (Intervalo de Fecha)</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">Formato de Reporte: </h6>
                                <asp:DropDownList runat="server" CssClass="form-select" ID="ddlFormatoReporte" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione el formato del reporte" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="I">Inicial</asp:ListItem>
                                    <asp:ListItem Value="P">Promociones</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                        </div>

                        <br />
                        <br />


                        <div class="row" runat="server" id="fechas">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <center>
                                    <h6 class="help-block text-muted small-font">Desde: </h6>
                                    <div class="table-responsive">
                                        <asp:TextBox ID="calFechaDesde" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </div>
                                </center>
                            </div>  
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <center>
                                    <h6 class="help-block text-muted small-font">Hasta: </h6>
                                    <div class="table-responsive">
                                    <asp:TextBox ID="calFechaHasta" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                                    </div>
                                </center>
                            </div>
                        </div>


                        <br />
                        <br />

                        
                            <%--<asp:Button ID="btnConsultarReporte" runat="server" CssClass="btn btn-success align-content-center" Text="Consultar Reporte" OnClick="btnConsultarReporte_Click" />--%>

                        <center>
                        <asp:Button ID="btnMostrarInforme" CssClass="btn btn-success align-content-center" runat="server" Text="Generar Reporte" OnClick="btnMostrarInforme_Click" />
                            </center>
                        <br />
                        <iframe id="iframePDF" runat="server" width="100%" height="500px" visible="false" frameborder="0"></iframe>



                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
            </ContentTemplate>
        </asp:UpdatePanel>




<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
            <script src="Scripts/consignaciones/Consignaciones.js"></script>
