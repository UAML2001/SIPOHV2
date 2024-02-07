<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="Consulta.aspx.cs" Inherits="SIPOH.Consulta" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentCausa" runat="server">


    <%--Importacion de Controles--%>
    <%@ Register Src="~/Views/ConsultarControl.ascx" TagPrefix="form" TagName="ConsultarControl" %>
    <form:ConsultarControl runat="server" ID="ConsultarControl" />

</asp:Content>