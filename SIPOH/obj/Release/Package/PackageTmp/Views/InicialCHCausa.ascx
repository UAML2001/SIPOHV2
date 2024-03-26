<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialCHCausa.ascx.cs" Inherits="SIPOH.Views.InicialCHCausa" %>
<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%--customs--%>




    <asp:UpdatePanel ID="HistoricoCausaJuicioOral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div  class="col-12 vh-100 d-flex justify-content-center align-items-center "  id="buttonContainer" runat="server" >                
                    <asp:Button ID="btnActualizar" CssClass="btn btn-success border-0" runat="server" Text="➕" OnClick="btnActualizar_Click" />                
            </div>
            <asp:Placeholder ID="CausaCustom" runat="server" Visible="false"></asp:Placeholder>
            <asp:Placeholder ID="JuicioOralCustom" runat="server" Visible="false"></asp:Placeholder>    
            
          <Script type="text/javascript">
              
            </Script>
        </ContentTemplate>
    </asp:UpdatePanel>
