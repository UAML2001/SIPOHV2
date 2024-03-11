<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="SolicitudCDs.aspx.cs" Inherits="SIPOH.SolicitudCDs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentCausa" runat="server">

    <%--Importacion de Controles--%>
    <%@ Register Src="~/Views/SolicitudCD.ascx" TagPrefix="form" TagName="SolicitudCD" %>
    <form:SolicitudCD runat="server" ID="SolicitudCD" />

</asp:Content>
