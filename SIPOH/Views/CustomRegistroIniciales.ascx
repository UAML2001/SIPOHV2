<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomRegistroIniciales.ascx.cs" Inherits="SIPOH.Views.CustomRegistroIniciales" %>


<script type="text/javascript">

    function seleccionarOpcion() {
        var ddlTipoFiltrado = document.getElementById('<%= ddlTipoFiltrado.ClientID %>');
        var inputBuscarInicial = document.getElementById('<%= inputBuscarInicial.ClientID %>');

        if (ddlTipoFiltrado.value === "NUC") {
            inputBuscarInicial.setAttribute("onblur", "formatNuc(this)");
        } else {
            inputBuscarInicial.removeAttribute("onblur");
        }
    }





    function mostrarTituloSello() {
        var tituloSello = document.getElementById('tituloSelloIniciales');
        if (tituloSello) {
            tituloSello.style.display = 'block';
        }
    }
    function imprimirTicketIniciales() {
        var contenido = document.getElementById('<%= TicketDiv.ClientID %>').innerHTML;
        var ventanaImpresion = window.open('', '_blank');

        var estilos = `<style>
                pre {
                    font-family: monospace;
                    white-space: pre;
                    margin: 1em 0;
                }
            </style>`;

        ventanaImpresion.document.write(estilos + '<pre>' + contenido + '</pre>');
        ventanaImpresion.document.close();
        ventanaImpresion.print();

    }
    function cambiarTipoPersona() {
        var valueDropDownList = $("#<%= ddlPersonaVictima.ClientID %>");
        var txtValue = $("#<%= lblAP.ClientID %>");
        var lblRazonSocialVictima = $("#<%= txtRazonSocialVictima.ClientID %>");
        var lblApellidoPaternoVictima = $("#<%= txtAPVictima.ClientID %>");
        var lblNombreVictima = $("#<%= txtNombreVictima.ClientID %>");
        var lblApellidoMaternoVictima = $("#<%= txtAMVictima.ClientID %>");
        var lblApellidoMaternoVictima = $("#<%= txtAMVictima.ClientID %>");
        var ddlGeneroVictima = $("#<%= ddlSexoVictima.ClientID %>");

        var txtApellidoMaternoVictima = $("#<%= lblAM.ClientID %>");
        var txtNombreVictima = $("#<%= lblNombre.ClientID %>");

        var txtGeneroVictima = $("#<%= lblSexo.ClientID %>");






        if (valueDropDownList.val() === "F") {
            txtValue.html("Apellido paterno: ");
            txtValue.fadeIn();
            txtNombreVictima.fadeIn();
            txtApellidoMaternoVictima.fadeIn();
            lblApellidoPaternoVictima.fadeIn();
            lblApellidoMaternoVictima.fadeIn();
            txtGeneroVictima.fadeIn();
            lblNombreVictima.fadeIn();
            ddlGeneroVictima.fadeIn();
            lblRazonSocialVictima.fadeOut();

        } else if (valueDropDownList.val() === "M") {
            txtValue.html("Persona moral:");
            txtValue.fadeIn();
            txtNombreVictima.fadeOut();
            txtApellidoMaternoVictima.fadeOut();
            lblApellidoPaternoVictima.fadeOut();
            lblApellidoMaternoVictima.fadeOut();
            txtGeneroVictima.fadeOut();
            lblNombreVictima.fadeOut();
            lblRazonSocialVictima.fadeIn();
            ddlGeneroVictima.fadeOut();
        } else if (valueDropDownList.val() === "") {
            var mensaje = "Selecciona un tipo de persona";
            txtValue.fadeOut();
            toastError(mensaje);
            txtGeneroVictima.fadeOut();
            txtNombreVictima.fadeOut();
            txtApellidoMaternoVictima.fadeOut();
            lblApellidoPaternoVictima.fadeOut();
            lblApellidoMaternoVictima.fadeOut();
            lblRazonSocialVictima.fadeOut();
            ddlGeneroVictima.fadeOut();
        }
    }


    function validarNumero(input) {
        // Obtener el valor del campo
        var valor = input.value;

        // Verificar si el valor es un número
        if (isNaN(valor)) {
            // Si no es un número, mostrar un mensaje de error y limpiar el campo
            var mensaje = "Este  campo solo acepta numeros";
            //alert("Este  campo solo acepta numeros");
            toastError(mensaje);


            input.value = "";
        }
    }


    function mostrarOcultarDescripcion() {
        var dropdown = $("#<%= txtAnexosTipo.ClientID %>");
        var contenedor = $("#contenedorDescripcion");

        if (dropdown.val() === "Otro") {
            contenedor.fadeIn();
        } else {
            contenedor.fadeOut();
        }
    }
    function valoresFinales() {
        copiarNUC();
        copiarTipoAsunto();
        copiarRadicacion();
        copiarFechaRecepcion();
        copiarNumeroFojas();
        copiarQuienIngresa();
        copiarEspecificarNombre();
        copiarTipoRadicacion();

        copiarObservacione();

    }
    function copiarNUC() {
        var inputNUC = $("#<%= inputNUC.ClientID %>");
        var copyTextBoxNUC = $("#<%= copyTextBoxNUC.ClientID %>");

        copyTextBoxNUC.val(inputNUC.val());
    }

    function copiarTipoAsunto() {
        var inputTipoAsunto = $("#<%= inputTipoAsunto.ClientID %>");
        var copyDropDownTipoAsunto = $("#<%= copyDropDownTipoAsunto.ClientID %>");

        copyDropDownTipoAsunto.val(inputTipoAsunto.val());
    }
    function copiarRadicacion() {
        var inputRadicacion = $("#<%= inputRadicacion.ClientID %>");
        var copyDropDownTipoSolicitud = $("#<%= copyDropDownTipoSolicitud.ClientID %>");
        copyDropDownTipoSolicitud.val(inputRadicacion.val());
    }
    function copiarFechaRecepcion() {
        var inputFechaRecepcion = $("#<%= inputFechaRecepcion.ClientID %>");
        var copyFechaRecepcion = $("#<%= copyFechaRecepcion.ClientID %>");
        copyFechaRecepcion.val(inputFechaRecepcion.val());
    }
    function copiarNumeroFojas() {
        var inputNumeroFojas = $("#<%= inputNumeroFojas.ClientID %>");
        var copyNumeroFojas = $("#<%= copyNumeroFojas.ClientID %>");
        copyNumeroFojas.val(inputNumeroFojas.val());
    }
    function copiarQuienIngresa() {
        var inputQuienIngresa = $("#<%= inputQuienIngresa.ClientID %>");
        var copyQuienIngresa = $("#<%= copyQuienIngresa.ClientID %>");
        copyQuienIngresa.val(inputQuienIngresa.val());
    }
    function copiarEspecificarNombre() {
        var inputNombreParticular = $("#<%= inputNombreParticular.ClientID %>");
        var copyinputNombreParticular = $("#<%= copyinputNombreParticular.ClientID %>");
        copyinputNombreParticular.val(inputNombreParticular.val());
    }
    function copiarTipoRadicacion() {
        var inpuTipoRadicacion = $("#<%= inpuTipoRadicacion.ClientID %>");
        var copyTipoRadicacion = $("#<%= copyTipoRadicacion.ClientID %>");
        copyTipoRadicacion.val(inpuTipoRadicacion.val());
    }

    function copiarObservacione() {
        var inputObservaciones = $("#<%= inputObservaciones.ClientID %>");
        var copyObservaciones = $("#<%= copyObservaciones.ClientID %>");
        copyObservaciones.val(inputObservaciones.val());
    }





