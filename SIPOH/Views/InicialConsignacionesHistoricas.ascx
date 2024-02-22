<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialConsignacionesHistoricas.ascx.cs" Inherits="SIPOH.Views.InicialConsignacionesHistoricas" %>

<!-- Modal de resultado de búsqueda (oculto por defecto) -->
<div class="modal fade" id="exito" role="dialog">
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
<!-- Modal de error (oculto por defecto) -->
<div class="modal fade" id="modalError" role="dialog">
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
                <asp:Button CssClass="btn btn-outline-success" ID="btnCambiarPestanaJS" runat="server" Text="Registrar Causa" OnClientClick="cambiarPestañaJS(); return false;" />
            </div>
        </div>
    </div>
</div>


<asp:UpdatePanel ID="UpdateInicialesConH" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="mb-5 col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12">
                <label for="inputRadicacion" class="form-label text-secondary">Selecciona el tipo de sistema a usar</label>
                <asp:DropDownList ID="ddlSistemasAT" runat="server" CssClass="form-select form-select-sm mayusculas" AutoPostBack="True" OnSelectedIndexChanged="ddlSistemasAT_SelectedIndexChanged">
                    <asp:ListItem Text="ACUSATORIO" Value="acusatorio"></asp:ListItem>
                    <asp:ListItem Text="TRADICIONAL" Value="tradicional"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        
        <div class="row" id="divAcusatorio" runat="server">
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label for="inputRadicacion" class="form-label text-secondary">Juzgado de Procedencia</label>
                <asp:DropDownList ID="JuzgadoProcedenciaCHA" runat="server" CssClass="form-select form-select-sm mayusculas"></asp:DropDownList>
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label for="inputIncomJuzgado" class="form-label text-secondary">Causa | Nuc | Juicio Oral</label>
                <asp:DropDownList ID="CausaNucCHA" runat="server" CssClass="form-select form-select-sm mayusculas" ClientIDMode="Static">
                    <asp:ListItem Text="Causa" Value="1"></asp:ListItem>
                    <asp:ListItem Text="NUC" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Juicio Oral" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label for="inputNuc" class="form-label text-secondary">Numero de Causa</label>
                <div class="input-group">
                    <asp:TextBox ID="causaNucAcusatorio" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="16" ClientIDMode="Static" onblur="aplicarFormatoSegunSeleccion(this)"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="btnBuscarCHA" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBuscarAcusatorio_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="divTradicional" runat="server">
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label for="inputRadicacion" class="form-label text-secondary">Distrito de Procedencia</label>
                <asp:DropDownList ID="ddlDistritoProcedencia" runat="server" CssClass="form-select form-select-sm mayusculas" AutoPostBack="True" OnSelectedIndexChanged="ddlDistritoProcedencia_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label for="inputIncomJuzgado" class="form-label text-secondary">Juzgado de Procedencia</label>
                <asp:DropDownList ID="ddlJuzgadoProcedencia" runat="server" CssClass="form-select form-select-sm mayusculas"></asp:DropDownList>
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label for="inputcausa" class="form-label text-secondary">Numero de Causa</label>
                <div class="input-group">
                    <asp:TextBox ID="InputCausaTradicional" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="16" onblur="padLeadingZeros(this)"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="BuscarTradicional" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBuscarTradicional_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:GridView ID="GridViewCausas" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-striped table-hover">
                <Columns>
                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="HiddenIdAsunto" runat="server" Value='<%# Eval("IdAsunto") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Causa|Nuc">
                        <ItemTemplate>
                            <%# Eval("NumeroCausa") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NUC">
                        <ItemTemplate>
                            <%# Eval("NUC") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre Juzgado">
                        <ItemTemplate>
                            <%# Eval("NombreJuzgado") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ofendido(s)">
                        <ItemTemplate>
                            <%# Eval("NombreOfendido") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inculpado(s)">
                        <ItemTemplate>
                            <%# Eval("NombreInculpado") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delito(s)">
                        <ItemTemplate>
                            <%# Eval("NombreDelito") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div id="divOcultarSinCausa" style="display: none;" runat="server">


            <div class="row">
                <div class="mb-5 col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 col-xxl-3">
                    <label class="form-label-sm text-secondary">Salas Disponibles</label>
                    <asp:DropDownList ID="selectSalas" runat="server" CssClass="mayusculas form-select form-select-sm">
                        <asp:ListItem Text="Seleccionar" />
                    </asp:DropDownList>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Número de Toca</label>
                    <div class="input-group">
                        <asp:TextBox ID="inputNumeroToca" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="" onblur="padLeadingZeros(this)" MaxLength="9"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnAddSalayToca" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="AgregarSalayTocaATabla" Text="Agregar" />
                        </div>
                    </div>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <label class="form-label text-secondary">Sentencia de Amparo Vinculada</label>
                    <div class="input-group">
                        <asp:TextBox ID="inputSentencia" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Busca Sentencia" MaxLength="15"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnAddSentencia" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="AgregarSentenciasATabla" Text="Agregar" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <asp:Label ID="tituloSalasCon" runat="server" CssClass="textoTablasArriba">
                <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla De Salas</i></h2>
                    </asp:Label>
                    <asp:GridView ID="tablaSalasCon" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDeleting="BorrarSala">
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
            <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla De Amparos</i></h2>
                    </asp:Label>

                    <asp:GridView ID="tablaSentenciasCon" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDeleting="BorrarSentencia">
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
            <div class="row" id="primerRowPromocion" runat="server">
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <label for="lblNombreJuzgado" class="form-label text-secondary">
                        Nombre de Juzgado</label>
                    <asp:DropDownList ID="busNombreJuzEjec" runat="server" CssClass="mayusculas form-select form-select-sm">
                        <asp:ListItem Text="Seleccionar" />
                    </asp:DropDownList>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                    <label for="lblNumeroEjecucion" class="form-label text-secondary">
                        Número de Ejecución</label>
                    <div class="input-group">
                        <asp:TextBox ID="inputBusNumeroEjecucion" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="9" onblur="padLeadingZeros(this)"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnBuscarNoEjecucion" runat="server" Text="Buscar"
                                CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarNoEjecucion_Click" />
                            <asp:Button ID="btnLimpiarNumeroEjecucion" runat="server" Text="Limpiar"
                                CssClass="btn btn-outline-secondary btn-sm ml-2" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divResultado" runat="server" style="display: none;">
            <div class="row">
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label for="lblNombreBusqueda" class="form-label text-secondary">Nombre(s) Parte</label>
                    <asp:TextBox ID="InputNombreBusqueda" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="250"></asp:TextBox>
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label for="lblApPaternoBusqueda" class="form-label text-secondary">Ap. Paterno Parte</label>
                    <asp:TextBox ID="InputApPaternoBusqueda" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="100"></asp:TextBox>
                    
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label for="lblMaternoBusqueda" class="form-label text-secondary">Ap. Materno de Causa</label>
                    <div class="input-group">
                        <asp:TextBox ID="inputApMaterno" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
            </div>
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
                    <asp:DropDownList ID="CatSolicitantesDDCon" runat="server" CssClass="form-select form-select-sm mayusculas">
                        <asp:ListItem Text="Seleccionar" />
                    </asp:DropDownList>
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                    <label class="form-label text-secondary">Detalle del Solicitante</label>
                    <asp:TextBox ID="detalleSolicitantes" CssClass="form-control form-control-sm mayusculas" runat="server" MaxLength="250"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                    <label class="form-label-sm text-secondary">Solicitud</label>
                    <asp:DropDownList ID="CatSolicitudDDCon" runat="server" AutoPostBack="true" CssClass="form-select form-select-sm mayusculas" OnSelectedIndexChanged="CatSolicitudDDCon_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccionar" />
                    </asp:DropDownList>
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                    <label class="form-label text-secondary">Otra Solicitud</label>
                    <input type="text" id="InputOtraSolicitud" class="form-control form-control-sm mayusculas" runat="server" MaxLength="250" clientidmode="Static">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                    <label class="form-label-sm text-secondary">Anexos</label>
                    <asp:DropDownList ID="CatAnexosDDCon" runat="server" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true" OnSelectedIndexChanged="CatAnexosDDCon_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccionar" />
                    </asp:DropDownList>
                </div>
                <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                    <label class="form-label text-secondary">Otro Anexo</label>
                    <input type="text" id="OtroAnexo" class="form-control form-control-sm mayusculas" runat="server" MaxLength="100" clientidmode="Static">
                </div>
                <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                    <label class="form-label text-secondary">Cantidad</label>
                    <div class="input-group">
                        <input type="number" id="CantidadInput" class="form-control form-control-sm mayusculas" runat="server" max="1000" clientidmode="Static">
                        <div class="input-group-append">
                            <asp:Button ID="AgregarBtn" runat="server" Text="Agregar" CssClass="btn btn-outline-secondary btn-sm" OnClick="AgregarATablaCon" />
                        </div>
                    </div>

                </div>
            </div>
            <div class="row">
                <asp:Label ID="TablasAnexosCon" runat="server" CssClass="textoTablasArriba">
                       <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla de Anexos</i></h2>
                </asp:Label>
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12">
                    <asp:GridView ID="tablaAnexosCon" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowDeleting="BorrarFilaCon">
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
                    <asp:Button ID="btnGuardarDatosModal" runat="server" Text="Guardar Datos" CssClass="btn btn-outline-secondary btn-sm col-12" OnClick="btnGuardarDatosCon_Click" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

<script>
    function mostrarAlerta(message) {
        $('#exito .modal-body b').text(message);
        $('#exito').modal('show');
    }
    function mostrarError(mensaje) {
        $('#modalError .modal-body b').text(mensaje);
        $('#modalError').modal('show');
    }
 
</script>
<script>
    window.addEventListener('keydown', function (e) {
        var node = (e.target) ? e.target : ((e.srcElement) ? e.srcElement : null);
        if ((e.keyCode == 13) && (node.type == "text")) {
            e.preventDefault();
            return false;
        }
    }, true);

</script>
<script type="text/javascript">
    function cambiarPestañaJS() {
        new bootstrap.Tab(document.querySelector('a[href="#ICHCausa"]')).show();
        $('#modalError').modal('hide');
        $('body').removeClass('modal-open').css('overflow', '');
        $('.modal-backdrop').remove();
    }
</script>
