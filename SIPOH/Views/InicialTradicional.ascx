<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialTradicional.ascx.cs" Inherits="SIPOH.Views.InicialTradicional" %>

<div class="modal fade" id="exitoTradicional" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalExitoso">
                <h4 class="modal-title">Resultado Encontrado </h4>
            </div>
            <div class="modal-body">
                <b>Exito</b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-success" data-bs-dismiss="modal">Aceptar</button>
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="modalErrorTradicional" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalError">
                <h4 class="modal-title">Error</h4>
            </div>
            <div class="modal-body">
                <b>Error</b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="DatosTradicional" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalGuardar">
                <h4 class="modal-title">Guardar Datos en la causa</h4>
                <button type="button" class="close" data-bs-dismiss="modal">×</button>
            </div>
            <div class="modal-body">
                <p>¿Seguro que quieres guardar los datos?</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnGuardarTradicional" runat="server" CssClass="btn btn-outline-warning" Text="Guardar" />
            </div>

        </div>
    </div>
</div>

<asp:UpdatePanel ID="UpdateTradicional1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label class="form-label text-secondary">Distrito de Procedencia</label>
                <asp:DropDownList ID="InDistritoTra" runat="server" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="InDistritoTra_SelectedIndexChanged">
                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label class="form-label text-secondary">Juzgado de Procedencia</label>
                <asp:DropDownList ID="InJuzgadoProcedenciaTra" runat="server" CssClass="form-select form-select-sm">
                    <asp:ListItem Text="Seleccionar" Value="0" Selected="True" />
                </asp:DropDownList>
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label class="form-label text-secondary">Numero de Causa</label>
                <div class="input-group">
                    <input type="text" class="form-control form-control-sm" id="InCausaTra" runat="server" placeholder="0000/0000">
                    <div class="input-group-append">
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBuscarTradicional_Click" Text="Buscar" />
                    </div>
                </div>
            </div>
        </div>
        <div class="p-4">
            <div id="tablaResultadosHtmlDivTradicional" class="table-responsive" runat="server"></div>
            <div class="nav-item d-flex justify-content-end mt-2">
                <a class="nav-link btn btn-outline-secondary btn-sm rounded-pill mr-1" role="tab"><span class="fs-7">Anterior</span></a>
                <a class="nav-link btn-secondary btn-sm rounded-circle mr-1 fs-7"><span class="fs-7">1</span></a>
                <a class="nav-link btn btn-outline-secondary btn-sm rounded-pill" role="tab"><span class="fs-7">Siguiente</span></a>
            </div>
        </div>
        <!-- INICIO DIV OCULTO UNO-->
        <div class="container" id="OcultarTradicional" runat="server">

            <div class="row">
                <div class="mb-5 col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 col-xxl-3">
                    <label class="form-label-sm text-secondary">Salas Disponibles (CAMBIAR QUERY)</label>
                    <asp:DropDownList ID="selectSalasTradicional" runat="server" CssClass="form-select form-select-sm">
                        <asp:ListItem Text="Seleccionar" />
                    </asp:DropDownList>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Numero de Toca</label>
                    <div class="input-group">
                        <asp:TextBox ID="inputNumeroTocaTradicional" runat="server" CssClass="form-control form-control-sm " placeholder="0000/0000"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnAgregarTradicional" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="AgregarSalaATablaTradicional" Text="Agregar" />
                        </div>
                    </div>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <label class="form-label text-secondary">Sentencia de Amparo Vinculada</label>
                    <div class="input-group">
                        <asp:TextBox ID="inputSentenciaTradicional" runat="server" CssClass="form-control form-control-sm" placeholder="Busca Sentencia"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="Button1Tradicional" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="AgregarSentenciaATablaTradicional" Text="Agregar" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <asp:Label ID="tituloSalas" runat="server" CssClass="textoTablasArriba">
                <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla De Salas</i></h2>
                    </asp:Label>
                    <asp:GridView ID="tablaSalasTradicional" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDeleting="BorrarSalaTradicional">
                        <Columns>
                            <asp:BoundField DataField="NombreSala" HeaderText="Sala">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumeroToca" HeaderText="No° Toca">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar" DeleteText="Borrar">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <asp:Label ID="tituloSentencias" runat="server" CssClass="textoTablasArriba">
            <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla De Sentencias</i></h2>
                    </asp:Label>

                    <asp:GridView ID="tablaSentenciasTradicional" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDeleting="BorrarSentenciaTradicional">
                        <Columns>
                            <asp:BoundField DataField="Sentencia" HeaderText="Sentencia">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar" DeleteText="Borrar">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-lg-7 col-md-7 col-sm-12 text-center">
                    <div style="display: flex; align-items: center; justify-content: center;">
                        <p style="margin-right: 10px; margin-top: 10px;">¿Consultar Sentenciado / Beneficiario? (</p>
                        <div class="form-check" style="margin-right: 10px;">
                            <asp:RadioButton ID="CheckSiTradicional" runat="server" GroupName="respuestaTradicional" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckedChanged" />
                            <label class="form-check-label" for="CheckSiTradicional">
                                Si
                            </label>
                        </div>
                        <div class="form-check">
                            <asp:RadioButton ID="CheckNoTradicional" runat="server" GroupName="respuestaTradicional" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckedChanged" />
                            <label class="form-check-label" for="CheckNoTradicional">
                                No
                            </label>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <!-- FIN DIV OCULTO UNO -->


        <!-- INICIO DIV OCULTO DOS -->
        <div class="container" id="RegistroPartesInTradicional" runat="server">
            <div class="row">
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label for="InputNombreBusquedaTradicional" class="form-label text-secondary">Nombre(s) Parte</label>
                    <input type="text" id="InputNombreBusquedaTradicional" class="form-control form-control-sm" placeholder="" runat="server" />
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label for="InputApPaternoBusquedaTradicional" class="form-label text-secondary">Ap. Paterno Parte</label>
                    <input type="text" id="InputApPaternoBusquedaTradicional" class="form-control form-control-sm" placeholder="" runat="server" />
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label for="inputApMaternoTradicional" class="form-label text-secondary">Ap. Materno de Causa</label>
                    <div class="input-group">
                        <input type="text" id="inputApMaternoTradicional" class="form-control form-control-sm" placeholder="" runat="server">
                        <div class="input-group-append">
                            <asp:Button ID="BuscarPartesTradicional" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="BuscarPartesTradicional_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- GridView -->
            <div class="row">
                <asp:GridView ID="GridView1Tradicional" runat="server" CssClass="table" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="NoEjecucion" HeaderText="No°Ejecucion">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Juzgado" HeaderText="Juzgado">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApPaterno" HeaderText="Apellido Paterno">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApMaterno" HeaderText="Apellido Materno">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaEjecucion" HeaderText="Fecha Ejecucion">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Botones Sí y No -->
            <div class="row justify-content-center">
                <div class="col-lg-4 col-md-6 col-sm-12 text-center">
                    <p>¿Deseas Continuar con el registro?</p>
                    <div>
                        <button type="button" id="idBotonSiTradicional" class="btn btn-success" runat="server" onserverclick="btSiRegistro_Click"><i class="bi bi-check-circle"></i></button>
                        <button type="button" id="idBotonNoTradicional" class="btn btn-danger" runat="server" onserverclick="btNoRegistro_Click"><i class="bi bi-x-circle"></i></button>
                    </div>
                </div>
            </div>

            <!-- Continuar Registro -->
            <div id="ContinuarRegistroTradicional" runat="server">
                <div class="col-12 col-sm-6 col-md-4 col-lg-6" style="display: flex; align-items: center;">
                    <p style="margin-right: 10px; margin-top: 10px;">¿Sentenciado Interno? (</p>
                    <div class="form-check" style="margin-right: 10px;">
                        <input class="form-check-input" type="radio" value="S" id="siInterno" runat="server">
                        <label class="form-check-label">
                            Si
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" value="N" id="noInterno" runat="server">
                        <label class="form-check-label">
                            No )
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                        <label class="form-label-sm text-secondary">Solicitante</label>
                        <asp:DropDownList ID="CatSolicitantesDDTradicional" runat="server" CssClass="form-select form-select-sm">
                            <asp:ListItem Text="Seleccionar" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                        <label class="form-label text-secondary">Detalle del Solicitante</label>
                        <input type="text" class="form-control form-control-sm" id="detalleSolicitantes" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                        <label class="form-label-sm text-secondary">Solicitud</label>
                        <asp:DropDownList ID="CatSolicitudDDTradicional" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CatSolicitudDDTradicional_SelectedIndexChanged" CssClass="form-select form-select-sm">
                            <asp:ListItem Text="Seleccionar" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                        <label class="form-label text-secondary">Otra Solicitud</label>
                        <input type="text" id="InputOtraSolicitud" runat="server" class="form-control form-control-sm" clientidmode="Static" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                        <label class="form-label-sm text-secondary">Anexos</label>
                        <asp:DropDownList ID="CatAnexosDDTradicional" runat="server" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="CatAnexosDDTradicional_SelectedIndexChanged">
                            <asp:ListItem Text="Seleccionar" />
                        </asp:DropDownList>
                      
                  

                    </div>
                    <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                        <label class="form-label text-secondary">Otro Anexo</label>
                        <input type="text" id="OtroAnexoTradicional" class="form-control form-control-sm" runat="server" clientidmode="Static" />
                    </div>
                    <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                        <label for="inputNuc" class="form-label text-secondary">Cantidad</label>
                        <div class="input-group">
                            <input type="number" id="CantidadInputTradicional" class="form-control form-control-sm" runat="server" clientidmode="Static">
                            <div class="input-group-append">
                                <asp:Button ID="AgregarBtn" runat="server" Text="Agregar" CssClass="btn btn-outline-secondary btn-sm" OnClick="AgregarATablaTradicional" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <h2 class="textoTablasArriba"><i class="bi bi-table"></i>Tabla de Anexos</i></h2>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12">
                        <asp:GridView ID="tablaDatosTradicional" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowDeleting="BorrarFilaTradicional">
                            <Columns>
                                <asp:BoundField DataField="NombreSala" HeaderText="Anexo">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NumeroToca" HeaderText="Cantidad">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar" DeleteText="Borrar">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row justify-content-md-center">
                    <div class="col-xl-2 col-sm-12 col-md-2">
                        <asp:Button ID="btnGuardarDatosModal" runat="server" Text="Guardar Datos" CssClass="btn btn-outline-secondary btn-sm col-12" OnClientClick="ModalDatosTradicional(); return false;" />
                    </div>
                </div>
            </div>
        </div>
        <!-- FIN DIV OCULTO DOS -->
    </ContentTemplate>
</asp:UpdatePanel>

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://rawgit.com/RobinHerbots/Inputmask/5.x/dist/jquery.inputmask.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
<script>
    function mostrarModalExito(message) {
        $('#exitoTradicional .modal-body b').text(message); // Actualizar el texto del modal de éxito
        $('#exitoTradicional').modal('show');
    }
    function mostrarModalError(message) {
        $('#modalErrorTradicional .modal-body b').text(message); // Actualizar el texto del modal de error
        $('#modalErrorTradicional').modal('show');
    }
    function ModalDatosTradicional() {
        $('#DatosTradicional').modal('show');
    }
</script>
