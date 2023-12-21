<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusNoNuc.ascx.cs" Inherits="SIPOH.Views.InicialBusNoNuc" %>

<asp:UpdatePanel ID="UpdateBusNoNuc" runat="server">
    <ContentTemplate>

<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
            <label for="inputRadicacion" class="form-label text-secondary">Juzgado de Procedencia</label>
              <select class="form-select form-select-sm" id="InputDistritoProcedencia" runat="server">
                <option selected>Seleccionar...</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
            <label for="inputNuc" class="form-label text-secondary">Numero Unico de Caso</label>
            <div class="input-group">
                <input type="text" class="form-control form-control-sm" id="ola" placeholder="00-0000-0000">
                <div class="input-group-append">
                   <asp:Button ID="btnBuscarPCausa4" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm ml-2" OnClick="btnBuscarPCausa4_Click" />
                    <button id="btnLimpiar4" runat="server" type="button" class="btn btn-outline-danger btn-sm ml-2" OnServerClick="btnLimpiar4_Click">Limpiar</button>
                </div>
            </div>
        </div>
</div>


    </ContentTemplate>
</asp:UpdatePanel>