<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="ReportesEjec.aspx.cs" Inherits="SIPOH.ReportesEjec" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentCausa" runat="server">

    <%--Importacion de Controles--%>
    <%@ Register Src="~/Views/ReportesEjecucion.ascx" TagPrefix="form" TagName="ReportesEjecucion" %>
    <form:ReportesEjecucion runat="server" ID="ReportesEjecucion" />

</asp:Content>
