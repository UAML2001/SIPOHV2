<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportesEjecucion.ascx.cs" Inherits="SIPOH.Views.ReportesEjecucion" %>

<style type="text/css">
    .mayusculas {
        text-transform: uppercase;
    }
</style>

    <!-- Include Toastr CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />

    <!-- Include jQuery (Toastr depends on it) -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- Include Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

    <!-- Toastr initialization -->
    <script>
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "timeOut": "5000", // Time in milliseconds
            // You can customize other options as needed
        };
    </script>


<div>
            <h1 style="margin-left: 5%" class="h5">Ejecucion <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"> Reportes</span> </h1>
        </div>

    <!-- Include Toastr CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />

    <!-- Include jQuery (Toastr depends on it) -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- Include Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>


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
                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <h6 class="help-block text-muted small-font">Tipo de Reporte: </h6>
                                <asp:DropDownList runat="server" CssClass="form-select mayusculas" ID="ddlTipoReporte" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                                    <asp:ListItem Value="S" Text="Seleccione el tipo de reporte" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="D">Dia (Hoy)</asp:ListItem>
                                    <asp:ListItem Value="F">Fecha (Intervalo de Fecha)</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <h6 class="help-block text-muted small-font">Formato de Reporte: </h6>
                                <asp:DropDownList runat="server" CssClass="form-select mayusculas" ID="ddlFormatoReporte" AutoPostBack="true">
                                    <asp:ListItem Value="S" Text="Seleccione el formato del reporte" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="I">Inicial</asp:ListItem>
                                    <asp:ListItem Value="P">Promociones</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <h6 class="help-block text-muted small-font">Juzgado: </h6>
                                <asp:DropDownList runat="server" CssClass="form-select mayusculas" ID="JuzgadoEjec" AutoPostBack="true">
                                    <asp:ListItem Value="S" Text="Seleccione el juzgado del reporte..." Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>

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

<%--                        <!-- Botón -->
                        <asp:Button ID="btnMostrar" runat="server" Text="Mostrar" OnClick="btnMostrar_Click" CssClass="btn btn-primary" />

                        <!-- Tabla -->
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="NoEjecucion" HeaderText="No. Ejecución" />
                                <asp:BoundField DataField="DetalleSolicitante" HeaderText="Detalle Solicitante" />
                                <asp:BoundField DataField="FechaEjecucion" HeaderText="Fecha Ejecución" />
                                <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" />
                                <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario" />
                                <asp:BoundField DataField="Causa" HeaderText="Causa" />
                                <asp:BoundField DataField="NUC" HeaderText="NUC" />
                                <asp:BoundField DataField="Juzgado" HeaderText="Juzgado" />
                            </Columns>
                        </asp:GridView>--%>




                        <%--<asp:Button ID="btnConsultarReporte" runat="server" CssClass="btn btn-success align-content-center" Text="Consultar Reporte" OnClick="btnConsultarReporte_Click" />--%>
                        <center>
                        <asp:Button ID="btnMostrarInforme" CssClass="btn btn-success align-content-center" runat="server" Text="📄 Generar Reporte" OnClick="btnMostrarInforme_Click" />
                        <%--<asp:Button ID="Button1" CssClass="btn btn-success align-content-center" runat="server" Text="📄 Generar promocion" OnClick="botonmostrarpromocion" />--%>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="TituloReporte" runat="server" CssClass="h4 text-center" Text="¡Su reporte esta listo! 🎉" Visible="false" />
                            <br />
                            <br />
                        <asp:Button ID="GenerarOtro" CssClass="btn btn-success align-content-center" runat="server" Text="🔙 Generar otro reporte" Visible="false" OnClick="GenerarOtro_Click" />
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

<script>

    // Default Configuration
    $(document).ready(function () {
        toastr.options = {
            'closeButton': true,
            'debug': false,
            'newestOnTop': false,
            'progressBar': true,
            'positionClass': 'toast-bottom-right',
            'preventDuplicates': true,
            'showDuration': '1000',
            'hideDuration': '1000',
            'timeOut': '5000',
            'extendedTimeOut': '1000',
            'showEasing': 'swing',
            'hideEasing': 'linear',
            'showMethod': 'fadeIn',
            'hideMethod': 'fadeOut',
        }
    });

    function ValidacionReportesEjecucion() {
        // Obtener los valores de los TextBox
        var ddlTipoReporte = document.getElementById('<%= ddlTipoReporte.ClientID %>').value;
        var ddlFormatoReporte = document.getElementById('<%= ddlFormatoReporte.ClientID %>').value;
        var JuzgadoEjec = document.getElementById('<%= JuzgadoEjec.ClientID %>').value;

        if (ddlTipoReporte.value === 'S') {
            e.preventDefault();
            toastr.error('Por favor, selecciona el tipo para generar el reporte.');
        }

        if (ddlFormatoReporte.value === 'Seleccione el formato del reporte') {
            e.preventDefault();
            toastr.error('Por favor, selecciona el formato para generar el reporte.');
        }

        if (JuzgadoEjec.value === 'Seleccione el juzagado del reporte...') {
            e.preventDefault();
            toastr.error('Por favor, selecciona el juzgado para generar el reporte.');
        }
    }

    function cerrarIFrame() {
        document.getElementById('iframePDF').style.display = 'none';
    }

</script>