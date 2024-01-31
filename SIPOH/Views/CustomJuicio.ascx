<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomJuicio.ascx.cs" Inherits="SIPOH.Views.CustomJuicio" %>
<asp:UpdatePanel runat="server" ID="JuicioOralPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
            <h5 class="text-secondary mb-4">Registro de promociones</h5>
            
            <div class="row d-flex justify-content-end align-content-end">
                <div class=" col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNuc" class="form-label text-secondary">Número de Causa:</label>
                    <div class="input-group">
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="inputNumero" onblur="padLeadingZeros(this)" MaxLength="10" />
                        <div class="input-group-append">
                            <asp:Button runat="server" CssClass="btn btn-outline-success btn-sm" Text="Buscar" OnClick="btnConsultaCausa" AutoPostBack="true" />
                        </div>
                    </div>
                </div>
            </div>                  
        
                <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i> Inculpados</span>


        <div class="border border-light border-1 rounded my-2">
            <asp:Repeater ID="RepeaterListaPersonas" runat="server">
                <ItemTemplate>
                    <div class="d-flex flex-column justify-content-between align-content-center p-2 btn btn-outline-light border-1 border-bottom" id="ItemSelectedPersona" onclick="toggleSeleccion(this)">
                        <div class="d-flex justify-content-between align-content-center" id="ItemSelectedBox" >
                            <div>
                                <%--<input class="form-check-input" type="checkbox" value="" id="flexCheckDefault">--%>
                                <i class="bi bi-dash-square text-secondary mr-2" id="x-squareJuicio"></i>
                               
                                <i class="bi bi-check-square text-success mr-2" id="check-square" style="display: none;"></i>
                                <b class="mr-2 text-secondary" id="labelApellido"><%# Eval("Nombre") %></b>
                            </div>
                            <span><%# Eval("Genero") %></span>
                            <i class="mr-2 text-secondary" id="labelNombre"><%# Eval("Apellido") %></i>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <div class="row mt-4">

            <span class="text-success fw-bold m-2"><i class="bi bi-folder2-open"></i> Delitos relacionados a <b class="" id="labelGetNameSelected"></b>:</span>

        </div>

        <div class="col-12 border-2 border-top border-bottom py-3 mb-3 row justify-content-around px-3 mx-0" style="background-color: #3F5259;">
            <div class="form-check col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkDelito1" onclick="selectCheckbox('checkDelito1')">
                <b class="form-check-label text-white" for="checkDelito1">AMENAZAS
                </b>
            </div>
            <div class="form-check col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkDelito2" onclick="selectCheckbox('checkDelito2')">
                <b class="form-check-label text-white" for="checkDelito2">ACTOS LIBIDINOSOS
                </b>
            </div>
            <div class="form-check col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkDelito3" onclick="selectCheckbox('checkDelito3')">
                <b class="form-check-label text-white" for="checkDelito3">ABUSO SEXUAL
                </b>
            </div>
            <div class="form-check col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkDelito4" onclick="selectCheckbox('checkDelito4')">
                <b class="form-check-label text-white" for="checkDelito4">ABUSO SEXUAL
                </b>
            </div>
            <div class="form-check col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 mx-0">
                <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkDelito5" onclick="selectCheckbox('checkDelito5')">
                <b class="form-check-label text-white" for="checkDelito5">ABUSO SEXUAL
                </b>
            </div>
        </div>
        <div class="row mt-4">

            <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i> Victimas relacionadas:</span>
              
        </div>
           <div class=" col-12 border-2 border-top border-bottom py-3 mb-3 ">
                <div class="form-check col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 mb-2">
                    <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkVictimas1">
                    <span class="form-check-label text-dark" for="checkVictimas1">                         
                        <b class="text-dark">ROBERTO JAVIER</b> PAZ VALENCIA
                    </span>
                </div>
                <div class="form-check col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 mb-2">
                    <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkVictimas2">
                    <span class="form-check-label text-dark" for="checkVictimas2">
                        <b>NELSON VLADIMIR</b> PINADA PEÑA                        
                    </span>
                </div>
                <div class="form-check col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 mb-2">
                    <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkVictimas3">
                    <span class="form-check-label text-dark" for="checkVictimas3">                        
                        <b>LUCIA ABIGAIL</b> PERÈZ SOSA
                    </span>
                </div>
                <div class="form-check col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 mb-2">
                    <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkVictimas4">
                    <span class="form-check-label text-dark" for="checkVictimas4">
                        <b>YLLA XIOMARA</b> PEREIRA MORAZANI                        
                    </span>
                </div>
                <div class="form-check col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 mb-2">
                    <input class="form-check-input bg-success text-white border-0" type="checkbox" value="" id="checkVictimas5">
                    <span class="form-check-label text-dark" for="checkVictimas5">                        
                        <b>MICHEL VALERIA</b> TOBAL LOPEZ
                    </span>
                </div>
                
            </div>
        <div class="mt-5 mb-3 d-flex justify-content-center">
            <a class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalAgregarJuicioOral">
                <i class="bi bi-diagram-3-fill"></i> Agregar relación
            </a>
        </div>
        <div class="row">
            <span class="text-success fw-bold m-2"> <i class="bi bi-diagram-3-fill"></i> Tabla de relación:</span>
            <div class="table-responsive">
                <table class="table table-striped table-hover mb-0 table-sm">
                    <thead class=" text-center ">
                        <tr class="">
                            <th scope="col" class="bg-success text-white">Inclulpado</th>
                            <th scope="col" class="bg-success text-white">Delitos</th>
                            <th scope="col" class="bg-success text-white">Victima</th>
                            <th scope="col" class="bg-success text-white">Acciones</th>
                            <th scope="col" class="bg-success text-white" style="display:none;">ID delitos <b class="text-dark">inculpado</b></th>
                            <th scope="col" class="bg-success text-white" style="display:none;">ID delitos <b class="text-dark">victima</b></th>
                    </thead>
                    <tbody class="table table-striped text-center table-sm">
                        <tr>
                            <th scope="row" >MARCO MEGIA SALAS</th>
                            <td class="text-secondary text-center">ABUSO DE AUTORIDAD</td>
                            <td class="text-secondary text-center">MARCO ALBERTO HERNANDEZ MEJIA</td>
                            <td class="text-secondary text-center">✖️</td>
                            <td class="text-dark text-center" style="display:none;">502</td>
                            <td class="text-dark text-center" style="display:none;">200</td>
                            
                        </tr>
                        <tr>
                            <th scope="row" >JUAN HECTOR ESTRADA JUSTO </th>
                            <td class="text-secondary text-center">ABUSO DE AUTORIDAD</td>
                            <td class="text-secondary text-center">MARCO ALBERTO HERNANDEZ MEJIA</td>
                            <td class="text-secondary text-center">✖️</td>
                            <td class="text-dark text-center" style="display:none;">102</td>
                            <td class="text-dark text-center" style="display:none;">200</td>
                            
                        </tr>


                    </tbody>
                </table>
            </div>
            
        </div>
        <%--<hr class="bg-success mt-5" />--%>
        <div class="row mt-5 px-3 pt-3 pb-0 mb-1">
            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label for="inputTipoAudiencia" class="form-label text-secondary">Tipo solicitud: </label>
                <asp:DropDownList runat="server" ID="inputTipoAudiencia" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true" AutoPostBack="false">
                    <asp:ListItem Text="Selecciona una opción" Value="" />
                </asp:DropDownList>
            </div>
            <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Quién ingresa: </label>

                    <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" ID="inputQuienIngresa" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="inputQuienIngresa_SelectedIndexChanged">
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
            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                <label class="help-block text-muted small-font">Observaciones: </label>
                <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="observacionesIncial" MaxLength="600" />
            </div>
        </div>

        <%--Tabla de anexos--%>
         <div class="row p-0 m-0">
                        <div class="col-md-4 col-sm-4 col-xs-4">
                            <label for="inputAnexos" class="help-block text-muted small-font">Anexos: </label>
                            <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary " ID="txtAnexosTipoJuicio" AppendDataBoundItems="true" AutoPostBack="false" onchange="mostrarOcultarDescripcion()">
                                <asp:ListItem Text="Selecciona una opción" Value="" />
                            </asp:DropDownList>
                        </div>

                        <div id="contenedorDescripcion" style="display: none;" class="col-md-4 col-sm-4 col-xs-4">
                            <label class="help-block text-muted small-font">Descripción: </label>
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
                                        <th scope="col" class="bg-success text-white">Descripción</th>
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
        <div class=" d-flex justify-content-center my-5">
                    <a class="btn btn-success btn-sm" data-bs-toggle="modal" onclick="valoresFinales();" data-bs-target="#envioRelaciones"><i class="bi bi-floppy-fill mr-1"></i>Enviar</a>
        </div>        

<%--modales--%>
<%--        MODAL AGREGAR RELACION--%>
        <div class="modal fade" id="modalAgregarJuicioOral" tabindex="-1" aria-labelledby="modalAgregarJuicioOral" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
                <h5 class="modal-title text-secondary fs-4 ">Guardar cambios.</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                
            </div>
            <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <button type="button" class="btn btn-success btn-sm">Agregar relación</button>
      </div>
    </div>
  </div>
</div>
        <%-- Modal envio de relaciones--%>

  <div class="modal fade" id="envioRelaciones" tabindex="-1" aria-labelledby="envioRelaciones" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
                <h5 class="modal-title text-secondary fs-4 "> Guardar cambios.</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                
            </div>
            <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <button type="button" class="btn btn-success btn-sm">Enviar relación</button>
      </div>
    </div>
  </div>
</div>
        <script src="../Scripts/consignaciones/JuicioOral.js"></script>
    </ContentTemplate>
</asp:UpdatePanel>

