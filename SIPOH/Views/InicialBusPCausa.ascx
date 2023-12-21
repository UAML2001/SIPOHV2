<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusPCausa.ascx.cs" Inherits="SIPOH.Views.InicialBusPCausa" %>

<div class="modal fade" id="exitoBuscarPartes" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalExitoso">
                <h4 class="modal-title">Resultado Encontrado</h4>
            </div>
            <div class="modal-body">
                <b>Exito, se ha encontrado resultados para tu busqueda</b>
            </div>
            <div class="modal-footer">
                <button id="btnCancelar" runat="server" type="button" class="btn btn-outline-danger" OnServerClick="btnCancelar_Click">Cancelar</button>
                <button type="button" class="btn btn-outline-success" data-bs-dismiss="modal">Aceptar</button>
            </div>
        </div>
    </div>
</div>

<asp:UpdatePanel ID="UpdateBusquedaPartesCausa" runat="server">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridViewPCausa" EventName="PageIndexChanging" />
    </Triggers>
    <ContentTemplate>
<div class="container">
    <div class="row">
        <div class="col-sm-6 col-md-4">
            <label for="inputNombre" class="form-label text-secondary">Nombre(s) Parte</label>
            <input type="text" class="form-control form-control-sm" id="inputNombre" runat="server" />
        </div>
        <div class="col-sm-6 col-md-3">
            <label for="inputApellidoPaterno" class="form-label text-secondary">Apellido Paterno</label>
            <input type="text" class="form-control form-control-sm" id="inputApellidoPaterno" runat="server" />
        </div>
        <div class="col-sm-6 col-md-3">
            <label for="inputApellidoMaterno" class="form-label text-secondary">Apellido Materno</label>
            <input type="text" class="form-control form-control-sm" id="inputApellidoMaterno" runat="server" />
        </div>
        <div class="col-sm-6 col-md-2 d-flex align-items-end justify-content-center">
            <asp:Button ID="btnBuscarPCausa" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBuscarPCausa_Click" />
            <button id="btnLimpiar" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar_Click">Limpiar</button>

        </div>
    </div>
</div>
<p></p>
<div class="row">
    <p></p>
<asp:Label ID="tituloPartesCausa" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta partes de la causa</i></h2>
</asp:Label>
<asp:GridView ID="GridViewPCausa" CssClass="table custom-gridview" runat="server" OnRowCommand="GridViewPCausa_RowCommand" OnRowDataBound="GridViewPCausa_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="1" OnPageIndexChanging="GridViewPCausa_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="NoEjecucion" HeaderText="N° Ejecución">
           <HeaderStyle CssClass="bg-success text-white" />
         </asp:BoundField>
        <asp:BoundField DataField="Juzgado" HeaderText="Juzgado" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="FechaEjecucion" HeaderText="Fecha Ejecución" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="DetalleSolicitante" HeaderText="Detalle del Solicitante" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="TipoExpediente" HeaderText="Tipo Expediente" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Proviene" HeaderText="Proviene" >
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderStyle CssClass="bg-success text-white" />
            <ItemTemplate>
                <asp:Button ID="btnVerDetalles" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("IdAsunto") %>' Text="Ver" CssClass="btn btn-secondary" />
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
</asp:GridView>


</div>
<div class="row">
<asp:Label ID="tituloDetalles" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Detalles de la parte de la causa</i></h2>
</asp:Label>
       <!-- Contenedor para mostrar los detalles de la búsqueda -->
        <div id="detallesConsulta" class="table-responsive" runat="server"></div>


</div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function modalBuscarPartes() {
        $('#exitoBuscarPartes').modal('show');
    }
    function cerrarBuscarPartes() {
        $('#exitoBuscarPartes').modal('hide');
    }
</script>