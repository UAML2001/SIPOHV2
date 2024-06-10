<%@ Page Title="" Language="C#" MasterPageFile="~/Externo/MasterExterno.Master" AutoEventWireup="true" CodeBehind="BandejaSeguimiento.aspx.cs" Inherits="SIPOH.Externo.BandejaSeguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        function seleccionarOpcion() {
            var ddlTipoFiltrado = document.getElementById('<%= ddlTipoAsunto.ClientID %>');
            var inputBuscarInicial = document.getElementById('<%= txtasunto.ClientID %>');

            if (ddlTipoFiltrado.value === "N") {
                inputBuscarInicial.setAttribute("onblur", "formatNuc(this)");
            } else if (ddlTipoFiltrado.value === "F" || ddlTipoFiltrado.value === "T"  ) {
                inputBuscarInicial.removeAttribute("onblur");
            }
            else {
                inputBuscarInicial.setAttribute("onblur", "padLeadingZeros(this)");

            }
        }
    </script>



    <div class="card mt-5 mb-1 ml-2 mr-2 p-4" runat="server" id="divFirmar">
        <h5 class="card-header">Seguimiento de buzón electrónico</h5>
        <div class="card-body">


              <div class="row mb-4">
                         
                  <div class="col-md-3">
                    <div class="form-outline">
                        <asp:DropDownList runat="server" ID="ddlTipoAsunto" CssClass="form-select" onchange="seleccionarOpcion()">
                        </asp:DropDownList>
                        <label class="form-label" for="ddlTipoAsunto">Tipo de busqueda</label>
                    </div>
                </div>

                                  
                  <div class="col-md-3">
                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" type="text" ID="txtasunto" class="form-control" />
                        <label class="form-label" for="txtasunto">Número de NUC, asunto o folio</label>
                    </div>
                </div>


                <div class="col-md-4">
                    <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" CssClass="btn btn-success btn-block mb-4" />

                </div>
                  </div>



            <div class="row">

                <div class="col-12 col-lg-12 mt-4 table-responsive">
                    <asp:GridView ID="gridbuzon" runat="server" DataKeyNames="IdSolicitudBuzon, RutaDoc" OnRowCommand="gridbuzon_RowCommand"
                        AutoGenerateColumns="false" CssClass="table table-sm table-striped table-hover table-bordered "
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="IdSolicitudBuzon" HeaderText="Folio">
                                <HeaderStyle CssClass="bg-success text-white" />
                                <ItemStyle CssClass="p-2" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Solicitud" HeaderText="Solicitud">
                                <HeaderStyle CssClass="bg-success text-white" />
                                <ItemStyle CssClass="p-2" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha de Solicitud">
                                <ItemStyle CssClass="p-2" />
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                <ItemStyle CssClass="p-2" />
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Estatus" HeaderText="Estatus">
                                <ItemStyle CssClass="p-2" />
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TipoAsunto" HeaderText="Tipo de Asunto">
                                <ItemStyle CssClass="p-2" />
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Numero" HeaderText="Número de Asunto">
                                <ItemStyle CssClass="p-2" />
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FeIngresoMostrar" HeaderText="Fecha">
                                <ItemStyle CssClass="p-2" />
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>

                            <asp:ButtonField CommandName="Ver" Text="<i class='fa fa-eye'></i>"
                                ButtonType="Link" ControlStyle-CssClass="btn btn-primary" HeaderText="Ver Documento">
                                <ItemStyle CssClass="p-2 text-center" />
                                <HeaderStyle CssClass="bg-success text-white" />

                            </asp:ButtonField>

                        </Columns>
                    </asp:GridView>
                </div>

            </div>


        </div>
    </div>



</asp:Content>