</script>







<asp:UpdatePanel runat="server" ID="updPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">

    <ContentTemplate>




        <div class="d-flex justify-content-between align-items-center flex-wrap">
            <h5 class="text-secondary mb-0">Registro de iniciales</h5>

            <div class="row g-1 ">
                <div class="  d-flex col">
                    <label class="form-label text-secondary d-flex p-0 m-0">Filtrar: </label>
                    <asp:DropDownList runat="server" ID="ddlTipoFiltrado" CssClass="form-select form-select-sm text-secondary" ClientIDMode="Static" onchange="seleccionarOpcion()">
                        <asp:ListItem Text="Selecciona una opción" Value="" />
                        <asp:ListItem Text="NUC" Value="NUC" />
                        <asp:ListItem Text="Víctima" Value="V" />
                        <asp:ListItem Text="Imputado" Value="I" />
                    </asp:DropDownList>

                </div>
                <div class="col-auto d-flex col">
                    <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="inputBuscarInicial" Text="" />
                </div>
                <div class="col-auto ">
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-outline-success btn-sm" Text="Buscar" OnClick="btnFiltrarInicial" AutoPostBack="true"></asp:Button>
                </div>
            </div>
        </div>
        <asp:Panel ID="BusquedaIniciales" runat="server" Visible="false">

            <div class="card mt-3 ">
                <div class="card-body table-responsive p-0 mx-0 mt-3 ">
                    <asp:GridView ID="grdTblGetPromociones" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm table-borderless ">
                        <Columns>
                            <asp:BoundField DataField="Victimas" HeaderText="Victimas Asociadas" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />
                            <asp:BoundField DataField="Inculpados" HeaderText="Inculpados Asociados" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />
                            <asp:BoundField DataField="TipoAsunto" HeaderText="Tipo de asunto" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />
                            <%--<asp:BoundField DataField="Numero" HeaderText="Numero" ItemStyle-CssClass="text-secondary text-center" HeaderStyle-CssClass="bg-success text-white text-center" />--%>
                            <asp:BoundField DataField="NUC" HeaderText="NUC" ItemStyle-CssClass="text-success fw-bold text-center " HeaderStyle-CssClass="bg-success text-white text-center" />
                            <asp:BoundField DataField="Delitos" HeaderText="Delitos" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <asp:Label runat="server" CssClass="text-dark text-center text-success" ID="errorConsulta" Text=""></asp:Label>

        <div class="row pt-5">

            <form class="container-lg col-xxl-12">
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNUC" class="form-label text-secondary">NUC:</label>
                    <asp:TextBox runat="server" ID="inputNUC" CssClass="form-control form-control-sm " MaxLength="20" onblur="formatNuc(this)"/>

                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 ">
                    <label for="inputTipoAsunto" class="form-label text-secondary">Tipo de asunto: </label>
                    <asp:DropDownList runat="server" ID="inputTipoAsunto" AutoPostBack="true" CssClass="form-select form-select-sm text-secondary" OnSelectedIndexChanged="inputTipoAsunto_SelectedIndexChanged">
                        <asp:ListItem Text="Selecciona una opción" Value="" />
                        <asp:ListItem Text="Causa" Value="C" />
                        <asp:ListItem Text="Cupre" Value="CP" />
                    </asp:DropDownList>
                </div>

                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inpuTipoSolicitud" class="form-label text-secondary">Tipo solicitud: </label>

                    <asp:DropDownList runat="server" ID="inputRadicacion" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true">
                        <asp:ListItem Text="Selecciona una opción" Value="" />
                    </asp:DropDownList>

                </div>
                <div class=" mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputFechaRecepcion" class="form-label text-secondary">Fecha de recepción</label>
                    <asp:TextBox runat="server" ID="inputFechaRecepcion" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                </div>

                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNumeroFojas" class="form-label text-secondary">Número de fojas:</label>
                    <asp:TextBox runat="server" ID="inputNumeroFojas" CssClass="form-control form-control-sm" oninput="validarNumero(this)" MaxLength="6" />

                </div>
                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Quién ingresa: </label>

                    <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" ID="inputQuienIngresa" AutoPostBack="true" OnSelectedIndexChanged="inputQuienIngresa_SelectedIndexChanged">
                        <asp:ListItem Text="Selecciona una opción" Value="" />
                        <asp:ListItem Text="MP" Value="M" />
                        <asp:ListItem Text="Particular" Value="P" />
                        <asp:ListItem Text="Otro" Value="O" />
                    </asp:DropDownList>

                </div>
                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNombreParticular" class="form-label text-secondary">
                        Especificar nombre de 
                       
                        <asp:Label ID="lblTipoPersona" runat="server" Text=""></asp:Label>:
                    </label>
                    <asp:TextBox runat="server" ID="inputNombreParticular" CssClass="form-control form-control-sm " MaxLength="150" />
                </div>

                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Tipo de radicación: </label>
                    <div class="d-flex align-items-center">
                        <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" ID="inpuTipoRadicacion" AutoPostBack="false">
                            <asp:ListItem Text="Selecciona una opción" Value="" />
                            <asp:ListItem Text=" C/Detenido" Value="C" CssClass="form-check-input" />
                            <asp:ListItem Text=" S/Detenido" Value="S" CssClass="form-check-input" />
                        </asp:DropDownList>

                    </div>

                </div>


                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Prioridad: </label>
                    <div class=" d-flex align-items-center">
                        <asp:RadioButtonList runat="server" ID="inputPrioridad" CssClass="form-check text-secondary d-flex flex-row" OnSelectedIndexChanged="GetLabelPrioridad" AutoPostBack="true">
                            <asp:ListItem Text="Alta" Value="A" Selected="False" />
                            <asp:ListItem Text="Normal" Value="N" Selected="False" />
                        </asp:RadioButtonList>

                    </div>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputObservaciones" class="form-label text-secondary">Observaciones: </label>
                    <asp:TextBox runat="server" ID="inputObservaciones" CssClass="form-control form-control-sm" MaxLength="8000"></asp:TextBox>
                </div>

                <%--first part--%>
                <div class="row">


                    <div class="col-12 col-lg-6 text-left ">

                        <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Víctima</span>
                        <i class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modal2">+</i>

                        <div class="table-responsive mt-2">



                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class="text-center">
                                    <tr>
                                        <th scope="col" class="bg-success text-white">Nombre</th>
                                        <th scome="col" class="bg-success text-white">Apellidos</th>
                                        <th scope="col" class="bg-success text-white">Género</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody class="table table-striped text-center">
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <th scope="row" class=""><%# !string.IsNullOrEmpty(Eval("Nombre").ToString()) ? Eval("Nombre") : Eval("ApellidoPaterno") %></th>
                                                <th class="text-secondary"><%# Eval("ApellidoPaterno") %> <%# Eval("ApellidoMaterno") %></th>
                                                <td class="text-secondary "><%# Eval("Genero") %></td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarVictima" /></i></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>




                    <div class="col-12 col-lg-6 text-left">

                        <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i>Imputado</span>
                        <i class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modal1">+</i>
                        <div class="table-responsive mt-2">

                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class=" text-center">
                                    <tr class="">

                                        <th scope="col" class="bg-success text-white">Nombre</th>
                                        <th scope="col" class="bg-success text-white">Apellidos</th>
                                        <th scope="col" class="bg-success text-white">Género</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>

                                    </tr>
                                </thead>
                                <tbody class="table table-striped text-center ">
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <th scope="row" class=""><%# Eval("NombreCulpado") %></th>
                                                <th class="text-secondary"><%# Eval("APCulpado") %> <%# Eval("AMCulpado") %></th>
                                                <td class="text-secondary "><%# Eval("GeneroCulpado") %></td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarCulpado" /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>




                <%--second part--%>
                <div class="row mt-4 d-flex">
                    <div class="col-12 col-lg-6 text-left">

                        <div class="mb-0 row">
                            <div class="col-md-5">

                                <label for="inputDelitos" class="form-label text-secondary align-self-center">Delitos: </label>


                                <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true" ID="inputDelitos" AutoPostBack="false">
                                    <asp:ListItem Text="Selecciona una opcion" Value="" />
                                </asp:DropDownList>
                                <%--<asp:Label runat="server" ID="lblNombresDelitos" Text=""></asp:Label>--%>
                            </div>
                            <div class="col-1 d-flex align-items-end ">
                                <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="+" OnClick="btnEnviarDelito_Click" UseSubmitBehavior="false" />

                            </div>
                        </div>

                        <div class="table-responsive mt-2">
                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class=" text-center">
                                    <tr>

                                        <th scope="col" class="bg-success text-white">Delito</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>
                                    </tr>
                                </thead>
                                <%--Boton de eliminar delitos no funciona--%>
                                <tbody class="table table-striped text-center ">
                                    <asp:Repeater ID="RepeaterDelitos" runat="server">
                                        <ItemTemplate>
                                            
                                            <tr>
                                                <td class="text-secondary text-capitalize "><%# Eval("DescripcionDelito") %></td>
                                                <td class="">
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarDelito" />                                                    
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>

                        </div>
                    </div>
                    <div class="col-12 col-lg-6 text-left">

                        <div class="mb-0 row">
                            <div class="col-md-4">
                                <label for="txtAnexosTipo" class="form-label text-secondary align-self-center">Anexos: </label>
                                <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" ID="txtAnexosTipo" AppendDataBoundItems="true" AutoPostBack="false" onchange="mostrarOcultarDescripcion()">
                                    <asp:ListItem Text="Selecciona una opción" Value="" />
                                </asp:DropDownList>

                            </div>

                            <div id="contenedorDescripcion" style="display: none;" class="col-md-4">
                                <asp:Label runat="server" AssociatedControlID="txtAnexosTipo" class="form-label text-secondary align-self-center">Descripción: </asp:Label>
                                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtDescripcionAnexos" MaxLength="8000" />
                            </div>
                            <div class="col-md-3">

                                <label for="inputCantidad" class="form-label text-secondary align-self-center">Cantidad: </label>
                                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtCantidadAnexos" oninput="validarNumero(this)" MaxLength="10" />
                            </div>
                            <div class="col-1 d-flex align-items-end">

                                <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="+" OnClick="btnGuardarAnexos_Click" />
                            </div>
                        </div>
                        <div class="table-responsive mt-2">

                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class=" text-center">
                                    <tr class="">

                                        <th scope="col" class="bg-success text-white">Descripción</th>
                                        <th scope="col" class="bg-success text-white">Cantidad</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>

                                    </tr>
                                </thead>
                                <tbody class="table table-striped text-center ">
                                    <asp:Repeater ID="Repeater3" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <th class="text-secondary"><%# Eval("DescripcionAnexo") %></th>
                                                <td class="text-secondary"><%# Eval("CantidadAnexo") %></td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarAnexo" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>



                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class=" d-flex justify-content-center mt-5">
                    <a class="btn btn-success btn-sm" data-bs-toggle="modal" onclick="valoresFinales();" data-bs-target="#modal3"><i class="bi bi-floppy-fill mr-1"></i>Enviar</a>
                </div>
                <asp:Label runat="server" ID="lblSuccess" Text="" CssClass="text-success text-center"></asp:Label>
                <asp:Label runat="server" ID="lblError" Text="" CssClass="text-danger text-center"></asp:Label>
                <!-- FIN DIV OCULTO DOS -->
                <div class=" d-flex justify-content-center align-content-center w-100vw  mt-3">
                    <pre id="TicketDiv" runat="server"></pre>

                </div>
                <div class="container d-flex flex-row-reverse justify-content-center align-content-center w-100vw m-0 " id="tituloSelloIniciales" style="display: none !important;" runat="server">
                    <div class="col-auto flex-column-reverse btn btn-info d-flex btn-sm" onclick="imprimirTicketIniciales()" style="cursor: pointer;">
                        <h6 class="text-center align-self-center m-0 p-0 ">¡Imprimir <span class="text-black">ticket!</span></h6>
                        <div class="col-auto ">
                            <i class="bi bi-printer-fill  btn-sm"></i>
                        </div>
                    </div>
                </div>

            </form>

        </div>


        <%--modales--%>
        <!-- Modal imputado -->
        <div class="modal fade" id="modal1" tabindex="-1" aria-labelledby="modal1Label" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <%--contenido--%>
                    <div class="modal-header ">
                        <div class="row d-flex align-items-center justify-content-center text-center col-12">
                            <i class="bi bi-emoji-frown text-success pr-2" style="font-size: 56px;"></i>
                            <h4 class="modal-title text-secondary ">Agregar partes</h4>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body d-flex flex-wrap justify-content-start">
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAPaternoImputado" runat="server" AssociatedControlID="txtAPaternoImputado" CssClass="form-label text-secondary">Apellido Paterno:</asp:Label>
                            <asp:TextBox ID="txtAPaternoImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="50" />
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAMaternoImputado" runat="server" AssociatedControlID="txtAMaternoImputado" CssClass="form-label text-secondary">Apellido Materno:</asp:Label>
                            <asp:TextBox ID="txtAMaternoImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="50" />
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblNombreImputado" runat="server" AssociatedControlID="txtNombreImputado" CssClass="form-label text-secondary">Nombre (s):</asp:Label>
                            <asp:TextBox ID="txtNombreImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                        </div>


                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblGeneroImputado" runat="server" AssociatedControlID="txtGeneroImputado" CssClass="form-label text-secondary">Sexo:</asp:Label>
                            <asp:DropDownList runat="server" ID="txtGeneroImputado" CssClass="form-select form-select-sm text-secondary">
                                <asp:ListItem Text="Selecciona una opción" Value="" />
                                <asp:ListItem Text="Femenino" Value="F" />
                                <asp:ListItem Text="Masculino" Value="M" />
                                <asp:ListItem Text="Otro" Value="O" />
                            </asp:DropDownList>
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAliasImputado" runat="server" AssociatedControlID="txtAliasImputado" CssClass="form-label text-secondary">Alias:</asp:Label>
                            <asp:TextBox ID="txtAliasImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                        </div>


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Guardar" OnClick="btnGuardarImputado_Click" data-bs-dismiss="modal" />

                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Partes victima -->
        <div class="modal fade" id="modal2" tabindex="-1" aria-labelledby="modal2Label" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <!-- Contenido del segundo modal -->
                    <div class="modal-header">
                        <div class="row d-flex align-items-center justify-content-center text-center col-12">
                            <i class="bi bi-emoji-smile text-success pr-2" style="font-size: 56px;"></i>
                            <h4 class="modal-title text-secondary ">Agregar partes</h4>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div class="modal-body d-flex flex-wrap justify-content-start">


                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <label for="inputvictimapersona" class="form-label text-secondary">Persona:</label>

                            <asp:DropDownList ID="ddlPersonaVictima" runat="server" CssClass="form-select form-select-sm text-secondary" AutoPostBack="true" onchange="cambiarTipoPersona()">
                                <asp:ListItem Text="Selecciona una opción" Value="" />
                                <asp:ListItem Text="Física" Value="F" />
                                <asp:ListItem Text="Moral" Value="M" />
                            </asp:DropDownList>
                        </div>

                        <%--//Apellido paterno es utizado para el registro de RAZON SOCIAL--%>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAP" runat="server" class="form-label text-secondary" Style="display: none;"></asp:Label>
                            <asp:TextBox ID="txtAPVictima" runat="server" CssClass="form-control form-control-sm" Style="display: none;" MaxLength="40" />
                            <asp:TextBox ID="txtRazonSocialVictima" runat="server" CssClass="form-control form-control-sm" Style="display: none;" MaxLength="40" />
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAM" runat="server" class="form-label text-secondary" Style="display: none;">Apellido materno:</asp:Label>
                            <asp:TextBox ID="txtAMVictima" runat="server" CssClass="form-control form-control-sm" Style="display: none;" MaxLength="40" />
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblNombre" runat="server" CssClass="form-label text-secondary" Style="display: none;">Nombre(s):</asp:Label>
                            <asp:TextBox ID="txtNombreVictima" runat="server" CssClass="form-control form-control-sm" Style="display: none;" MaxLength="40" />
                        </div>


                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblSexo" runat="server" class="form-label text-secondary" Style="display: none;">Sexo:</asp:Label>
                            <asp:DropDownList ID="ddlSexoVictima" runat="server" CssClass="form-select form-select-sm text-secondary" Style="display: none;">
                                <asp:ListItem Text="Selecciona una opción" Value="" />
                                <asp:ListItem Text="Femenino" Value="F" />
                                <asp:ListItem Text="Masculino" Value="M" />
                                <asp:ListItem Text="Otro" Value="O" />
                            </asp:DropDownList>
                        </div>





                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <asp:Button runat="server" Text="Guardar" CssClass="btn btn-success btn-sm" OnClick="btnGuardarVictima_Click" OnClientClick="ocultarElementos();" data-bs-dismiss="modal" />


                    </div>
                </div>
            </div>
        </div>



        <!-- Agrega esto en tu HTML donde tengas tus otros elementos -->
        <div id="errorModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Contenido del modal -->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Error</h4>
                    </div>
                    <div class="modal-body">
                        <p>Los datos no fueron correctamente llenados. Por favor, verifica e intenta nuevamente.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- MOdal Save Changes -->
        <div class="modal fade" id="modal3" tabindex="1" aria-labelledby="modal3Label" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <!-- Contenido del segundo modal -->
                    <div class="modal-header">
                        <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
                        <h5 class="modal-title text-secondary fs-4 ">Guardar cambios.</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body row">
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Número de NUC:</span>
                            <asp:TextBox runat="server" ID="copyTextBoxNUC" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Tipo de Asunto:</span>
                            <asp:TextBox runat="server" ID="copyDropDownTipoAsunto" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6  pb-2">
                            <span class="text-secondary">Tipo de solicitud:</span>
                            <asp:TextBox runat="server" ID="copyDropDownTipoSolicitud" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Fecha de recepción:</span>
                            <asp:TextBox runat="server" ID="copyFechaRecepcion" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Número de Forjas:</span>
                            <asp:TextBox runat="server" ID="copyNumeroFojas" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Quién Ingresa:</span>
                            <asp:TextBox runat="server" ID="copyQuienIngresa" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Nombre de quién Ingresa:</span>
                            <asp:TextBox runat="server" ID="copyinputNombreParticular" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Tipo de Radicación:</span>
                            <asp:TextBox runat="server" ID="copyTipoRadicacion" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-12  pb-5">
                            <span class="text-secondary">Prioridad:</span>
                            <asp:TextBox runat="server" ID="copyPrioridad" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" ClientIDMode="Static" />

                        </div>
                        <div class="col-12">
                            <span class="text-secondary">Observaciones:</span>
                            <asp:TextBox runat="server" ID="copyObservaciones" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" TextMode="MultiLine" Rows="4" />

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Enviar" OnClick="btnEnviarInicial_Click" OnClientClick="return validarNumero();" data-bs-dismiss="modal" postback="true" />

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="../Scripts/Ejecucion/formatoInput.js"></script>

<script src="../Scripts/consignaciones/RegistroIniciales.js"></script>
<!-- Asegúrate de tener una referencia a la biblioteca jQuery en tu proyecto -->

<script>
    //FUNCIONES DE ALERTAS EXITO Y ERROR
    function mostrarAlerta(message) {
        $('#exito .modal-body b').text(message); // Establece el mensaje en el modal
        $('#exito').modal('show'); // Muestra el modal
    }
    function mostrarError(mensaje) {
        $('#modalError .modal-body b').text(mensaje); // Establece el mensaje de error en el modal
        $('#modalError').modal('show'); // Muestra el modal de error
    }
    // Función para abrir el modal
    function abrirModalGuardarDatos() {
        $('#guardarDatos').modal('show');
    }

    function CerrarModalGuardarDatos() {
        $('#guardarDatos').modal('hide');
        $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
        $('.modal-backdrop').remove();
    }

    function mostrarModalError() {
        $('#errorModal').modal('show');
        alert("Entro modal error insert");
    }

</script>
