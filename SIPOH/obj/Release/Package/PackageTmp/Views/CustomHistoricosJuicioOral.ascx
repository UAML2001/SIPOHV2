<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomHistoricosJuicioOral.ascx.cs" Inherits="SIPOH.Views.CustomHistoricosJuicioOral" %>
<asp:UpdatePanel runat="server" ID="JuicioOralHistoricoPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>



        <div class="d-flex justify-content-between align-items-center flex-wrap mb-0">
            <div></div>
            <div class="row g-1 ">
                <div class=" col-auto d-flex col">

                    <b for="inputNuc" class="form-label text-dark mr-1">Causa: </b>
                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-secondary" ID="inputNumero" onblur="padLeadingZeros(this)" MaxLength="9" />

                </div>
                <div class="col-auto">

                    <asp:Button runat="server" CssClass="btn btn-outline-success btn-sm" Text="Buscar" OnClick="btnConsultaHistoricoCausa"  AutoPostBack="true" />
                </div>

            </div>
        </div>

        <div class="row mt-4 px-0 pt-0 pb-0 mb-1">
            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label class="help-block text-muted small-font">Número de juicio oral: </label>
                <asp:TextBox runat="server" CssClass="form-control form-control-sm text-secondary" ID="inputNumeroArchivo" onblur="padLeadingZeros(this)"  MaxLength="9"/>
            </div>
            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label for="inputTipoAudiencia" class="form-label text-secondary">Tipo solicitud: </label>
                <asp:DropDownList runat="server" ID="inputTipoAudiencia" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoAudiencia_Selected">
                    <asp:ListItem Text="Selecciona una opción" Value="" />
                </asp:DropDownList>
            </div>
            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label  class="form-label text-secondary">Fecha recepción: </label>
                <asp:TextBox runat="server" CssClass="form-control form-control-sm text-secondary" ID="fechaRecepcion" TextMode="DateTimeLocal" onblur="validarFecha()"   MaxLength="6"/>
            </div>
            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label class="help-block text-muted small-font">Número fojas: </label>
                <asp:TextBox runat="server" CssClass="form-control form-control-sm text-secondary" ID="numeroFojas"   oninput="validarNumero(this)" MaxLength="6"/>
            </div>
            
            
            <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label class="help-block text-muted small-font">Observaciones: </label>
                <asp:TextBox runat="server" CssClass="form-control form-control-sm text-secondary" ID="observacionesIncial" MaxLength="8000" />
            </div>

        </div>

        <asp:Panel ID="ConsultaCausa" runat="server" Visible="false">       

        <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i> Inculpados de causa:</span>


        <div class="border border-light border-1 rounded my-2 ">
            <asp:Repeater ID="RepeaterListaPartes" runat="server" OnItemDataBound="RepeaterListaPersonas_Items">
                <ItemTemplate>
                    <div class="row   ml-3 py-2 btn btn-outline-light border-1 border-bottom">

                        <div class=" justify-content-center flex-grow-2" id="ItemSelectedBox">
                            <div>
                                <b class="mr-2 text-secondary" id="labelApellido"><%# Eval("Nombre") %></b> <i class="mr-2 text-secondary" id="labelNombre"><%# Eval("Apellidos") %></i>
                            </div>
                            <asp:Button runat="server"  CssClass="btn  btn-sm border btn-light border-0 rounded " ID="controlSelected" Text="➕" OnClick="ObtenerDelitos_Click" CommandArgument='<%# Eval("IdPartes") %>' />
                            

                            <b style="display: none;"><%# Eval("IdAsuntoCausa") %></b>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:HiddenField ID="HiddenIdAsuntoCausa" runat="server" />

        <asp:Label ID="lblIdSeleccionado" runat="server" Text=""></asp:Label>

