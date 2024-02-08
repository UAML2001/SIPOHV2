<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusDetSolicitante.ascx.cs" Inherits="SIPOH.Views.InicialBusDetSolicitante" %>

<asp:UpdatePanel ID="UpdateBusDetSolicitante" runat="server">
        <ContentTemplate>


<div class="row">
    <div class="col-12 d-flex flex-column">
        <label for="inputDetalleSolicitante6" class="form-label text-secondary">Detalle del Solicitante</label>
        <div class="d-flex w-100">
            <input type="text" class="form-control form-control-sm flex-grow-1 mayusculas" id="inputDetalleSolicitante6" runat="server" />
            <asp:Button ID="btnBuscarPCausa6" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarPCausa6_Click" />
            <button id="btnLimpiar6" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar6_Click">Limpiar</button>
        </div>
    </div>
</div>


<p></p>
<div class="row">
    <asp:Label ID="tituloPartesCausa6" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta partes de la causa</i></h2>
    </asp:Label>
    <asp:GridView ID="GridViewPCausa6" CssClass="table custom-gridview" runat="server" OnRowCommand="GridViewPCausa6_RowCommand" OnRowDataBound="GridViewPCausa6_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="1" OnPageIndexChanging="GridViewPCausa6_PageIndexChanging">
 <Columns>
        <asp:BoundField DataField="NoEjecucion" HeaderText="N° Ejecución">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Juzgado" HeaderText="Juzgado">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="FechaEjecucion" HeaderText="Fecha Ejecución">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="DetalleSolicitante" HeaderText="Detalle del Solicitante">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="TipoExpediente" HeaderText="Tipo Expediente">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:BoundField DataField="Proviene" HeaderText="Proviene">
            <HeaderStyle CssClass="bg-success text-white" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderStyle CssClass="bg-success text-white" />
            <ItemTemplate>
                <asp:Button ID="btnVerDetalles2" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("IdEjecucion") %>' Text="Ver" CssClass="btn btn-secondary" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
<div class="row">
    <asp:Label ID="tituloDetalles6" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Detalles de la parte de la causa</i></h2>
    </asp:Label>
    <div id="detallesConsulta6" class="table-responsive" runat="server"></div>
</div>


    </ContentTemplate>
</asp:UpdatePanel>
<script>
    window.addEventListener('keydown', function (e) {
        var node = (e.target) ? e.target : ((e.srcElement) ? e.srcElement : null);
        if ((e.keyCode == 13) && (node.type == "text")) {
            e.preventDefault();
            return false;
        }
    }, true);
</script>