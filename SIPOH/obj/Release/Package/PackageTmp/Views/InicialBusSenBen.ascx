<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusSenBen.ascx.cs" Inherits="SIPOH.Views.InicialBusSenBen" %>

<asp:UpdatePanel ID="UpdateBusSentenciadoBeneficiario" runat="server">
        <ContentTemplate>

<div class="container">
    <div class="row">
        <div class="col-sm-6 col-md-4">
            <label for="inputNombreBeneficiario2" class="form-label text-secondary">Nombre(s) Beneficiario</label>
            <input type="text" class="form-control form-control-sm" id="inputNombreBeneficiario2" runat="server" />
        </div>
        <div class="col-sm-6 col-md-3">
            <label for="inputApellidoPaterno2" class="form-label text-secondary">Apellido Paterno</label>
            <input type="text" class="form-control form-control-sm" id="inputApellidoPaterno2" runat="server" />
        </div>
        <div class="col-sm-6 col-md-3">
            <label for="inputApellidoMaterno2" class="form-label text-secondary">Apellido Materno</label>
            <input type="text" class="form-control form-control-sm" id="inputApellidoMaterno2" runat="server" />
        </div>
        <div class="col-sm-6 col-md-2 d-flex align-items-end justify-content-center">
            <asp:Button ID="btnBuscarPCausa2" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBuscarPCausa2_Click" />
            <button id="btnLimpiar2" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar2_Click">Limpiar</button>
        </div>
    </div>
</div>
<p></p>
<div class="row">
    <p></p>
    <asp:Label ID="tituloPartesCausa2" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Consulta partes de la causa</i></h2>
    </asp:Label>
    <asp:GridView ID="GridViewPCausa2" CssClass="table custom-gridview" runat="server" OnRowCommand="GridViewPCausa2_RowCommand" OnRowDataBound="GridViewPCausa2_RowDataBound" AutoGenerateColumns="False" AllowPaging="True" PageSize="1" OnPageIndexChanging="GridViewPCausa2_PageIndexChanging">
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
    <asp:Label ID="tituloDetalles2" runat="server" CssClass="textoTablasArriba">
        <h2 class="textoTablasArriba"><i class="bi bi-table">Detalles de la parte de la causa</i></h2>
    </asp:Label>
    <!-- Contenedor para mostrar los detalles de la búsqueda -->
    <div id="detallesConsulta2" class="table-responsive" runat="server"></div>
    <!-- Aquí puedes agregar la paginación si es necesaria -->
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