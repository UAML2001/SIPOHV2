<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusNoCausa.ascx.cs" Inherits="SIPOH.Views.InicialBusNoCausa" %>

<asp:UpdatePanel ID="UpdateBusNoCausa" runat="server">
<ContentTemplate>

<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
        <label for="inputRadicacion" class="form-label text-secondary">Distrito de Procedencia</label>
        <asp:DropDownList ID="InputDistritoProcedencia" runat="server" 
                          AutoPostBack="True" 
                          OnSelectedIndexChanged="InputDistritoProcedencia_SelectedIndexChanged" 
                          CssClass="form-select form-select-sm">
            <asp:ListItem Value="" Text="Seleccionar" Selected="True"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
        <label for="inputIncomJuzgado" class="form-label text-secondary">Juzgado de Procedencia</label>
        <asp:DropDownList ID="inputJuzgadoProcedencia" runat="server" 
                          CssClass="form-select form-select-sm">
            <asp:ListItem Value="" Text="Seleccionar" Selected="True"></asp:ListItem>
        </asp:DropDownList>
    </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputNuc" class="form-label text-secondary">Numero de Causa</label>
            <div class="input-group">
                <input type="text" class="form-control form-control-sm" id="inputNucBusqueda" runat="server" placeholder="0000/0000">
                <div class="input-group-append">
                     <asp:Button ID="btnBuscarPCausa3" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarPCausa3_Click" />
                    <button id="btnLimpiar3" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar3_Click">Limpiar</button>
                </div>
            </div>
        </div>
</div>
<p></p>
<div class="row">
    <asp:Label ID="tituloPartesCausa3" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta partes de la causa</i></h2>
    </asp:Label>
    <asp:GridView ID="GridViewPCausa3" CssClass="table custom-gridview" runat="server" OnRowCommand="GridViewPCausa3_RowCommand" OnRowDataBound="GridViewPCausa3_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="GridViewPCausa3_PageIndexChanging">
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
                <asp:Button ID="btnVerDetalles3" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("IdAsunto") %>' Text="Ver" CssClass="btn btn-secondary" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
<div class="row">
    <asp:Label ID="tituloDetalles3" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Detalles de la parte de la causa</i></h2>
    </asp:Label>
    <div id="detallesConsulta3" class="table-responsive" runat="server"></div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>
