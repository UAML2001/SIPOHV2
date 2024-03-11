<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialCHCausa.ascx.cs" Inherits="SIPOH.Views.InicialCHCausa" %>
<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<h4>Datos recuperados de la variable de sesión</h4>
<div class="row">
    <asp:UpdatePanel ID="UpdateChCausa" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblTipoSistema" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblJuzgadoSeleccionado" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblTipo" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lblNumeroCausaNucJuicio" runat="server" Text=""></asp:Label><br />
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Datos" OnClick="btnActualizar_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
