<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="ExpeOficios.aspx.cs" Inherits="SIPOH.ExpeOficios" %>

<asp:Content ID="ExpeOficios" ContentPlaceHolderID="ExpeOficios" runat="server">

    <div>
        <h1 class="h5"><i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold"> Expedientes para Oficios</span> </h1>
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

    <script src="/Scripts/tinymce/tinymce.min.js" referrerpolicy="origin"></script>

    <script>
        tinymce.init({
            selector: '#mytextarea',
            license_key: 'gpl|<your-license-key>'
        });
    </script>

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
                                <h5 class="text-secondary mb-3"><b>Expedientes para Oficios</b></h5>
                            </div>
                            <div class="row pt-5">
                                <label for="numdesp" class="form-label text-secondary"><b>Ingrese número de expediente: </b></label>
                                <div class="col-md-6 col-sm-6 col-xs-6 input-group mb-3">
                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de Expediente" ID="numexpe" onchange="formatoNumeroToca(this)"></asp:TextBox>
                                    <asp:Button ID="btnBuscar" runat="server" Text="🔎 Ubicar Expediente" CssClass="btn btn-outline-success btn-sm mayusculas"/>
                                </div>
                                <div class="scrollable" runat="server">
                                    <br />
                                    <div class="mb-2" style="display: flex; justify-content: space-between; align-items: center;">
                                        <span class="text-success fw-bold m-2">
                                            <i class="bi bi-file-earmark-text-fill"></i> Oficios:
                                        </span>
                                    </div>
                                    <asp:GridView ID="ubiExpe" CssClass="table table-striped text-center table-hover table-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="3">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelected" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="bg-success text-white" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NumeroExpediente" HeaderText="N° de Expediente" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="Delitos" HeaderText="Delitos" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="Procedimiento" HeaderText="Procedimiento" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="FechaRecepcion" HeaderText="Fecha de Recepcion" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="EstatusRevisionJuez" HeaderText="Estatus de Revision Juez" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </div>

                                 <div class="row">
                                        <div class="mb-2" style="display: flex; justify-content: space-between; align-items: center;">
                                            <span class="text-success fw-bold m-2">
                                                <i class="bi bi-info-circle"></i> Información General del Oficio:
                                            </span>
                                        </div>
                                    </div>

                                <div class="row border border-top-0 border-1 my-3">
                                    <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4 col-xxl-4">
                                        <h5><b>Fecha de Presentación:</b></h5>
                                        <asp:Label runat="server" CssClass="text-success" ID="lblFePresenta">Fecha de Presentación</asp:Label>
                                    </div>
                                    <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4 col-xxl-4">
                                        <h5><b>Número de Asunto:</b></h5>
                                        <asp:Label runat="server" CssClass="text-success" ID="lblNumeroAsunto">Tipo Asunto / Número Expediente</asp:Label>
                                    </div>
                                    <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-4 col-xxl-4">
                                        <h5><b>Delito(s):</b></h5>
                                        <asp:Label runat="server" CssClass="text-success" ID="lblDelitos">Delitos</asp:Label>
                                    </div>
                                </div>

                                <div class="row mt-4">
                                    <div class="mb-2 d-flex justify-content-between align-items-center">
                                        <span class="text-success fw-bold m-2">
                                            <i class="bi bi-info-circle"></i>Información Extra del Oficio:
                                        </span>
                                    </div>
                                </div>


                                <div id="accVictim" runat="server">
                                    <div class="accordion" id="AccordionVictimas">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Element1" aria-expanded="true" aria-controls="Element1">
                                                    <b>Partes Asociadas</b>
                                                </button>
                                            </h2>
                                            <div id="Element1" class="accordion-collapse collapse" data-bs-parent="#AccordionVictimas">
                                                <div class="accordion-body">
                                                    <h5 class="text-secondary mb-4"><b>Partes Asociadas</b></h5>
                                                    <div class="row">
                                                        <!-- Contenido de Partes Asociadas -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Element2" aria-expanded="false" aria-controls="Element2">
                                                    <b>Etapas Procesales</b>
                                                </button>
                                            </h2>
                                            <div id="Element2" class="accordion-collapse collapse" data-bs-parent="#AccordionVictimas">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <div class="accordion-body">
                                                            <h5 class="text-secondary mb-4"><b>Etapas Procesales</b></h5>
                                                            <div class="row">
                                                                <!-- Contenido de Etapas Procesales -->
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Element3" aria-expanded="false" aria-controls="Element3">
                                                    <b>Anexos</b>
                                                </button>
                                            </h2>
                                            <div id="Element3" class="accordion-collapse collapse" data-bs-parent="#AccordionVictimas">
                                                <div class="accordion-body">
                                                    <h5 class="text-secondary mb-4"><b>Anexos</b></h5>
                                                    <div class="row">
                                                        <!-- Contenido de Datos para Audiencia y Notificación -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>




                                 <div class="col-md-6 col-sm-6 col-xs-6 mt-4">
                                    <label class="form-label text-secondary"><b>Tipo de oficio: </b></label>
                                    <asp:DropDownList ID="TAsunto" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                        <asp:ListItem runat="server" Value="SO" Selected="True" Text="----- SELECCIONE -----"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <br />
                                <div class="mt-4">
                                    <textarea id="default">Hello, World!</textarea>
                                </div>
                                <br />

                            </div>
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
       } </script>    <script>
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
    <script>
        tinymce.init({
            selector: 'textarea',  // change this value according to your HTML
            license_key: 'gpl'
        });
    </script>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js" integrity="sha384-0pUGZvbkm6XF6gxjEnlmuGrJXVbNuzT9qBBavbLwCsOGabYfZo0T0to5eqruptLy" crossorigin="anonymous"></script>
  </asp:Content>