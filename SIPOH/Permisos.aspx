<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="SIPOH.Permisos" %>

<asp:Content ID="Content10" ContentPlaceHolderID="ContentPermisos" runat="server">
    <link href="Content/css/Permiso.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="UpdateTablaPermisos" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="d-flex justify-content-between">
                <h1 style="margin-left: 5%" class="h5">Permisos <i class="fas fa-angle-right"></i><span class="text-success fw-bold">ADMINISTRADOR</span> <i class="bi bi-person-fill-lock text-secondary"></i></h1>
                <button type="button" class="btn btn-success fw-bold btn-sm mr-5 mb-1" data-bs-toggle="modal" data-bs-target="#modalCrearPerfil">Crear perfil <i class="bi bi-person-vcard-fill"></i></button>

            </div>
            <div class="m-0">
                <div class="row">
                    <div class="col-md-12 col-md-12 ml-auto col-xl-12 mr-auto">
                        <!-- Nav tabs -->
                        <div class="card ">
                            <div class="card-header bg-white">
                                <div class="d-flex justify-content-between">

                                    <div class="row g-3 ">
                                        <div class="col-auto">
                                            <label for="inputMostrar" class="col-form-label">Mostrar</label>
                                        </div>
                                        <div class="col-auto">
                                            <select class="form-select form-select-sm" aria-label="Small select " id="cantidadFiltro">
                                                <option value="5">5</option>
                                                <option value="10">10</option>
                                                <option value="15">15</option>
                                                <option value="20">20</option>
                                            </select>
                                        </div>
                                        <div class="col-auto">
                                            <span id="spanHelpInline" class="form-text">Registros
                                            </span>
                                        </div>
                                    </div>
                                    <div class="row g-3">
                                        <div class="col-sm-auto col-12">
                                            <label for="inputBuscar" class="col-form-label text-secondary">Nombre de perfil</label>
                                        </div>
                                        <div class="col-sm-auto col-12 pr-0 mr-0">
                                            <asp:TextBox runat="server" ID="txtbxBusquedaPerfil" CssClass="form-control form-control-sm" MaxLength="30" />
                                        </div>
                                        <div class="col-sm-auto col-12">
                                            <asp:Button class="btn btn-outline-success fw-bold btn-sm mr-5 mb-1" runat="server" OnClick="btnBuscarPerfil" Text="Buscar" />
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table class="table" id="tablaPermisos">
                                        <thead class="">
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">Nombre / Area</th>
                                                <th scope="col">Enlaces / Permisos</th>
                                                <th scope="col">Sub-Permisos</th>
                                            </tr>
                                        </thead>
                                        <tbody>



                                            <asp:Repeater ID="busquedaPerfilAsociado" runat="server" OnItemDataBound="RepeaterPermisoAsociado_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <th class="text-secondary text-capitalize"><%# Eval("IdPerfil") %> </th>

                                                        <td class="fw-bold"><%# Eval("Perfil") %></td>
                                                        <td class="text-secondary d-flex">
                                                            <details>
                                                                <summary class="text-secondary link-underline link-success ">Enlaces asignados</summary>
                                                                <ul>
                                                                    <%# GenerarListaEnlaces(Eval("Enlaces").ToString()) %>
                                                                </ul>
                                                            </details>

                                                        </td>
                                                        <td class="text-success fw-bolder"><%# Eval("TipoCircuito") %></td>

                                                        <td class="text-center">
                                                            <asp:Button ID="btnEditarPerfil" runat="server" CssClass="btn btn-outline-info rounded btn-sm m-1" Text=' 🖋️ ' OnClick="btnEditarEnlacesPerfil_Click" CommandArgument='<%# Eval("IdPerfil")%>' data-bs-toggle="offcanvas" data-bs-target="#offcanvasWithBackdropEnlacesdePerfil" aria-controls="offcanvasWithBackdrop" />
                                                            <asp:Button ID="btnGuardarSubPermiso" runat="server" CssClass="btn btn-outline-warning rounded btn-sm m-1" Text=' ⚙️ ' OnClick="btnGuardarSubPermiso_Click" CommandArgument='<%# Eval("IdPerfil")%>' OnClientClick="$('#modalEditarSubPermiso').modal('show');" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr>
                                                <th class="text-secondary"></th>
                                                <td class="fw-bold"></td>
                                                <td class="text-success form-label">No hay mas contenido por mostrar.</td>
                                                <td class="text-center text-secondary"></td>
                                                <td class="text-center p-1"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <asp:HiddenField ID="HiddenIdPermisoAsociado" runat="server" />

                                <asp:Label ID="lblIdSeleccionado" runat="server" Text=""></asp:Label>
                                <div class="row g-3 d-flex justify-content-end">
                                    <div class="col-auto btn-group-sm pr-0">
                                        <a class="btn btn-outline-secondary rounded-pill">Anterior</a>
                                    </div>
                                    <div class="col-auto btn-group-sm pr-0">
                                        <a class="btn btn-outline-secondary">1</a>
                                    </div>
                                    <div class="col-auto btn-group-sm">
                                        <a class="btn btn-outline-secondary rounded-pill">Siguiente</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="PanelPermisosAsociados" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- Modal Partes -->
            <div class="modal fade " id="modalGuardarSubPermiso" tabindex="1" aria-labelledby="modalGuardarSubPermisoLabel" aria-hidden="true">
                <div class="modal-dialog modal-xl modal-dialog-scrollable">
                    <div class="modal-content bg-white">
                        <!-- Contenido del segundo modal -->
                        <div class="modal-header">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <i class="bi bi-person-gear text-warning subPermisosIcono pr-2 text-center"></i>
                        <h5 class="modal-title fs-4 text-center border-1 border-bottom pb-1">Subpermisos asociados</h5>
                        <div class="modal-body">

                            <h5 class="text-success pt-4 pr-5 fw-bold text-right">Compartidos<i class="fas fa-fw fa-landmark "></i></h5>
                            <%--tabla permiso consignaciones--%>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead class="">
                                        <tr>
                                            <th scope="col"># </th>
                                            <th scope="col" class="d-flex justify-content-between">Enlace <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                            <th scope="col">Ver</th>
                                            <th scope="col">Editar</th>
                                            <th scope="col">Eliminar</th>
                                            <th scope="col">SuperUsuario</th>
                                            <th scope="col">Administrador</th>
                                            <th scope="col">Usuario</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterGetSubpermisoAsociadoCompartidos" runat="server">
                                            <ItemTemplate>

                                                <tr>
                                                    <th class="text-secondary"><%# Eval("IdSubpermiso") %></th>
                                                    <td class="fw-bold"><%# Eval("linkEnlace") %> </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">

                                                            <label class='<%# Convert.ToBoolean(Eval("Ver")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckCrear"><%# Eval("Ver") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                                            <label class='<%# Convert.ToBoolean(Eval("Editar")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckVer"><%# Eval("Editar") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar" checked>
                                                            <label class='<%# Convert.ToBoolean(Eval("Eliminar")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckActualizar"><%# Eval("Eliminar") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("SuperUser")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("SuperUser") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("Administrador")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("Administrador") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("Usuario")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("Usuario") %></label>
                                                        </div>
                                                    </td>


                                                </tr>
                                            </ItemTemplate>

                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <span class="text-success form-label text-center d-flex justify-content-center  ">No hay mas contenido por mostrar.</span>
                            </div>

                            <%--tabla permisos agenda--%>
                            <h5 class="text-success pt-4 pr-5 fw-bold text-right">Control <i class="bi bi-calendar-day-fill"></i></h5>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead class="">
                                        <tr>
                                            <th scope="col"># </th>
                                            <th scope="col" class="d-flex justify-content-between">Enlace <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                            <th scope="col">Ver</th>
                                            <th scope="col">Editar</th>
                                            <th scope="col">Eliminar</th>
                                            <th scope="col">SuperUsuario</th>
                                            <th scope="col">Administrador</th>
                                            <th scope="col">Usuario</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <asp:Repeater ID="RepeaterGetSubpermisoAsociadoControl" runat="server">
                                            <ItemTemplate>

                                                <tr>
                                                    <th class="text-secondary"><%# Eval("IdSubpermiso") %></th>
                                                    <td class="fw-bold"><%# Eval("linkEnlace") %> </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                                            <label class='<%# Convert.ToBoolean(Eval("Ver")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckCrear"><%# Eval("Ver") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                                            <label class='<%# Convert.ToBoolean(Eval("Editar")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckVer"><%# Eval("Editar") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar" checked>
                                                            <label class='<%# Convert.ToBoolean(Eval("Eliminar")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckActualizar"><%# Eval("Eliminar") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("SuperUser")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("SuperUser") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("Administrador")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("Administrador") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("Usuario")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("Usuario") %></label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>
                                </table>
                                <span class="text-success form-label text-center d-flex justify-content-center  ">No hay mas contenido por mostrar.</span>
                            </div>
                            <%--tabla permisos Promociones--%>
                            <h5 class="text-success pt-4 pr-5 fw-bold text-right">Ejecucion <i class="bi bi-calendar-day-fill"></i></h5>
                            <div class="table-responsive">
                                <table class="table">

                                    <thead class="">
                                        <tr>
                                            <th scope="col"># </th>
                                            <th scope="col" class="d-flex justify-content-between">Enlace <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                            <th scope="col">Ver</th>
                                            <th scope="col">Editar</th>
                                            <th scope="col">Eliminar</th>
                                            <th scope="col">SuperUsuario</th>
                                            <th scope="col">Administrador</th>
                                            <th scope="col">Usuario</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepeaterGetSubpermisoAsociadoEjecucion" runat="server">
                                            <ItemTemplate>

                                                <tr>
                                                    <th class="text-secondary"><%# Eval("IdSubpermiso") %></th>
                                                    <td class="fw-bold"><%# Eval("linkEnlace") %> </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                                            <label class='<%# Convert.ToBoolean(Eval("Ver")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckCrear"><%# Eval("Ver") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                                            <label class='<%# Convert.ToBoolean(Eval("Editar")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckVer"><%# Eval("Editar") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar" checked>
                                                            <label class='<%# Convert.ToBoolean(Eval("Eliminar")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckActualizar"><%# Eval("Eliminar") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("SuperUser")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("SuperUser") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("Administrador")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("Administrador") %></label>
                                                        </div>
                                                    </td>
                                                    <td class="">
                                                        <div class="form-check form-switch">
                                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                                            <label class='<%# Convert.ToBoolean(Eval("Usuario")) ? "form-check-label text-success" : "form-check-label text-secondary" %>' for="flexSwitchCheckEliminar"><%# Eval("Usuario") %></label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <span class="text-success form-label text-center d-flex justify-content-center  ">No hay mas contenido por mostrar.</span>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                            <button type="button" class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalEnviarCambiosPermisos"><i class="bi bi-check-lg"></i>Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--editar subpermiso modal--%>
    <div class="modal fade" id="modalEditarSubPermiso" aria-hidden="true" aria-labelledby="ModalPermisosAsociadosToggleLabel" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="ModalPermisosAsociadosToggleLabel">¿Seguro que quiere editar este perfil?</h1>
                <div class="modal-body">
                    <span>Recuerda que estas añadiendo permisos para cada pantalla del sistema SIPOH.</span>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-warning" data-bs-target="#modalGuardarSubPermiso" data-bs-toggle="modal">No</button>
                    <i class="btn btn-info" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso">Si</i>
                </div>
            </div>
        </div>
    </div>
    <%--Offcanvas--%>

    <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasWithBackdropEnlacesdePerfil" aria-labelledby="offcanvasWithBackdropLabelEnlacesdePerfil">
        <div class="offcanvas-header  d-flex flex-column py-1" style="background-color: #3F5259;">
            <i class="bi bi-link text-success fs-2 align-self-center py-0 my-0" data-bs-dismiss="offcanvas" aria-label="Close"></i>
            <h5 class="offcanvas-title text-light fw-bolder lh-1 mb-2" id="offcanvasWithBackdropLabelEnlacesdePerfil">Enlaces Asignados</h5>

        </div>
        <div class="offcanvas-body">
            <asp:UpdatePanel ID="UpdateAgregarEnlace" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
            <div class="mb-4 col-12 d-flex" id="EquipoTrabajo" runat="server" visble="false">
                <div class="col-auto">
                    <label for="inputTipoAsunto" class="form-label text-secondary">Agregar enlace: </label>
                    <asp:DropDownList runat="server" ID="inputCatenlace" AutoPostBack="true" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true"  >
                        <asp:ListItem Text="Selecciona una opción" Value="0" Selected="True" />                                
                    </asp:DropDownList>

                </div>
                <div class="col-auto align-self-end">
                    <asp:Button class="btn btn-sm btn-success small" runat="server" data-bs-dismiss="offcanvas" aria-label="Close" OnClick="btnAgregarEnlace" Text="Agregar" />
                </div>
            </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="EnlacesPerfiles" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
            <div class=" text-start pt-0 pb-2 d-flex align-content-center justify-content-center">
                <span class="text-success border-0  fw-bolder small" > Compartidos </span>
            </div>
            
            <div class="  CatSubpermisosControl row mb-3 justify-content-center align-content-center" >
                <%--items--%>
                        <asp:Repeater ID="EnlacesCompartidos" runat="server">
                            <ItemTemplate>
                                 <div class=" item1 col-12 col-md-12 col-lg-12 rounded border bg-white my-1 mx-1">
                                        <div class=" row d-flex justify-content-between ">                                        
                                            <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                                <span class="text-secondary ml-1 "><%# Eval("Nombre") %> </span>
                                                <h6 class="fw-bold text-black pb-0 mb-0 "><%# Eval("linkEnlace") %></h6>
                                            </div>
                                            <div class="col-auto rounded-end d-flex text-center">
                                                <asp:CheckBox ID="chkIdPermiso" runat="server" CssClass="form-check-input  align-self-center" AutoPostBack="true" data-IdPermiso='<%# Eval("IdPermiso") %>' OnCheckedChanged="PermisosCompartidos_CheckedChanged" />  
                                            </div>
                                        </div>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        </div>
                   
                        <%--control--%>
                        <div class=" text-start my-2 pt-0 pb-2 d-flex align-content-center justify-content-center">
                            <span class="text-success border-0 fw-bolder small "> Control</span>
                        </div>                        
                        <div class=" CatSubpermisosControl row mb-3 justify-content-center align-content-center" >
                            <%--items--%>
                            <asp:Repeater ID="EnlacesControl" runat="server">
                                <ItemTemplate>
                                     <div class=" item1 col-12 col-md-12 col-lg-12 rounded border bg-white my-1 mx-1">
                                        <div class=" row d-flex justify-content-between ">                                        
                                            <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                                <span class="text-secondary ml-1 "><%# Eval("Nombre") %> </span>
                                                <h6 class="fw-bold text-black pb-0 mb-0 "><%# Eval("linkEnlace") %></h6>
                                            </div>
                                            <div class="col-auto rounded-end d-flex text-center  ">
                                                <asp:CheckBox ID="chkIdPermiso" runat="server" CssClass="form-check-input  align-self-center" AutoPostBack="true" data-IdPermiso='<%# Eval("IdPermiso") %>' OnCheckedChanged="PermisosControl_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <%--ejecucion--%> 
                        <div class="text-start pt-0 pb-2 d-flex align-content-center justify-content-center" >
                            <span class="text-success border-0 fw-bolder small"> Ejecucion </span>
                        </div>                        
                        <div class=" CatSubpermisosControl row mb-3 justify-content-center align-content-center" >
                            <%--items--%>
                            <asp:Repeater ID="EnlacesEjecucion" runat="server">
                                <ItemTemplate>
                                    <div class=" item1 col-12 col-md-12 col-lg-12 rounded border bg-white my-1 mx-1">
                                        <div class=" row d-flex justify-content-between ">                                        
                                            <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                                <span class="text-secondary ml-1 "><%# Eval("Nombre") %> </span>
                                                <h6 class="fw-bold text-black pb-0 mb-0 "><%# Eval("linkEnlace") %></h6>
                                            </div>
                                            <div class="col-auto rounded-end d-flex text-center  ">
                                                <asp:CheckBox ID="chkIdPermiso" runat="server" CssClass="form-check-input  align-self-center" AutoPostBack="true" data-IdPermiso='<%# Eval("IdPermiso") %>' OnCheckedChanged="PermisosEjecucion_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
            </div>
        <asp:Label runat="server" CssClass="text-light" ID="IdPerfilSelected" Text=""></asp:Label>
        <div class="d-flex justify-content-center align-content-center">
            <i class="bi bi-trash3-fill btn btn-sm btn-danger small" data-bs-toggle="modal" data-bs-target="#modalEnlaces" OnClick="btnEnviarModificacionEnlace" ></i>
        </div>
            
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </div>
    
    <%--editar a Enlaces  modal--%>
    
    <div class="modal fade" id="modalEnlaces" aria-hidden="true" aria-labelledby="ModalmodalEnlacesToggleLabel" tabindex="4">
        <div class="modal-dialog modal-sm modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="ModalmodalEnlacesToggleLabel">¿Seguro quieres eliminar los enlaces en selección?</h1>
                <div class="modal-body">
                    <span>Recuerda! Estas eliminando vistas de acceso a este perfil.</span>
                </div>
                <div class="modal-footer">
                    <asp:Button class="btn btn-sm btn-success small" runat="server"  OnClick="btnEnviarModificacionEnlace" Text="Si" />
                    <button class="btn btn-sm small btn-danger" data-bs-dismiss="modal" aria-label="Close">No</button>
                </div>
            </div>
        </div>
    </div>           
    <%--guardar cambios--%>
    <div class="modal fade " id="modalEnviarCambiosPermisos" aria-hidden="true" aria-labelledby="exampleModalEnviarCambiosPermisosToggleLabel" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="exampleModalEnviarCambiosPermisosToggleLabel">¿Seguro que quiere guardar los cambios?</h1>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-target="#modalGuardarSubPermiso" data-bs-toggle="modal">Cancelar</button>
                    <button class="btn btn-success">Guardar</button>
                </div>
            </div>
        </div>
    </div>


    <%--Crear perfil modal--%>
    <asp:UpdatePanel runat="server" ID="PermisosPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modalCrearPerfil" aria-hidden="true" aria-labelledby="exampleModalCrearPerfilToggleLabel" tabindex="-1">
                <div class="modal-dialog modal-xl ">
                    <div class="modal-content ">
                        <div class="modal-header bg-light">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <i class="bi bi-person-fill-add superpermisoGuardarCambios text-center text-success"></i>
                        <h1 class="modal-title fs-5 text-center" id="exampleModalCrearPerfilToggleLabel">Crear Perfil</h1>
                        <div class="modal-body container ">
                            <div class="row align-content-center justify-content-end">
                                <div class="col-12 col-md-4 col-xl-3 col-sm-12">
                                    <label for="inputNombrePerfil" class="col-form-label">Nombre de perfil:</label>
                                    <asp:TextBox runat="server" ID="inputNombrePerfil" CssClass="form-control form-control-sm" MaxLength="250" />
                                </div>
                                <div class="col-12 col-md-5 col-xl-4 col-sm-12">
                                    <label for="inputTipoCircuito" class="col-form-label">Tipo de circuito:</label>
                                    <asp:DropDownList runat="server" ID="inputTipoCircuito" AutoPostBack="true" CssClass="form-select form-select-sm text-secondary" OnSelectedIndexChanged="CatalogoTipoCircuito" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Selecciona una opción" Value="" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <%--compartido--%>
                            <div class="col-auto mt-4 ">

                                <div class="row  pt-4">
                                    <i class="bi bi-share-fill text-success text-center iconoModulos"></i>
                                    <h4 class="text-success mb-0 pb-0 text-center">Compartidos</h4>
                                </div>
                                <hr class="border border-success border-1 opacity-90">
                                <div class="CatSubpermisosControl row mb-3 justify-content-center align-content-center">
                                    <%--items--%>
                                    <asp:Repeater ID="CatSubpermisosCompartidos" runat="server">
                                        <ItemTemplate>
                                            <div class=" item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light my-1 mx-1">
                                                <div class=" row d-flex justify-content-between ">
                                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start ">
                                                        <i class="<%# Eval("Nombreicono") %> align-self-center p-auto"></i>
                                                    </div>
                                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                                        <h6 class="fw-bold text-black pb-0 mb-0 "><%# Eval("linkEnlace") %></h6>
                                                        <span class="text-secondary ml-1 "><%# Eval("Nombre") %> </span>
                                                    </div>

                                                    <div class="col-auto rounded-end d-flex text-center  ">
                                                        <asp:CheckBox ID="chkIdPermiso" runat="server" CssClass="form-check-input  align-self-center" AutoPostBack="true" data-IdPermiso='<%# Eval("IdPermiso") %>' />

                                                    </div>

                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <div class="col-auto mt-4 ">

                                    <div class="row  pt-4">
                                        <i class="bi bi-bank2 text-success text-center iconoModulos"></i>
                                        <h4 class="text-success mb-0 pb-0 text-center">Control</h4>
                                    </div>
                                    <hr class="border border-success border-1 opacity-90">
                                    <div class="CatSubpermisosControl row mb-3 justify-content-center align-content-center">
                                        <%--items--%>
                                        <asp:Repeater ID="CatSubpermisosControl" runat="server">
                                            <ItemTemplate>
                                                <div class=" item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light my-1 mx-1">
                                                    <div class=" row d-flex justify-content-between ">
                                                        <div class="bg-success text-white col-auto d-flex text-center rounded-start ">
                                                            <i class="<%# Eval("Nombreicono") %> align-self-center p-auto"></i>
                                                        </div>
                                                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                                            <h6 class="fw-bold text-black pb-0 mb-0 "><%# Eval("linkEnlace") %></h6>
                                                            <span class="text-secondary ml-1 "><%# Eval("Nombre") %> </span>
                                                        </div>
                                                        <div class="col-auto rounded-end d-flex text-center  ">
                                                            <asp:CheckBox ID="chkIdPermisoControl" runat="server" CssClass="form-check-input  align-self-center" AutoPostBack="true" data-IdPermisoControl='<%# Eval("IdPermiso") %>' />
                                                        </div>

                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <%--enlaces ejecucion--%>
                                    <div class="row pt-4">
                                        <i class="fas fa-fw fa-gavel text-success text-center w-100 iconoModulos"></i>
                                        <h4 class="text-success mb-0 pb-0 text-center">Ejecucion</h4>
                                    </div>
                                    <hr class="border border-success border-1 opacity-90">
                                    <div class="itemsEnlacesControl row mb-3 justify-content-center align-content-center">

                                        <%--items--%>
                                        <asp:Repeater ID="CatSubpermisosEjecucion" runat="server">
                                            <ItemTemplate>
                                                <div class=" item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light my-1 mx-1">
                                                    <div class=" row d-flex justify-content-between ">
                                                        <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                                            <i class="<%# Eval("Nombreicono") %> align-self-center p-auto "></i>
                                                        </div>
                                                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                                            <h6 class="fw-bold text-black pb-0 mb-0 "><%# Eval("linkEnlace") %></h6>
                                                            <span class="text-secondary ml-1 "><%# Eval("Nombre") %> </span>
                                                        </div>
                                                        <div class="col-auto rounded-end d-flex text-center  ">
                                                            <asp:CheckBox ID="chkIdPermisoEjecucion" runat="server" CssClass="form-check-input  align-self-center" data-IdPermisoEjecucion='<%# Eval("IdPermiso") %>' />
                                                        </div>

                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <hr class="border border-success border-1 opacity-90">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer bg-white">
                            <button class="btn btn-sm btn-secondary" data-bs-toggle="modal"><i class="bi bi-x-lg"></i>Cancelar</button>
                            <button type="button" class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalEnviarCambiosPerfil"><i class="bi bi-check-lg"></i>Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
            <%--guardar cambios--%>
            <div class="modal fade" id="modalEnviarCambiosPerfil" aria-hidden="true" aria-labelledby="exampleModalEnviarCambiosPerfilToggleLabel" tabindex="-1">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                        <h1 class="modal-title fs-5 text-center" id="exampleModalEnviarCambiosPerfilToggleLabel">¿Seguro que quiere guardar los cambios Perfil?</h1>
                        <div class="modal-body">
                        </div>
                        <div class="modal-footer">
                            <a class="btn btn-secondary" data-bs-target="#modalCrearPerfil" data-bs-toggle="modal">Cancelar</a>
                            <asp:Button class="btn btn-success" runat="server" data-bs-toggle="modal" OnClick="btnEnviarPerfil" Text="Guardar" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Include Toastr CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />

    <!-- Include jQuery (Toastr depends on it) -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- Include Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

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
        function mostrarToast() {
            toastr.toastSuccess(mensaje, "Exito");
        }
        function toastError(mensaje) {
            toastr.error(mensaje, "Error");
        }
        function toastInfo(mensaje) {
            toastr.info(mensaje, "Informacion");
        }
    </script>

    <script src="Scripts/Permisos/Permisos.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>






</asp:Content>

