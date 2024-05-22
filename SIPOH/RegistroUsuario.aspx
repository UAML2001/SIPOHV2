<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="SIPOH.RegistroUsuario" %>
<%--<asp:Content ID="RegistroUsuario" ContentPlaceHolderID="RegistroUsuario" runat="server">--%>
<asp:Content ID="Contents" ContentPlaceHolderID="RegistroUsuario" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin=""></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
            <asp:ScriptManager ID="ScriptManagerRegistroPersonal" runat="server"></asp:ScriptManager>
    
    <div class="d-flex justify-content-between px-3 mb-4">
        <span class="text-sm">Personal de SIPOH <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold "> Registro de usuarios</span> </span>
        <b>Registro</b>
    </div>
     <div class="col-md-12  col-xl-12 m-0 px-0 col-12 col-sm-12 mx-2 ">
        <!-- Nav tabs -->
        <div class="card border border-1 shadow px-3 mb-5 bg-body rounded">
            <div class="card-header  bg-white ">
                <div class="row g-3 d-flex flex-row justify-content-end  align-items-center  v-100 my-0 mx-3" >
                      <div class="col-auto">
                        <label for="inputNombre" class="col-form-label text-dark fw-bold" runat="server" id="lblNombreUsuario">Nombre de usuario:</label>
                      </div>
                      <div class="col-auto">
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm text-secondary " ID="inputBuscarusuario"  />
                      </div>
                      <div class="col-auto">
                        <asp:Button runat="server" ID="btnSearchUsuario" CssClass="btn btn-outline-success btn-sm" Text="Buscar" OnClick="btnBuscarUsuario" AutoPostBack="true"></asp:Button>
                        <asp:Button runat="server" ID="btnSearchNuevoUsuario" CssClass="btn btn-success btn-sm" Text="+" OnClick="btnNuevoUsuario" AutoPostBack="true"></asp:Button>
                      </div>
                 </div> 
                <asp:UpdatePanel ID="UpdatePanelPersonal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                <div class="card-body mx-0 mt-4  p-0">
                <!-- Tab panes -->
                <div class="tab-content ">
                    
                    <%--Table resultados --%>
                    
                <asp:UpdatePanel ID="UpdatePanelBusquedaUsuario" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>                   
                        <asp:GridView ID="grdVwBusquedaUsuarios" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm table-borderless rounded border-0 table-responsive"  OnSelectedIndexChanged="grdVwBusquedaUsuarios_SelectedIndexChanged" OnRowEditing="grdVwBusquedaUsuarios_RowEditing" OnRowUpdating="grdVwBusquedaUsuarios_RowUpdating" OnRowCancelingEdit="grdVwBusquedaUsuarios_RowCancelingEdit" DataKeyNames="IdUsuario" OnRowDataBound="grdVwBusquedaUsuarios_RowDataBound">
                        <Columns>                           
                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-dark fw-bold text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Eval("IdUsuario") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtIdUsuario" runat="server" Text='<%# Bind("IdUsuario") %>' CssClass="form-control" Visible="false"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-CssClass="text-dark fw-bold text-center " HeaderStyle-CssClass="bg-success text-white text-center" />--%>
                            <asp:TemplateField HeaderText="Nombre" ItemStyle-CssClass="text-dark fw-bold text-center " HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>' CssClass="form-control form-select-sm  border-0 rounded bg-light"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Apellidos" HeaderText="Apellidos" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />--%>
                            <asp:TemplateField HeaderText="Apellido paterno" ItemStyle-CssClass="text-secondary text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAPaterno" runat="server" Text='<%# Eval("APaterno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtAPaterno" runat="server" Text='<%# Bind("APaterno") %>' CssClass="form-control form-select-sm  border-0 rounded bg-light"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apellido materno" ItemStyle-CssClass="text-secondary text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAMaterno" runat="server" Text='<%# Eval("AMaterno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtAMaterno" runat="server" Text='<%# Bind("AMaterno") %>' CssClass="form-control form-select-sm  border-0 rounded bg-light"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Usuario" HeaderText="Usuario de login" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />--%>                            
                            <asp:TemplateField HeaderText="Usuario" ItemStyle-CssClass="text-dark fw-bold text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsuario" runat="server" Text='<%# Eval("Usuario") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUsuario" runat="server" Text='<%# Bind("Usuario") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Contraseña" HeaderText="Contraseña" ItemStyle-CssClass="text-success fw-bold text-center " HeaderStyle-CssClass="bg-success text-white text-center" />--%>
                            <%--<asp:BoundField DataField="ContraseñaDesencriptada" HeaderText="Contraseña DES" ItemStyle-CssClass="text-success fw-bold text-center " HeaderStyle-CssClass="bg-success text-white text-center" />--%>
                            <asp:TemplateField HeaderText="Contraseña" ItemStyle-CssClass="text-secondary  text-center col-auto" HeaderStyle-CssClass="bg-success text-white text-center col-auto">
                                <ItemTemplate>
                                    <asp:Label ID="lblContraseñaDesencriptada" runat="server" Text='<%# Eval("ContraseñaDesencriptada") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtContraseñaDesencriptada" runat="server" Text='<%# Bind("ContraseñaDesencriptada") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="Juzgado" ItemStyle-CssClass="text-secondary text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblJuzgado" runat="server" Text='<%# Eval("Juzgado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtJuzgado" runat="server" Text='<%# Bind("Juzgado") %>' CssClass="form-control form-select-sm  border-0 rounded bg-light"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Status" HeaderText="Estado" ItemStyle-CssClass="text-secondary text-center " HeaderStyle-CssClass="bg-success text-white text-center" />--%>
                            <asp:TemplateField HeaderText="Estado" ItemStyle-CssClass="text-success fw-bold text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>' CssClass="form-control form-select-sm  border-0 rounded bg-light"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Perfil" ItemStyle-CssClass="text-secondary fw-bold text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerfil" runat="server" Text='<%# Eval("Perfil") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblPerfil" runat="server" Text='<%# Bind("Perfil") %>' CssClass="form-control form-select-sm  border-0 rounded bg-light"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>                          
                            <asp:TemplateField HeaderText="Telefono" ItemStyle-CssClass="text-secondary text-center" HeaderStyle-CssClass="bg-success text-white text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lbltelefono" runat="server" Text='<%# Eval("telefono") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txttelefono" runat="server" Text='<%# Bind("telefono") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>  
                                                     
                             <asp:CommandField ShowEditButton="True" EditText="✏️" HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="bg-light border-0 rounded text-center" HeaderText="Actions" CausesValidation="false"   />                                                       
                                 <asp:TemplateField HeaderStyle-CssClass="bg-success text-white text-center" ItemStyle-CssClass="text-secondary text-center d-flex justify-content-center align-content-center"  >
                                    <ItemTemplate>
                                        <asp:Button ID="btnAlta" runat="server" Text="🔓" OnClick="btnCustom_AltaUsuario"  CssClass="bg-light border-0 rounded" CommandArgument='<%# Eval("IdUsuario") %>' data-bs-toggle="tooltip" data-bs-placement="button" title="Dar de ALTA a un usuario" />                                       
                                        <asp:Button ID="btnBaja" runat="server" Text="🔒" OnClick="btnCustom_BajaUsuario"  CssClass="bg-light border-0 rounded text-center" CommandArgument='<%# Eval("IdUsuario") %>'  data-bs-toggle="tooltip" data-bs-placement="left" title="Dar de BAJA a un usuario"  />
                                    </ItemTemplate>
                                </asp:TemplateField>                                                      
                                 
                            <asp:TemplateField HeaderStyle-CssClass="bg-success text-white text-center"  ItemStyle-CssClass="text-secondary text-center"   >
                                    <ItemTemplate>                                        
                                        <asp:Button ID="btnIpUsuario" runat="server" Text=" 🔐 " OnClick="btnCustom_DesbloquearIpUsuario" CssClass="bg-light border-0 rounded" CommandArgument='<%# Eval("Nombre") %>' data-bs-toggle="tooltip" data-bs-placement="left" title="Desbloquear IP de usuario." />
                                    </ItemTemplate>
                                </asp:TemplateField>                            
                        </Columns>
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
                    
                    <%--Form registro usuarios--%>
                    <div class="row g-3 "  runat="server" id="formRegistroUsuario">
                    <h6 class="text-success fw-bolder bi bi-person-fill-add"> Datos generales del usuario:</h6>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4 " id="contenedorDrplstJuzgados" runat="server">
                            <label for="inputTipoAsunto" class="form-label text-secondary">Juzgado: </label>
                            <asp:DropDownList runat="server" ID="inputJuzgado" AutoPostBack="true" CssClass="form-select form-select-sm text-secondary" OnSelectedIndexChanged="inputCatJuzgado_SelectedIndexChanged" AppendDataBoundItems="true" >
                                <asp:ListItem Text="Selecciona una opción" Value="0" Selected="True" />                                
                            </asp:DropDownList>
                        </div>
                        
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                            <label for="inputPerfil" class="form-label text-secondary">Perfil: </label>
                            <asp:DropDownList runat="server" ID="inputPerfil" AutoPostBack="true" CssClass="form-select form-select-sm text-secondary" OnSelectedIndexChanged="inputCatPerfil_SelectedIndexChanged" AppendDataBoundItems="true" >
                                <asp:ListItem Text="Selecciona una opción" Value="" Selected="True"  />                                
                            </asp:DropDownList>
                        </div>
                                 <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4 " id="EquipoTrabajo" runat="server" visble="false">
                                    <label for="inputTipoAsunto" class="form-label text-secondary">Equipo de trabajo: </label>
                                    <asp:DropDownList runat="server" ID="inputEquipoTrabajo" AutoPostBack="true" CssClass="form-select form-select-sm text-secondary" OnSelectedIndexChanged="inputCatEquipoTrabajo_SelectedIndexChanged" AppendDataBoundItems="true"  >
                                        <asp:ListItem Text="Selecciona una opción" Value="0" Selected="True" />                                
                                    </asp:DropDownList>
                                </div>                            
                           
                        <div class="row mt-4">
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                <label for="inputNUC" class="form-label text-secondary">Nombre:</label>
                                <asp:TextBox runat="server" ID="inputNombre" CssClass="form-control form-control-sm " MaxLength="30"/>
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                <label for="inpuTipoSolicitud" class="form-label text-secondary">Apellido paterno: </label>
                                <asp:TextBox runat="server" ID="inputApellidoPaterno" CssClass="form-control form-control-sm " MaxLength="150"/>
                            </div>
                                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                <label for="inpuTipoSolicitud" class="form-label text-secondary">Apellido materno: </label>
                                <asp:TextBox runat="server" ID="inputApellidoMaterno" CssClass="form-control form-control-sm " MaxLength="30"/>
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                <label for="inpuTipoSolicitud" class="form-label text-secondary">Usuario: </label>
                                <asp:TextBox runat="server" ID="inputUsuario" CssClass="form-control form-control-sm " MaxLength="20"/>
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                <label for="inpuTipoContrasena" class="form-label text-secondary">Contraseña: </label>
                                <asp:TextBox runat="server" ID="inputContraseña" CssClass="form-control form-control-sm " />
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                                <label for="inpuTelefono" class="form-label text-secondary">Teléfono: </label>
                                <asp:TextBox runat="server" ID="inputTelefono" CssClass="form-control form-control-sm " onblur="validarNumero(this)" MaxLength="10"/>
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                <label for="inpuTipoDireccion" class="form-label text-secondary">Dirección: </label>
                                <asp:TextBox runat="server" ID="inputDomicilio" CssClass="form-control form-control-sm " MaxLength="30"/>
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                <label for="inpuEmail" class="form-label text-secondary">Correo electrónico: </label>
                                <asp:TextBox runat="server" ID="inputEmail" CssClass="form-control form-control-sm " MaxLength="30"/>
                            </div>
                             <div class=" d-flex justify-content-center mt-5" runat="server" id="btnModalGuardar">                                 
                                <a class="btn btn-success btn-sm" data-bs-toggle="modal" OnClick="valoresUsuarioRegistro();" data-bs-target="#modalEnviarUsuarios"><i class="bi bi-floppy-fill mr-1"></i>Guardar</a>
                                  
                            </div>
                           
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
     </div>
         <!-- MOdal Save Changes -->
 <div class="modal fade" id="modalEnviarUsuarios" tabindex="1" aria-labelledby="modal3Label" aria-hidden="true">
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
                            <span class="text-secondary">Perfil:</span>
                            <asp:TextBox runat="server" ID="copyPerfil" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Juzgado:</span>
                            <asp:TextBox runat="server" ID="copyJuzgado" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6  pb-2">
                            <span class="text-secondary">Nombre:</span>
                            <asp:TextBox runat="server" ID="copyNombre" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Apellido paterno:</span>
                            <asp:TextBox runat="server" ID="copyAPaterno" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Apellido materno:</span>
                            <asp:TextBox runat="server" ID="copyAMaterno" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Usuario de login:</span>
                            <asp:TextBox runat="server" ID="copyUsuario" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Contraseña:</span>
                            <asp:TextBox runat="server" ID="copyPass" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Domicilio:</span>
                            <asp:TextBox runat="server" ID="copyDomicilio" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Telefono:</span>
                            <asp:TextBox runat="server" ID="copyTelefono" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" ClientIDMode="Static" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Correo electronico:</span>
                            <asp:TextBox runat="server" ID="copyEmail" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true"  />

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>                            
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Guardar" OnClick="btnEnviarUsuario_Click" OnClientClick="return validarNumero();" data-bs-dismiss="modal" postback="true" />                              
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
   
    <script>
        

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
        toastr.options = {
            closeButton: true,
            debug: false,
            newestOnTop: false,
            progressBar: true,
            positionClass: "toast-bottom-right",
            preventDuplicates: false,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            timeOut: "5000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut"
        };

        function toastSuccess() {
            toastr.success(mensaje, "Exito");
        }
        function toastError(mensaje) {
            toastr.error(mensaje, "Error");
        }
        function toastInfo(mensaje) {
            toastr.info(mensaje, "Información");
        }
        function valoresUsuarioRegistro() {
            copytxtNombre();
            copytxtAPaterno();
            copytxtAMaterno();
            copytxtUsuario();
            copytxtPassword();
            
            copytxtDomicilio();
            copytxtTelefono();
            copytxtEmail();
        }
        function copytxtNombre() {
            var inputNombre = $("#<%= inputNombre.ClientID %>");
            var copyNombre = $("#<%= copyNombre.ClientID %>");

            copyNombre.val(inputNombre.val());
        }
        function copytxtAPaterno() {
            var inputAPaterno = $("#<%= inputApellidoPaterno.ClientID %>");
            var copyAPaterno = $("#<%= copyAPaterno.ClientID %>");

            copyAPaterno.val(inputAPaterno.val());
        }
        function copytxtAMaterno() {
            var inputAMaterno = $("#<%= inputApellidoMaterno.ClientID %>");
            var copyAMaterno = $("#<%= copyAMaterno.ClientID %>");

            copyAMaterno.val(inputAMaterno.val());
        }
        function copytxtUsuario() {
            var inputUsuario = $("#<%= inputUsuario.ClientID %>");
            var copyUsuario = $("#<%= copyUsuario.ClientID %>");

            copyUsuario.val(inputUsuario.val());
        }
        function copytxtPassword() {
            var inputPass = $("#<%= inputContraseña.ClientID %>");
            var copyPass = $("#<%= copyPass.ClientID %>");

            copyPass.val(inputPass.val());
        }
        
        function copytxtDomicilio() {
            var inputDomicilio = $("#<%= inputDomicilio.ClientID %>");
            var copyDomicilio = $("#<%= copyDomicilio.ClientID %>");

            copyDomicilio.val(inputDomicilio.val());
        }
        function copytxtTelefono() {
            var inputTelefono = $("#<%= inputTelefono.ClientID %>");
            var copyTelefono = $("#<%= copyTelefono.ClientID %>");

            copyTelefono.val(inputTelefono.val());
        }
        function copytxtEmail() {
            var inputEmail = $("#<%= inputEmail.ClientID %>");
            var copyEmail = $("#<%= copyEmail.ClientID %>");

            copyEmail.val(inputEmail.val());
        }
</script>
    
</asp:Content>
