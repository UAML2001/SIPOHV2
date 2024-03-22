<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="PortadasDigitalizacion.aspx.cs" Inherits="SIPOH.PortadasDigitalizacion" %>

<asp:Content ID="ContentPortadasDigit" ContentPlaceHolderID="ContentCausa" runat="server">

    <div>
    <h1 class="h5"><i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold"> Impresión de Portadas</span> </h1>
</div>

<link href="Content/css/Consignaciones.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

<style type="text/css">
    .mayusculas {
        text-transform: uppercase;
    }

    .h5 {
        margin-left: 5%;
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
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
</script>

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="m-0">
        <div class="row">
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <!-- Nav tabs -->
                <div class="card">
                    <div class="card-body">
                        <div class="container col-12">

                            <div class="row pt-5">
                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <label class="form-label text-secxondary">Tipo de asunto: </label>
                                    <asp:DropDownList ID="TAsunto" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                        <asp:ListItem runat="server" Value="SO" Selected="True" Text="Seleccione el tipo de asunto..."></asp:ListItem>
                                        <asp:ListItem runat="server" Value="C" Text="CAUSA"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="CP" Text="CUPRE"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="E" Text="EXHORTO"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="JO" Text="JUICIO ORAL"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="T" Text="TRADICIONAL"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6 col-sm-6 col-xs-6">
                                    <label for="numdesp" class="form-label text-secondary">Ingrese número de expediente:</label>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de Expediente" ID="numexpe" onchange="formatoNumeroToca(this)"></asp:TextBox>
                                </div>
                            </div>

                            <br />

                            <center>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                        <asp:Button ID="btnBuscar" runat="server" Text="🔎 Buscar Asunto" CssClass="btn btn-success btn-sm mayusculas" OnClick="btnBuscar_Click" />
                                    </div>
                                </div>
                            </center>

                            <br />

                            <div class="row ">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <asp:Label ID="lblInicialInfo" runat="server" CssClass="help-block text-muted small-font" Visible="False"><b>Informacion generada: </b></asp:Label>
                                    <br />
                                    <br />
                                    <div runat="server" id="infoInicial">
                                        <asp:Label ID="lblAsunto" runat="server" Text="Tipo Asunto: " Font-Bold="True" Font-Size="Larger" Visible="False" /><asp:Label ID="descripNum" runat="server" Font-Bold="True" Font-Size="Larger" Visible="False" />
                                        <br />
                                        <asp:Label ID="lblDeli" runat="server" Text="Delito(s): " Font-Bold="True" Font-Size="Larger" Visible="False" /><asp:Label ID="delitos" runat="server" Font-Bold="True" Font-Size="Larger" Visible="False" />
                                        <br />
                                        <br />
                                        <center>
                                            <asp:Label ID="lblPartes" runat="server" Font-Bold="True" Text="Parte(s)" Font-Size="Larger" Visible="False"/>
                                        </center>
                                        <br />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                    <center>
                                        <asp:Label ID="lblVictima" runat="server" Font-Bold="True" Text="Victima(s):" Font-Size="Larger" Visible="False" />
                                    </center>
                                    <br />
                                    <asp:GridView ID="infoVictima" CssClass="table table-borderless d-sm-table-row" GridLines="None" runat="server" AutoGenerateColumns="False" ShowHeader="False" Visible="False">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div style="font-size: larger; font-weight: bold; font-style: italic;">
                                                        <span style="font-size: 1em;" class="text-success">• </span>
                                                        <asp:Label ID="NCC" runat="server" CssClass="text-success" Text='<%# Eval("Victimas") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                    <center>
                                        <asp:Label ID="lblImputado" runat="server" Font-Bold="True" Text="Imputado(s):" Font-Size="Larger" Visible="False" />
                                    </center>
                                    <br />
                                    <asp:GridView ID="infoImputado" CssClass="table table-borderless d-sm-table-row" GridLines="None" runat="server" AutoGenerateColumns="False" ShowHeader="False" Visible="False">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div style="font-size: larger; font-weight: bold; font-style: italic;">
                                                        <span style="font-size: 1em;" class="text-success">• </span>
                                                        <asp:Label ID="NCC" runat="server" CssClass="text-success" Text='<%# Eval("Imputados") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                             <br />

                            <center>
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                        <asp:Button ID="ImpPortada" runat="server" Text="🖨️ Generar Portada" CssClass="btn btn-success btn-sm mayusculas" Visible="False" OnClick="ImpPortada_Click"/>
                                        <br /><br />
                                        <iframe id="VPPortada" runat="server" width="100%" height="500px" visible="false" frameborder="0"></iframe>
                                    </div>
                                </div>

                                
                            </center>

                            <br />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>


    <script type="text/javascript">
        function formatoNumeroToca(input) {
            // Divide el valor ingresado en el número y el año
            var [number, year] = input.value.split("/");

            // Rellena el número con ceros a la izquierda hasta que tenga 4 dígitos
            number = number.padStart(4, '0');

            // Combina el número y el año para obtener el número de toca
            var numeroToca = number + "/" + year;

            // Establece el valor del input al número de toca
            input.value = numeroToca;
        }
    </script>

</asp:Content>