</asp:Panel>
        <asp:Panel ID="DelitosInculpado" runat="server" Visible="false">
        <div class="row mt-4">

            <span class="text-success fw-bold m-2"><i class="bi bi-folder2-open"></i>Delitos del inculpado <b class="" id="labelGetNameSelected"></b>:</span>

        </div>

        <div class=" border-2 border-top border-bottom py-3 mb-3 row justify-content-center px-3 mx-0" style="background-color: #3F5259;">
            <asp:Repeater ID="RepeaterListaDelitos" runat="server">
                <ItemTemplate>
                    <div class=" col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4 flex-wrap">
                        <asp:CheckBox ID="chkDelito1" runat="server" CssClass=""
                            Value='<%# Eval("IdDelito") %>' OnCheckedChanged="chkDelito_CheckedChanged" AutoPostBack="true"
                            data-idDeliAsunto='<%# Eval("IdDelito") %>'
                            data-idInculpado='<%# Eval("IdInculpadoDel") %>' data-DelitoSelected='<%# Eval("Delito") %>' data-NombreInculpado ='<%# Eval("NombreInculpado") %>'/>
                        <span class="form-check-label text-white" for="checkDelito1"><%# Eval("Delito") %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <%-- <asp:Label ID="lblIdDeliAsunto" runat="server" Text=""></asp:Label>--%>
            </asp:Panel>
        <asp:Panel ID="VictimasDelDelito" runat="server" Visible="false">
        <div class="row mt-4">

            <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Victimas a las que se les cometio el delito:</span>

        </div>
        <div class="border border-light border-1 rounded my-2 pr-4 ">
            <asp:Repeater ID="RepeaterListaVictimasJO" runat="server">
                <ItemTemplate>
                    <div class="col-12 pr-5 d-flex flex-column justify-content-between align-content-center ml-3 p-2 btn btn-outline-light border-1 border-bottom">
                        <div class="d-flex justify-content-between align-content-center" id="ItemSelectedBox">
                            <div class="ml-2">
                                <b class="text-dark" id="labelApellido"><%# Eval("Nombre") %></b>
                            <i class="text-dark" id="labelNombre"><%# Eval("Apellidos") %></i>
                            </div>
                                <asp:CheckBox ID="chkVictima1" runat="server" CssClass=""
    Value='<%# Eval("IdVictimaDel") %>' OnCheckedChanged="chkVictimaDel_CheckedChanged" AutoPostBack="true"
    data-idVictimaDel='<%# Eval("IdVictimaDel") %>' data-idDeliAsunto='<%# Eval("IdDeliAsunto") %>' 
    data-NombreVictima='<%# Eval("Nombre") %>' data-ApellidosVictima='<%# Eval("Apellidos") %>' />


                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="mt-5 mb-3 d-flex justify-content-center">
            <a class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalAgregarJuicioOral">
                <i class="bi bi-diagram-3-fill"></i>Agregar relación
            </a>
        </div>
            </asp:Panel>
        <div class="row mb-3">
            <span class="text-success fw-bold m-2 mt-4"><i class="bi bi-diagram-3-fill"></i>Tabla de relación:</span>
            <div class="table-responsive">
                <asp:GridView ID="GridRelaciones" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm table-borderless" OnRowDeleting="GridRelaciones_RowDeleting">
    <Columns>
        <asp:BoundField DataField="Inculpado" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-dark text-center" HeaderText="Inculpado" />
        <asp:BoundField DataField="Delitos" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-secondary text-center" HeaderText="Delitos" />
        <asp:BoundField DataField="Victimas" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-secondary text-center" HeaderText="Victimas" />
        <asp:CommandField ShowDeleteButton="True" DeleteText="✖️"  HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-secondary text-center"/>
        
        <asp:BoundField DataField="IdDelitosInculpado" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-secondary text-center" HeaderText="Id Delitos Inculpado" Visible="false" />
        <asp:BoundField DataField="IdDelitosVictima" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-secondary text-center" HeaderText="Id Delitos Victima" Visible="false" />
    </Columns>
