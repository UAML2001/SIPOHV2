<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPromocion.ascx.cs" Inherits="SIPOH.Views.CustomPromocion" %>



<link href="Content/css/Consignaciones.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
<script type="text/javascript">
    function valoresFinales() {
        valorPromovente();
        valorFechaRecepcion();
    }
    function valorFechaRecepcion() {
        var inputFechaRecepcion = $("#<%= inputFechaRecepcion.ClientID %>");
        var copyFechaRecepcion = $("#<%= copyFechaRecepcion.ClientID %>");
        copyFechaRecepcion.val(inputFechaRecepcion.val());
        
    }
    function valorPromovente() {
        var inputPromovente = $("#<%= inputPromovente.ClientID %>");
        var copyPromovente = $("#<%= copyPromovente.ClientID %>");
        copyPromovente.val(inputPromovente.val());
    }
    //JS PROMOCIONES
    function mostrarTituloSello() {
        var tituloSello = document.getElementById('tituloSello');
        if (tituloSello) {
            tituloSello.style.display = 'block';
        }
    }
    function imprimirTicket() {
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
        ventanaImpresion.onfocus = function () { setTimeout(function () { ventanaImpresion.close(); }, 500); }
    }
    function seleccionarVictima(victima) {
        var inputPromovente = document.getElementById('<%= inputPromovente.ClientID %>');
        inputPromovente.value = victima;
    }
    function seleccionarImputado(imputado) {
        var inputPromovente = document.getElementById('<%= inputPromovente.ClientID %>');
        inputPromovente.value = imputado;
    }
    function mostrarOcultarDescripcion() {
        var dropdown = $("#<%= txtAnexosTipo.ClientID %>")
        var contenedor = $("#contenedorDescripcion");
        if (dropdown.val() === "Otro") {
            contenedor.fadeIn();
        } else {
            contenedor.fadeOut();
        }
    }
    function validarNumero(input) {
        var valor = input.value;
        if (isNaN(valor)) {
            var mensaje = "Este campo solo acepta numeros";
            toastError(mensaje);
            input.value = "";
        }
    }
    
    

    
    
