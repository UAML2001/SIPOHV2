<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusSolicitante.ascx.cs" Inherits="SIPOH.Views.InicialBusSolicitante" %>

<asp:UpdatePanel ID="UpdateBusSolicitante" runat="server">
        <ContentTemplate>

<div class="row">
    <div class="col-12 d-flex flex-column">
        <label for="inputDetalleSolicitante5" class="form-label text-secondary">Solicitante</label>
        <div class="d-flex w-100">
            <select class="form-select form-select-sm flex-grow-1 mayusculas" id="selectDetalleSolicitante5" runat="server">
                <option value="">Seleccionar</option>
            </select>
            <asp:Button ID="btnBuscarPCausa5" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarPCausa5_Click" />
            <button id="btnLimpiar5" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar5_Click">Limpiar</button>
        </div>
    </div>
</div>
<p></p>
<div class="row">
    <asp:Label ID="tituloPartesCausa5" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta partes de la causa</i></h2>
    </asp:Label>
    <asp:GridView ID="GridViewPCausa5" CssClass="table custom-gridview" runat="server" OnRowCommand="GridViewPCausa5_RowCommand" OnRowDataBound="GridViewPCausa5_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="GridViewPCausa5_PageIndexChanging">
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
                <asp:Button ID="btnVerDetalles5" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("IdEjecucion") %>' Text="Ver" CssClass="btn btn-secondary" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
<div class="row">
    <asp:Label ID="tituloDetalles5" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Detalles de la parte de la causa</i></h2>
    </asp:Label>
    <div id="detallesConsulta5" class="table-responsive" runat="server"></div>
</div>


    </ContentTemplate>
</asp:UpdatePanel>