</asp:GridView>

                
            </div>

        </div>
        <%--<hr class="bg-success mt-5" />--%>

        

       
        <div class=" d-flex justify-content-center align-content-center w-100vw  mt-3">
            <pre id="TicketJO" runat="server"></pre>

        </div>
        <%--<div class=" d-flex justify-content-center my-5" id="ocultarAGuardar" runat="server">--%>
        <div class=" d-flex justify-content-center my-5" id="ocultarAGuardar" runat="server" style="display:none !important;">
            <a class="btn btn-success btn-sm" data-bs-toggle="modal" onclick="valoresFinales();" data-bs-target="#envioRelaciones"><i class="bi bi-floppy-fill mr-1"></i>Guardar</a>
        </div>
            <div class="container d-flex flex-row-reverse justify-content-center align-content-center w-100vw m-0 " Id="tituloSelloJOIniciales" style="display: none !important ;" runat="server">
                <div class="col-auto flex-column-reverse btn btn-info d-flex btn-sm" onclick="imprimirTicketJOIniciales()" style="cursor: pointer; ">
                    <h6 class="text-center align-self-center m-0 p-0 " >¡Imprimir <span class="text-black">ticket!</span></h6>
                    <div class="col-auto ">
                        <i class="bi bi-printer-fill  btn-sm"></i>
                    </div>
                </div>
            </div>
        <br />
        <%--modales--%>
        <%--        MODAL AGREGAR RELACION--%>
        <div class="modal fade" id="modalAgregarJuicioOral" tabindex="-1" aria-labelledby="modalAgregarJuicioOral" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-sm">
                <div class="modal-content d-flex">
                        <button type="button" class="btn-close align-self-end" data-bs-dismiss="modal" aria-label="Close"></button>
                    <div class="modal-header d-flex flex-column justify-content-around align-content-center">
                        <div class=" align-self-center">
                        <i class="bi bi-diagram-3-fill text-warning fs-3 pr-2"></i>

                        </div>
                        <h4 class="modal-title text-secondary fs-4 text-center ">¿Deseas agregar relación de partes?</h4>
                    </div>
                    
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Generar relación" OnClick="btnGenerarRelacion_Click" data-bs-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
        <%-- Modal envio de JO--%>

        <div class="modal fade" id="envioRelaciones" tabindex="-1" aria-labelledby="envioRelaciones" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
                        <h5 class="modal-title text-secondary fs-4 ">Guardar cambios.</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                         <div class="col-12 pb-2">
                            <span class="text-secondary">Número de causa:</span>
                            <asp:TextBox runat="server" ID="copyInputNumero" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-12 pb-2">
                            <span class="text-secondary">Número de juicio oral histórico:</span>
                            <asp:TextBox runat="server" ID="copyInputNumeroArchivo" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-12 pb-2">
                            <span class="text-secondary">Tipo de audiencia:</span>
                            <asp:TextBox runat="server" ID="copyInputTipoAudiencia" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-12 pb-2">
                            <span class="text-secondary">Fecha de recepción:</span>
                            <asp:TextBox runat="server" ID="copyFechaRecepcion" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-12 pb-2">
                            <span class="text-secondary">Número de fojas:</span>
                            <asp:TextBox runat="server" ID="copyNumeroFojas" CssClass="form-control form-control-sm text-center text-success text-uppercase" ReadOnly="true" />
                        </div>
                        <div class="col-12 pb-2">
                            <span class="text-secondary">Observaciones:</span>
                            <asp:TextBox runat="server" ID="copyObservacionesInicial" CssClass="form-control form-control-sm text-center text-success text-uppercase" ReadOnly="true" TextMode="MultiLine" Rows="4" />
                        </div>
                                  

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>

                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Guardar" OnClick="btnGuardarJO_Click" data-bs-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">    
                
            
            

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
            function valoresFinales() {
                copyInputNumero();
                copyInputNumeroArchivo();
                //copyInputTipoAudiencia();
                copyFechaRecepcion();
                copyNumeroFojas();
                copyObservacionesInicial();
            }
            function copyInputNumero() {
                var inputNumero = $("#<%= inputNumero.ClientID %>");
                var copyInputNumero = $("#<%= copyInputNumero.ClientID %>");
                copyInputNumero.val(inputNumero.val());
            }
            function copyInputNumeroArchivo() {
                var inputNumeroArchivo = $("#<%= inputNumeroArchivo.ClientID %>");
                var copyInputNumeroArchivo = $("#<%= copyInputNumeroArchivo.ClientID %>");
                copyInputNumeroArchivo.val(inputNumeroArchivo.val());
            }
            
            function copyFechaRecepcion() {
                var FechaRecepcion = $("#<%= fechaRecepcion.ClientID %>");
                var copyFechaRecepcion = $("#<%= copyFechaRecepcion.ClientID %>");
                copyFechaRecepcion.val(FechaRecepcion.val());
            }

            function copyNumeroFojas() {
                var numeroFojas = $("#<%= numeroFojas.ClientID %>");
                var copyNumeroFojas = $("#<%= copyNumeroFojas.ClientID %>");
                copyNumeroFojas.val(numeroFojas.val());
            }
            
            function copyObservacionesInicial() {
                var observacionesIncial = $("#<%= observacionesIncial.ClientID %>");
                var copyObservaciones = $("#<%= copyObservacionesInicial.ClientID %>");
                copyObservaciones.val(observacionesIncial.val());
            }
            
            function validarFecha() {
                var inputFechaRecepcion = document.getElementById('<%= fechaRecepcion.ClientID %>');
                var fechaSeleccionada = new Date(inputFechaRecepcion.value);
                var fechaActual = new Date();
                fechaActual.setHours(0, 0, 0, 0);

                if (fechaSeleccionada > fechaActual) {
                    //toastError("!Estas loco!, o ¿Vives en el futuro?");
                    toastError("No se puede seleccionar una fecha posterior a hoy.");
                    inputFechaRecepcion.value = "";
                }
            }

        </script>
        <script src="../Scripts/consignaciones/JuicioOral.js"></script>
    </ContentTemplate>
</asp:UpdatePanel>