</script>
<asp:UpdatePanel runat="server" ID="promocionPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
        <div class=" px-2 mx-1">
            <h5 class="text-secondary mb-4">Registro de promociones</h5>

            <div class="row d-flex justify-content-end align-content-end">
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <h6 class="help-block text-muted small-font">Tipo de Documento: </h6>
                    <asp:DropDownList runat="server" ID="DrpLstTipoDocumento" class="form-select form-select-sm text-secondary " AutoPostBack="true" OnSelectedIndexChanged="DrpLstObtenerTipoDocumento" >
                        <asp:ListItem Text="seleccionar" Value="" Selected="True" />                        
                        <asp:ListItem Text="Causa" Value="C"/>
                        <asp:ListItem Text="Juicio Oral" Value="JO" />
                        <asp:ListItem Text="Exhorto" Value="E" />
                        <asp:ListItem Text="Cupre" Value="CP" />
                    </asp:DropDownList>
                </div>
           
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <h6 class="help-block text-muted small-font" >Numero de <asp:Label ID="itemNombre" runat="server" Text=""></asp:Label>: </h6>
                        <div class="input-group">
                            <asp:TextBox runat="server"  CssClass="form-control form-control-sm " ID="inputNumero"  MaxLength="10"/>
                            <div class="input-group-append">
                                <asp:Button runat="server" CssClass="btn btn-outline-success btn-sm" Text="Buscar" OnClick="btnGetConsultaPromocion"  AutoPostBack="true"/>                                                                                                                                    
                            </div>
                        </div>
                    </div>
                <div class="card">
                    <div class="card-body">

                        <div class="">                                                               
                            <div class="d-flex justify-content-end">
                                <i class="bi bi-people-fill text-success mr-3"></i>
                                <h5 class="text-success text-right">Relacion de partes</h5>
                            </div>
                            <br />
                            <br />
                            <%-- First part --%>
                            <div class="row border border-top-0 border-1 my-3 ">
                                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                    <h5 ><b>Victima</b></h5>
                                    <asp:Label runat="server" CssClass="text-secondary " ID="lblVictimasPromocion"></asp:Label>
                                </div>   
                                 <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                    <h5><b>Numero Documento</b></h5>
                                    <asp:Label runat="server" CssClass="text-secondary" ID="lblNumeroPromocion"></asp:Label>
                                </div>                            
                            </div>
                            <%-- Second part --%>
                            <div class="row mb-1 border border-top-0 border-1 my-3">
                                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                   
                                   <h5><b>Imputado</b></h5>
                                    <asp:Label runat="server" CssClass="text-secondary" ID="lblInculpadosPromocion"></asp:Label>
                                </div>                                
                                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                    <h5><b>Delitos:</b></h5>
                                    <asp:Label runat="server" CssClass="text-secondary" ID="lblDelitosPromocion"></asp:Label>
                                </div>
                                
                            </div> 
                            <%-- third part --%>
                             <div class="row mb-4 pt-3 " style="background-color: #3F5259;">
                                 <div class="mb-2 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                    <h6><b class="text-success">Numero de amparo:</b></h6>
                                    <asp:Label runat="server" CssClass="text-light" ID="lblNumeroAmparoPromocion"></asp:Label>
                                </div>
                                <div class="mb-3 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                   
                                   <h6><b  class="text-success">Autoridad responsable</b></h6>
                                    <asp:Label runat="server" CssClass="text-light " ID="lblAutoridadResponsablePromocion"></asp:Label>
                                </div>                                
                                <div class="mb-2 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                    <h6><b class="text-success">Estatus:</b></h6>
                                    <asp:Label runat="server" CssClass="text-light" ID="lblEstatusPromocion"></asp:Label>
                                </div>
                                 <div class="mb-2 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                    <h6><b class="text-success">Etapa:</b></h6>
                                    <asp:Label runat="server" CssClass="text-light" ID="lblEtapaPromocion"></asp:Label>
                                </div>
                            </div> 
                                <asp:Label runat="server" CssClass="text-success " ID="ResultadoSolicitudPromociones"></asp:Label>
                           
                        </div>

                    </div>
                </div>
                <br />

                <div class="row p-0 mx-0 my-4">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                        <label for="inputPromovente" class="form-label text-secondary"><b>Promovente:</b></label>                
                        <asp:TextBox runat="server" ID="inputPromovente" CssClass="form-control form-control-sm "  MaxLength="500"></asp:TextBox>
                    </div>
                    
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                        <label for="inputFechaRecepcion" class="form-label text-secondary"><b>Fecha de Recepcion:</b></label>                                        
                        <asp:TextBox runat="server" ID="inputFechaRecepcion" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <br />                
                  
                <div >
                    <div class="row p-0 m-0">
                        <div class="col-md-4 col-sm-4 col-xs-4">
                            <label for="inputAnexos" class="help-block text-muted small-font">Anexos: </label>
                            <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary " ID="txtAnexosTipo" AutoPostBack="false" onchange="mostrarOcultarDescripcion()">
                                <asp:ListItem Text="Seleccionar" Value="1" Selected="True" />
                            </asp:DropDownList>
                        </div>

                        <div id="contenedorDescripcion" style="display: none;" class="col-md-4 col-sm-4 col-xs-4">
                            <label class="help-block text-muted small-font">Descripcion: </label>
                            <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="txtDescripcionAnexos" MaxLength="600" />
                        </div>

                        <div class="mb-4 col-4 col-sm-4 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                            <label class="help-block text-muted small-font">Cantidad: </label>
                            <div class="input-group">
                                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtCantidadAnexos" oninput="validarNumero(this)"  MaxLength="10"/>
                                <div class="input-group-append">
                                    <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="+"   OnClick="btnAñadirAnexo"  />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row m-0 p-0">
                        <div class="table-responsive">
                            <table class="table table-striped table-hover mb-0  table-sm">
                                <thead class=" text-center ">
                                    <tr class="">
                                        <th scope="col" class="bg-success text-white">Descripcion</th>
                                        <th scope="col" class="bg-success text-white">Cantidad</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody class="table table-striped text-center table-sm">
                                    <asp:Repeater ID="RepeaterAnexos" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <th scope="row"><%# Eval("DescripcionAnexo") %></th>
                                                <td class="text-secondary"><%# Eval("CantidadAnexo") %></td>
                                                <td class="text-secondary">
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarAnexo"  />
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class=" d-flex justify-content-center mt-1 mt-3">
                    <a  class="btn btn-success btn-sm" data-bs-toggle="modal" onclick="valoresFinales();" data-bs-target="#modalEnviarPromocion"><i class="bi bi-floppy-fill mr-1"></i>Enviar</a>
                </div>
                <asp:Label runat="server" ID="lblSuccess" Text="" CssClass="text-success text-center"></asp:Label>
                <asp:Label runat="server" ID="lblError" Text="" CssClass="text-danger text-center"></asp:Label>
                <!-- FIN DIV OCULTO DOS -->
                <div class="container d-flex justify-content-center align-content-center w-100vw  mt-3">
                    <pre id="TicketDiv" runat="server" ></pre>

                </div>
                <div class="container d-flex flex-row-reverse justify-content-center align-content-center w-100vw m-0 "  id="tituloSello" style="display:none !important;" runat="server">
                    <div class="col-auto flex-column-reverse btn btn-info d-flex btn-sm" onclick="imprimirTicket()" style="cursor: pointer;">
                        <h6 class="text-center align-self-center m-0 p-0 ">¡Imprimir <span class="text-black">ticket!</span></h6>
                        <div class="col-auto ">
                            <i class="bi bi-printer-fill  btn-sm"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modales CustomPromociones -->
        <div class="modal fade" id="modalEnviarPromocion" tabindex="-1" aria-labelledby="modalEnviarPromocion" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
                        <h5 class="modal-title text-secondary fs-4 "> Guardar cambios.</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body row">
                        <div class="mb-6 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                            <span class="text-secondary">Número documento:</span>
                            <asp:TextBox runat="server" ID="copyNumeroDocumento" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" ClientIDMode="Static"/>
                        </div>
                        <div class="mb-6 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                            <span class="text-secondary">Fecha de Recepción:</span>
                            <asp:TextBox runat="server" ID="copyFechaRecepcion" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-12  pb-5">
                            <span class="text-secondary">Promovente:</span>
                            <asp:TextBox runat="server" ID="copyPromovente" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-12  pb-5">
                            <span class="text-secondary">Anexos:</span>
                            <asp:Repeater ID="RepeaterAnexosPrev" runat="server">
                                <ItemTemplate>                                    
                                    <ol class="list-group mb-1 mt-2 ">
                                        <li class="list-group-item d-flex justify-content-between align-items-start bg-light">
                                            <div class="ms-2 me-auto">
                                                <div class="fw-bold text-secondary"><%# Eval("DescripcionAnexo") %></div>                                                
                                            </div>
                                            <span class=" text-success "><%# Eval("CantidadAnexo") %></span>
                                        </li>                                                                                
                                    </ol>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>


                    </div>
                    <div class="modal-footer">
                         <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <asp:Button runat="server" ID="btnPromocion" CssClass="btn btn-success btn-sm" Text="Enviar" OnClick="btnEnviarPromocion" AutoPostBack="true" data-bs-dismiss="modal"/>
                    </div>
                </div>
            </div>
        </div>
        

    </ContentTemplate>
</asp:UpdatePanel>

<script>
   
</script>


<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
<script src="Scripts/consignaciones/Consignaciones.js"></script>
