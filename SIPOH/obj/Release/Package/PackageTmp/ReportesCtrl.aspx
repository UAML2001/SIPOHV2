<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="ReportesCtrl.aspx.cs" Inherits="SIPOH.ReportesCtrl" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentCausa" runat="server">

    <%--Importacion de Controles--%>
    <%@ Register Src="~/Views/ReportesControl.ascx" TagPrefix="form" TagName="ReportesControl" %>
    <form:ReportesControl runat="server" ID="ReportesControl" />

</asp:Content>
