<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusNoNuc.ascx.cs" Inherits="SIPOH.Views.InicialBusNoNuc" %>

<asp:UpdatePanel ID="UpdateBusNoNuc" runat="server">
     <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridViewPCausa4" EventName="PageIndexChanging" />
    </Triggers>
<ContentTemplate>

<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
            <label for="inputJuzgadoProcedencia" class="form-label text-secondary">Juzgado de Procedencia</label>
              <select class="form-select form-select-sm" id="InputDistritoProcedencia" runat="server">
                <option selected>Seleccionar</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
            <label for="inputNuc" class="form-label text-secondary">Numero Unico de Caso</label>
            <div class="input-group">
                <input type="text" class="form-control form-control-sm" id="inputNucBusqueda" runat="server" onblur="formatNuc(this)" maxlength="12" placeholder="">
                <div class="input-group-append">
                   <asp:Button ID="btnBuscarPCausa4" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarPCausa4_Click" />
                    <button id="btnLimpiar4" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar4_Click">Limpiar</button>
                </div>
            </div>
        </div>
</div>
<p></p>
<div class="row">
    <asp:Label ID="tituloPartesCausa4" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta partes de la causa</i></h2>
    </asp:Label>
    <asp:GridView ID="GridViewPCausa4" CssClass="table custom-gridview" runat="server" OnRowCommand="GridViewPCausa4_RowCommand" OnRowDataBound="GridViewPCausa4_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="2" OnPageIndexChanging="GridViewPCausa4_PageIndexChanging">
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
                <asp:Button ID="btnVerDetalles4" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("IdAsunto") %>' Text="Ver" CssClass="btn btn-secondary" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
<div class="row">
    <asp:Label ID="tituloDetalles4" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Detalles de la parte de la causa</i></h2>
    </asp:Label>
    <div id="detallesConsulta4" class="table-responsive" runat="server"></div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>