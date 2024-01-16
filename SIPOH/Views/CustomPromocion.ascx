<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPromocion.ascx.cs" Inherits="SIPOH.Views.CustomPromocion" %>



<link href="Content/css/Consignaciones.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
<script type="text/javascript">
    //JS PROMOCIONES

    function seleccionarVictima(victima) {
        var inputPromovente = document.getElementById('<%= inputPromovente.ClientID %>');
        inputPromovente.value = victima;
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
    function toggleAnexos() {
        var btnAnexos = $("#btn-anexos");
        var contenedor = $("#ContenidoAnexos");

        if (btnAnexos.prop("checked")) {
            //contenedor.show();
            contenedor.fadeIn();
        }else {
            //contenedor.hide();
            contenedor.fadeOut();
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
                    <asp:DropDownList runat="server" ID="DrpLstTipoDocumento" class="form-select form-select-sm text-secondary" AutoPostBack="true" OnSelectedIndexChanged="DrpLstObtenerTipoDocumento" >
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
                            <asp:TextBox runat="server"  CssClass="form-control form-control-sm" ID="inputNumero"/>
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
                                    <asp:Label runat="server" CssClass="text-secondary" ID="lblVictimasPromocion"></asp:Label>
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
                                    <asp:Label runat="server" CssClass="text-light" ID="lblAutoridadResponsablePromocion"></asp:Label>
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
                        <asp:TextBox runat="server" ID="inputPromovente" CssClass="form-control form-control-sm "></asp:TextBox>
                    </div>
                    
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                        <label for="inputFechaRecepcion" class="form-label text-secondary"><b>Fecha de Recepcion:</b></label>                                        
                        <asp:TextBox runat="server" ID="inputFechaRecepcion" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <br />                
                   <div class="row mx-0 bg-light my-4 py-4 px-0">
                       <div class="col d-flex justify-content-center ">
                           <div class="row justify-content-center">
                                <h6><b class="text-dark ">¿Desea añadir mas anexos?</b></h6>
                                <input type="checkbox" class="btn-check" id="btn-anexos"  autocomplete="off" onclick="toggleAnexos();">
                                <label class="bi bi-pencil-square btn btn-outline-success col-auto" for="btn-anexos"></label>                              
                           </div>
                       </div>
                   </div>
                <div id="ContenidoAnexos" style="display:none;">
                    <div class="row p-0 m-0">
                        <div class="col-md-4 col-sm-4 col-xs-4">
                            <label for="inputAnexos" class="help-block text-muted small-font">Anexos: </label>
                            <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" ID="txtAnexosTipo" AutoPostBack="false" onchange="mostrarOcultarDescripcion()">
                                <asp:ListItem Text="Seleccionar" Value="1" Selected="True" />
                            </asp:DropDownList>
                        </div>

                        <div id="contenedorDescripcion" style="display: none;" class="col-md-4 col-sm-4 col-xs-4">
                            <label class="help-block text-muted small-font">Descripcion: </label>
                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtDescripcionAnexos" />
                        </div>

                        <div class="mb-4 col-4 col-sm-4 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                            <label class="help-block text-muted small-font">Cantidad: </label>
                            <div class="input-group">
                                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtCantidadAnexos" oninput="validarNumero(this)" />
                                <div class="input-group-append">
                                    <asp:Button runat="server" CssClass="btn btn-outline-success btn-sm" Text="➕" OnClick="btnAñadirAnexo"  />
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
                                                <td class="text-secondary"><i class="bi bi-trash-fill text-danger"></i></td>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class=" d-flex justify-content-center mt-1">
                    <a type="submit" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalEnviarPromocion"><i class="bi bi-floppy-fill mr-1"></i>Enviar</a>
                </div>
                <asp:Label runat="server" ID="lblSuccess" Text="" CssClass="text-success text-center"></asp:Label>
                <asp:Label runat="server" ID="lblError" Text="" CssClass="text-danger text-center"></asp:Label>
            </div>
        </div>

        <!-- Modales CustomPromociones -->
        <div class="modal fade" id="modalEnviarPromocion" tabindex="-1" aria-labelledby="modalEnviarPromocion" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">¿Los datos son correctos?</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ...
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button runat="server" ID="btnPromocion" CssClass="btn btn-success btn-sm" Text="Enviar" OnClick="btnEnviarPromocion" AutoPostBack="true" data-bs-dismiss="modal"/>
                    </div>
                </div>
            </div>
        </div>
        

    </ContentTemplate>
</asp:UpdatePanel>

<script>
    const formSelector = document.getElementById('formSelector');
    const formularios = document.querySelectorAll('.formulario');

    formSelector.addEventListener('change', () => {
        // Oculta todos los formularios al principio
        formularios.forEach(formulario => {
            formulario.style.display = 'none';
        });

        // Muestra el formulario seleccionado
        const selectedForm = document.getElementById(formSelector.value);
        selectedForm.style.display = 'block';
    });
</script>


<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
<script src="Scripts/consignaciones/Consignaciones.js"></script>
