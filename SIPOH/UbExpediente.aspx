<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="UbExpediente.aspx.cs" Inherits="SIPOH.UbExpediente" %>

<asp:Content ID="ContentUbiExpe" ContentPlaceHolderID="ContentUbiExpe" runat="server">
     <div>
        <h1 class="h5"><i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold"> Ubicacion de Expedientes</span> </h1>
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

        .myRadioButtonList input[type="radio"] {
            margin-left: 10px;
            margin-right: 5px;
        }

        .accordion-button:not(.collapsed) {
            background-color: #1bbe83; /* Color success de Bootstrap */
            color: white;
            border-color: #1bbe83; /* Un verde más oscuro para el borde */
        }
        .center-panel {
            margin: 0 auto; /* Esto centra horizontalmente */
            max-width: 400px; /* Esto limita el ancho máximo del panel */
            width: 100%; /* Esto asegura que el panel se ajuste al contenedor */
            text-align: center; /* Esto centra el contenido interno */
        }
        .button-width {
            white-space: nowrap; /* Asegura que el texto del botón no se envuelva */
            width: auto; /* O puedes especificar una anchura fija si lo prefieres */
            min-width: 100px; /* Establece una anchura mínima para el botón */
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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="m-0">
    <div class="row">
        <div class="col-md-10 ml-auto col-xl-11 mr-auto">
            <!-- Nav tabs -->
            <div class="card">
                <div class="card-body">
                    <div class="container col-12">
                        <div style="padding: 2% 2% 0;">
                            <h5 class="text-secondary mb-3"><b>Ubicación de Expedientes</b></h5>
                        </div>
                        
                        <!-- UpdatePanel for Search -->
                        <asp:UpdatePanel ID="UpdatePanelBusqueda" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelBusqueda" runat="server">
                                    <div class="row pt-5">
                                        <div class="col-md-6 col-sm-6 col-xs-6">
                                            <label class="form-label text-secondary"><b>Tipo de asunto: </b></label>
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
                                            <label for="numdesp" class="form-label text-secondary"><b>Ingrese número de expediente: </b></label>
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de Expediente" ID="numexpe" onchange="formatoNumeroToca(this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <center>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                                <asp:Button ID="btnBuscar" runat="server" Text="🔎 Ubicar Expediente" CssClass="btn btn-outline-success btn-sm mayusculas" OnClick="btnBuscar_Click" />
                                            </div>
                                        </div>
                                    </center>
                                </asp:Panel>
                                <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <br />

                        <!-- UpdatePanel for Expediente Info -->
                        <asp:UpdatePanel ID="UpdatePanelExpediente" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PanelExpediente" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="mb-2" style="display: flex; justify-content: space-between; align-items: center;">
                                            <span class="text-success fw-bold m-2">
                                                <i class="bi bi-info-circle"></i> Información General del Expediente:
                                            </span>
                                            <asp:Button ID="BuscarOtro" runat="server" Text="⬅️ Ubicar Otro Expediente" CssClass="btn btn-outline-success btn-sm mayusculas button-width" OnClick="BuscarOtro_Click" />
                                        </div>
                                    </div>

                                    <div class="row border border-top-0 border-1 my-3">
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4 col-xxl-4">
                                            <h5><b>Fecha de Presentación:</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="lblVictimasPromocion">Fecha de Presentación</asp:Label>
                                        </div>
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4 col-xxl-4">
                                            <h5><b>Número de Asunto:</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="lblNumeroAsunto"></asp:Label>
                                        </div>
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4 col-xxl-4">
                                            <h5><b>Delito(s):</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="lblDelitos"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row border border-top-0 border-1 my-3">
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                            <h5><b>Juez:</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="Label2">Juez</asp:Label>
                                        </div>
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                            <h5><b>Jefe de Unidad de Causa:</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="Label3">JUC</asp:Label>
                                        </div>
                                    </div>

                                    <div class="row mb-1 border border-top-0 border-1 my-3">
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                            <h5><b>Victima(s):</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="lblVictimas"></asp:Label>
                                        </div>
                                        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                            <h5><b>Imputado(s):</b></h5>
                                            <asp:Label runat="server" CssClass="text-success" ID="lblImputados"></asp:Label>
                                        </div>
                                    </div>

                                    <br />

                                    <div class="scrollable" runat="server">
                                        <div class="mb-2" style="display: flex; justify-content: space-between; align-items: center;">
                                            <span class="text-success fw-bold m-2">
                                                <i class="bi bi-geo-alt-fill"></i>Ubicación del Expediente:
                                            </span>
                                            <asp:Button ID="AddComentario" runat="server" Text="💬 Agregar Comentario" CssClass="btn btn-outline-primary btn-sm mayusculas button-width" OnClick="AddComentario_Click" />
                                        </div>
                                        <asp:GridView ID="ubiExpe" CssClass="table table-striped text-center table-hover table-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="3">
                                            <Columns>
                                                <asp:BoundField DataField="Perfil" HeaderText="Área" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="Funcionario" HeaderText="Funcionario" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="Actividad" HeaderText="Actividad" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                                <asp:BoundField DataField="FeRecepcion" HeaderText="Fecha de Recepción" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>


                                <asp:Panel ID="PanelIngComent" runat="server" CssClass="center-panel" Style="display: none;">
                                    <div class="mb-2">
                                        <span class="text-success fw-bold m-2">
                                            <i class="bi bi-chat-fill"></i>Ingrese su Comentario:
                                        </span>
                                    </div>
                                    <div class="form-floating">
                                        <textarea runat="server" class="form-control" placeholder="Leave a comment here" id="IngresaComentario" style="height: 200px; width: 100%; max-width: 400px;"></textarea>
                                        <label for="floatingTextarea2" class="text-secondary">Ingrese su comentario...</label>
                                        <div class="mt-3 text-center">
                                            <!-- Botones centrados -->
                                            <asp:Button ID="SvComent" runat="server" Text="💾 Guardar" CssClass="btn btn-outline-success btn-sm mayusculas" OnClick="SvComent_Click" />
                                            <asp:Button ID="CancComent" runat="server" AutoPostBack="True" Text="✖️ Cancelar" CssClass="btn btn-outline-danger btn-sm mayusculas" OnClick="CancComent_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelComentIng" runat="server" CssClass="center-panel" Style="display: none;">
                                    <div class="mb-2">
                                        <span class="text-success fw-bold m-2 aliing">
                                            <i class="bi bi-chat-fill"></i>Comentario Ingresado:
                                        </span>
                                        <br />
                                    </div>
                                    <div class="card text-justify" style="max-width: 400px; width: 100%; margin: 0 auto;">
                                        <div class="card-body">
                                            <h4 class="card-title text-center">Comentario:</h4>
                                            <div class="mt-3 text-center">
                                                <asp:Label runat="server" ID="lblComent" CssClass="card-text fw-bold fst-italic" Text="Aquí va el comentario ingresado"></asp:Label>
                                            </div>
                                            <div class="mt-3">
                                                <asp:Label ID="Label7" Text="Fecha del comentario:" runat="server" CssClass="card-text fw-bold"></asp:Label>
                                                <asp:Label ID="lblFecha" Text=" Aquí va la fecha" runat="server" CssClass="card-text fst-italic"></asp:Label>
                                            </div>
                                            <div class="mt-3">
                                                <asp:Label ID="Label9" Text="Usuario que comentó:" runat="server" CssClass="card-text fw-bold"></asp:Label>
                                                <asp:Label ID="lblUsuario" Text=" Aquí va el usuario" runat="server" CssClass="card-text fst-italic"></asp:Label>
                                            </div>

                                            <asp:Panel ID="ModificarComentario" runat="server" Style="display: none;">
                                                <br />
                                                <span class="text-success fw-bold m-2 aliing">
                                                    <i class="bi bi-pencil-fill"></i>Modificar Comentario:
                                                </span>
                                                <div class="mt-3">
                                                    <asp:TextBox ID="ComentModificado" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3"></asp:TextBox>
                                                </div>
                                                <div class="mt-3 text-center">
                                                    <!-- Botones centrados -->
                                                    <asp:Button ID="GuardarModificacion" runat="server" Text="💾 Guardar" CssClass="btn btn-outline-success btn-sm mayusculas" OnClick="GuardarModificacion_Click" />
                                                    <asp:Button ID="CancelarModificacion" runat="server" Text="✖️ Cancelar" CssClass="btn btn-outline-danger btn-sm mayusculas" OnClick="CancelarModificacion_Click1" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="card-footer text-body-secondary">
                                            <asp:Button ID="ModComent" runat="server" Text="✍️ Modificar" CssClass="btn btn-outline-primary btn-sm mayusculas" OnClick="ModComent_Click" />
                                            <asp:Button ID="DelComent" runat="server" Text="🗑️ Eliminar" CssClass="btn btn-outline-danger btn-sm mayusculas" OnClick="DelComent_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>




                           
            <script>
                function formatoRFC(input) {
                    // Eliminar caracteres que no son dígitos ni letras
                    var rfc = input.value.replace(/[^\dA-Za-z]/g, '');

                    // Verificar que la longitud del RFC es al menos de 13 caracteres
                    if (rfc.length >= 13) {
                        // Separar los últimos 3 caracteres con un guión
                        var formattedRFC = rfc.substring(0, rfc.length - 3) + '-' + rfc.substring(rfc.length - 3);
                        input.value = formattedRFC;
                    }
                } </script>

    <script>
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

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js" integrity="sha384-0pUGZvbkm6XF6gxjEnlmuGrJXVbNuzT9qBBavbLwCsOGabYfZo0T0to5eqruptLy" crossorigin="anonymous"></script>

</asp:Content>
