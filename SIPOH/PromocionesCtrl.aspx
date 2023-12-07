<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="PromocionesCtrl.aspx.cs" Inherits="SIPOH.PromocionesCtrl" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentCausa" runat="server">


    <%--Importacion de Controles--%>
    <%@ Register Src="~/Views/CustomPromocion.ascx" TagPrefix="form" TagName="CustomPromocion" %>
    <form:CustomPromocion runat="server" ID="CustomPromocion" />

</asp:Content>